using Elwin.GoGroceries.Domain.Models.GroceryLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elwin.GoGroceries.Infrastructure.Config
{
    public class GroceryListProductConfig : IEntityTypeConfiguration<GroceryListProduct>
    {
        public void Configure(EntityTypeBuilder<GroceryListProduct> builder)
        {
            builder.ToTable("grocery_list_product");

            builder.HasOne(x => x.GroceryList).WithMany(t => t.ListProducts).HasForeignKey(tc => tc.GroceryListId);
            builder.HasOne(x => x.Product).WithMany(c => c.ListProducts).HasForeignKey(tc => tc.ProductId);
        }
    }
}
