using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Domain.Primitives;

namespace TodoList.Domain.Entities;

[Table("Category", Schema = "dbo")]
public partial class Category : AggregateRoot
{
    public Category(int id) : base(id)
    {
        Id = id;
    }

    [Key]
    public new int Id { get; set; }
    public string Name { get; set; } = null!;
}
