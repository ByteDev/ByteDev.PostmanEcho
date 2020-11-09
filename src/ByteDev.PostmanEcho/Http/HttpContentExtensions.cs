using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ByteDev.PostmanEcho.Contract;
using ByteDev.PostmanEcho.Json;

namespace ByteDev.PostmanEcho.Http
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Net.Http.HttpContent" />.
    /// </summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Deserialize the HTTP content to a dictionary of headers.
        /// </summary>
        /// <param name="source">HTTP content to read from.</param>
        /// <returns>Headers as a dictionary.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Task<Dictionary<string, string>> ReadAsPostmanEchoResponseHeadersAsync(this HttpContent source)
        {
            return ReadAsJsonAsync<Dictionary<string, string>>(source);
        }

        /// <summary>
        /// Deserialize the HTTP content to a <see cref="T:ByteDev.PostmanEcho.Contract.PostmanEchoMethodResponse" />.
        /// </summary>
        /// <param name="source">HTTP content to read from.</param>
        /// <returns>Method response.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static async Task<PostmanEchoMethodResponse> ReadAsPostmanEchoMethodResponseAsync(this HttpContent source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var json = await source.ReadAsStringAsync();

            return new PostmanEchoMethodResponse(json);
        }

        /// <summary>
        /// Deserialize the HTTP content to a <see cref="T:ByteDev.PostmanEcho.Contract.PostmanEchoCookiesResponse" />.
        /// </summary>
        /// <param name="source">HTTP content to read from.</param>
        /// <returns>Cookies response.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static async Task<PostmanEchoCookiesResponse> ReadAsPostmanEchoCookiesResponseAsync(this HttpContent source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var json = await source.ReadAsStringAsync();

            return new PostmanEchoCookiesResponse(json);
        }

        /// <summary>
        /// Decompress and deserialize the HTTP content to a <see cref="T:ByteDev.PostmanEcho.Contract.PostmanEchoCompressedResponse" />.
        /// </summary>
        /// <param name="source">HTTP content to read from.</param>
        /// <returns>GZip decompressed response.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static async Task<PostmanEchoCompressedResponse> ReadAsPostmanEchoGZipResponseAsync(this HttpContent source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var stream = await source.ReadAsStreamAsync();

            stream.Seek(0, SeekOrigin.Begin);

            using (var gzipStream = new GZipStream(stream, CompressionMode.Decompress))
            {
                using (var sr = new StreamReader(gzipStream))
                {
                    var json = await sr.ReadToEndAsync();

                    return new PostmanEchoCompressedResponse(json);
                }
            }
        }

        /// <summary>
        /// Decompress and deserialize the HTTP content to a <see cref="T:ByteDev.PostmanEcho.Contract.PostmanEchoCompressedResponse" />.
        /// </summary>
        /// <param name="source">HTTP content to read from.</param>
        /// <returns>Deflate decompressed response.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static async Task<PostmanEchoCompressedResponse> ReadAsPostmanEchoDeflateResponseAsync(this HttpContent source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var stream = await source.ReadAsStreamAsync();

            stream.Seek(2, SeekOrigin.Begin);

            using (var deflatStream = new DeflateStream(stream, CompressionMode.Decompress))
            {
                using (var sr = new StreamReader(deflatStream))
                {
                    var json = await sr.ReadToEndAsync();

                    return new PostmanEchoCompressedResponse(json);
                }
            }
        }

        /// <summary>
        /// Serialize the HTTP content to a IP address of the request.
        /// </summary>
        /// <param name="source">HTTP content to read from.</param>
        /// <returns>IP address of the request.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static async Task<string> ReadAsPostmanEchoIpAddressAsync(this HttpContent source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var json = await source.ReadAsStringAsync();

            var doc = JsonDocument.Parse(json);

            return doc.GetProperty("ip").GetPropertyString();
        }

        /// <summary>
        /// Deserialize the HTTP content to a <see cref="T:ByteDev.PostmanEcho.Contract.PostmanEchoOAuth1FailureResponse" />.
        /// </summary>
        /// <param name="source">HTTP content to read from.</param>
        /// <returns>OAuth1 failure response.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static async Task<PostmanEchoOAuth1FailureResponse> ReadAsPostmanEchoOAuth1FailureResponseAsync(this HttpContent source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var json = await source.ReadAsStringAsync();

            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<PostmanEchoOAuth1FailureResponse>(json);
        }

        public static async Task<bool> ReadIsHawkAuthSuccessfulAsync(this HttpContent source)
        {
            var body = await source.ReadAsStringAsync();

            return body.Contains("Hawk Authentication Successful");
        }

        internal static async Task<T> ReadAsJsonAsync<T>(this HttpContent source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var json = await source.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}