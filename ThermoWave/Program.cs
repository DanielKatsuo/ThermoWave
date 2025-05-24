using ThermoWave.Domain.Entities;
using ThermoWave.Domain.Interfaces;
using ThermoWave.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMicrowavesService, MicrowavesService>();
builder.Services.AddSingleton<Microwaves>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Microwaves}/{action=Index}");

app.Run();
