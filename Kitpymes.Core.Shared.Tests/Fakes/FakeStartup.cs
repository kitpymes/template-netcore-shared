using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Kitpymes.Core.Shared.Tests
{
    public class FakeStartup : StartupBase
    {
        public IConfiguration Configuration { get; }

        public FakeStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override void Configure(IApplicationBuilder app)
        {
            //var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            //using (var serviceScope = serviceScopeFactory.CreateScope())
            //{
            //    var dbContext = serviceScope.ServiceProvider.GetService<DemoDbContext>();
            //    if (dbContext == null)
            //    {
            //        throw new NullReferenceException("Cannot get instance of dbContext");
            //    }

            //    if (dbContext.Database.GetDbConnection().ConnectionString.ToLower().Contains("live.db"))
            //    {
            //        throw new Exception("LIVE SETTINGS IN TESTS!");
            //    }

            //    dbContext.Database.EnsureDeleted();
            //    dbContext.Database.EnsureCreated();

            //    dbContext.Customers.Add(new Customer { Id = 1, Name = "Customer 1" });
            //    dbContext.Customers.Add(new Customer { Id = 2, Name = "Customer 2" });
            //    dbContext.SaveChanges();
            //}
        }
    }
}
