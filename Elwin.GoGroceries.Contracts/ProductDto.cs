using Elwin.GoGroceries.Contracts.DtoBases;

namespace Elwin.GoGroceries.Contracts
{
    public record ProductDto : NamedDtoBase
    {
        public Guid? CategoryId { get; set; }
        public int? Quantity { get; set; }
        public int? Weight { get; set; }
        public bool IsCheckedOff { get; set; } = false;
    }
}
