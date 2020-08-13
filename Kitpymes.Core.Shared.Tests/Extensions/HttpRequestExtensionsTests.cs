using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class HttpRequestExtensionsTests
    {
        [TestMethod]
        public void ToHeader_Passing_HttpRequest_Header_Value_Returns_Value()
        {
            var key = Guid.NewGuid().ToString();
            var valueExpected = Guid.NewGuid().ToString();

            var fakeHttpRequest = FakeHttpRequest.Configure(x => x.AddHeader(key, valueExpected));

            var valueActual = fakeHttpRequest.ToHeader(key);

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToHeader_Passing_HttpRequest_Header_Many_Values_Returns_Values()
        {
            var key = Guid.NewGuid().ToString();
            var value1Expected = Guid.NewGuid().ToString();
            var value2Expected = Guid.NewGuid().ToString();

            var fakeHttpRequest = FakeHttpRequest.Configure(x => x.AddHeader(key, value1Expected, value2Expected));

            var valueActual = fakeHttpRequest.ToHeader(key);

            if (valueActual != null)
            {
                Assert.IsTrue(valueActual.Contains(value1Expected));
                Assert.IsTrue(valueActual.Contains(value2Expected));
            }
        }

        [TestMethod]
        public void ToTryGetHeader_Passing_HttpRequest_Header_Value_Returns_Value()
        {
            var key = Guid.NewGuid().ToString();
            var valueExpected = Guid.NewGuid().ToString();

            var fakeHttpRequest = FakeHttpRequest.Configure(x => x.AddHeader(key, valueExpected));

            fakeHttpRequest.ToTryHeader(key, out var valueActual);

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToTryGetHeader_Passing_HttpRequest_Header_Many_Values_Returns_Values()
        {
            var key = Guid.NewGuid().ToString();
            var value1Expected = Guid.NewGuid().ToString();
            var value2Expected = Guid.NewGuid().ToString();

            var fakeHttpRequest = FakeHttpRequest.Configure(x => x.AddHeader(key, value1Expected, value2Expected));

            if (fakeHttpRequest.ToTryHeader(key, out var valueActual) && !string.IsNullOrWhiteSpace(valueActual))
            {
                Assert.IsTrue(valueActual.Contains(value1Expected));
                Assert.IsTrue(valueActual.Contains(value2Expected));
            }
        }

        [TestMethod]
        public void ToPath_Passing_HttpRequest_Scheme_Host_Path_Returns_Url()
        {
            var scheme = Guid.NewGuid().ToString();
            var host = Guid.NewGuid().ToString();
            var path = "/" + Guid.NewGuid().ToString();
            var urlExpected = $"{scheme}://{host}{path}";

            var fakeHttpRequest = FakeHttpRequest.Configure(x =>
            {   
                x.Scheme = scheme;
                x.Host = new HostString(host);
                x.Path = path;
            });

            var urlActual = fakeHttpRequest.ToPath();

            Assert.AreEqual(urlExpected, urlActual);
        }

        [TestMethod]
        public void ToSubdomain_Passing_HttpRequest_Host_Returns_Subdomain()
        {
            var host = new HostString(Guid.NewGuid().ToString());
            var subdomainExpected = host.Host?.Split('.')[0].Trim();

            var fakeHttpRequest = FakeHttpRequest.Configure(x =>
            {
                x.Host = host;
            });

            var subdomainActual = fakeHttpRequest.ToSubdomain();

            Assert.AreEqual(subdomainExpected, subdomainActual);
        }

        [TestMethod]
        public void ToTryContentType_Passing_HttpRequestWithContentType_Value_Returns_ContentType_String_Value()
        {
            var valueExpected = Guid.NewGuid().ToString();

            var fakeHttpRequest = FakeHttpRequest.Configure(x => x.ContentType = valueExpected);

            fakeHttpRequest.ToTryContentType(out var valueActual);

            Assert.AreEqual(valueExpected, valueActual);
        }
    }
}
