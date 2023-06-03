using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mpagopay.Identity;
using Mpagopay.Identity.Models;
using Mpagopay.Persistence;

namespace Mpagopay.Api.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<MpagopayDbContext>(options =>
                {
                    options.UseInMemoryDatabase("MpagopayDbContextInMemoryTest");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<MpagopayDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                    

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    var contextIdentity = scopedServices.GetRequiredService<MpagopayIdentityDbContext>();
                    var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                        Utilities.InitializeDbIdentityForTests(userManager).ConfigureAwait(true);
                    }
                    catch(Exception ex)
                    {
                        logger.LogError(ex, $"An error occured seeding the database with test lessage. Error: {ex.Message}");
                    }
                }
            });
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
