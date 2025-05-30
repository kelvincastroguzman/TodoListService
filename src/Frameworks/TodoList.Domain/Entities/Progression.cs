using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Domain.Primitives;

namespace TodoList.Domain.Entities;

[Table("Progression", Schema = "dbo")]
public partial class Progression : AggregateRoot
{
    public Progression()
    {
        TodoItem = new TodoItem();
    }

    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Percent { get; set; }
    public int TodoItemId { get; set; }

    public virtual TodoItem TodoItem { get; set; }
}
