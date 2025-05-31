using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Dtos;
using TodoList.Application.TodoList.Queries.Contracts;

namespace TodoList.WebApi.Controllers.TodoList.Queries
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListQueriesController : ControllerBase
    {
        private readonly ITodoListQueriesService _todoListQueriesService;

        public TodoListQueriesController(ITodoListQueriesService todoListQueriesService)
        {
            _todoListQueriesService = todoListQueriesService;
        }

        [HttpGet]
        [Route("GetPrintItems")]
        public IActionResult GetPrintItems()
        {
            IReadOnlyCollection<TodoItemDto> response;

            if (!ModelState.IsValid)
            {
                return Conflict($"ModelState is not valid: {ModelState?.ToString()}");
            }

            try
            {
                response = _todoListQueriesService.PrintItems();

                if (response.Count == 0)
                    return NotFound("Procol not found");

            }
            catch (Exception ex)
            {
                return Conflict($"An error occurred while processing your request: {ex.Message} - {ex.StackTrace}.");
            }

            return Ok(response);
        }
    }
}
