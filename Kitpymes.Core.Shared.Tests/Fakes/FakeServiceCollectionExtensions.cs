using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Kitpymes.Core.Shared.Tests
{
    public static class FakeServiceCollectionExtensions
    {
        public static IServiceCollection FakeAddEnviroment(this IServiceCollection services, Action<FakeEnvironment> action)
        => services.AddSingleton(FakeEnvironment.Configure(action));
        
    }
}
