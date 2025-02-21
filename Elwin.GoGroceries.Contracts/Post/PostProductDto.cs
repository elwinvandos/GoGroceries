using Elwin.GoGroceries.Contracts.DtoBases;

namespace Elwin.GoGroceries.Contracts.Post
{
    public record PostProductDto : NamedDtoBase
    {
        public CategoryDto Category { get; set; } = new();
        public int? Quantity { get; set; }
        public string? Measurement { get; set; }
        public int? MeasurementQuantity { get; set; }
    }
}
