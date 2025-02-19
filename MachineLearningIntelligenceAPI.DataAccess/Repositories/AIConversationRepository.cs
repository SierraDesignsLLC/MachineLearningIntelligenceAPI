using MachineLearningIntelligenceAPI.Common;
using MachineLearningIntelligenceAPI.Common.StringConstants;
using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using MachineLearningIntelligenceAPI.DomainModels;
using Microsoft.Extensions.Logging;
using OpenAI.Chat;
using System.Text.Json;

namespace MachineLearningIntelligenceAPI.DataAccess.Repositories
{
    public class AIConversationRepository : RepositoryBase, IAIConversationRepository
    {
        private readonly ILogger<AIConversationRepository> _logger;
        private static readonly string _openApiKey = Environment.GetEnvironmentVariable(ConnectionStrings.OpenApiSecret);
        private const string AIModel = AIModels.Gpt4oMini;   // TODO: make feature flag

        internal const string RedditServiceV1BaseRoutePrefix = "/api/v1/";
        internal const string RedditServiceV1DeletePost = $"post/delete";

        private static readonly ChatTool getCurrentLocationTool = ChatTool.CreateFunctionTool(
            functionName: nameof(GetCurrentLocation),
            functionDescription: "Get the user's current location"
        );
        
        // TODO, make translation tool to standardize format
        private static readonly ChatTool getCurrentWeatherTool = ChatTool.CreateFunctionTool(
            functionName: nameof(GetCurrentWeather),
            functionDescription: "Get the current weather in a given location",
            functionParameters: BinaryData.FromBytes(
                """
                    {
                        "type": "object",
                        "properties": {
                            "location": {
                                "type": "string",
                                "description": "The city and state, e.g. Boston, MA"
                            },
                            "unit": {
                                "type": "string",
                                "enum": [ "celsius", "fahrenheit" ],
                                "description": "The temperature unit to use. Infer this from the specified location."
                            }
                        },
                        "required": [ "location" ]
                    }
                """u8.ToArray())
        );

        public AIConversationRepository(ILogger<AIConversationRepository> logger)
        {
            _logger = logger;
            
            if(_openApiKey == null)
            {
                _logger.LogError($"{InternalServerErrorString.OpenApiKeyNullError}");
                throw new Exception(InternalServerErrorString.OpenApiKeyNullError);
            }
        }

        /// <summary>
        /// Talk to AI with given input text. Get response from AI as string
        /// </summary>
        public async Task<string> TalkToAI(ConversationRequest conversation, string aiModel = null)
        {
            ChatClient client = new(model: aiModel ?? AIModel, apiKey: _openApiKey);

            // build the profile here for the user for context
            // Example of chat history with context retention (starting with a user message)
            var messages = new List<ChatMessage>();

            foreach (var message in conversation.Context)
            {
                messages.Add(new UserChatMessage(message));
            }

            if (messages.Count != 0)
            {
                // Start streaming chat completion
                Console.Write("[ASSISTANT]: ");
                var completionUpdates = client.CompleteChatStreamingAsync(messages);    // this prints to console? or does it below

                // Process the streaming response chunks as they arrive
                await foreach (StreamingChatCompletionUpdate update in completionUpdates)
                {
                    // If there are updates to content, append them
                    if (update.ContentUpdate.Count > 0)
                    {
                        Console.Write(update.ContentUpdate[0].Text);
                    }
                }
            }

            // Simulate further conversation by adding more messages and maintaining context
            messages.Add(new UserChatMessage(conversation.InputString));

            // Streaming again to retain context
            Console.Write("[ASSISTANT]: ");
            var secondCompletionUpdates = client.CompleteChatStreamingAsync(messages);

            await foreach (StreamingChatCompletionUpdate update in secondCompletionUpdates)
            {
                if (update.ContentUpdate.Count > 0)
                {
                    Console.Write(update.ContentUpdate[0].Text);
                }
            }

            ChatCompletionOptions options = new()
            {
                Tools = { getCurrentLocationTool, getCurrentWeatherTool },  // TODO: add more tools for users. Can execute functions with these, essentially hooks
            };

            var response = ProcessUntilCompletion(client, messages, options);

            return string.Join("", response.SelectMany(x => x.Content.Select(t => t.Text)));
        }

        private List<ChatMessage> ProcessUntilCompletion(ChatClient client, List<ChatMessage> messages, ChatCompletionOptions options)
        {
            var responseMessages = new List<ChatMessage>();
            bool requiresAction;

            do
            {
                requiresAction = false;
                ChatCompletion completion = client.CompleteChat(messages, options);

                switch (completion.FinishReason)
                {
                    case ChatFinishReason.Stop:
                        {
                            // Add the assistant message to the conversation history.
                            messages.Add(new AssistantChatMessage(completion));
                            responseMessages.Add(new AssistantChatMessage(completion));
                            break;
                        }

                    case ChatFinishReason.ToolCalls:
                        {
                            HandleToolCall(ref messages, ref responseMessages, ref requiresAction, completion);
                            break;
                        }

                    case ChatFinishReason.Length:
                        _logger.LogError("Incomplete model output due to MaxTokens parameter or token limit exceeded.");
                        throw new Exception(BadRequestString.RepositoryError);

                    case ChatFinishReason.ContentFilter:
                        _logger.LogError("Omitted content due to a content filter flag.");
                        throw new Exception(BadRequestString.RepositoryError);

                    case ChatFinishReason.FunctionCall:
                        _logger.LogError("Deprecated in favor of tool calls.");
                        throw new Exception(BadRequestString.RepositoryError);

                    default:
                        _logger.LogError(completion.FinishReason.ToString());
                        throw new Exception(BadRequestString.RepositoryError);
                }
            } while (requiresAction);

            return responseMessages;
        }

        private void HandleToolCall(ref List<ChatMessage> messages, ref List<ChatMessage> responseMessages, ref bool requiresAction, ChatCompletion completion)
        {
            // First, add the assistant message with tool calls to the conversation history.
            messages.Add(new AssistantChatMessage(completion));
            responseMessages.Add(new AssistantChatMessage(completion));

            // Then, add a new tool message for each tool call that is resolved.
            foreach (ChatToolCall toolCall in completion.ToolCalls)
            {
                switch (toolCall.FunctionName)
                {
                    case nameof(GetCurrentLocation):
                        {
                            string toolResult = GetCurrentLocation();
                            messages.Add(new ToolChatMessage(toolCall.Id, toolResult));
                            responseMessages.Add(new ToolChatMessage(toolCall.Id, toolResult));
                            break;
                        }

                    case nameof(GetCurrentWeather):
                        {
                            // The arguments that the model wants to use to call the function are specified as a
                            // stringified JSON object based on the schema defined in the tool definition. Note that
                            // the model may hallucinate arguments too. Consequently, it is important to do the
                            // appropriate parsing and validation before calling the function.
                            using JsonDocument argumentsJson = JsonDocument.Parse(toolCall.FunctionArguments);
                            bool hasLocation = argumentsJson.RootElement.TryGetProperty("location", out JsonElement location);
                            bool hasUnit = argumentsJson.RootElement.TryGetProperty("unit", out JsonElement unit);

                            if (!hasLocation)
                            {
                                throw new ArgumentNullException(nameof(location), "The location argument is required.");
                            }

                            string toolResult = hasUnit
                                ? GetCurrentWeather(location.GetString(), unit.GetString())
                                : GetCurrentWeather(location.GetString());
                            messages.Add(new ToolChatMessage(toolCall.Id, toolResult));
                            responseMessages.Add(new ToolChatMessage(toolCall.Id, toolResult));
                            break;
                        }

                    default:
                        {
                            // Handle other unexpected calls.
                            throw new NotImplementedException();
                        }
                }
            }

            requiresAction = true;
            return;
        }

        private static string GetCurrentLocation()
        {
            // Call the location API here.
            return "San Francisco";
        }

        private static string GetCurrentWeather(string location, string unit = "celsius")
        {
            // Call the weather API here.
            return $"31 {unit}";
        }
    }
}
