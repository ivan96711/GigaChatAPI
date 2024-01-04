using Newtonsoft.Json;

namespace GigaChatAPI.Models
{
    public class TokenCountResponse : Base
    {
        /// <summary>
        /// Количество токенов в соответствующей строке
        /// </summary>
        [JsonProperty("tokens")]
        public int Tokens { get; set; }

        /// <summary>
        /// Количество символов в соответствующей строке
        /// </summary>
        [JsonProperty("characters")]
        public int Characters { get; set; }
    }
}
