using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Owin.AuthenticatedTests.Tests
{
    [TestClass]
    public class ValuesControllerTests : BaseServerTest
    {
        [TestMethod]
        public async Task ShouldGetUnauthorizedWithoutLogin()
        {
            var response = await GetAsync();

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        protected override string Uri
        {
            get { return "/api/values"; }
        }
    }

    [TestClass]
    public class ValuesAuthenticatedControllerTests : BaseAuthenticatedTests
    {
        [TestMethod]
        public async Task ShouldGetValuesWhenAuthenticated()
        {
            var response = await GetAsync<IEnumerable<string>>();

            Assert.AreEqual(2, response.Count());
        }

        protected override string Uri
        {
            get { return "/api/values"; }
        }
    }
}
