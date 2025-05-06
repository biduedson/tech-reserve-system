using DotNetEnv;
using TechReserveSystem.API.Filters;
using TechReserveSystem.API.Middleware;
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
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));



builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
