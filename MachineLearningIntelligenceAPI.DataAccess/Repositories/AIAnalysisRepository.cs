using MachineLearningIntelligenceAPI.Common.StringConstants;
using MachineLearningIntelligenceAPI.Common;
using MachineLearningIntelligenceAPI.DomainModels;
using Microsoft.Extensions.Logging;
using OpenAI.Chat;
using System.Text.Json;
using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;

namespace MachineLearningIntelligenceAPI.DataAccess.Repositories
{
    public class AIAnalysisRepository : IAIAnalysisRepository
    {
        private readonly ILogger<AIAnalysisRepository> _logger;
        private readonly HttpClient _httpClient;
        private static readonly string _openApiKey = Environment.GetEnvironmentVariable(ConnectionStrings.OpenApiSecret);
        private const string AIModel = AIModels.Gpt4oMini;   // TODO: make feature flag

        private static readonly ChatTool getTranslationTool = ChatTool.CreateFunctionTool(
            functionName: nameof(GetTranslation),
            functionDescription: "Translate text from one language to another",
            functionParameters: BinaryData.FromBytes(
                """
                {
                    "type": "object",
                    "properties": {
                        "text": {
                            "type": "string",
                            "description": "The text to translate"
                        },
                        "sourceLanguage": {
                            "type": "string",
                            "description": "The language of the input text (e.g., 'en' for English)"
                        },
                        "targetLanguage": {
                            "type": "string",
                            "description": "The language to translate the text into (e.g., 'fr' for French)"
                        }
                    },
                    "required": [ "text", "targetLanguage" ]
                }
                """u8.ToArray())
        );

        public AIAnalysisRepository(ILogger<AIAnalysisRepository> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient(ConnectionStrings.RedditService);

            if (_openApiKey == null)
            {
                _logger.LogError($"{InternalServerErrorString.OpenApiKeyNullError}");
                throw new Exception(InternalServerErrorString.OpenApiKeyNullError);
            }
        }

        /// <summary>
        /// Analyze the passed in user engagements from the perspective of an expert social media manager
        /// </summary>
        public async Task<List<string>> AnalyzeEngagements(AnalyzeRequest analysis, string aiModel = null)
        {
            ChatClient client = new(model: aiModel ?? AIModel, apiKey: _openApiKey);

            // build the profile here for the user for context
            // Example of chat history with context retention (starting with a user message)
            var messages = new List<ChatMessage>();

            messages.Add(new UserChatMessage($"You are an expert social media manager, analyze the following data and create an analysis in {analysis.Culture}, return the analysis as a string array eg. [\"analysis1\", \"analysis2\"] and say nothing else"));
            messages.Add(new UserChatMessage($"{analysis.Prompt}"));

            foreach (var message in analysis.InputStrings)
            {
                messages.Add(new UserChatMessage(message));
            }

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

            ChatCompletionOptions options = new()
            {
                Tools = { getTranslationTool },
            };

            var response = ProcessUntilCompletion(client, messages, options);

            return new List<string> { string.Join("", response.Last().Content.Select(t => t.Text)) };
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
                    case nameof(GetTranslation):
                        {
                            // The arguments that the model wants to use to call the function are specified as a
                            // stringified JSON object based on the schema defined in the tool definition. Note that
                            // the model may hallucinate arguments too. Consequently, it is important to do the
                            // appropriate parsing and validation before calling the function.
                            using JsonDocument argumentsJson = JsonDocument.Parse(toolCall.FunctionArguments);
                            string text = argumentsJson.RootElement.GetProperty("text").GetString();
                            string sourceLanguage = argumentsJson.RootElement.TryGetProperty("sourceLanguage", out JsonElement srcLang)
                                ? srcLang.GetString()
                                : null;
                            string targetLanguage = argumentsJson.RootElement.GetProperty("targetLanguage").GetString();

                            string translatedText = GetTranslation(text, sourceLanguage, targetLanguage);
                            messages.Add(new ToolChatMessage(toolCall.Id, translatedText));
                            responseMessages.Add(new ToolChatMessage(toolCall.Id, translatedText));
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

        private static string GetTranslation(string text, string sourceLanguage, string targetLanguage)
        {
            // Call an external translation API or AI model
            //return $"[Translated '{text}' from {sourceLanguage ?? "auto"} to {targetLanguage}]";
            return $"[Translated '{text}' from {sourceLanguage ?? "auto"} to {targetLanguage}]";
        }
    }
}
