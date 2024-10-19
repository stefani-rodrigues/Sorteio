using Microsoft.EntityFrameworkCore;
using sorteio.Aplic.Testes;
using sorteio.Dominio.Base;
using sorteio.Repositorio.Contexto;
using sorteio.Repositorio.Repositorios.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(AplicTeste), typeof(Teste), typeof(RepTeste))
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());

// Configurações de acordo com o ambiente - mudar no ambientelaunch
builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables();

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<ContextoBanco>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
