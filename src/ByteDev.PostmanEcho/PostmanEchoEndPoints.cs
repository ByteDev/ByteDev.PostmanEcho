using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using ByteDev.ResourceIdentifier;

namespace ByteDev.PostmanEcho
{
    /// <summary>
    /// Represents the Postman Echo API endpoints.
    /// </summary>
    public static class PostmanEchoEndPoints
    {
        private const string BaseUri = "https://postman-echo.com/";

        /// <summary>
        /// Request method related endpoints.
        /// </summary>
        public static class RequestMethods
        {
            /// <summary>
            /// GET request to this endpoint causes the server to respond echoing the HTTP headers, request parameters
            /// and complete URI requested.
            /// </summary>
            public static Uri GetUri(NameValueCollection query = null)
            {
                return CreateUri(new Uri(BaseUri + "get"), query);
            }

            /// <summary>
            /// POST request to this endpoint causes the server to respond echoing the HTTP headers, request parameters,
            /// the contents of the request body and complete URI requested.
            /// </summary>
            public static Uri PostUri(NameValueCollection query = null)
            {
                return CreateUri(new Uri(BaseUri + "post"), query);
            }

            /// <summary>
            /// PUT request to this endpoint causes the server to respond echoing the HTTP headers, request parameters,
            /// the contents of the request body and complete URI requested.
            /// </summary>
            public static Uri PutUri(NameValueCollection query = null)
            {
                return CreateUri(new Uri(BaseUri + "put"), query);
            }

            /// <summary>
            /// PATCH request to this endpoint causes the service to respond echoing the HTTP headers, request parameters,
            /// the contents of the request body and complete URI requested.
            /// </summary>
            public static Uri PatchUri(NameValueCollection query = null)
            {
                return CreateUri(new Uri(BaseUri + "patch"), query);
            }

            /// <summary>
            /// DELETE request to this endpoint causes the service to respond echoing the HTTP headers, request parameters
            /// and complete URI requested.
            /// </summary>
            public static Uri DeleteUri(NameValueCollection query = null)
            {
                return CreateUri(new Uri(BaseUri + "delete"), query);
            }
        }

        /// <summary>
        /// HTTP headers related endpoints.
        /// </summary>
        public static class Headers
        {
            /// <summary>
            /// A GET request to this endpoint returns the list of all request headers as part of the response JSON.
            /// </summary>
            public static readonly Uri RequestHeadersUri = new Uri(BaseUri + "headers");

            /// <summary>
            /// A GET request with querystring name/value pairs to this endpoint causes the server to respond with
            /// the name/value pairs in the response headers and body.
            /// </summary>
            public static Uri ResponseHeadersUri(NameValueCollection query = null)
            {
                return CreateUri(new Uri(BaseUri + "response-headers"), query);
            }
        }

        /// <summary>
        /// Authentication related endpoints.
        /// </summary>
        public static class AuthMethods
        {
            /// <summary>
            /// A GET request to this endpoint simulates a basic-auth request.
            /// </summary>
            public static readonly Uri BasicAuthUri = new Uri(BaseUri + "basic-auth");
            
            /// <summary>
            /// A GET request to this endpoint simulates a digest authorization based request.
            /// </summary>
            public static readonly Uri DigestAuthUri = new Uri(BaseUri + "digest-auth");

            /// <summary>
            /// A GET request to this endpoint simulates a hawk authorization based request.
            /// </summary>
            public static Uri HawkAuthUri => new Uri(BaseUri + "auth/hawk");

            /// <summary>
            /// A GET request to this endpoint simulates a OAuth1.0 authorization based request.
            /// </summary>
            public static readonly Uri OAuth1Uri = new Uri(BaseUri + "oauth1");
        }

        /// <summary>
        /// Cookie related endpoints.
        /// </summary>
        public static class Cookies
        {
            /// <summary>
            /// A GET request to this endpoint with a list of cookies and their values as part of the URL parameters
            /// saves the cookies on the server. The response body returns a list of the currently set the cookies.
            /// </summary>
            public static Uri SetUri(NameValueCollection cookies)
            {
                return CreateUri(new Uri(BaseUri + "cookies/set"), cookies);
            }

            /// <summary>
            /// A GET request to this endpoint results in a response body containing a list of the currently
            /// set cookies.
            /// </summary>
            public static readonly Uri GetUri = new Uri(BaseUri + "cookies");

            /// <summary>
            /// A GET request to this endpoint with a list of cookies as part of the URL parameters deletes the
            /// cookies from the server. The response body returns a list of the currently set the cookies.
            /// </summary>
            public static Uri DeleteUri(IEnumerable<string> cookieNames)
            {
                return new Uri(BaseUri + "cookies/delete").SetQuery(cookieNames.ToQueryString());
            }
        }

        /// <summary>
        /// Utility related endpoints.
        /// </summary>
        public static class Utilities
        {
            /// <summary>
            /// A GET request to this endpoint returns an UTF-8 character encoded response body with
            /// text in various languages such as Greek, Latin, East Asian, etc.
            /// </summary>
            public static readonly Uri GetUtf8EncodedResponseUri = new Uri(BaseUri + "encoding/utf8");
            
            /// <summary>
            /// A GET request to this endpoint returns the response using gzip compression algoritm. The uncompressed
            /// response is a JSON string containing the details of the request sent by the client.
            /// </summary>
            public static readonly Uri GZipCompressedUri = new Uri(BaseUri + "gzip");

            /// <summary>
            /// A GET request to this endpoint returns the response using deflate compression algorithm. The uncompressed
            /// response is a JSON string containing the details of the request sent by the client.
            /// </summary>
            public static readonly Uri DeflateCompressedUri = new Uri(BaseUri + "deflate");

            /// <summary>
            /// A GET request to this endpoint returns the IP address of the source request.
            /// </summary>
            public static readonly Uri IpAddressUri = new Uri(BaseUri + "ip");

            /// <summary>
            /// A GET request to this endpoint returns a response with the given HTTP status code.
            /// </summary>
            /// <param name="code">The HTTP status code to respond with.</param>
            /// <returns>Response status code URI.</returns>
            public static Uri StatusCodeUri(int code)
            {
                if (code < 100 || code > 599)
                    throw new ArgumentOutOfRangeException(nameof(code), "Invalid status code. Status code cannot be less than 100 or more than 599.");

                return new Uri(BaseUri + $"status/{code}");
            }

            /// <summary>
            /// A GET request to this endpoint returns a response with the given HTTP status code.
            /// </summary>
            /// <param name="code">The HTTP status code to respond with.</param>
            /// <returns>Response status code URI.</returns>
            public static Uri StatusCodeUri(HttpStatusCode code)
            {
                return new Uri(BaseUri + $"status/{(int)code}");
            }

            /// <summary>
            /// A GET request to this endpoint allows one to recieve streaming HTTP response using
            /// chunked transfer encoding of a configurable length.
            /// </summary>
            /// <param name="length">Length of chunck.</param>
            /// <returns>Streamed response URI.</returns>
            public static Uri StreamedResponseUri(int length)
            {
                if (length < 0)
                    throw new ArgumentOutOfRangeException(nameof(length), "Invalid length. Length cannot be less than zero.");

                return new Uri(BaseUri + $"stream/{length}");
            }

            /// <summary>
            /// A GET request to this endpoint simulates a delayed response from the server.
            /// </summary>
            /// <param name="seconds">Number of seconds to delay the response (0-10).</param>
            /// <returns>Delay response URI.</returns>
            public static Uri DelayResponseUri(int seconds)
            {
                if (seconds < 0 || seconds > 10)
                    throw new ArgumentOutOfRangeException(nameof(seconds), "Invalid seconds. Seconds cannot be less than zero or more than 10.");

                return new Uri(BaseUri + $"delay/{seconds}");
            }
        }

        internal static Uri CreateUri(Uri uri, NameValueCollection query)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return query == null ? uri : uri.SetQuery(query);
        }
    }
}