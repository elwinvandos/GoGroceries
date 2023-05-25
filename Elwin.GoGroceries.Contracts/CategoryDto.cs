using Elwin.GoGroceries.Contracts.DtoBases;

namespace Elwin.GoGroceries.Contracts
{
    public record CategoryDto : NamedDtoBase
    {
        public string? ColorCode { get; set; }

        // NOTE: these overrides are required for mudblazor select to work properly.
        // When we switch to Smart Search, remove these.
        // We cannot override Equals in a record, as it is already overriden there.
        // For now the MudSelect seems to be working without it.
        // see https://mudblazor.com/components/select

        // Note: this is important so the MudSelect can compare pizzas
        //public override bool Equals(object o)
        //{
        //    var other = o as CategoryDto;
        //    return other?.Name == Name;
        //}

        // Note: this is important too!
        public override int GetHashCode() => Name?.GetHashCode() ?? 0;

        // Implement this for the Pizza to display correctly in MudSelect
        public override string ToString() => Name;
    }
}
