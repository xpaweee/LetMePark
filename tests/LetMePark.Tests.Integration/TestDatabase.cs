using LetMePark.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace LetMePark.Tests.Integration;

internal class TestDatabase : IDisposable
{
    public LetMeParkDbContext DbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("postgres");
        DbContext = new LetMeParkDbContext(new DbContextOptionsBuilder<LetMeParkDbContext>()
            .UseNpgsql(options.ConnectionString).Options);
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext?.Dispose();
    }
}