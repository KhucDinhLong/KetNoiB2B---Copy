using Newtonsoft.Json;

namespace SecureNetRestApiSDK.Api.Responses
{
    public class TokenCardResponse : SecureNetResponse
    {
        #region Properties

        [JsonProperty("token")]
        public string Token { get; set; }

        public string CustomerId { get; set; }
        #endregion
    }
}
