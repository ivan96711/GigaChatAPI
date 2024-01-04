using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class ResponseQuery : Base
    {
        /// <summary>
        /// Массив ответов модели
        /// </summary>
        [JsonProperty("choices")]
        public IEnumerable<GeneratedMessage> Choices { get; set; } = Array.Empty<GeneratedMessage>();

        /// <summary>
        /// Дата и время создания ответа в формате Unix time
        /// </summary>
        [JsonProperty("created")]
        public int Created { get; set; }

        /// <summary>
        /// Название модели, которая вернула ответ
        /// </summary>
        [JsonProperty("model")]
        public string? Model { get; set; }

        /// <summary>
        /// Данные об использовании модели
        /// </summary>
        [JsonProperty("usage")]
        public ModelUsage? Usage { get; set; }
    }
}
