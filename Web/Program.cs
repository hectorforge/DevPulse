using Application;
using Application.Interfaces;
using Application.Services;
using Application.Services.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
// Inyeccion de servicios de las capas anteriores
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();