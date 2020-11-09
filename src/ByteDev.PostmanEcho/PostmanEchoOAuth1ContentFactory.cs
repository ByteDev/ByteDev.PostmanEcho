using System.Collections.Generic;
using System.Net.Http;

namespace ByteDev.PostmanEcho
{
    public static class PostmanEchoOAuth1ContentFactory
    {
        public static HttpContent Create()
        {
            IEnumerable<KeyValuePair<string, string>> nameValues = new []
            {
                new KeyValuePair<string, string>("code", "xWnkliVQJURqB2x1"), 
                new KeyValuePair<string, string>("grant_type", "authorization_code"), 
                new KeyValuePair<string, string>("redirect_uri", "https://www.getpostman.com/oauth2/callback"),
                new KeyValuePair<string, string>("client_id", "abc123"), 
                new KeyValuePair<string, string>("client_secret", "ssh-secret")
            };

            return new MultipartFormDataContent
            {
                new FormUrlEncodedContent(nameValues)
            };
        }
    }
}