[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.PostmanEcho?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-PostmanEcho/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.PostmanEcho.svg)](https://www.nuget.org/packages/ByteDev.PostmanEcho)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.PostmanEcho/blob/master/LICENSE)

# ByteDev.PostmanEcho

Postman Echo is an API that lets you test your HTTP based client.

This .NET Standard library provides functionality to help when consuming the Postman Echo API.

## Installation

ByteDev.PostmanEcho is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.PostmanEcho`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.PostmanEcho/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.PostmanEcho/blob/master/docs/RELEASE-NOTES.md).

## Usage

```csharp
// GET example

var client = new HttpClient();

var query = new NameValueCollection
{
    {"Name1", "Value1"}, 
    {"Name2", "Value2"}
};

var response = await client.GetAsync(PostmanEchoEndPoints.RequestMethods.GetUri(query));

var echoResponse = await response.Content.ReadAsPostmanEchoMethodResponseAsync();

// echoResponse.Url         // string
// echoResponse.Headers     // IDictionary<string, string>
// echoResponse.QueryString // IDictionary<string, string>
// echoResponse.Body        // string
```

```csharp
// POST example

var client = new HttpClient();

var content = new StringContent("Some content");

var response = await client.PostAsync(PostmanEchoEndPoints.RequestMethods.PostUri, content);

var echoResponse = await response.Content.ReadAsPostmanEchoMethodResponseAsync();

// echoResponse.Url         // string
// echoResponse.Headers     // IDictionary<string, string>
// echoResponse.QueryString // IDictionary<string, string>
// echoResponse.Body        // string
```

The following table describes the various endpoints (on the `PostmanEchoEndPoints` class), the HTTP method to use when making a request and how to handle the response from the `HttpClient`.

| Method | URI (`PostmanEchoEndPoints`) | Response Handler |
| --- | --- | --- |
| GET | `RequestMethods.GetUri(query)` | `HttpResponseMessage.Content.ReadAsPostmanEchoMethodResponseAsync()` |
| POST | `RequestMethods.PostUri(query)` | `HttpResponseMessage.Content.ReadAsPostmanEchoMethodResponseAsync()` |
| PUT | `RequestMethods.PutUri(query)` | `HttpResponseMessage.Content.ReadAsPostmanEchoMethodResponseAsync()` |
| PATCH | `RequestMethods.PatchUri(query)` | `HttpResponseMessage.Content.ReadAsPostmanEchoMethodResponseAsync()` |
| DELETE | `RequestMethods.DeleteUri(query)` | `HttpResponseMessage.Content.ReadAsPostmanEchoMethodResponseAsync()` |
| GET | `Headers.RequestHeadersUri` | `HttpResponseMessage.Content.ReadAsPostmanEchoMethodResponseAsync()` |
| GET | `Headers.ResponseHeadersUri(query)` | `HttpResponseMessage.Content.ReadAsPostmanEchoResponseHeadersAsync()` |
| GET | `AuthMethods.BasicAuthUri` | `HttpResponseMessage.StatusCode` |
| GET | `AuthMethods.DigestAuthUri` | `HttpResponseMessage.StatusCode` |
| GET | `AuthMethods.HawkAuthUri(query)` | `HttpResponseMessage.StatusCode` and `HttpResponseMessage.Content.ReadIsHawkAuthSuccessfulAsync()` |
| GET | `AuthMethods.OAuth1Uri` | `HttpResponseMessage.StatusCode` and `HttpResponseMessage.Content.ReadAsPostmanEchoOAuth1FailureResponseAsync()` |
| GET | `Cookies.SetUri(cookies)` | `HttpResponseMessage.Content.ReadAsPostmanEchoCookiesResponseAsync()` |
| GET | `Cookies.GetUri` | `HttpResponseMessage.Content.ReadAsPostmanEchoCookiesResponseAsync()` |
| GET | `Cookies.DeleteUri(cookieNames)` | `HttpResponseMessage.Content.ReadAsPostmanEchoCookiesResponseAsync()` |
| GET | `Utilities.StatusCodeUri(int code)` | `HttpResponseMessage.StatusCode` |
| GET | `Utilities.StreamedResponseUri(length)` | `HttpResponseMessage` |
| GET | `Utilities.DelayResponseUri(seconds)` | `HttpResponseMessage` |
| GET | `Utilities.GetUtf8EncodedResponseUri` | `HttpResponseMessage.Content.ReadAsStringAsync()` |
| GET | `Utilities.GZipCompressedUri` | `HttpResponseMessage.Content.ReadAsPostmanEchoGZipResponseAsync()` |
| GET | `Utilities.DeflateCompressedUri` | `HttpResponseMessage.Content.ReadAsPostmanEchoDeflateResponseAsync()` |
| GET | `Utilities.IpAddressUri` | `HttpResponseMessage.Content.ReadAsPostmanEchoIpAddressAsync()` |

See the implementation of `PostmanEchoClient` (in `ByteDev.PostmanEcho.IntTests` project) for more hints on how to use the API and handle the response using your `HttpClient`.

## Further Information

- [Postman Echo API documentation](https://docs.postman-echo.com/)

