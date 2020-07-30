using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.TestHost;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeHost
    {
        public string EnvironmentName { get; set; } = "Testing";
        public string JsonFile { get; set; } = "appsettings.json";
        public string DirectoryPath { get; set; } = Directory.GetCurrentDirectory();
        public string? SolutionRelativeContentRoot { get; set; }

        public static IWebHostBuilder Create<TStartup>(Action<FakeHost>? action = null)   
            where TStartup : StartupBase
        {
            var options = action.ToConfigureOrDefault();

            return WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>()
                .UseEnvironment(options.EnvironmentName)
                .UseSolutionRelativeContentRoot(options.SolutionRelativeContentRoot)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var configPath = Path.Combine(options.DirectoryPath, options.JsonFile);

                    config.AddJsonFile(configPath, true);
                });
        }
    }
}
