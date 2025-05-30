using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Mappings
{
    internal class ProgressionMap
    {
        internal static void Configure(EntityTypeBuilder<Progression> entity)
        {
            entity.ToTable("Progression");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.TodoItem).WithMany(p => p.Progressions)
                .HasForeignKey(d => d.TodoItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Progression_TodoItem");
        }
    }
}
