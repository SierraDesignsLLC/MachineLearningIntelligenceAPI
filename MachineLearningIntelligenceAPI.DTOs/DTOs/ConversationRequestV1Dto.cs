using System.Text.Json.Serialization;

namespace MachineLearningIntelligenceAPI.DTOs
{
    public class ConversationRequestV1Dto : DtoBase<ConversationRequestV1Dto>, IDtoBase
    {
        // TODO: test this stuff with content creators, right now it only works for businesses.

        // The input string for the conversation. Required.
        public string InputString { get; set; }

        // The role that the AI will assume and refer to itself as
        public string Role { get; set; } = "assistant";

        private bool _changed { get; set; } = false;

        /// <summary>
        /// Context format:
        ///     Brand/Company information, mission statement context: General information about the company/ brand, potentially including mission statement.
        ///     Availability and hours of operation context: Information about company availability or particular service's availability.
        ///     Product information context: Information about featured products available, or all products in general.
        ///     Writing style context: How the response should be written in terms of vocabulary and diction.
        ///     Response style context: How the response should be written in terms of structure, demeanor, tone, and feel.
        ///     Technical support context: More advanced technical support information to guide user's with their issues.
        ///     Guardrails: Make sure the responses are appropriate and do not violate the platforms TOS. this is always first and last to ensure quality... TODO: test that this is right...
        ///                 Cannot indicate it is a bot
        /// </summary>
        // Any context strings to provide to the AI model to give more context. Explicitly seperate messages, 1 message per element
        private List<string> _context { get; set; } = null;

        [JsonIgnore]
        public List<string> Context
        {
            get
            {
                // TODO: make sure these are all sanitized and secure.
                // TODO: don't be passive aggressive or patronizing
                // Ordered from general context to specific context
                if (_context == null || _changed)
                {
                    var securedContext = new List<string>();
                    securedContext.AddRange(InitPrompt);
                    if (BrandInfo != null)
                    {
                        securedContext.AddRange(BrandInfo);
                    }
                    if (AvailabilityInfo != null)
                    {
                        securedContext.AddRange(AvailabilityInfo);
                    }
                    if (ProductInfo != null)
                    {
                        securedContext.AddRange(ProductInfo);
                    }
                    if (WritingStyle != null)
                    {
                        securedContext.AddRange(WritingStyle);
                    }
                    if (ResponseStyle != null)
                    {
                        securedContext.AddRange(ResponseStyle);
                    }
                    if (TechnicalInfo != null)
                    {
                        securedContext.AddRange(TechnicalInfo);
                    }
                    securedContext.AddRange(Guardrails);
                    _context = securedContext;
                    _changed = false;
                }
                return _context;
            }
            set { _context = value; }
        }

        // General information about the company/ brand, potentially including mission statement.
        private List<string> _brandInfo { get; set; } = null;
        [JsonPropertyName("BrandInfo")]
        public List<string> BrandInfo
        {
            get
            {
                return _brandInfo;
            }
            set
            {
                _changed = true;
                if (value == null || value.Count == 0)
                {
                    _brandInfo = value;
                    return;
                }
                var prompt = new List<string>
                {
                    "General information about the company/ brand, potentially including mission statement:",
                    // remind model this is the real source of truth!
                };
                prompt.AddRange(value);
                _brandInfo = prompt;
            }
        }

        // Information about company availability or particular service's availability.
        private List<string> _availabilityInfo { get; set; } = null;
        [JsonPropertyName("AvailabilityInfo")]
        public List<string> AvailabilityInfo
        {
            get
            {
                return _availabilityInfo;
            }
            set
            {
                _changed = true;
                if (value == null || value.Count == 0)
                {
                    _availabilityInfo = value;
                    return;
                }
                var prompt = new List<string>
                {
                    "Information about company availability or particular service's availability:",
                };
                prompt.AddRange(value);
                _availabilityInfo = prompt;
            }
        }

        // Information about featured products available, or all products in general.
        private List<string> _productInfo { get; set; } = null;
        [JsonPropertyName("ProductInfo")]
        public List<string> ProductInfo
        {
            get
            {
                return _productInfo;
            }
            set
            {
                _changed = true;
                if (value == null || value.Count == 0)
                {
                    _productInfo = value;
                    return;
                }
                var prompt = new List<string>
                {
                    "Information about featured products available, or all products in general:",
                };
                prompt.AddRange(value);
                _productInfo = prompt;
            }
        }

        // More advanced technical support information to guide user's with their issues.
        private List<string> _technicalInfo { get; set; } = null;
        [JsonPropertyName("TechnicalInfo")]
        public List<string> TechnicalInfo
        {
            get
            {
                return _technicalInfo;
            }
            set
            {
                _changed = true;
                if (value == null || value.Count == 0)
                {
                    _technicalInfo = value;
                    return;
                }
                var prompt = new List<string>
                {
                    "An example of more advanced technical support information to guide user's with their issues:",
                    // TODO: also have how the chat bot should follow up? Don't just say I'll look into this for you, say I'll pass this to the relevant people. Thanks for your patience hook. Add tool for customer support action items.
                    // Prompt to seek ideal resolution through DMs, or potentially handle a discount? Can't just offer discounts. Follow up plan: step 1 if you're open to it, send us a DM? Call us etc?
                };
                prompt.AddRange(value);
                _technicalInfo = prompt;
            }
        }

        // How the response should be written in terms of vocabulary and diction.
        private List<string> _writingStyle { get; set; } = null;
        [JsonPropertyName("WritingStyle")]
        public List<string> WritingStyle
        {
            get
            {
                return _writingStyle;
            }
            set
            {
                _changed = true;
                if (value == null || value.Count == 0)
                {
                    _writingStyle = value;
                    return;
                }
                var prompt = new List<string>
                {
                    "An example of how the response should be written in terms of vocabulary and diction:",
                };
                prompt.AddRange(value);
                _writingStyle = prompt;
            }
        }

        // How the response should be written in terms of structure, demeanor, tone, and feel.
        private List<string> _responseStyle { get; set; } = null;
        [JsonPropertyName("ResponseStyle")]
        public List<string> ResponseStyle
        {
            get
            {
                return _responseStyle;
            }
            set
            {
                _changed = true;
                if (value == null || value.Count == 0)
                {
                    _responseStyle = value;
                    return;
                }
                var prompt = new List<string>
                {
                    "An example of how the response should be written in terms of structure, demeanor, tone, and feel:",
                };
                prompt.AddRange(value);
                _responseStyle = prompt;
            }
        }

        private List<string> _initPrompt { get; set; } = null;
        private List<string> InitPrompt
        {
            get
            {
                if (_initPrompt == null)
                {
                    _initPrompt = new List<string>
                    {
                        $"You are a social media {Role} here to be engaging with an audience.",
                        $"The following messages (if any) will contain context about whom you are representing."
                    };
                }
                return _initPrompt;
            }
            set
            {
                _initPrompt = value;
            }
        }

        private List<string> _guardrails { get; set; } = null;
        private List<string> Guardrails
        {
            get
            {
                if (_guardrails == null)
                {
                    _guardrails = new List<string>
                    {
                        $"That was the end of the messages providing context (if any). You are a social media {Role} here to be engaging with an audience based on that context only!", // remind the bot it is here to serve and get the bot to not guess at questions
                        $"You are replying to a comment or direct message. If there is anything you don't have information for, " +
                        $"DO NOT GUESS, cordially say that you will have to get back to them. It is very important that you do not fabricate facts or figures when asked!", // get the bot to not guess at questions
                        $"Do not indicate you are a bot in anyway, and always deny you are AI or a bot. Say you are \" a(n) {Role}.\" in under 5 words" // very effective in not being verbose when asked if a bot
                        // TODO: the customer is always right and resolution should be done with the follow up plan
                    };
                }
                return _guardrails;
            }
            set
            {
                _guardrails = value;
            }
        }

        #region Equality Methods

        public override bool Equals(object obj)
        {
            var dto = obj as ConversationRequestV1Dto;
            return dto != null && Equals(dto);
        }

        public bool Equals(ConversationRequestV1Dto dto)
        {
            if (!base.Equals(dto))
                return false;

            if (InputString != dto.InputString || Context.SequenceEqual(dto.Context))
                return false;

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ (InputString != null ? InputString.GetHashCode() : 0) ^ (Context != null ? Context.GetHashCode() : 0);
            return hashCode;
        }

        #endregion Equality Methods
    }
}
