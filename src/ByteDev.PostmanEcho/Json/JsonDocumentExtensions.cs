using System.Linq;
using System.Text.Json;

namespace ByteDev.PostmanEcho.Json
{
    internal static class JsonDocumentExtensions
    {
        public static JsonProperty GetProperty(this JsonDocument source, string name)
        {
            return source.RootElement.EnumerateObject().FirstOrDefault(r => r.Name == name);
        }
    }
}