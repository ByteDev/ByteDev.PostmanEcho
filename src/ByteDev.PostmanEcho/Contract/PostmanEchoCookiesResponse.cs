using System;
using System.Collections.Generic;
using System.Text.Json;
using ByteDev.PostmanEcho.Json;

namespace ByteDev.PostmanEcho.Contract
{
    /// <summary>
    /// Responses a response from a Postman Echo API cookies endpoint.
    /// </summary>
    public class PostmanEchoCookiesResponse
    {
        private IDictionary<string, string> _cookies;

        /// <summary>
        /// Cookies currently set on the Postman Echo API.
        /// </summary>
        public IDictionary<string, string> Cookies
        {
            get => _cookies ?? (_cookies = new Dictionary<string, string>());
            private set => _cookies = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.PostmanEcho.Contract.PostmanEchoCookiesResponse" /> class.
        /// </summary>
        /// <param name="json">JSON returned in the response body from the Postman Echo API.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="json" />JSON was null or empty.</exception>
        public PostmanEchoCookiesResponse(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("Response JSON was null or empty.", nameof(json));
            
            var doc = JsonDocument.Parse(json);

            Cookies = doc.GetProperty("cookies").GetPropertyDictionary();
        }
    }
}