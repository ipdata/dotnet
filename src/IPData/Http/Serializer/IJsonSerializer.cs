namespace IPData.Http.Serializer
{
    public interface ISerializer
    {
        string Serialize(object item);

        T Deserialize<T>(string json);
    }
}
