using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.TodoList.Commands;
using TodoList.Application.TodoList.Commands.Contracts;
using TodoList.Application.TodoList.Queries;
using TodoList.Application.TodoList.Queries.Contracts;
using TodoList.Application.TodoList.Validators;
using TodoList.Application.TodoList.Validators.Contracts;
using TodoList.Domain.IRepositories.TodoList.Commands;
using TodoList.Domain.IRepositories.TodoList.Queries;
using TodoList.Infrastructure.Repositories.TodoList.Commands;
using TodoList.Infrastructure.Repositories.TodoList.Queries;

namespace TodoList.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTodoListFramework(this IServiceCollection services)
        {
            // Application Commands
            services.AddTransient<ITodoListCommandsService, TodoListCommandsService>();

            // Application Queries
            services.AddTransient<ITodoListQueriesService, TodoListQueriesService>();

            // Application Validators
            services.AddScoped<ITodoListValidator, CreateTodoItemValidator>();
            services.AddScoped<ITodoListValidator, UpdateTodoItemValidator>();
            services.AddScoped<ITodoListValidator, RemoveTodoItemValidator>();
            services.AddScoped<ITodoListValidator, RegisterProgressionTodoItemValidator>();

            // Repository Commands 
            services.AddTransient<IProgressionCommandsRepository, ProgressionCommandsRepository>();
            services.AddTransient<ITodoItemCommandsRepository, TodoItemCommandsRepository>();

            // Repository Queries 
            services.AddTransient<IProgressionQueriesRepository, ProgressionQueriesRepository>();
            services.AddTransient<ITodoItemQueriesRepository, TodoItemQueriesRepository>();
            services.AddTransient<ITodoListQueriesRepository, TodoListQueriesRepository>();

            // Mappers

            
            return services;
        }
    }
}
