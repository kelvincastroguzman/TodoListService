using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoList.Application.Dtos;
using TodoList.Application.TodoList.Commands.Contracts;

namespace TodoList.WebApi.Controllers.TodoList.Commands
{
    public class TodoListCommandsControllerTests
    {
        [Fact]
        public void CreateTodoItem_ReturnsOk_WhenValid()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var dto = new TodoItemDto { Id = 1, Title = "T", Description = "D", Category = "C" };

            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.CreateTodoItem(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Todo item added successfully.", okResult.Value);
            mockService.Verify(s => s.AddItem(dto.Id, dto.Title, dto.Description, dto.Category), Times.Once);
        }

        [Fact]
        public void CreateTodoItem_ReturnsBadRequest_WhenDtoNull()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.CreateTodoItem(null);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Todo item cannot be null.", badRequest.Value);
        }

        [Fact]
        public void CreateTodoItem_ReturnsConflict_WhenModelStateInvalid()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var controller = new TodoListCommandsController(mockService.Object);
            controller.ModelState.AddModelError("key", "error");

            var result = controller.CreateTodoItem(new TodoItemDto());

            var conflict = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("ModelState is not valid", conflict.Value?.ToString());
        }

        [Fact]
        public void CreateTodoItem_ReturnsConflict_OnException()
        {
            var mockService = new Mock<ITodoListCommandsService>();

            var dto = new TodoItemDto { Id = 1, Title = "T", Description = "D", Category = "C" };
            mockService.Setup(s => s.AddItem(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception("fail"));

            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.CreateTodoItem(dto);

            var conflict = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("fail", conflict.Value?.ToString());
        }

        [Fact]
        public void UpdateTodoItem_ReturnsOk_WhenValid()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var dto = new TodoItemDto { Id = 2, Description = "D" };

            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.UpdateTodoItem(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Todo item updated successfully.", okResult.Value);
            mockService.Verify(s => s.UpdateItem(dto.Id, dto.Description), Times.Once);
        }

        [Fact]
        public void UpdateTodoItem_ReturnsBadRequest_WhenDtoNull()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.UpdateTodoItem(null);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Todo item data.", badRequest.Value);
        }

        [Fact]
        public void UpdateTodoItem_ReturnsConflict_WhenModelStateInvalid()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var controller = new TodoListCommandsController(mockService.Object);
            controller.ModelState.AddModelError("key", "error");

            var result = controller.UpdateTodoItem(new TodoItemDto());

            var conflict = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("ModelState is not valid", conflict.Value?.ToString());
        }

        [Fact]
        public void UpdateTodoItem_ReturnsConflict_OnException()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var dto = new TodoItemDto { Id = 2, Description = "D" };
            mockService.Setup(s => s.UpdateItem(It.IsAny<int>(), It.IsAny<string>()))
                .Throws(new Exception("fail"));

            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.UpdateTodoItem(dto);

            var conflict = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("fail", conflict.Value?.ToString());
        }

        [Fact]
        public void RemoveTodoItem_ReturnsOk_WhenValid()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.RemoveTodoItem(3);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Todo item deleted successfully.", okResult.Value);
            mockService.Verify(s => s.RemoveItem(3), Times.Once);
        }

        [Fact]
        public void RemoveTodoItem_ReturnsConflict_WhenModelStateInvalid()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var controller = new TodoListCommandsController(mockService.Object);
            controller.ModelState.AddModelError("key", "error");

            var result = controller.RemoveTodoItem(3);

            var conflict = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("ModelState is not valid", conflict.Value?.ToString());
        }

        [Fact]
        public void RemoveTodoItem_ReturnsConflict_OnException()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            mockService.Setup(s => s.RemoveItem(It.IsAny<int>()))
                .Throws(new Exception("fail"));

            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.RemoveTodoItem(3);

            var conflict = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("fail", conflict.Value?.ToString());
        }

        [Fact]
        public void RegisterProgression_ReturnsOk_WhenValid()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var dto = new RegisterProgressionDto { TodoItemId = 4, Date = DateTime.UtcNow, Percent = 10 };

            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.RegisterProgression(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Progression registered successfully.", okResult.Value);
            mockService.Verify(s => s.RegisterProgression(dto.TodoItemId, dto.Date, dto.Percent), Times.Once);
        }

        [Fact]
        public void RegisterProgression_ReturnsBadRequest_WhenDtoNull()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.RegisterProgression(null);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid progression data.", badRequest.Value);
        }

        [Fact]
        public void RegisterProgression_ReturnsConflict_WhenModelStateInvalid()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var controller = new TodoListCommandsController(mockService.Object);
            controller.ModelState.AddModelError("key", "error");

            var result = controller.RegisterProgression(new RegisterProgressionDto());

            var conflict = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("ModelState is not valid", conflict.Value?.ToString());
        }

        [Fact]
        public void RegisterProgression_ReturnsConflict_OnException()
        {
            var mockService = new Mock<ITodoListCommandsService>();
            var dto = new RegisterProgressionDto { TodoItemId = 4, Date = DateTime.UtcNow, Percent = 10 };
            mockService.Setup(s => s.RegisterProgression(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Throws(new Exception("fail"));

            var controller = new TodoListCommandsController(mockService.Object);
            var result = controller.RegisterProgression(dto);

            var conflict = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("fail", conflict.Value?.ToString());
        }
    }
}