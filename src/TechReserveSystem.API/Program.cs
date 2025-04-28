using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
// Adicione estes serviços antes do Build()

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddAuthentication(); // Adiciona serviços de autenticação
builder.Services.AddAuthorization(); // Adiciona serviços de autorização

builder.Services.AddControllers();
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
