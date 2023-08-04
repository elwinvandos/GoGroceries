using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elwin.GoGroceries.Domain.Models.GroceryLists;
using Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryListTemplates;
using Elwin.GoGroceries.Domain.Models.GroceryListTemplates;

namespace Elwin.GoGroceries.Infrastructure.Config;

public class GroceryListProductTemplateConfig : IEntityTypeConfiguration<GroceryListTemplateProduct>
{
    public void Configure(EntityTypeBuilder<GroceryListTemplateProduct> builder)
    {
        builder.ToTable("grocery_list_template_product");

        builder.HasOne(x => x.GroceryListTemplate).WithMany(c => c.ListTemplateProducts);
        builder.HasOne(x => x.Product).WithMany(p => p.ListTemplateProducts);
    }
}