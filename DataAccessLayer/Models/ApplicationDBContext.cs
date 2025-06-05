using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }

    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Category> Categories { get; set; }
}