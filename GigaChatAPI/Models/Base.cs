using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class Base
    {
        [JsonProperty("object")]
        public string? Object { get; set; }
    }
}
