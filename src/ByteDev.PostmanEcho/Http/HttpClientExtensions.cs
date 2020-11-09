using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ByteDev.PostmanEcho.Http
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Net.Http.HttpClient" />.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Send a PATCH request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="client" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="requestUri" /> is null.</exception>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            return client.PatchAsync(CreateUri(requestUri), content);
        }

        /// <summary>
        /// Send a PATCH request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="client" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="requestUri" /> is null.</exception>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content)
        {
            return client.PatchAsync(requestUri, content, CancellationToken.None);
        }
        /// <summary>
        /// Send a PATCH request with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="client" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="requestUri" /> is null.</exception>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return client.PatchAsync(CreateUri(requestUri), content, cancellationToken);
        }

        /// <summary>
        /// Send a PATCH request with a cancellation token as an asynchronous operation.
        /// </summary>
        /// <param name="client">The instantiated Http Client <see cref="HttpClient"/></param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="content">The HTTP request content sent to the server.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1"/>.The task object representing the asynchronous operation.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="client" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="requestUri" /> is null.</exception>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return client.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = content
            }, cancellationToken);
        }

        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = requestUri,
                Content = content
            };

            return client.SendAsync(req, cancellationToken);
        }

        private static Uri CreateUri(string uri)
        {
            return string.IsNullOrEmpty(uri) ? null : new Uri(uri, UriKind.RelativeOrAbsolute);
        }
    }
}