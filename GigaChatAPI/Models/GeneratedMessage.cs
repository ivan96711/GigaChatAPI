using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class GeneratedMessage
    {
        /// <summary>
        /// Сгенерированное сообщение
        /// </summary>
        [JsonProperty("message")]
        public Message? Message { get; set; }

        /// <summary>
        /// Индекс сообщения в массиве начиная с ноля
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// Причина завершения гипотезы
        /// </summary>
        /// <remarks>
        /// Например, stop сообщает, что модель закончила формировать гипотезу и вернула полный ответ
        /// </remarks>
        [JsonProperty("finish_reason")]
        public string? FinishReason { get; set; }
    }
}
