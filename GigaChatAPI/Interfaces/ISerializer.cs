namespace GigaChatAPI.Interfaces
{
    internal interface ISerializer
    {
        T Deserialiaze<T>(string data);

        string Serialize<T>(T data);
    }
}
