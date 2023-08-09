using Elwin.GoGroceries.Domain.Models.GroceryLists.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elwin.GoGroceries.Infrastructure.Config
{
    public class GroceryListTemplateProductConfig : IEntityTypeConfiguration<GroceryListTemplateProduct>
    {
        public void Configure(EntityTypeBuilder<GroceryListTemplateProduct> builder)
        {
            builder.ToTable("grocery_list_template_product");

            builder.HasOne(x => x.GroceryListTemplate).WithMany(t => t.TemplateProducts).HasForeignKey(tc => tc.GroceryListTemplateId);
            builder.HasOne(x => x.Product).WithMany(c => c.TemplateProducts).HasForeignKey(tc => tc.ProductId);
        }
    }
}
