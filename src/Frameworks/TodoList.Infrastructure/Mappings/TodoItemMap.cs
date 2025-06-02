using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Mappings
{
    internal class TodoItemMap
    {
        internal static void Configure(EntityTypeBuilder<TodoItem> entity)
        {
            entity.ToTable("TodoItem");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(100);
        }
    }
}
