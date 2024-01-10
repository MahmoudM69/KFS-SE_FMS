using System;
using System.Linq.Expressions;
using System.Text.Json;

namespace Common.Extensions;

public static class GenericExtensions
{
    public static T? DeserializeJson<T>(this string toDeserialize, JsonSerializerOptions? options = null) where T : class
    {
        try
        {
            return JsonSerializer.Deserialize<T>(toDeserialize, options);
        }
        catch
        {
            return null;
        }
    }

    public static string SerializeJson<T>(this T toSerialize, JsonSerializerOptions? options = null) where T : class
    {
        try
        {
            return JsonSerializer.Serialize(toSerialize, options);
        }
        catch
        {
            return string.Empty;
        }
    }
}
