using Kitpymes.Core.Shared.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Extensions.Tests
{
    [TestClass]
    public class HttpContextExtensionsTests
    {
        [TestMethod]
        public void ToIPv6_Passing_HttpContext_IP_Value_Returns_IP()
        {
            var ipExpected = new System.Net.IPAddress(int.MaxValue);

            var httpContext = FakeHttpContext.Configure(x =>
            {
                x.Connection.RemoteIpAddress = ipExpected;
            });

            Assert.AreEqual(ipExpected.MapToIPv6().ToString(), httpContext.ToIPv6());
        }

        [TestMethod]
        public void ToTryIPv6_Passing_HttpContext_IP_Value_Returns_IP()
        {
            var ipExpected = new System.Net.IPAddress(int.MaxValue);

            var httpContext = FakeHttpContext.Configure(x =>
            {
                x.Connection.RemoteIpAddress = ipExpected;
            });

            httpContext.ToTryIPv6(out var ipActual);

            Assert.AreEqual(ipExpected.MapToIPv6().ToString(), ipActual);
        }

        [TestMethod]
        public void ToDetails_Passing_HttpContext_Returns_Details_HttpContext()
        {
            var ipExpected = new System.Net.IPAddress(int.MaxValue);
            var headerKey = "User-Agent";
            var headerValueExpected = Guid.NewGuid().ToString();
            var contentTypeExpected = Guid.NewGuid().ToString();
            var schemeExpected = Guid.NewGuid().ToString();
            var hostExpected = Guid.NewGuid().ToString();
            var pathExpected = "/" + Guid.NewGuid().ToString();
            var methodExpected = Guid.NewGuid().ToString();
            var authenticationType = Guid.NewGuid().ToString();
            var claimTypeExpected = Guid.NewGuid().ToString();
            var claimValueExpected = Guid.NewGuid().ToString();

            var httpContext = FakeHttpContext.Configure(x =>
            {
                x.Connection.RemoteIpAddress = ipExpected;
                x.User?.ToAdd(authenticationType, (claimTypeExpected, claimValueExpected));
                x.Request = new FakeHttpRequest
                {
                    ContentType = contentTypeExpected,
                    Scheme = schemeExpected,
                    Host = new HostString(hostExpected),
                    Path = pathExpected,
                    Method = methodExpected,
                }.AddHeader(headerKey, headerValueExpected);
            });

            var result = httpContext.ToDetails();

            if (result != null)
            {
                Assert.IsTrue(result.Contains(ipExpected.MapToIPv6().ToString()));
                Assert.IsTrue(result.Contains(contentTypeExpected));
                Assert.IsTrue(result.Contains(schemeExpected));
                Assert.IsTrue(result.Contains(hostExpected));
                Assert.IsTrue(result.Contains(pathExpected));
                Assert.IsTrue(result.Contains(methodExpected));
                Assert.IsTrue(result.Contains(headerValueExpected));
            }
        }
    }
}
