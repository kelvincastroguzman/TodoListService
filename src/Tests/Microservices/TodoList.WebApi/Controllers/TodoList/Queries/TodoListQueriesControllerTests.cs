using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoList.Application.Dtos;
using TodoList.Application.TodoList.Queries.Contracts;

namespace TodoList.WebApi.Controllers.TodoList.Queries
{
    public class TodoListQueriesControllerTests
    {
        [Fact]
        public void GetPrintItems_ReturnsOk_WithItems()
        {
            // Arrange
            var mockService = new Mock<ITodoListQueriesService>();
            var items = new List<TodoItemDto>
            {
                new TodoItemDto { Id = 1, Title = "T1", Description = "D1", Category = "C1", IsCompleted = false, Progressions = new List<ProgressionDto>() }
            };
            mockService.Setup(s => s.PrintItems()).Returns(items);
            var controller = new TodoListQueriesController(mockService.Object);

            // Act
            var result = controller.GetPrintItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(items, okResult.Value);
        }

        [Fact]
        public void GetPrintItems_ReturnsNotFound_WhenNoItems()
        {
            // Arrange
            var mockService = new Mock<ITodoListQueriesService>();
            mockService.Setup(s => s.PrintItems()).Returns(new List<TodoItemDto>());
            var controller = new TodoListQueriesController(mockService.Object);

            // Act
            var result = controller.GetPrintItems();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Procol not found", notFoundResult.Value);
        }

        [Fact]
        public void GetPrintItems_ReturnsConflict_WhenModelStateInvalid()
        {
            // Arrange
            var mockService = new Mock<ITodoListQueriesService>();
            var controller = new TodoListQueriesController(mockService.Object);
            controller.ModelState.AddModelError("key", "error");

            // Act
            var result = controller.GetPrintItems();

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("ModelState is not valid", conflictResult.Value?.ToString());
        }

        [Fact]
        public void GetPrintItems_ReturnsConflict_OnException()
        {
            // Arrange
            var mockService = new Mock<ITodoListQueriesService>();
            mockService.Setup(s => s.PrintItems()).Throws(new Exception("fail"));
            var controller = new TodoListQueriesController(mockService.Object);

            // Act
            var result = controller.GetPrintItems();

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Contains("An error occurred while processing your request", conflictResult.Value?.ToString());
        }
    }
}
