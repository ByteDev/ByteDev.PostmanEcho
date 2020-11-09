using System.Text.Json.Serialization;

namespace ByteDev.PostmanEcho.Contract
{
    public class PostmanEchoOAuth1FailureResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("base_uri")]
        public string BaseUri { get; set; }

        [JsonPropertyName("normalized_param_string")]
        public string NormalizedParamString { get; set; }
        
        [JsonPropertyName("base_string")]
        public string BaseString { get; set; }

        [JsonPropertyName("signing_key")]
        public string SigningKey { get; set; }
    }
}