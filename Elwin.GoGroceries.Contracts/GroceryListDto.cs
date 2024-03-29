﻿using Elwin.GoGroceries.Contracts.DtoBases;
using Elwin.GoGroceries.Contracts.Product;

namespace Elwin.GoGroceries.Contracts
{
    public record GroceryListDto : NamedDtoBase
    {
        public ICollection<ListProductDto>? Products { get; set; }
    }
}
