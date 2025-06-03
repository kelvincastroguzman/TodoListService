using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration.AzureKeyVault;
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
    IConfigurationSection keyVaultKeyVaultUrl = builder.Configuration.GetSection("KeyVault:KeyVaultUrl");
    IConfigurationSection keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
    IConfigurationSection keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
    IConfigurationSection keyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID");

    var credential = new ClientSecretCredential(keyVaultDirectoryID.Value, keyVaultClientId.Value, keyVaultClientSecret.Value);

    builder.Configuration.AddAzureKeyVault(keyVaultKeyVaultUrl.Value, keyVaultClientId.Value, keyVaultClientSecret.Value, new DefaultKeyVaultSecretManager());

    var client = new SecretClient(new Uri(keyVaultKeyVaultUrl.Value), credential);

    string todoListDbConnectionString = client.GetSecret("TodoListDB-ConnectionString").Value.Value;

    return todoListDbConnectionString;
}