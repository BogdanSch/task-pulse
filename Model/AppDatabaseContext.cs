using Microsoft.EntityFrameworkCore;

namespace TaskPulse.Model;
public class AppDatabaseContext : DbContext
{
    public DbSet<TaskItem> TaskItems { get; set; }
    private readonly string _databaseName = "app.db";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=${_databaseName}");
    }
}
