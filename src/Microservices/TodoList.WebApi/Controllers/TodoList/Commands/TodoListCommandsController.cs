using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Dtos;
using TodoList.Application.TodoList.Commands.Contracts;

namespace TodoList.WebApi.Controllers.TodoList.Commands
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListCommandsController : ControllerBase
    {
        private readonly ITodoListCommandsService _todoListCommandsService;

        public TodoListCommandsController(ITodoListCommandsService todoListCommandsService)
        {
            _todoListCommandsService = todoListCommandsService;
        }

        [HttpPost]
        [Route("CreateTodoItem")]
        public IActionResult CreateTodoItem([FromBody] TodoItemDto todoItemDto)
        {
            if (!ModelState.IsValid)
            {
                return Conflict($"ModelState is not valid: {ModelState?.ToString()}");
            }

            if (todoItemDto is null)
            {
                return BadRequest("Todo item cannot be null.");
            }

            try
            {
                _todoListCommandsService.AddItem(todoItemDto.Id, todoItemDto.Title, todoItemDto.Description, todoItemDto.Category);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok("Todo item added successfully.");
        }

        [HttpPut]
        [Route("UpdateTodoItem")]
        public IActionResult UpdateTodoItem([FromBody] TodoItemDto todoItemDto)
        {
            if (!ModelState.IsValid)
            {
                return Conflict($"ModelState is not valid: {ModelState?.ToString()}");
            }

            if (todoItemDto is null)
            {
                return BadRequest("Invalid Todo item data.");
            }

            try
            {
                _todoListCommandsService.UpdateItem(todoItemDto.Id, todoItemDto.Description);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok("Todo item updated successfully.");
        }

        [HttpDelete]
        [Route("RemoveTodoItem/{id}")]
        public IActionResult RemoveTodoItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return Conflict($"ModelState is not valid: {ModelState?.ToString()}");
            }

            try
            {
                _todoListCommandsService.RemoveItem(id);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok("Todo item deleted successfully.");
        }

        [HttpPost]
        [Route("RegisterProgression")]
        public IActionResult RegisterProgression([FromBody] RegisterProgressionDto registerProgressionDto)
        {
            if (!ModelState.IsValid)
            {
                return Conflict($"ModelState is not valid: {ModelState?.ToString()}");
            }

            if (registerProgressionDto is null)
            {
                return BadRequest("Invalid progression data.");
            }

            try
            {
                _todoListCommandsService.RegisterProgression(registerProgressionDto.TodoItemId, registerProgressionDto.Date, registerProgressionDto.Percent);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok("Progression registered successfully.");
        }
    }
}
