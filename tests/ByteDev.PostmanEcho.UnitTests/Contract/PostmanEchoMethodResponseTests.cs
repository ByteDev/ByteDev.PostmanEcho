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

        [TestFixture]
        public class Constructor : PostmanEchoMethodResponseTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenJsonIsNullOrEmpty_ThenThrowException(string json)
            {
                Assert.Throws<ArgumentException>(() => _ = new PostmanEchoMethodResponse(json));
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