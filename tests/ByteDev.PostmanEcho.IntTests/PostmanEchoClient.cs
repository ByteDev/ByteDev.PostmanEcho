using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ByteDev.PostmanEcho.Contract;
using ByteDev.PostmanEcho.Http;

namespace ByteDev.PostmanEcho.IntTests
{
    public class PostmanEchoClient
    {
        private readonly HttpClient _httpClient;

        public PostmanEchoClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        #region Request Methods

        public async Task<PostmanEchoMethodResponse> GetAsync(NameValueCollection query = null, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.RequestMethods.GetUri(query), cancellationToken);

            return await response.Content.ReadAsPostmanEchoMethodResponseAsync();
        }

        public async Task<PostmanEchoMethodResponse> PostAsync(HttpContent content, NameValueCollection query = null, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync(PostmanEchoEndPoints.RequestMethods.PostUri(query), content, cancellationToken);

            return await response.Content.ReadAsPostmanEchoMethodResponseAsync();
        }

        public async Task<PostmanEchoMethodResponse> PutAsync(HttpContent content, NameValueCollection query = null, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsync(PostmanEchoEndPoints.RequestMethods.PutUri(query), content, cancellationToken);

            return await response.Content.ReadAsPostmanEchoMethodResponseAsync();
        }

        public async Task<PostmanEchoMethodResponse> PatchAsync(HttpContent content, NameValueCollection query = null, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsync(PostmanEchoEndPoints.RequestMethods.PatchUri(query), content, cancellationToken);

            return await response.Content.ReadAsPostmanEchoMethodResponseAsync();
        }

        public async Task<PostmanEchoMethodResponse> DeleteAsync(NameValueCollection query = null, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync(PostmanEchoEndPoints.RequestMethods.DeleteUri(query), cancellationToken);

            return await response.Content.ReadAsPostmanEchoMethodResponseAsync();
        }

        #endregion

        #region Headers

        /// <summary>
        /// Returns request headers in the response body.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        public async Task<PostmanEchoMethodResponse> GetRequestHeadersAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Headers.RequestHeadersUri, cancellationToken);

            return await response.Content.ReadAsPostmanEchoMethodResponseAsync();
        }

        /// <summary>
        /// Returns request querystring name/values in the response header and body.
        /// </summary>
        /// <param name="query">URI query string.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>HttpResponseMessage.</returns>
        public Task<HttpResponseMessage> GetResponseHeadersAsync(NameValueCollection query = null, CancellationToken cancellationToken = default)
        {
            return _httpClient.GetAsync(PostmanEchoEndPoints.Headers.ResponseHeadersUri(query), cancellationToken);
        }

        #endregion

        #region Auth

        public async Task<HttpStatusCode> GetBasicAuthAsync(bool isAuthorized, CancellationToken cancellationToken = default)
        {
            if (isAuthorized)
                _httpClient.DefaultRequestHeaders.AddPostmanEchoBasicAuth();
            else
                _httpClient.DefaultRequestHeaders.Remove("Authorization");

            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.AuthMethods.BasicAuthUri, cancellationToken);

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> GetDigestAuthAsync(bool isAuthorized, CancellationToken cancellationToken = default)
        {
            if (isAuthorized)
                _httpClient.DefaultRequestHeaders.AddPostmanEchoDigestAuth();
            else
                _httpClient.DefaultRequestHeaders.Remove("Authorization");

            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.AuthMethods.DigestAuthUri, cancellationToken);

            return response.StatusCode;
        }

        public async Task<bool> GetHawkAuthAsync(bool isAuthorized, CancellationToken cancellationToken = default)
        {
            // var hawkAuthId = "dh37fgj492je";
            // var hawkAuthKey = "werxhqb98rpaxn39848xrunpaw3489ruxnpa98w4rxn";

            if (isAuthorized)
            {
                var headerValue = "Hawk id=\"dh37fgj492je\", ts=\"1604891768\", nonce=\"qPnAq_\", mac=\"d9VSLEOODbLWsBkOKBatOZtJr5y9+gvNO57yxOKVu+k=\"";

                _httpClient.DefaultRequestHeaders.AddPostmanEchoHawkAuth(headerValue);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }

            var content = PostmanEchoHawkContentFactory.Create();

            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.AuthMethods.HawkAuthUri, content, cancellationToken);

            var isSuccessful = await response.Content.ReadIsHawkAuthSuccessfulAsync();

            return response.StatusCode == HttpStatusCode.OK && isSuccessful;
        }

        public async Task<HttpStatusCode> GetOAuth1AuthAsync(bool isAuthorized, CancellationToken cancellationToken = default)
        {
            // var consumerKey = "RKCGzna7bv9YD57c";
            // var consumerSecret = "D+EdQ-gs$-%@2Nu7";

            HttpResponseMessage response;

            if (isAuthorized)
            {
                var headerValue = "OAuth oauth_consumer_key=\"RKCGzna7bv9YD57c\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"1472121261\",oauth_nonce=\"ki0RQW\",oauth_version=\"1.0\",oauth_signature=\"s0rK92Myxx7ceUBVzlMaxiiXU00%3D\"";

                _httpClient.DefaultRequestHeaders.AddPostmanEchoOAuth1Auth(headerValue);

                var content = PostmanEchoOAuth1ContentFactory.Create();

                response = await _httpClient.GetAsync(PostmanEchoEndPoints.AuthMethods.OAuth1Uri, content, cancellationToken);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");

                response = await _httpClient.GetAsync(PostmanEchoEndPoints.AuthMethods.OAuth1Uri, cancellationToken);
            }
            
            if (!response.IsSuccessStatusCode)
            {
                PostmanEchoOAuth1FailureResponse failureResponse = await response.Content.ReadAsPostmanEchoOAuth1FailureResponseAsync();
            }

            return response.StatusCode;
        }

        #endregion

        #region Cookies

        public async Task<PostmanEchoCookiesResponse> SetCookiesAsync(NameValueCollection cookies = null, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Cookies.SetUri(cookies), cancellationToken);

            return await response.Content.ReadAsPostmanEchoCookiesResponseAsync();
        }

        public async Task<PostmanEchoCookiesResponse> GetCookiesAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Cookies.GetUri, cancellationToken);

            return await response.Content.ReadAsPostmanEchoCookiesResponseAsync();
        }

        public async Task<PostmanEchoCookiesResponse> DeleteCookiesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Cookies.DeleteUri(names), cancellationToken);

            return await response.Content.ReadAsPostmanEchoCookiesResponseAsync();
        }

        #endregion

        #region Utilities

        public async Task<HttpStatusCode> ResponseStatusCodeAsync(HttpStatusCode code, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Utilities.StatusCodeUri(code), cancellationToken);

            return response.StatusCode;
        }

        public async Task<HttpResponseMessage> StreamedResponseAsync(int length, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Utilities.StreamedResponseUri(length), cancellationToken);

            return response;
        }

        public async Task<HttpResponseMessage> DelayResponseAsync(int seconds, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Utilities.DelayResponseUri(seconds), cancellationToken);

            return response;
        }

        public async Task<string> GetUtf8EncodedResponseAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Utilities.GetUtf8EncodedResponseUri, cancellationToken);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<PostmanEchoCompressedResponse> GZipCompressedAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Utilities.GZipCompressedUri, cancellationToken);

            return await response.Content.ReadAsPostmanEchoGZipResponseAsync();
        }

        public async Task<PostmanEchoCompressedResponse> DeflateCompressedAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Utilities.DeflateCompressedUri, cancellationToken);

            return await response.Content.ReadAsPostmanEchoDeflateResponseAsync();
        }

        public async Task<string> IpAddressAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync(PostmanEchoEndPoints.Utilities.IpAddressUri, cancellationToken);

            return await response.Content.ReadAsPostmanEchoIpAddressAsync();
        }
        
        #endregion
    }
}