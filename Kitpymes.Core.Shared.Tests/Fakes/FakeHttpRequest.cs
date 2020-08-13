using Moq;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeHttpRequest : HttpRequest
    {
        public static HttpRequest Configure(Action<FakeHttpRequest> action)
        {
            var mock = new Mock<HttpRequest>();

            var options = action.ToConfigureOrDefault();

            if (!string.IsNullOrWhiteSpace(options.ContentType))
            {
                mock.Setup(x => x.ContentType).Returns(options.ContentType);
            }

            if (!string.IsNullOrWhiteSpace(options.Scheme))
            {
                mock.Setup(x => x.Scheme).Returns(options.Scheme);
            }

            if (!string.IsNullOrWhiteSpace(options.Method))
            {
                mock.Setup(x => x.Method).Returns(options.Method);
            }

            if (options.Body != null)
            {
                mock.Setup(x => x.Body).Returns(options.Body);
            }

            mock.Setup(x => x.Host).Returns(options.Host);
            mock.Setup(x => x.Path).Returns(options.Path);
            mock.Setup(x => x.PathBase).Returns(options.PathBase);
            mock.Setup(x => x.QueryString).Returns(options.QueryString);
            mock.Setup(x => x.Headers).Returns(options.Headers);

            return mock.Object;
        }

        public FakeHttpRequest AddHeader(string key, params string[] values)
        {
            Headers.AppendList(key, values);

            return this;
        }

        public override Stream? Body { get; set; }
        public override long? ContentLength { get; set; }
        public override string? ContentType { get; set; }
        public override IRequestCookieCollection? Cookies { get; set; }
        public override IFormCollection? Form { get; set; }
        public override IHeaderDictionary Headers { get; } = new HeaderDictionary();
        public override HostString Host { get; set; }
        public override bool IsHttps { get; set; }
        public override string? Method { get; set; }
        public override PathString Path { get; set; }
        public override PathString PathBase { get; set; }
        public override string? Protocol { get; set; }
        public override IQueryCollection? Query { get; set; }
        public override QueryString QueryString { get; set; }
        public override string? Scheme { get; set; }

        public override bool HasFormContentType { get; }

        public override HttpContext? HttpContext { get; }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
