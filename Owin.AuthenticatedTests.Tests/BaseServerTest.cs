using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace Owin.AuthenticatedTests.Tests
{
    public abstract class BaseServerTest
    {
        protected TestServer server;

        [TestInitialize]
        public void Setup()
        {
            server = TestServer.Create(app =>
            {
                var startup = new Startup();
                startup.ConfigureAuth(app);

                var config = new HttpConfiguration();
                WebApiConfig.Register(config);

                app.UseWebApi(config);
            });

            PostSetup(server);
        }

        protected virtual void PostSetup(TestServer server)
        {
        }

        [TestCleanup]
        public void Teardown()
        {
            if (server != null)
                server.Dispose();
        }

        protected abstract string Uri { get; }

        protected virtual async Task<HttpResponseMessage> GetAsync()
        {
            return await server.CreateRequest(Uri).GetAsync();
        }

        protected virtual async Task<HttpResponseMessage> PostAsync<TModel>(TModel model)
        {
            return await server.CreateRequest(Uri)
                .And(request => request.Content = new ObjectContent(typeof(TModel), model, new JsonMediaTypeFormatter()))
                .PostAsync();
        }
    }
}
