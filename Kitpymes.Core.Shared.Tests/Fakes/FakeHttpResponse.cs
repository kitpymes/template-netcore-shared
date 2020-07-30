using Moq;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeHttpResponse : HttpResponse
    {
        public static HttpResponse Configure(Action<FakeHttpResponse> action)
        {
            var mock = new Mock<HttpResponse>();

            var options = action.ToConfigureOrDefault();

            mock.Setup(x => x.ContentType).Returns(options.ContentType);
            mock.Setup(x => x.StatusCode).Returns(options.StatusCode);
            mock.Setup(x => x.Headers).Returns(options.Headers);
            mock.Setup(x => x.Body).Returns(options.Body);
            mock.Setup(x => x.Cookies).Returns(options.Cookies);

            return mock.Object;
        }

        public FakeHttpResponse AddHeader(string key, params string[] values)
        {
            Headers.AppendList(key, values);

            return this;
        }

        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }
        public override IResponseCookies Cookies { get; }
        public override IHeaderDictionary Headers { get; } = new HeaderDictionary();
        public override int StatusCode { get; set; }
        public override Stream Body { get; set; }
        public override bool HasStarted => throw new NotImplementedException();
        public override HttpContext HttpContext => throw new NotImplementedException();

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void Redirect(string location, bool permanent)
        {
            throw new NotImplementedException();
        }
    }
}
