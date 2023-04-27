using Elwin.GoGroceries.Contracts.DtoBases;

namespace Elwin.GoGroceries.Contracts
{
    public record GroceryListDto : NamedDtoBase
    {
        public ICollection<ProductDto>? Products { get; set; }
    }
}
