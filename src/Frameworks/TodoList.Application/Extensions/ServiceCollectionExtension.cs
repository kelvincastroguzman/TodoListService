using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.TodoList.Commands;
using TodoList.Application.TodoList.Commands.Contracts;
using TodoList.Application.TodoList.Queries;
using TodoList.Application.TodoList.Queries.Contracts;
using TodoList.Application.TodoList.Validators;
using TodoList.Application.TodoList.Validators.Contracts;
using TodoList.Domain.IRepositories.TodoList.Commands;
using TodoList.Domain.IRepositories.TodoList.Queries;
using TodoList.Infrastructure.Persistence;
using TodoList.Infrastructure.Repositories.TodoList.Commands;
using TodoList.Infrastructure.Repositories.TodoList.Queries;

namespace TodoList.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTodoListFramework(this IServiceCollection services, string dbConnectionString)
        {
            // Application Commands (Depend on DbContext) 
            services.AddScoped<ITodoListCommandsService, TodoListCommandsService>();

            // Application Queries (Depend on DbContext) 
            services.AddScoped<ITodoListQueriesService, TodoListQueriesService>();

            // Application Validators (Stateless) 
            services.AddTransient<ITodoListValidator, CreateTodoItemValidator>();
            services.AddTransient<ITodoListValidator, UpdateTodoItemValidator>();
            services.AddTransient<ITodoListValidator, RemoveTodoItemValidator>();
            services.AddTransient<ITodoListValidator, RegisterProgressionTodoItemValidator>();

            // Repository Commands (Depend on DbContext) 
            services.AddScoped<IProgressionCommandsRepository, ProgressionCommandsRepository>();
            services.AddScoped<ITodoItemCommandsRepository, TodoItemCommandsRepository>();

            // Repository Queries (Depend on DbContext) 
            services.AddScoped<IProgressionQueriesRepository, ProgressionQueriesRepository>();
            services.AddScoped<ITodoItemQueriesRepository, TodoItemQueriesRepository>();
            services.AddScoped<ITodoListQueriesRepository, TodoListQueriesRepository>();

            // DbContext
            services.AddDbContext<TodoListDbContext>(options => options.UseSqlServer(dbConnectionString));

            return services;
        }
    }
}
