using Microsoft.EntityFrameworkCore;
using TaskPulse.Model;

namespace TaskPulse.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
