using Moq;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using System.Threading;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeHttpContext
    {
        public static HttpContext Configure(Action<FakeHttpContext> action)
        {
            var mock = new Mock<HttpContext>();

            var options = action.ToConfigureOrDefault();

            if (options.User != null)
            {
                mock.Setup(m => m.User).Returns(options.User);
            }
            
            mock.Setup(m => m.Request).Returns(options.Request);
            mock.Setup(m => m.Connection).Returns(options.Connection);
            mock.Setup(m => m.Response).Returns(options.Response);

            return mock.Object;
        }

        public FakeConnectionInfo Connection { get; set; } = new FakeConnectionInfo();
        public FakeHttpRequest Request { get; set; } = new FakeHttpRequest();
        public ClaimsPrincipal? User { get; set; } = new ClaimsPrincipal();
        public FakeHttpResponse Response { get; set; } = new FakeHttpResponse();

        public IFeatureCollection? Features { get; set; }
        public IDictionary<object, object>? Items { get; set; }
        public CancellationToken RequestAborted { get; set; }
        public IServiceProvider? RequestServices { get; set; }
        public ISession? Session { get; set; }
        public string? TraceIdentifier { get; set; }
        public WebSocketManager? WebSockets { get; }
    }
}
