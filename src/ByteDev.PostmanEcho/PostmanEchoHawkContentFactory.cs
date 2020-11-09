using System.Collections.Generic;
using System.Net.Http;

namespace ByteDev.PostmanEcho
{
    public static class PostmanEchoHawkContentFactory
    {
        public static HttpContent Create()
        {
            IEnumerable<KeyValuePair<string, string>> q = new []
            {
                new KeyValuePair<string, string>("access_token", "xyz1"),
                new KeyValuePair<string, string>("id", "U1"), 
                new KeyValuePair<string, string>("server_secret", "zeppelin"), 
                new KeyValuePair<string, string>("admin", "true")
            };

            return new MultipartFormDataContent
            {
                new FormUrlEncodedContent(q)
            };
        }
    }
}