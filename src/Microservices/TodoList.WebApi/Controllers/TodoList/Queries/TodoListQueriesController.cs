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
        [Route("GetNextTodoItemId")]
        public IActionResult GetNextTodoItemId()
        {
            if (!ModelState.IsValid)
            {
                return Conflict($"ModelState is not valid: {ModelState?.ToString()}");
            }

            int nextId;

            try
            {
                nextId = _todoListQueriesService.GetNextId();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(nextId);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            if (!ModelState.IsValid)
            {
                return Conflict($"ModelState is not valid: {ModelState?.ToString()}");
            }

            IReadOnlyCollection<string> categories;

            try
            {
                categories = _todoListQueriesService.GetAllCategories();
                if (categories.Count == 0)
                    return NotFound("No categories found");
            }

            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(categories);
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
