using Microsoft.EntityFrameworkCore;

namespace App08_TodoDb.Data;

/// <summary>
/// EF Core DbContext connected to SQL Server (localhost\SQLEXPRESS01 - AutoCareDB)
/// </summary>
public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<TodoTask> Tasks => Set<TodoTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoTask>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Title).IsRequired().HasMaxLength(200);
            e.Property(t => t.IsCompleted).HasDefaultValue(false);
            e.Property(t => t.CreatedAt).HasDefaultValueSql("GETDATE()");
            e.ToTable("TodoTasks");
        });
    }
}

public class TodoTask
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
