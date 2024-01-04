using GigaChatAPI.Interfaces;
using Newtonsoft.Json;

namespace GigaChatAPI
{
    internal class JsonSerializer : ISerializer
    {
        readonly JsonSerializerSettings settings;

        public JsonSerializer()
        {
            settings = new JsonSerializerSettings
            {
                Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() }
            };
        }

        public T Deserialiaze<T>(string data)
        {
            var result = JsonConvert.DeserializeObject<T>(data, settings)
                ?? throw new InvalidOperationException();
            return result;
        }

        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, settings);
        }
    }
}
