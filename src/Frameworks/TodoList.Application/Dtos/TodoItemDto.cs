namespace TodoList.Application.Dtos
{
    public class TodoItemDto
    {
        public TodoItemDto()
        {
            Progressions = new List<ProgressionDto>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsCompleted { get; set; }

        public virtual ICollection<ProgressionDto> Progressions { get; set; }
    }
}
