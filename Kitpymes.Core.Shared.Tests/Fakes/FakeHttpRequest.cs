using Moq;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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

        public FakeHttpRequest AddHeader(IDictionary<string, IEnumerable<string>>? headers)
        {
            if (headers?.Count > 0)
            {
                foreach (var (key, values) in headers)
                {
                    Headers.AppendList(key, values.ToList());
                }
            }

            return this;
        }

#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override Stream? Body { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override long? ContentLength { get; set; }
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override string? ContentType { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override IRequestCookieCollection? Cookies { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override IFormCollection? Form { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override IHeaderDictionary Headers { get; } = new HeaderDictionary();
        public override HostString Host { get; set; }
        public override bool IsHttps { get; set; }
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override string? Method { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override PathString Path { get; set; }
        public override PathString PathBase { get; set; }
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override string? Protocol { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override IQueryCollection? Query { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override QueryString QueryString { get; set; }
#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override string? Scheme { get; set; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).

        public override bool HasFormContentType { get; }

#pragma warning disable CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).
        public override HttpContext? HttpContext { get; }
#pragma warning restore CS8764 // La nulabilidad del tipo de valor devuelto no coincide con el miembro invalidado (posiblemente debido a los atributos de nulabilidad).

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
