using TodoList.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();

builder.Services.AddTodoListFramework(GetDbConnectionString(builder));

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