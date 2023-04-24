using Elwin.GoGroceries.Contracts.DtoBases;

namespace Elwin.GoGroceries.Contracts
{
    public record GroceryItemDto : NamedDtoBase
    {
        public Guid? CategoryId { get; set; }
    }
}
