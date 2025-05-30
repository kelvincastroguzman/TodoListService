using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Mappings;

namespace TodoList.Infrastructure.Persistence;

public partial class TodoListDbContext : DbContext
{
    public TodoListDbContext()
    {
    }

    public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Progression> Progressions { get; set; }

    public virtual DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CategoryMap
                .Configure(modelBuilder.Entity<Category>());

        ProgressionMap
                .Configure(modelBuilder.Entity<Progression>());

        TodoItemMap
                .Configure(modelBuilder.Entity<TodoItem>());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
