using Microsoft.EntityFrameworkCore;

namespace WebApi.Helpers;

public class SqliteDataContext : DataContext
{
    public SqliteDataContext(IConfiguration configuration) : base(configuration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        // dotnet ef migrations add InitialCreate --context SqliteDataContext --output-dir Migrations/SqliteMigrations -s .\WebApi\
        // dotnet ef database update --context SqliteDataContext -s .\WebApi\
        options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
    }
}
