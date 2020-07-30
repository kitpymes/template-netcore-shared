using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;

namespace Kitpymes.Core.Shared.Tests
{
    public static class FakeAuthenticated
    {
        public static void Configure(this IServiceCollection services, string authenticationType)
        {
            services.AddAuthentication(authenticationType);

            var application = new ApplicationBuilder(services.BuildServiceProvider());

            application.Use(async (context, next) =>
            {   
                await context.AuthenticateAsync(authenticationType).ConfigureAwait(false);

                await next().ConfigureAwait(false);
            });
        }
    }
}
