using GigaChatAPI;
using GigaChatAPI.Interfaces;
using GigaChatAPI.Models;

namespace GigaChatSimpleExample
{
    internal class Program
    {
        static string AuthData => throw new Exception("Вставь сюда авторизационные данные. Подробнее https://developers.sber.ru/docs/ru/gigachat/api/integration");
        
        static void Main(string[] args)
        {
            var gigaChat = new GigaChat(Scope.GIGACHAT_API_PERS, AuthData);
            ShowAllModels(gigaChat).Wait();
            Console.WriteLine();
            Chatting(gigaChat).Wait();
            Console.ReadLine();
        }

        static async Task ShowAllModels(IGigaChat gigaChat)
        {
            var models = await gigaChat.GetModelsAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Доступные модели:");
            Console.ResetColor();
            foreach (var model in models)
                Console.WriteLine(model.Id);
        }

        static async Task Chatting(IGigaChat gigaChat)
        {
            var data = new ModelConfiguration()
            {
                MaxTokens = 1024,
                Messages = new()
                {
                    new Message()
                    {
                        Role = Role.system,
                        Content = "Отвечай как научный сотрудник"
                    }
                }
            };

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Введи сообщение:");
                Console.ResetColor();

                var message = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(message))
                    continue;

                await ShowTokensCount(gigaChat, message);

                data.Messages.Add(new Message()
                {
                    Content = message,
                    Role = Role.user
                });

                var result = await gigaChat.SendMessage(data);
                var modelMessage = result.Choices.First().Message;
                if (modelMessage is not null)
                    data.Messages.Add(modelMessage);

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Ответ модели:");
                Console.ResetColor();
                Console.WriteLine(modelMessage!.Content);

                Console.WriteLine();
            }
        }

        static async Task ShowTokensCount(IGigaChat gigaChat, string message)
        {
            var t = await gigaChat.GetTokensCount(new TokenCountRequest()
            {
                Model = "GigaChat:latest",
                Input = new[]
                {
                    message
                }
            });
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Количество токенов: {t.First().Tokens}");
            Console.ResetColor();
        }
    }
}