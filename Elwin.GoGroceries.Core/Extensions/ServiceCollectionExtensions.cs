using Elwin.GoGroceries.Core.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Elwin.GoGroceries.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCoreExtensions(this IServiceCollection services)
    {
        services.AddScoped<IManageGroceryLists, ManageGroceryLists>();
    }
}

