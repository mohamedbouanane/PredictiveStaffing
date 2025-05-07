namespace Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("Data Source=../../app.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}
