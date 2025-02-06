using MachineLearningIntelligenceAPI.Common.StringConstants;
using System.Net;
using System.Text.Json;

namespace MachineLearningIntelligenceAPI.DataAccess.Repositories
{
    public class RepositoryBase
    {
        public static JsonSerializerOptions DefaultDeserializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public static JsonSerializerOptions TupleSerializerOptions = new JsonSerializerOptions { IncludeFields = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
        public static JsonSerializerOptions NoNullSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
        public virtual void HandleNonSuccessHttpResponseMessage(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception(BadRequestString.RepositoryError);
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Exception(UnauthorizedString.RepositoryError);
            }
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new Exception(ForbiddenString.RepositoryError);
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception(InternalServerErrorString.RepositoryError);
            }
            if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new Exception(ServiceUnavailableString.RepositoryError);
            }

            //default 500
            throw new Exception(InternalServerErrorString.RepositoryError);
        }
    }
}
