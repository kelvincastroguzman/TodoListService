using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Domain.Entities;

[Table("Category", Schema = "dbo")]
public partial class Category
{
    public Category()
    {
    }

    [Key]
    public string Name { get; set; } = null!;
}
