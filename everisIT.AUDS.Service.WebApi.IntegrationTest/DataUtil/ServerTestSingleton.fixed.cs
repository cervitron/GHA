using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest.DataUtil
{
    [ExcludeFromCodeCoverage]
    public partial class ServerTestSingleton : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient clientTest;
        public static readonly ServerTestSingleton instanceUnique = new ServerTestSingleton(new WebApplicationFactory<Startup>());

        public ServerTestSingleton(WebApplicationFactory<Startup> webApplicationFactory)
        {
            clientTest = webApplicationFactory.CreateClient();
            clientTest.DefaultRequestHeaders.Accept.Clear();
            clientTest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            clientTest.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(Startup.AccessTokenForTestIntegration);
        }
    }
}
