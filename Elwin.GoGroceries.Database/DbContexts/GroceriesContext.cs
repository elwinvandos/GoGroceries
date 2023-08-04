using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Domain.Models.GroceryLists;
using Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryListTemplates;
using Elwin.GoGroceries.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Elwin.GoGroceries.Infrastructure.DbContexts;

public class GroceriesContext : DbContext
{
    protected readonly InfrastructureSettings _settings;

    public GroceriesContext(IOptions<InfrastructureSettings> settings)
    {
        _settings = settings.Value;
    }

    public DbSet<GroceryList> GroceryLists { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<GroceryListTemplate> Templates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(_settings.ConnectionString)
                  .UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GroceryListConfig());
        modelBuilder.ApplyConfiguration(new GroceryListProductConfig());
    }
}
