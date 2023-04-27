using Elwin.GoGroceries.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
