using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class ModelConfiguration
    {
        /// <summary>
        /// Название модели, от которой нужно получить ответ.
        /// По умолчанию: GigaChat:latest (последняя актуальная модель)
        /// </summary>
        [JsonProperty("model")]
        public string Model { get; set; } = "GigaChat:latest";

        /// <summary>
        /// Массив сообщений
        /// </summary>
        /// <remarks>
        /// Передайте несколько прошлых сообщений, чтобы сервис учитывал историю чата.
        /// Подробнее читайте в разделе <see href="https://developers.sber.ru/docs/ru/gigachat/api/keeping-context">Работа с историей чата</see>
        /// </remarks>
        [JsonProperty("messages")]
        public List<Message> Messages { get; set; } = new();

        /// <summary>
        /// Температура выборки в диапазоне от ноля до двух.
        /// По умолчанию: 0.47
        /// </summary>
        /// <remarks>
        /// Чем выше значение, тем более случайным будет ответ модели.
        /// </remarks>
        [JsonProperty("temperature")]
        public float Temperature { get; set; } = 0.47f;

        /// <summary>
        /// Количество вариантов ответов, которые нужно сгенерировать для каждого входного сообщения. 
        /// По умолчанию: 1
        /// </summary>
        [JsonProperty("n")]
        public int N { get; set; } = 1;

        /// <summary>
        /// Максимальное количество токенов, которые будут использованы для создания ответов.
        /// По умолчанию: 512
        /// </summary>
        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; } = 512;

        /// <summary>
        /// Количество повторений слов.
        /// По умолчанию: 1.07
        /// </summary>
        /// <remarks>
        /// При значениях 0 до 1 модель будет повторять уже использованные слова.
        /// Значение 1.0 — нейтральное значение.
        /// При значении больше 1 модель будет стараться не повторять слова.
        /// </remarks>
        [JsonProperty("repetition_penalty")]
        public float RepetitionPenalty { get; set; } = 1.07f;

        ///// <summary>
        ///// Указывает, что сообщения надо передавать по частям в потоке.
        ///// По умолчанию: false
        ///// </summary>
        ///// <remarks>
        ///// Указывает, что сообщения надо передавать по частям в потоке. Сообщения передаются по протоколу SSE.
        ///// Подробнее читайте в разделе <see href="https://developers.sber.ru/docs/ru/gigachat/api/response-token-streaming">Потоковая передача токенов</see>
        ///// </remarks>
        //[JsonProperty("stream")]
        //public bool Stream { get; set; } = false;

        ///// <summary>
        ///// Параметр потокового режима (<see cref="Stream"/> = true).
        ///// По умолчанию: 0
        ///// </summary>
        ///// <remarks>
        ///// Задает минимальный интервал в секундах, который проходит между отправкой токенов. 
        ///// Например, если указать 1, сообщения будут приходить каждую секунду, 
        ///// но размер каждого из них будет больше, так как за секунду накапливается много токенов
        ///// </remarks>
        //[JsonProperty("update_interval")]
        //public float UpdateInterval { get; set; } = 0;
    }
}
