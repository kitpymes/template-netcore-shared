using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.IO;
using Kitpymes.Core.Shared.Tests;

namespace Kitpymes.Core.Shared.Extensions.Tests
{
    [TestClass]
    public class HttpResponseExtensionsTests
    {
        [TestMethod]
        [DataRow("/test")]
        public async Task ToResultAsync_Passing_StatusCodeAnd_MessageAndContentTypeAndHeader_Values_Returns_Values(string url)
        {
            // Arrange
            string key = Guid.NewGuid().ToString();
            string value1Expected = Guid.NewGuid().ToString();
            string value2Expected = Guid.NewGuid().ToString();

            var statusCodeExpected = HttpStatusCode.Unauthorized;
            string contentTypeExpected = Guid.NewGuid().ToString();
            string messageExpected = Guid.NewGuid().ToString();

            void HandleMapTest(IApplicationBuilder app)
            {
                app.Run(async context =>
                {
                    await context.Response.ToResultAsync
                    (
                        statusCodeExpected,
                        messageExpected,
                        contentTypeExpected,
                        (key, new string[] { value1Expected, value2Expected })
                    );
                });
            }

            var projectDirectoryName = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
                .Split("\\")
                .Last();

            var client = FakeHost
                .Create<FakeStartup>(host => host.SolutionRelativeContentRoot = projectDirectoryName)
                .Configure(app => app.Map(url, HandleMapTest))
                .FakeCreateClient();

            // Act
            var httpResponse = await client.GetAsync(url);
            var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(statusCodeExpected, httpResponse.StatusCode);
            StringAssert.Contains(messageExpected, httpResponseBody);
            StringAssert.Contains(contentTypeExpected, httpResponse.Content.Headers.GetValues("content-type").First());
            StringAssert.Contains(value1Expected, httpResponse.Headers.GetValues(key).First(x => x == value1Expected));
            StringAssert.Contains(value2Expected, httpResponse.Headers.GetValues(key).First(x => x == value2Expected));
        }
    }
}
