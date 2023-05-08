using Elwin.GoGroceries.API.Configurations;
using Elwin.GoGroceries.Core.Extensions;
using Elwin.GoGroceries.Infrastructure.DbContexts;
using Elwin.GoGroceries.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfigurations();

// Add services to the container.

builder.Services.AddInfrastructureExtensions();
builder.Services.AddCoreExtensions();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.AddConfigurationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    using (var context = app.Services.GetService<GroceriesContext>())
        context.Database.Migrate();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsProduction())
{
    using (var context = app.Services.GetService<GroceriesContext>())
        context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
