namespace TodoList.Application.Dtos
{
    public class ProgressionDto
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int Percent { get; set; } = 0;
    }
}
