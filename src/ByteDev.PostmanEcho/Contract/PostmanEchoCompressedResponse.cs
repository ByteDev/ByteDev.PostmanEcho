using System;
using System.Collections.Generic;
using System.Text.Json;
using ByteDev.PostmanEcho.Json;

namespace ByteDev.PostmanEcho.Contract
{
    /// <summary>
    /// Represents a response from a Postman Echo API compression related endpoint.
    /// </summary>
    public class PostmanEchoCompressedResponse
    {
        private IDictionary<string, string> _headers;

        /// <summary>
        /// Determines if the compression is of type GZip.
        /// </summary>
        public bool IsGZipped { get; }

        /// <summary>
        /// Determines if the compression is of type Deflated.
        /// </summary>
        public bool IsDeflated { get; }

        /// <summary>
        /// Headers sent in the request.
        /// </summary>
        public IDictionary<string, string> Headers
        {
            get => _headers ?? (_headers = new Dictionary<string, string>());
            private set => _headers = value;
        }

        /// <summary>
        /// HTTP request method.
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.PostmanEcho.Contract.PostmanEchoCompressedResponse" /> class.
        /// </summary>
        /// <param name="json">JSON returned in the response body from the Postman Echo API.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="json" />JSON was null or empty.</exception>
        public PostmanEchoCompressedResponse(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("Response JSON was null or empty.", nameof(json));
            
            var doc = JsonDocument.Parse(json);

            IsGZipped = doc.GetProperty("gzipped").GetPropertyBool();
            IsDeflated = doc.GetProperty("deflated").GetPropertyBool();
            Headers = doc.GetProperty("headers").GetPropertyDictionary();
            Method = doc.GetProperty("method").GetPropertyString();
        }
    }
}