using Elwin.GoGroceries.Contracts.DtoBases;

namespace Elwin.GoGroceries.Contracts.Post
{
    public record PostGroceryItemDto : NamedDtoBase
    {
        public CategoryDto? Category { get; set; } = new();
    }
}
