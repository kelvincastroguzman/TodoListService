using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Mappings
{
    internal class CategoryMap
    {
        internal static void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(10);
        }
    }
}
