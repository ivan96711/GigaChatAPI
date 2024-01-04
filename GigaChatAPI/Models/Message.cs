using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class Message
    {
        /// <summary>
        /// Роль автора сообщения
        /// </summary>
        [JsonProperty("role")]
        public Role Role { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [JsonProperty("content")]
        public string? Content { get; set; }
    }
}
