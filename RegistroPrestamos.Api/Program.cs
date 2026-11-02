using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiante.Services;   
using RegistroPrestamos.Data;          
using RegistroPrestamos.Services;   
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
builder.Services.AddDbContextFactory<RegistrolibrodbContext>(op => op.UseSqlServer(ConStr));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Registro de Inyección de Dependencias
builder.Services.AddScoped<LibrosServices>();
builder.Services.AddScoped<EstudiantesServices>();
builder.Services.AddScoped<PrestamosServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();