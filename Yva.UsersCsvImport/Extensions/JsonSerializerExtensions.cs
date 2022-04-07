using System.IO;
using Newtonsoft.Json;

namespace Yva.UsersCsvImport.Extensions;

public static class JsonSerializerExtensions
{
    public static T Deserialize<T>(this JsonSerializer serializer, Stream dataStream)
    {
        using var streamReader = new StreamReader(dataStream);
        using var jsonTextReader = new JsonTextReader(streamReader);
        return serializer.Deserialize<T>(jsonTextReader);
    }
}