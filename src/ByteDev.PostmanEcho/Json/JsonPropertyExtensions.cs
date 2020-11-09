using System.Collections.Generic;
using System.Text.Json;

namespace ByteDev.PostmanEcho.Json
{
    internal static class JsonPropertyExtensions
    {
        public static string GetPropertyString(this JsonProperty source)
        {
            if (source.Value.ValueKind != JsonValueKind.String)
                return null;

            return source.Value.GetString();
        }

        public static bool GetPropertyBool(this JsonProperty source)
        {
            if (source.Value.ValueKind != JsonValueKind.True)
                return false;

            return source.Value.GetBoolean();
        }

        public static IDictionary<string, string> GetPropertyDictionary(this JsonProperty source)
        {
            if (source.Value.ValueKind == JsonValueKind.Undefined)
                return new Dictionary<string, string>();

            return source.Value.GetJsonAs<Dictionary<string, string>>();
        }
    }
}