using GigaChatAPI.Interfaces;
using GigaChatAPI.Models;

namespace GigaChatAPI
{
    public class GigaChat : IGigaChat
    {
        const string authorizationUrl = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";
        const string chatUrl = "https://gigachat.devices.sberbank.ru/api/v1";

        readonly IWebClient client;
        readonly ISerializer serializer;
        readonly string authData;
        readonly Scope scope;
        readonly Guid rquid;

        readonly DateTime unixEpoch;

        public Guid RqUID => rquid;

        public bool IsAuthorized => Token is not null && DateTime.UtcNow - unixEpoch.AddMilliseconds(Token.ExpiresAt) > TimeSpan.Zero;

        Token? Token { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="scope">Версия gigachat</param>
        /// <param name="authData">Данные для авторизации. Можно взять в <see href="https://developers.sber.ru/docs/ru/gigachat/api/integration">тут</see></param>
        public GigaChat(Scope scope, string authData)
        {
            this.authData = authData;
            this.scope = scope;
            rquid = Guid.NewGuid();
            client = new HttpWebClient();
            serializer = new JsonSerializer();
            unixEpoch = new (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public async Task AuthorizeAsync()
        {
            Token = await UseProxyAsync<Token>(x => x.GetResponceAsync(
                url: authorizationUrl,
                method: "POST",
                content: $"scope={scope}",
                contentType: "application/x-www-form-urlencoded",
                headers: new[]
                {
                    ("Authorization", $"Bearer {authData}"),
                    ("RqUID", rquid.ToString())
                }));
        }

        public async Task<IEnumerable<Model>> GetModelsAsync()
        {
            if (!IsAuthorized)
                await AuthorizeAsync();

            var result = await UseProxyAsync<Models.Models>(x => x.GetResponceAsync(
                url: $"{chatUrl}/models",
                method: "GET",
                headers: new[]
                {
                    ("Authorization", $"Bearer {Token!.AccessToken}")
                }));

            return result.Data;
        }

        public async Task<Model> GetModelAsync(string modelName)
        {
            if (!IsAuthorized)
                await AuthorizeAsync();

            var result = await UseProxyAsync<Model>(x => x.GetResponceAsync(
                url: $"{chatUrl}/models/{modelName}",
                method: "GET",
                headers: new[]
                {
                    ("Authorization", $"Bearer {Token!.AccessToken}")
                }));

            return result;
        }

        public async Task<ResponseQuery> SendMessage(ModelConfiguration data)
        {
            if (!IsAuthorized)
                await AuthorizeAsync();
            var result = await UseProxyAsync<ResponseQuery>(x => x.GetResponceAsync(
                url: $"{chatUrl}/chat/completions",
                method: "POST",
                content: serializer.Serialize(data),
                headers: new[]
                {
                    ("Authorization", $"Bearer {Token!.AccessToken}")
                }));

            return result;
        }

        public async Task<IEnumerable<TokenCountResponse>> GetTokensCount(TokenCountRequest data)
        {
            if (!IsAuthorized)
                await AuthorizeAsync();

            var result = await UseProxyAsync<List<TokenCountResponse>>(x => x.GetResponceAsync(
                url: $"{chatUrl}/tokens/count",
                method: "POST",
                content: serializer.Serialize(data),
                headers: new[]
                {
                    ("Authorization", $"Bearer {Token!.AccessToken}")
                }));

            return result;
        }

        async Task<T> UseProxyAsync<T>(Func<IWebClient, Task<string>> accessor) where T : class
        {
            var sresult = await accessor.Invoke(client);
            return serializer.Deserialiaze<T>(sresult);
        }
    }
}