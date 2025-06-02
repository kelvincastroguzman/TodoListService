using Moq;
using TodoList.Application.TodoList.Commands;
using TodoList.Application.TodoList.Commands.Contracts;
using TodoList.Application.TodoList.Validators.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Commands;
using TodoList.Domain.IRepositories.TodoList.Queries;
using TodoList.Infrastructure.Repositories.TodoList.Queries;

namespace TodoList.Application.Tests.TodoList.Commands
{
    public class TodoListCommandsServiceTests
    {
        private readonly Mock<ITodoItemCommandsRepository> _todoItemCommandsRepoMock;
        private readonly Mock<IProgressionCommandsRepository> _progressionCommandsRepoMock;
        private readonly Mock<ITodoListQueriesRepository> _todoListQueriesRepoMock;
        private readonly Mock<IProgressionQueriesRepository> _progressionQueriesRepoMock;
        private readonly Mock<ITodoListValidator> _createValidatorMock;
        private readonly Mock<ITodoListValidator> _updateValidatorMock;
        private readonly Mock<ITodoListValidator> _removeValidatorMock;
        private readonly Mock<ITodoListValidator> _registerProgressionValidatorMock;
        private readonly ITodoListCommandsService _service;

        public TodoListCommandsServiceTests()
        {
            _todoItemCommandsRepoMock = new Mock<ITodoItemCommandsRepository>();
            _progressionCommandsRepoMock = new Mock<IProgressionCommandsRepository>();
            _todoListQueriesRepoMock = new Mock<ITodoListQueriesRepository>();
            _progressionQueriesRepoMock = new Mock<IProgressionQueriesRepository>();

            _createValidatorMock = new Mock<ITodoListValidator>();
            _updateValidatorMock = new Mock<ITodoListValidator>();
            _removeValidatorMock = new Mock<ITodoListValidator>();
            _registerProgressionValidatorMock = new Mock<ITodoListValidator>();

            _createValidatorMock.Setup(v => v.IsApplicable(Enums.TodoListActions.Create)).Returns(true);
            _updateValidatorMock.Setup(v => v.IsApplicable(Enums.TodoListActions.Update)).Returns(true);
            _removeValidatorMock.Setup(v => v.IsApplicable(Enums.TodoListActions.Remove)).Returns(true);
            _registerProgressionValidatorMock.Setup(v => v.IsApplicable(Enums.TodoListActions.RegisterProgression)).Returns(true);

            var validators = new List<ITodoListValidator>
            {
                _createValidatorMock.Object,
                _updateValidatorMock.Object,
                _removeValidatorMock.Object,
                _registerProgressionValidatorMock.Object
            };

            _service = new TodoListCommandsService(
                _todoItemCommandsRepoMock.Object,
                _progressionCommandsRepoMock.Object,
                _todoListQueriesRepoMock.Object,
                _progressionQueriesRepoMock.Object,
                validators
            );
        }

        [Fact]
        public void AddItem_CallsCreateAsyncAndValidator()
        {
            // Arrange
            int nextId = 10;
            string title = "TestTitle";
            string description = "TestDesc";
            string category = "TestCat";
            _todoListQueriesRepoMock.Setup(r => r.GetNextIdAsync()).ReturnsAsync(nextId);
            _todoItemCommandsRepoMock.Setup(r => r.CreateAsync(It.IsAny<TodoItem>())).ReturnsAsync(nextId);

            // Act
            _service.AddItem(0, title, description, category);

            // Assert
            _createValidatorMock.Verify(v => v.Validate(It.Is<TodoItem>(t =>
                t.Id == nextId &&
                t.Title == title &&
                t.Description == description &&
                t.Category == category
            )), Times.Once);

            _todoItemCommandsRepoMock.Verify(r => r.CreateAsync(It.Is<TodoItem>(t =>
                t.Id == nextId &&
                t.Title == title &&
                t.Description == description &&
                t.Category == category
            )), Times.Once);
        }

        [Fact]
        public void UpdateItem_CallsUpdateAsyncAndValidator()
        {
            // Arrange
            int id = 5;
            string description = "UpdatedDesc";
            _todoItemCommandsRepoMock.Setup(r => r.UpdateAsync(It.IsAny<TodoItem>())).ReturnsAsync(true);

            // Act
            _service.UpdateItem(id, description);

            // Assert
            _updateValidatorMock.Verify(v => v.Validate(It.Is<TodoItem>(t =>
                t.Id == id &&
                t.Description == description
            )), Times.Once);

            _todoItemCommandsRepoMock.Verify(r => r.UpdateAsync(It.Is<TodoItem>(t =>
                t.Id == id &&
                t.Description == description
            )), Times.Once);
        }

        [Fact]
        public void RemoveItem_CallsRemoveAsyncAndValidator()
        {
            // Arrange
            int id = 7;
            _todoItemCommandsRepoMock.Setup(r => r.RemoveAsync(id)).ReturnsAsync(true);

            // Act
            _service.RemoveItem(id);

            // Assert
            _removeValidatorMock.Verify(v => v.Validate(It.Is<TodoItem>(t => t.Id == id)), Times.Once);
            _todoItemCommandsRepoMock.Verify(r => r.RemoveAsync(id), Times.Once);
        }

        [Fact]
        public void AddItem_Throws_WhenValidatorFails()
        {
            // Arrange
            int nextId = 11;
            _todoListQueriesRepoMock.Setup(r => r.GetNextIdAsync()).ReturnsAsync(nextId);
            _createValidatorMock.Setup(v => v.Validate(It.IsAny<TodoItem>())).Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _service.AddItem(0, "t", "d", "c"));
        }

        [Fact]
        public void UpdateItem_Throws_WhenValidatorFails()
        {
            // Arrange
            _updateValidatorMock.Setup(v => v.Validate(It.IsAny<TodoItem>())).Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _service.UpdateItem(1, "desc"));
        }

        [Fact]
        public void RemoveItem_Throws_WhenValidatorFails()
        {
            // Arrange
            _removeValidatorMock.Setup(v => v.Validate(It.IsAny<TodoItem>())).Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _service.RemoveItem(1));
        }

        [Fact]
        public void RegisterProgression_Throws_WhenValidatorFails()
        {
            // Arrange
            _registerProgressionValidatorMock.Setup(v => v.Validate(It.IsAny<TodoItem>())).Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _service.RegisterProgression(1, DateTime.UtcNow, 10));
        }
    }
}