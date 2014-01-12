using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin.AuthenticatedTests.Models;
using System.Threading.Tasks;
using System.Net;

namespace Owin.AuthenticatedTests.Tests
{
    [TestClass]
    public class AccountControllerTests : BaseServerTest
    {
        [TestMethod]
        public async Task CanRegisterUser()
        {
            var model = new RegisterBindingModel
            {
                UserName = "aaronpowell" + DateTimeOffset.Now.Ticks,
                Password = "password",
                ConfirmPassword = "password"
            };

            uri = uriBase + "/register";

            var response = await PostAsync(model);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private string uriBase = "/api/account";
        private string uri = string.Empty;

        protected override string Uri
        {
            get { return uri; }
        }
    }
}
