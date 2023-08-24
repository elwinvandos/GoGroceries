using Elwin.GoGroceries.Contracts.DtoBases;

namespace Elwin.GoGroceries.Contracts.Product
{
    public record ProductDto : NamedDtoBase
    {
        public Guid CategoryId { get; set; }
        public int? Quantity { get; set; }
        public string? Measurement { get; set; }
        public int? MeasurementQuantity { get; set; }
    }
}
