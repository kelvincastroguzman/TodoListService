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
            if (!ModelState.IsValid)
            {
                return Conflict($"ModelState is not valid: {ModelState?.ToString()}");
            }

            IReadOnlyCollection<TodoItemDto> response;

            try
            {
                response = _todoListQueriesService.PrintItems();

                if (response.Count == 0)
                    return NotFound("Items to print not found");

            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(response);
        }
    }
}
