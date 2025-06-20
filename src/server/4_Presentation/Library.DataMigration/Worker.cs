using Library.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Library.DataMigration;

public class Worker(
    IHostApplicationLifetime hostApplicationLifetime,
    ActivitySource activitySource,
    IHostEnvironment env,
    IServiceProvider provider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();

            await RunMigrationAsync(context, stoppingToken);
            
            // Seed Data if you want
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        if (env.IsDevelopment())
        {
            hostApplicationLifetime.StopApplication();
        }
        else
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Keep the service running to allow for graceful shutdown
                await Task.Delay(10000, stoppingToken);
            }
        }
    }

    private static async Task RunMigrationAsync(LibraryContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }
}
