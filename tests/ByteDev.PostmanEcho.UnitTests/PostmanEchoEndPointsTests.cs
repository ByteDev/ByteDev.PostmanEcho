using System;
using System.Net;
using NUnit.Framework;

namespace ByteDev.PostmanEcho.UnitTests
{
    [TestFixture]
    public class PostmanEchoEndPointsTests
    {
        [TestFixture]
        public class CreateStatusCodeUri : PostmanEchoEndPointsTests
        {
            [Test]
            public void WhenCodeIntSupplied_ThenReturnUri()
            {
                var result = PostmanEchoEndPoints.Utilities.StatusCodeUri(200);

                Assert.That(result, Is.EqualTo(new Uri("https://postman-echo.com/status/200")));
            }

            [Test]
            public void WhenCodeEnumSupplied_ThenReturnUri()
            {
                var result = PostmanEchoEndPoints.Utilities.StatusCodeUri(HttpStatusCode.OK);

                Assert.That(result, Is.EqualTo(new Uri("https://postman-echo.com/status/200")));
            }

            [TestCase(99)]
            [TestCase(600)]
            public void WhenCodeIntIsOutOfRange_ThenThrowException(int code)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => PostmanEchoEndPoints.Utilities.StatusCodeUri(code));
            }
        }

        [TestFixture]
        public class CreateStreamedResponseUri : PostmanEchoEndPointsTests
        {
            [Test]
            public void WhenLengthIsValid_ThenReturnUri()
            {
                var result = PostmanEchoEndPoints.Utilities.StreamedResponseUri(5);

                Assert.That(result, Is.EqualTo(new Uri("https://postman-echo.com/stream/5")));
            }

            [Test]
            public void WhenLengthIsInvalid_ThenThrowException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => PostmanEchoEndPoints.Utilities.StreamedResponseUri(-1));
            }
        }

        [TestFixture]
        public class CreateDelayResponseUri : PostmanEchoEndPointsTests
        {
            [Test]
            public void WhenSecondsIsValid_ThenReturnUri()
            {
                var result = PostmanEchoEndPoints.Utilities.DelayResponseUri(10);

                Assert.That(result, Is.EqualTo(new Uri("https://postman-echo.com/delay/10")));
            }

            [TestCase(-1)]
            [TestCase(11)]
            public void WhenSecondsIsOutOfRange_ThenThrowException(int seconds)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => PostmanEchoEndPoints.Utilities.DelayResponseUri(seconds));
            }
        }
    }
}