using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProductService.Infrastructure.Persistence;

namespace ProductService.Infrastructure.Tests;

public static class TestDbContextFactory
{
    public static ProductDbContext Create()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseSqlite(connection)
            .Options;

        var context = new ProductDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }
}
