using Blazored.LocalStorage;
using Elwin.GoGroceries.API.Configurations;
using Elwin.GoGroceries.API.Services;
using Elwin.GoGroceries.Core.Extensions;
using Elwin.GoGroceries.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfigurations();

// Add services to the container.
builder.Services.AddScoped<IGetAppVersionService, GetAppVersionService>();
builder.Services.AddInfrastructureExtensions();
builder.Services.AddCoreExtensions();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSwaggerGen();

builder.AddConfigurationServices();

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapHealthChecks("/health");

app.Run();
