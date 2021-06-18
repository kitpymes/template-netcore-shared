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

            if (!string.IsNullOrWhiteSpace(options.ContentType))
            {
                mock.Setup(x => x.ContentType).Returns(options.ContentType);
            }

            if (options.Body != null)
            {
                mock.Setup(x => x.Body).Returns(options.Body);
            }

            if (options.Cookies != null)
            {
                mock.Setup(x => x.Cookies).Returns(options.Cookies);
            }

            mock.Setup(x => x.StatusCode).Returns(options.StatusCode);
            mock.Setup(x => x.Headers).Returns(options.Headers);

            return mock.Object;
        }

        public FakeHttpResponse AddHeader(string key, params string[] values)
        {
            Headers.AppendList(key, values);

            return this;
        }

        public override long? ContentLength { get; set; }
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override string? ContentType { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override IResponseCookies? Cookies { get; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override IHeaderDictionary Headers { get; } = new HeaderDictionary();
        public override int StatusCode { get; set; }
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override Stream? Body { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
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
