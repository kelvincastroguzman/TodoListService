namespace TodoList.Application.Dtos
{
    public class RegisterProgressionDto
    {
        public int TodoItemId { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int Percent { get; set; } = 0;
    }
}
