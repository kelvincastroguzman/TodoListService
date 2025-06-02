using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Domain.Primitives;

namespace TodoList.Domain.Entities;

[Table("TodoItem", Schema = "dbo")]
public partial class TodoItem : AggregateRoot
{
    public TodoItem(int id) : base(id)
    {
        Id = id;
        Progressions = new List<Progression>();
    }

    [Key]
    public new int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;

    public virtual ICollection<Progression> Progressions { get; set; }
}
