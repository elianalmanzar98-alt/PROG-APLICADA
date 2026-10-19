using GestionLibros.DAL;
using GestionLibros.Services;
using Microsoft.EntityFrameworkCore;
using GestionLibros.Components;
using GestionLibros.Models;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));


builder.Services.AddScoped<LibrosService>();
builder.Services.AddScoped<EstudiantesService>();
builder.Services.AddScoped<PrestamosService>();
builder.Services.AddScoped<IService<Prestamos>>(sp => sp.GetRequiredService<PrestamosService>());

builder.Services.AddControllers();
builder.Services.AddScoped(sp =>
{
    var navegador = sp.GetRequiredService<NavigationManager>();
    return new HttpClient {BaseAddress = new Uri(navegador.BaseUri)};

});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
