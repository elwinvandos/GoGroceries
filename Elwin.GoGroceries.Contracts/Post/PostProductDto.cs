using Elwin.GoGroceries.Contracts.DtoBases;

namespace Elwin.GoGroceries.Contracts.Post
{
    public record PostProductDto : NamedDtoBase
    {
        public CategoryDto Category { get; set; } = new();
        public int? Quantity { get; set; }
        public int? Weight { get; set; }
    }
}
