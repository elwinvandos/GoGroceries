using Elwin.GoGroceries.Domain.Models.GroceryLists.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elwin.GoGroceries.Infrastructure.Config
{
    public class GroceryListTemplateConfig : IEntityTypeConfiguration<GroceryListTemplate>
    {
        public void Configure(EntityTypeBuilder<GroceryListTemplate> builder)
        {
            builder.ToTable("grocery_list_templates");

            builder.Metadata?.FindNavigation(nameof(GroceryListTemplate.TemplateProducts))?
                            .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
