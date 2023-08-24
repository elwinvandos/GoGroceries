using Elwin.GoGroceries.Contracts.DtoBases;
using Elwin.GoGroceries.Contracts.Product;

namespace Elwin.GoGroceries.Contracts
{
    public record TemplateDto : NamedDtoBase
    {
        public ICollection<TemplateProductDto>? Products { get; set; }
    }
}
