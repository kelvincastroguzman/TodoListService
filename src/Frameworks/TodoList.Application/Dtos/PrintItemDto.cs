namespace TodoList.Application.Dtos
{
    public class PrintItemDto
    {
        public string Header { get; set; } = string.Empty;
        public List<ProgressionDto> Progressions { get; set; } = new List<ProgressionDto>();
    }
}
