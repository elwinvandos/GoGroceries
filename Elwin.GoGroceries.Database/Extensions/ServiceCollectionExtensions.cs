using Elwin.GoGroceries.Infrastructure.DbContexts;
using Elwin.GoGroceries.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Elwin.GoGroceries.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureExtensions(this IServiceCollection services)
    {
        services.AddDbContext<GroceriesContext>();
        services.AddScoped<IGroceryRepository, GroceryRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
