using Microsoft.AspNetCore.Http;
using System.Threading;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeConnectionInfo : ConnectionInfo
    {
        public override X509Certificate2? ClientCertificate { get; set; }
        public override string Id { get; set; } = string.Empty;
        public override IPAddress? LocalIpAddress { get; set; }
        public override int LocalPort { get; set; }
        public override IPAddress? RemoteIpAddress { get; set; }
        public override int RemotePort { get; set; }

        public override Task<X509Certificate2?> GetClientCertificateAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
