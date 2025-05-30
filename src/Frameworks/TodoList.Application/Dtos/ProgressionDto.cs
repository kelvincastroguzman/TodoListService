using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Application.Dtos
{
    public class ProgressionDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Percent { get; set; }

        public int TodoItemId { get; set; }
    }
}
