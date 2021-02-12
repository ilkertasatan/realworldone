using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RealWorldOne.UserManagement.Api;

namespace RealWorldOne.KittenGenerator.EndToEndTests
{
    public class TestServerFixture
    {
        public TestServerFixture()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Production")
                .UseContentRoot("")
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build()
                )
                .UseStartup<Startup>();
            
            TestServer = new TestServer(webHostBuilder);
        }

        public TestServer TestServer { get; }

        public async Task<TResponse> PostAsync<TResponse, TRequest>(string uri, TRequest request, CancellationToken cancellationToken)
        {
            var httpClient = TestServer.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(uri, content, cancellationToken);
            
            return JsonConvert.DeserializeObject<TResponse>(await httpResponse.Content.ReadAsStringAsync(cancellationToken));
        }
    }
}