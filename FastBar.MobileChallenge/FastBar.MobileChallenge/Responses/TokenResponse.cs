using Newtonsoft.Json;

namespace FastBar.MobileChallenge.Responses
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}