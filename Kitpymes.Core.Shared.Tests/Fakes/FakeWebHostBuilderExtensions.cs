using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace Kitpymes.Core.Shared.Tests
{
    public static class FakeWebHostBuilderExtensions
    {
        public static HttpClient FakeCreateClient(this IWebHostBuilder webHostBuilder)
        => new TestServer(webHostBuilder).CreateClient();
    }
}
