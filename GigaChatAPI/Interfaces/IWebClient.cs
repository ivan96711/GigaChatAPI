namespace GigaChatAPI.Interfaces
{
    internal interface IWebClient
    {
        Task<string> GetResponceAsync(
            string url,
            string method = "Get",
            string? content = null,
            string contentType = "application/json",
            IEnumerable<(string, string)>? headers = null,
            CancellationToken? canceletionToken = null);
    }
}
