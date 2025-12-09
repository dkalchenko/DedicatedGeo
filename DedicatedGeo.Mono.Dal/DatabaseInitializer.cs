using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DedicatedGeo.Mono.Dal;

public class DatabaseInitializer : IDatabaseInitializer
{
    private const int RetrySaltMaxSec = 5;

    private readonly DatabaseRepository _databaseDbContext;
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly Random _random;

    public DatabaseInitializer(DatabaseRepository dbContext,
        ILogger<DatabaseInitializer> logger)
    {
        _random = new Random();
        _databaseDbContext = dbContext;
        _logger = logger;
    }

    public async Task InitDatabaseAsync()
    {
        await MigrateAsync(_databaseDbContext);
    }

    private async Task MigrateAsync(DbContext dataContext)
    {
        var attempts = 10;

        do
        {
            _logger.LogInformation("[Context: {DataContext}] Updating database", dataContext);
            if (await ApplyMigrationAsync(dataContext, attempts))
            {
                _logger.LogInformation("[Context: {DataContext}] Updated database", dataContext);
                return;
            }

            await Task.Delay(TimeSpan.FromSeconds(_random.Next(RetrySaltMaxSec)));
        } while (--attempts > 0);
    }


    private static async Task<bool> ApplyMigrationAsync(DbContext dataContext, int attempts)
    {
        await dataContext.Database.MigrateAsync();
        return true;
    }
}