using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class Model : Base
    {
        /// <summary>
        /// Название модели
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Владелец модели
        /// </summary>
        [JsonProperty("owned_by")]
        public string? OwnedBy { get; set; }
    }
}
