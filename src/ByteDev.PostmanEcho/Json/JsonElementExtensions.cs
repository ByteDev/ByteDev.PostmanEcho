using System.Text.Json;

namespace ByteDev.PostmanEcho.Json
{
    internal static class JsonElementExtensions
    {
        public static T GetJsonAs<T>(this JsonElement source)
        {
            var value = source.GetRawText();

            return JsonSerializer.Deserialize<T>(value);
        }
    }
}