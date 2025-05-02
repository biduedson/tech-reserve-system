using DotNetEnv;
using TechReserveSystem.Application.Extensions;
using TechReserveSystem.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
// Adicione estes serviços antes do Build()
builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.Development", optional: true, reloadOnChange: true);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthentication(); // Adiciona serviços de autenticação
builder.Services.AddAuthorization(); // Adiciona serviços de autorização


builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
