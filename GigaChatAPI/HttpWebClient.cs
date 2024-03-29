﻿using GigaChatAPI.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GigaChatAPI
{
    internal class HttpWebClient : IWebClient
    {
        readonly HttpClient _client;

        public HttpWebClient()
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                }
            };

            _client = new HttpClient(handler);

            ServicePointManager.ServerCertificateValidationCallback += (
                object sender,
                X509Certificate? certificate,
                X509Chain? chain,
                SslPolicyErrors sslPolicyErrors) => true;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol =
                //SecurityProtocolType.Ssl3 |
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls;

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetResponceAsync(
            string url,
            string method = "Get",
            string? content = null,
            string contentType = "application/json",
            IEnumerable<(string, string)>? headers = null,
            CancellationToken? canceletionToken = null)
        {
            using var request = new HttpRequestMessage
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri(url),
            };

            if (headers is not null)
                foreach (var header in headers)
                    request.Headers.TryAddWithoutValidation(header.Item1, header.Item2);

            if (!string.IsNullOrEmpty(content))
                request.Content = new StringContent(content, Encoding.UTF8, contentType);

            using var response = await _client.SendAsync(request, canceletionToken ?? CancellationToken.None).ConfigureAwait(false);
            var result = await GetResponseTextAsync(response);
            return result;
        }

        private async Task<string> GetResponseTextAsync(HttpResponseMessage response)
        {
            string text = await response.Content.ReadAsStringAsync();
            return response.StatusCode switch
            {
                HttpStatusCode.OK or HttpStatusCode.Created => text,
                HttpStatusCode.BadRequest => throw new BadRequestException(text),
                HttpStatusCode.Unauthorized => throw new UnauthorizedAccessException(),
                HttpStatusCode.NotFound => throw new NoSuchModelException(text),
                HttpStatusCode.InternalServerError => throw new InternalServerErrorException(text),
                _ => throw new WebClientException(text),
            };
        }
    }

    public class WebClientException : Exception
    {
        public WebClientException() : base() { }

        public WebClientException(string msg) : base(msg) { }
    }

    public class BadRequestException : WebClientException
    {
        public BadRequestException(string msg) : base(msg) { }
    }

    public class NoSuchModelException : WebClientException
    {
        public NoSuchModelException(string msg) : base(msg) { }
    }

    public class InternalServerErrorException : WebClientException
    {
        public InternalServerErrorException(string msg) : base(msg) { }
    }
}
