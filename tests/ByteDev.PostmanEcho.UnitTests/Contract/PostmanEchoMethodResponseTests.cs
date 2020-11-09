using System;
using System.Collections.Generic;
using ByteDev.PostmanEcho.Contract;
using NUnit.Framework;

namespace ByteDev.PostmanEcho.UnitTests.Contract
{
    [TestFixture]
    public class PostmanEchoMethodResponseTests
    {
        private const string GetResponseJson = "{\n\t\"args\": {\n\t\t\"foo1\": \"bar1\",\n\t\t\"foo2\": \"bar2\"\n\t},\n\t\"headers\": {\n\t\t\"host\": \"postman-echo.com\"\n\t},\n\t\"url\": \"https://postman-echo.com/get?foo1=bar1&foo2=bar2\"\n}";

        private const string GetResponseJson2 = "{\"args\":{},\"data\":{\"Name\":\"John Smith\",\"CardNumber\":\"4111111111111111\"},\"files\":{},\"form\":{},\"headers\":{\"x-forwarded-proto\":\"https\",\"x-forwarded-port\":\"443\",\"host\":\"postman-echo.com\",\"x-amzn-trace-id\":\"Root=1-5fa8e7b2-7dde631368e226af03fcf44c\",\"content-length\":\"53\",\"x-newrelic-transaction\":\"PxQPUFdWWlUGU1lXBVIHAAJXFB8EBw8RVU4aWgkJAVBVUw5ZBFNQUgJQUUNKQQgDAgFVUA5TFTs=\",\"x-newrelic-id\":\"XAABUlZSGwcIXVFbAQQF\",\"content-type\":\"application/json\"},\"json\":{\"Name\":\"John Smith\",\"CardNumber\":\"4111111111111111\"},\"url\":\"https://postman-echo.com/post\"}";

        [TestFixture]
        public class Constructor : PostmanEchoMethodResponseTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenJsonIsNullOrEmpty_ThenThrowException(string json)
            {
                Assert.Throws<ArgumentException>(() => _ = new PostmanEchoMethodResponse(json));
            }

            [Test]
            public void WhenJsonIsValid_ThenSetProperties()
            {
                var sut = new PostmanEchoMethodResponse(GetResponseJson2);

                Assert.That(sut.QueryString, Is.Empty);
                
                Assert.That(sut.Body, Is.EqualTo("{\"Name\":\"John Smith\",\"CardNumber\":\"4111111111111111\"}"));
                
                Assert.That(sut.Headers.Count, Is.EqualTo(8));
                Assert.That(sut.Headers["x-forwarded-proto"], Is.EqualTo("https"));
                Assert.That(sut.Headers["x-forwarded-port"], Is.EqualTo("443"));
                Assert.That(sut.Headers["host"], Is.EqualTo("postman-echo.com"));
                Assert.That(sut.Headers["x-amzn-trace-id"], Is.EqualTo("Root=1-5fa8e7b2-7dde631368e226af03fcf44c"));
                Assert.That(sut.Headers["content-length"], Is.EqualTo("53"));
                Assert.That(sut.Headers["x-newrelic-transaction"], Is.EqualTo("PxQPUFdWWlUGU1lXBVIHAAJXFB8EBw8RVU4aWgkJAVBVUw5ZBFNQUgJQUUNKQQgDAgFVUA5TFTs="));
                Assert.That(sut.Headers["x-newrelic-id"], Is.EqualTo("XAABUlZSGwcIXVFbAQQF"));
                Assert.That(sut.Headers["content-type"], Is.EqualTo("application/json"));

                Assert.That(sut.Url, Is.EqualTo("https://postman-echo.com/post"));                
            }
        }

        [TestFixture]
        public class GetHeader : PostmanEchoMethodResponseTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenThrowException(string name)
            {
                var sut = new PostmanEchoMethodResponse(GetResponseJson);

                Assert.Throws<ArgumentException>(() => sut.GetHeader(name));
            }

            [Test]
            public void WhenHeaderExists_AndNameCaseDifferent_ThenReturnValue()
            {
                var sut = new PostmanEchoMethodResponse(GetResponseJson);

                var result = sut.GetHeader("HOST");

                Assert.That(result, Is.EqualTo("postman-echo.com"));
            }

            [Test]
            public void WhenHeaderDoesNotExist_ThenThrowException()
            {
                var sut = new PostmanEchoMethodResponse(GetResponseJson);

                Assert.Throws<KeyNotFoundException>(() => _ = sut.GetHeader("DoesNotExist"));
            }
        }
    }
}