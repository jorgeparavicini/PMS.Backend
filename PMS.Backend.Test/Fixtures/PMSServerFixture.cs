using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;
using Environment = PMS.Backend.Common.Models.Environment;

namespace PMS.Backend.Test.Fixtures;

public class PMSServerFixture : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }

    public IConfiguration Configuration { get; private set; } = null!;

    public PmsDbContext Context { get; private set; } = null!;

    private IServiceScope _scope = null!;

    private bool _isDisposed;

    public PMSServerFixture()
    {
        Client = CreateClient();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Environment.Staging);
        builder.ConfigureServices(services =>
        {
            var sp = services.BuildServiceProvider();

            _scope = sp.CreateScope();
            var scopedServices = _scope.ServiceProvider;
            Context = scopedServices.GetRequiredService<PmsDbContext>();

            var logger = scopedServices.GetRequiredService<ILogger<PMSServerFixture>>();

            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            try
            {
                SeedDatabase(Context);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError(e,
                    "An error occurred seeding the database with test messages. " +
                    "Error: {Message}",
                    e.Message);
            }
        });

        builder.ConfigureAppConfiguration(config => Configuration = config.Build());
    }

    private static void SeedDatabase(PmsDbContext context)
    {
        context.Agencies.AddRange(AgencyMockData.GetAgencies());
    }

    protected override void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                _scope.Dispose();
            }

            _isDisposed = true;
        }
    }
}
