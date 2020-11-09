using System;
using System.Net.Http.Headers;

namespace ByteDev.PostmanEcho.Http
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Net.Http.Headers.HttpRequestHeaders" />.
    /// </summary>
    public static class HttpRequestHeadersExtensions
    {
        /// <summary>
        /// Adds a header name/value if the header name does not exist; otherwise replaces the
        /// existing value.
        /// </summary>
        /// <param name="source">HTTP request headers.</param>
        /// <param name="name">Header name.</param>
        /// <param name="value">Header value.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static void AddOrUpdate(this HttpRequestHeaders source, string name, string value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source.Contains(name))
            {
                source.Remove(name);
            }

            source.Add(name, value);
        }

        /// <summary>
        /// Adds a Authorization header so the request will pass Basic authorization.
        /// </summary>
        /// <param name="source">HTTP request headers.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static void AddPostmanEchoBasicAuth(this HttpRequestHeaders source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.AddOrUpdate("Authorization", "Basic cG9zdG1hbjpwYXNzd29yZA==");
        }

        /// <summary>
        /// Adds a Authorization header so the request will pass Digest authorization.
        /// </summary>
        /// <param name="source">HTTP request headers.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static void AddPostmanEchoDigestAuth(this HttpRequestHeaders source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.AddOrUpdate("Authorization", "Digest username=\"postman\", realm=\"Users\", nonce=\"ni1LiL0O37PRRhofWdCLmwFsnEtH1lew\", uri=\"/digest-auth\", response=\"254679099562cf07df9b6f5d8d15db44\", opaque=\"\"");
        }

        /// <summary>
        /// Adds a Authorization header with the Hawk authorization value.
        /// </summary>
        /// <param name="source">HTTP request headers.</param>
        /// <param name="authValue">Hawk authorization value.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static void AddPostmanEchoHawkAuth(this HttpRequestHeaders source, string authValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.AddOrUpdate("Authorization", authValue);
        }

        /// <summary>
        /// Adds a Authorization header with the OAuth authorization value.
        /// </summary>
        /// <param name="source">HTTP request headers.</param>
        /// <param name="authValue">OAuth authorization value.</param>
        public static void AddPostmanEchoOAuth1Auth(this HttpRequestHeaders source, string authValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.AddOrUpdate("Authorization", authValue);
        }
    }
}