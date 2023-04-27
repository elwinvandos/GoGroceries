using Elwin.GoGroceries.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elwin.GoGroceries.Infrastructure.Config;

public class GroceryListConfig : IEntityTypeConfiguration<GroceryList>
{
    public void Configure(EntityTypeBuilder<GroceryList> builder)
    {
        builder.ToTable("grocery_lists");

        builder.Metadata?.FindNavigation(nameof(GroceryList.ListProducts))?
                        .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
 