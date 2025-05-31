using Microsoft.EntityFrameworkCore;
using TodoList.Application.Extensions;
using TodoList.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

string todoListDbConnectionString = GetDbConnectionString(builder);
builder.Services.AddDbContext<TodoListDbContext>(options => options.UseSqlServer(todoListDbConnectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();

builder.Services.AddTodoListFramework();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

static string GetDbConnectionString(WebApplicationBuilder builder)
{
    string todoListDbConnectionString = "Server=KELVINCASTRO; Database=TodoListDB; User Id=ke; Password=12345678; MultipleActiveResultSets=True; Encrypt=false; Connection Timeout=500;";
    return todoListDbConnectionString;
}