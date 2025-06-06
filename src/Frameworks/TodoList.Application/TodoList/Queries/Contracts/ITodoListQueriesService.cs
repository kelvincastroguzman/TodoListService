﻿using TodoList.Application.Dtos;

namespace TodoList.Application.TodoList.Queries.Contracts
{
    public interface ITodoListQueriesService
    {
        int GetNextId();
        IReadOnlyCollection<string> GetAllCategories();
        IReadOnlyCollection<TodoItemDto> PrintItems();
    }
}
