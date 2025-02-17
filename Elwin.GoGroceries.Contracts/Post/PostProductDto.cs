using Elwin.GoGroceries.Contracts.DtoBases;

namespace Elwin.GoGroceries.Contracts.Post
{
    public record PostProductDto : NamedDtoBase
    {
        // This used to be not nullable and constructed a new object, I removed it now keep an eye on this
        public CategoryDto? Category { get; set; }
        public int? Quantity { get; set; }
        public string? Measurement { get; set; }
        public int? MeasurementQuantity { get; set; }
    }
}
