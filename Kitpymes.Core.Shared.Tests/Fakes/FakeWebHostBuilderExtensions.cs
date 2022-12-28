using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Kitpymes.Core.Shared.Tests
{
    public static class FakeWebHostBuilderExtensions
    {
        public static HttpClient FakeCreateClient(this IWebHostBuilder webHostBuilder)
        => new TestServer(webHostBuilder).CreateClient();
    }
}
