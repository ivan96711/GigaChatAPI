using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }
    }
}
