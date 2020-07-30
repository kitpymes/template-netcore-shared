using Microsoft.AspNetCore.Hosting;
using Moq;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeEnvironment
    {
        public static IWebHostEnvironment Configure(Action<FakeEnvironment> action)
        {
            var mock = new Mock<IWebHostEnvironment>();

            var options = action.ToConfigureOrDefault();

            if (!string.IsNullOrWhiteSpace(options.EnvironmentName))
            {
                mock.Setup(m => m.EnvironmentName).Returns(options.EnvironmentName);
            }

            if (!string.IsNullOrWhiteSpace(options.ApplicationName))
            {
                mock.Setup(m => m.ApplicationName).Returns(options.ApplicationName);
            }

            if (!string.IsNullOrWhiteSpace(options.WebRootPath))
            {
                mock.Setup(m => m.WebRootPath).Returns(options.WebRootPath);
            }

            if (!string.IsNullOrWhiteSpace(options.ContentRootPath))
            {
                mock.Setup(m => m.ContentRootPath).Returns(options.ContentRootPath);
            }

            return mock.Object;
        }

        public string? EnvironmentName { get; set; }
        public string? ApplicationName { get; set; }
        public string? WebRootPath { get; set; }
        public string? ContentRootPath { get; set; }
    }
}
