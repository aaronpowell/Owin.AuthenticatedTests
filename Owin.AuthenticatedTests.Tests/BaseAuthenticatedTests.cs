using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Owin.AuthenticatedTests.Tests
{
    public abstract class BaseAuthenticatedTests : BaseServerTest
    {
        protected virtual string Username { get { return "aaronpowell"; } }
        protected virtual string Password { get { return "password"; } }

        private string token;

        protected override void PostSetup(TestServer server)
        {
            var tokenDetails = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", Username),
                    new KeyValuePair<string, string>("password", Password)
                };

            var tokenPostData = new FormUrlEncodedContent(tokenDetails);
            var tokenResult = server.HttpClient.PostAsync("/Token", tokenPostData).Result;
            Assert.AreEqual(HttpStatusCode.OK, tokenResult.StatusCode);

            var body = JObject.Parse(tokenResult.Content.ReadAsStringAsync().Result);

            token = (string)body["access_token"];
        }

        protected async Task<TResult> GetAsync<TResult>()
        {
            var response = await GetAsync();
            return await response.Content.ReadAsAsync<TResult>();
        }

        protected override async Task<HttpResponseMessage> GetAsync()
        {
            return await server.CreateRequest(Uri)
                .AddHeader("Authorization", "Bearer " + token)
                .GetAsync();
        }
    }
}
