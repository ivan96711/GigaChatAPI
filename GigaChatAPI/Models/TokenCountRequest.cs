using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class TokenCountRequest
    {
        /// <summary>
        /// Название модели, которая будет использована для подсчета количества токенов
        /// </summary>
        [JsonProperty("model")]
        public string? Model { get; set; }

        /// <summary>
        /// Массив строк, в которых надо подсчитать количество токенов
        /// </summary>
        [JsonProperty("input")]
        public IEnumerable<string> Input { get; set; } = Enumerable.Empty<string>();
    }
}
