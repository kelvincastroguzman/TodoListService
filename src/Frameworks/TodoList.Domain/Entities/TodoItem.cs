using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Domain.Entities;

[Table("TodoItem", Schema = "dbo")]
public partial class TodoItem
{
    public TodoItem()
    {
        Progressions = new List<Progression>();
    }

    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public virtual ICollection<Progression> Progressions { get; set; }
}
