using System;
using ByteDev.PostmanEcho.Http;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ByteDev.PostmanEcho.IntTests
{
    [TestFixture]
    public class PostmanEchoClientTests
    {
        private PostmanEchoClient _sut;
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();

            _sut = new PostmanEchoClient(_httpClient);
        }

        [TestFixture]
        public class GetAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenReturnResponse()
            {
                var query = new NameValueCollection
                {
                    {"Name1", "Value1"}, 
                    {"Name2", "Value2"}
                };

                var result = await _sut.GetAsync(query);

                Assert.That(result.Url, Is.EqualTo(PostmanEchoEndPoints.RequestMethods.GetUri(query).AbsoluteUri));
                Assert.That(result.QueryString["Name1"], Is.EqualTo("Value1"));
                Assert.That(result.QueryString["Name2"], Is.EqualTo("Value2"));
            }

            [Test]
            public async Task WhenHasNoQueryStringArgs_ThenReturnEmpty()
            {
                var result = await _sut.GetAsync();

                Assert.That(result.Url, Is.EqualTo(PostmanEchoEndPoints.RequestMethods.GetUri().AbsoluteUri));
                Assert.That(result.QueryString, Is.Empty);
            }

            [Test]
            public async Task WhenDoesNotHaveHeader_ThenThrowException()
            {
                var result = await _sut.GetAsync();

                Assert.Throws<KeyNotFoundException>(() => _ = result.GetHeader("TestName2"));
            }

            [Test]
            public async Task WhenHasHeader_ThenReturnHeader()
            {
                _httpClient.DefaultRequestHeaders.Add("TestName1", "TestValue1");

                var result = await _sut.GetAsync();

                Assert.That(result.GetHeader("TestName1"), Is.EqualTo("TestValue1"));
            }

            [Test]
            public async Task WhenHasHeaders_ThenReturnHeaders()
            {
                _httpClient.DefaultRequestHeaders.Add("TestName1", new []{ "TestValue1", "TestValue2" });

                var result = await _sut.GetAsync();

                Assert.That(result.GetHeader("TestName1"), Is.EqualTo("TestValue1, TestValue2"));
            }
        }

        [TestFixture]
        public class PostAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenHeaderQueryStringBody_ThenReturnsResponse()
            {
                var query = new NameValueCollection
                {
                    {"Name1", "Value1"}, 
                    {"Name2", "Value2"}
                };

                _httpClient.DefaultRequestHeaders.Add("PostName1", "PostValue1");

                var result = await _sut.PostAsync(new StringContent("Test data"), query);

                Assert.That(result.Url, Is.EqualTo(PostmanEchoEndPoints.RequestMethods.PostUri(query).AbsoluteUri));
                Assert.That(result.Body, Is.EqualTo("Test data"));
                Assert.That(result.GetHeader("PostName1"), Is.EqualTo("PostValue1"));
                Assert.That(result.QueryString["Name1"], Is.EqualTo("Value1"));
                Assert.That(result.QueryString["Name2"], Is.EqualTo("Value2"));
            }
        }

        [TestFixture]
        public class PutAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenHeaderQueryStringBody_ThenReturnsResponse()
            {
                var query = new NameValueCollection
                {
                    {"Name1", "Value1"}, 
                    {"Name2", "Value2"}
                };

                _httpClient.DefaultRequestHeaders.Add("PutName1", "PutValue1");

                var result = await _sut.PutAsync(new StringContent("Test data"), query);

                Assert.That(result.Url, Is.EqualTo(PostmanEchoEndPoints.RequestMethods.PutUri(query).AbsoluteUri));
                Assert.That(result.Body, Is.EqualTo("Test data"));
                Assert.That(result.GetHeader("PutName1"), Is.EqualTo("PutValue1"));
                Assert.That(result.QueryString["Name1"], Is.EqualTo("Value1"));
                Assert.That(result.QueryString["Name2"], Is.EqualTo("Value2"));
            }
        }

        [TestFixture]
        public class PatchAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenHeaderQueryStringBody_ThenReturnsResponse()
            {
                var query = new NameValueCollection
                {
                    {"Name1", "Value1"}, 
                    {"Name2", "Value2"}
                };

                _httpClient.DefaultRequestHeaders.Add("PatchName1", "PatchValue1");

                var result = await _sut.PatchAsync(new StringContent("Test data"), query);

                Assert.That(result.Url, Is.EqualTo(PostmanEchoEndPoints.RequestMethods.PatchUri(query).AbsoluteUri));
                Assert.That(result.Body, Is.EqualTo("Test data"));
                Assert.That(result.GetHeader("PatchName1"), Is.EqualTo("PatchValue1"));
                Assert.That(result.QueryString["Name1"], Is.EqualTo("Value1"));
                Assert.That(result.QueryString["Name2"], Is.EqualTo("Value2"));
            }
        }

        [TestFixture]
        public class DeleteAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenHeaderAndQueryString_ThenReturnsResponse()
            {
                var query = new NameValueCollection
                {
                    {"Name1", "Value1"}, 
                    {"Name2", "Value2"}
                };

                _httpClient.DefaultRequestHeaders.Add("DeleteName1", "DeleteValue1");

                var result = await _sut.DeleteAsync(query);

                Assert.That(result.Url, Is.EqualTo(PostmanEchoEndPoints.RequestMethods.DeleteUri(query).AbsoluteUri));
                Assert.That(result.Body, Is.Null);
                Assert.That(result.GetHeader("DeleteName1"), Is.EqualTo("DeleteValue1"));
                Assert.That(result.QueryString["Name1"], Is.EqualTo("Value1"));
                Assert.That(result.QueryString["Name2"], Is.EqualTo("Value2"));
            }
        }

        [TestFixture]
        public class GetRequestHeadersAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenHasHeader_ThenReturnHeader()
            {
                _httpClient.DefaultRequestHeaders.Add("TestName1", "TestValue1");

                var result = await _sut.GetRequestHeadersAsync();

                Assert.That(result.GetHeader("TestName1"), Is.EqualTo("TestValue1"));
            }
        }

        [TestFixture]
        public class GetResponseHeadersAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenHasQueryString_ThenReturnInHeadersAndBody()
            {
                var query = new NameValueCollection
                {
                    {"QsName1", "Value1"}, 
                    {"QsName2", "Value2"}
                };

                var response = await _sut.GetResponseHeadersAsync(query);

                Assert.That(response.Headers.GetValues("QsName1").Single(), Is.EqualTo("Value1"));
                Assert.That(response.Headers.GetValues("QsName2").Single(), Is.EqualTo("Value2"));

                var result = await response.Content.ReadAsPostmanEchoResponseHeadersAsync();

                Assert.That(result["QsName1"], Is.EqualTo("Value1"));
                Assert.That(result["QsName2"], Is.EqualTo("Value2"));
            }
        }

        [TestFixture]
        public class GetBasicAuthAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenIsAuthorized_ThenReturnOk()
            {
                var result = await _sut.GetBasicAuthAsync(true);

                Assert.That(result, Is.EqualTo(HttpStatusCode.OK));
            }

            [Test]
            public async Task WhenIsNotAuthorized_ThenReturnUnauthorized()
            {
                var result = await _sut.GetBasicAuthAsync(false);

                Assert.That(result, Is.EqualTo(HttpStatusCode.Unauthorized));
            }
        }

        [TestFixture]
        public class GetDigestAuthAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenIsAuthorized_ThenReturnOk()
            {
                var result = await _sut.GetDigestAuthAsync(true);

                Assert.That(result, Is.EqualTo(HttpStatusCode.OK));
            }

            [Test]
            public async Task WhenIsNotAuthorized_ThenReturnUnauthorized()
            {
                var result = await _sut.GetDigestAuthAsync(false);

                Assert.That(result, Is.EqualTo(HttpStatusCode.Unauthorized));
            }
        }

        [TestFixture]
        public class GetHawkAuthAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenIsAuthorized_ThenReturnTrue()
            {
                var result = await _sut.GetHawkAuthAsync(true);

                Assert.That(result, Is.True);
            }

            [Test]
            public async Task WhenIsNotAuthorized_ThenReturnFalse()
            {
                var result = await _sut.GetHawkAuthAsync(false);

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class GetOAuth1AuthAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenIsAuthorized_ThenReturnOk()
            {
                var result = await _sut.GetOAuth1AuthAsync(true);

                Assert.That(result, Is.EqualTo(HttpStatusCode.OK));
            }

            [Test]
            public async Task WhenIsNotAuthorized_ThenReturnUnauthorized()
            {
                var result = await _sut.GetOAuth1AuthAsync(false);

                Assert.That(result, Is.EqualTo(HttpStatusCode.Unauthorized));
            }
        }

        [TestFixture]
        public class SetCookiesAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenSetsCookies()
            {
                var query = new NameValueCollection
                {
                    {"Cookie1", "Value1"}, 
                    {"Cookie2", "Value2"}
                };

                var result = await _sut.SetCookiesAsync(query);

                Assert.That(result.Cookies.Count, Is.EqualTo(2));
                Assert.That(result.Cookies["Cookie1"], Is.EqualTo("Value1"));
                Assert.That(result.Cookies["Cookie2"], Is.EqualTo("Value2"));
            }
        }

        [TestFixture]
        public class GetCookiesAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenGetsCookies()
            {
                var query = new NameValueCollection
                {
                    {"Cookie10", "Value10"}, 
                    {"Cookie20", "Value20"}
                };

                await _sut.SetCookiesAsync(query);

                var result = await _sut.GetCookiesAsync();

                Assert.That(result.Cookies.Count, Is.EqualTo(2));
                Assert.That(result.Cookies["Cookie10"], Is.EqualTo("Value10"));
                Assert.That(result.Cookies["Cookie20"], Is.EqualTo("Value20"));
            }
        }

        [TestFixture]
        public class DeleteCookiesAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenDeletesCookies()
            {
                var query = new NameValueCollection
                {
                    {"Cookie100", "Value100"}, 
                    {"Cookie200", "Value200"}
                };

                await _sut.SetCookiesAsync(query);

                var result = await _sut.DeleteCookiesAsync(new []{ "Cookie100", "Cookie200" });

                Assert.That(result.Cookies, Is.Empty);
            }
        }

        [TestFixture]
        public class ResponseStatusCodeAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenReturnsStatusCode()
            {
                var result = await _sut.ResponseStatusCodeAsync(HttpStatusCode.BadRequest);

                Assert.That(result, Is.EqualTo(HttpStatusCode.BadRequest));
            }
        }

        [TestFixture]
        public class StreamedResponseAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenReturnStreamResponse()
            {
                var result = await _sut.StreamedResponseAsync(10);

                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }

        [TestFixture]
        public class DelayResponseAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenReturnsDelayedResponse()
            {
                var sw = new Stopwatch();

                sw.Start();
                var result = await _sut.DelayResponseAsync(3);
                sw.Stop();

                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(sw.ElapsedMilliseconds, Is.GreaterThan(3000));
            }
        }

        [TestFixture]
        public class GetUtf8EncodedResponseAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenReturnUtf8ResponseBody()
            {
                var result = await _sut.GetUtf8EncodedResponseAsync();

                Assert.That(result.Length, Is.GreaterThan(0));

                Console.WriteLine(result);
            }
        }

        [TestFixture]
        public class GZipCompressedAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenReturnCompressedBody()
            {
                var result = await _sut.GZipCompressedAsync();

                Assert.That(result.IsGZipped, Is.True);
                Assert.That(result.IsDeflated, Is.False);
                Assert.That(result.Method, Is.EqualTo("GET"));
                Assert.That(result.Headers.Count, Is.GreaterThan(0));
            }
        }

        [TestFixture]
        public class DeflateCompressedAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenReturnCompressedBody()
            {
                var result = await _sut.DeflateCompressedAsync();

                Assert.That(result.IsGZipped, Is.False);
                Assert.That(result.IsDeflated, Is.True);
                Assert.That(result.Method, Is.EqualTo("GET"));
                Assert.That(result.Headers.Count, Is.GreaterThan(0));
            }
        }

        [TestFixture]
        public class IpAddressAsync : PostmanEchoClientTests
        {
            [Test]
            public async Task WhenCalled_ThenReturnsSourceIpAddress()
            {
                var result = await _sut.IpAddressAsync();

                Assert.That(result.Length, Is.GreaterThan(0));

                Console.WriteLine(result);
            }
        }
    }
}