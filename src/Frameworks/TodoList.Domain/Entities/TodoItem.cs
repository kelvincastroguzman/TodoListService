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
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    public virtual ICollection<Progression> Progressions { get; set; }
}
