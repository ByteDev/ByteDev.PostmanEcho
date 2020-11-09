using System;
using System.Collections.Generic;
using System.Text.Json;
using ByteDev.PostmanEcho.Json;

namespace ByteDev.PostmanEcho.Contract
{
    /// <summary>
    /// Represents a response from a Postman Echo API request method and headers endpoints.
    /// </summary>
    public class PostmanEchoMethodResponse
    {
        private IDictionary<string, string> _headers;
        private IDictionary<string, string> _queryString;

        /// <summary>
        /// The URL the request was made to.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Headers sent in the request.
        /// </summary>
        public IDictionary<string, string> Headers
        {
            get => _headers ?? (_headers = new Dictionary<string, string>());
            private set => _headers = value;
        }

        /// <summary>
        /// Query string name values sent in the request (args JSON field).
        /// </summary>
        public IDictionary<string, string> QueryString
        {
            get => _queryString ?? (_queryString = new Dictionary<string, string>());
            private set => _queryString = value;
        }

        /// <summary>
        /// Body content sent in the request (data JSON field).
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.PostmanEcho.Contract.PostmanEchoMethodResponse" /> class.
        /// </summary>
        /// <param name="json">JSON returned in the response body from the Postman Echo API.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="json" />JSON was null or empty.</exception>
        public PostmanEchoMethodResponse(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("Response JSON was null or empty.", nameof(json));

            var doc = JsonDocument.Parse(json);

            Url = doc.GetProperty("url").GetPropertyString();
            Headers = doc.GetProperty("headers").GetPropertyDictionary();
            QueryString = doc.GetProperty("args").GetPropertyDictionary();
            Body = doc.GetProperty("data").GetPropertyString();
        }

        /// <summary>
        /// Retrieves a header value.
        /// </summary>
        /// <param name="name">Header name.</param>
        /// <returns>Header value</returns>
        /// <exception cref="T:System.ArgumentException">Name cannot be null or empty.</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Header name does not exist.</exception>
        public string GetHeader(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.");

            return Headers[name.ToLower()];
        }
    }
}