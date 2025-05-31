namespace TodoList.Application.Dtos
{
    public class TodoItemDto
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;

        public virtual ICollection<ProgressionDto> Progressions { get; set; } = new List<ProgressionDto>();
    }
}
