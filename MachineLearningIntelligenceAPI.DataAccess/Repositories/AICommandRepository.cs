using MachineLearningIntelligenceAPI.Common.StringConstants;
using MachineLearningIntelligenceAPI.Common;
using Microsoft.Extensions.Logging;
using OpenAI.Chat;
using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using MachineLearningIntelligenceAPI.DomainModels;

namespace MachineLearningIntelligenceAPI.DataAccess.Repositories
{
    public class AICommandRepository : IAICommandRepository
    {
        private readonly ILogger<AICommandRepository> _logger;
        private static readonly string _openApiKey = Environment.GetEnvironmentVariable(ConnectionStrings.OpenApiSecret);
        private const string AIModel = AIModels.Gpt4oMini;   // TODO: make feature flag

        public AICommandRepository(ILogger<AICommandRepository> logger)
        {
            _logger = logger;

            if (_openApiKey == null)
            {
                _logger.LogError($"{InternalServerErrorString.OpenApiKeyNullError}");
                throw new Exception(InternalServerErrorString.OpenApiKeyNullError);
            }
        }

        /// <summary>
        /// Respond to passed in command
        /// </summary>
        public async Task<string> RespondToCommand(CommandRequest command, string aiModel = null)
        {
            ChatClient client = new(model: aiModel ?? AIModel, apiKey: _openApiKey);  // TODO should this be a singleton?? maybe not? google? chatgpt

            // build the profile here for the user for context
            // Example of chat history with context retention (starting with a user message)
            var messages = new List<ChatMessage>();

            var message = $"You are an expert social media manager, analyze the following command and craft a response ";
            if (!string.IsNullOrEmpty(command.Culture))
            {
                message += $"in {command.Culture} ";
            }
            message += $"say nothing but the response and say nothing else. " +
                $"Additional details are as follows: ";
            messages.Add(new UserChatMessage(message));
            messages.Add(new UserChatMessage($"{command.Prompt}"));
            messages.Add(new UserChatMessage(command.InputString));

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

            var response = ProcessUntilCompletion(client, messages);

            return string.Join("", response.Last().Content.Select(t => t.Text));
        }

        private List<ChatMessage> ProcessUntilCompletion(ChatClient client, List<ChatMessage> messages, ChatCompletionOptions options = null)
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
    }
}
