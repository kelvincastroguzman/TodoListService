using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Domain.Primitives;

namespace TodoList.Domain.Entities;

[Table("Category", Schema = "dbo")]
public partial class Category
{
    [Key]
    public string Name { get; set; } = null!;
}
