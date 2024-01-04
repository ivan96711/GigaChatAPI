using GigaChatAPI.Models;

namespace GigaChatAPI.Interfaces
{
    public interface IGigaChat
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        /// <remarks>
        /// Параметр для журналирования входящих вызовов и разбора инцидентов
        /// </remarks>
        Guid RqUID { get; }

        /// <summary>
        /// Произведена ли авторизация и актуален ли еще токен
        /// </summary>
        bool IsAuthorized { get; }

        /// <summary>
        /// Произвести авторизацию
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Токен живет 30 минут
        /// </remarks>
        Task AuthorizeAsync();

        /// <summary>
        /// Возвращает массив объектов с данными доступных моделей
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Model>> GetModelsAsync();

        /// <summary>
        /// Возвращает объект с описанием указанной модели
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        Task<Model> GetModelAsync(string modelName);

        /// <summary>
        /// Возвращает ответ модели с учетом переданных сообщений
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<ResponseQuery> SendMessage(ModelConfiguration data);

        /// <summary>
        /// Возвращает объект с информацией о количестве токенов, посчитанных заданной моделью в строках, переданных в массиве Input
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<IEnumerable<TokenCountResponse>> GetTokensCount(TokenCountRequest data);
    }
}
