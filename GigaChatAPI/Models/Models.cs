using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class Models : Base
    {
        [JsonProperty("data")]
        public IEnumerable<Model> Data { get; set; } = Array.Empty<Model>();
    }
}
