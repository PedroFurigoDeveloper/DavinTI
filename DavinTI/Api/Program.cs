using DavinTI.Application.Interfaces;
using DavinTI.Application.Interfaces.Service;
using DavinTI.Application.Services;
using DavinTI.Data;
using DavinTI.Domain.Interfaces;
using DavinTI.Domain.Repository;
using DavinTI.Infra.Repositories;
using DavinTI.Infra.Repository;
using DavinTI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuração do banco de dados
builder.Services.AddDbContext<DavinTIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<ITelefoneRepository, TelefoneRepository>();

// Services
builder.Services.AddScoped<IContatoService, ContatoService>();
builder.Services.AddScoped<ITelefoneService, TelefoneService>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ativa o CORS antes de redirecionar HTTPS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
