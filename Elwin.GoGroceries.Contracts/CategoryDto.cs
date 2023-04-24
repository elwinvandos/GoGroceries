namespace Elwin.GoGroceries.Contracts
{
    public class CategoryDto
    {
        // NOTE: these overrides are required for mudblazor select to work properly.
        // When we switch to Smart Search, remove all code below, re-inherit NamedDtoBase
        // and convert the dto back to a record
        // see https://mudblazor.com/components/select

        public Guid? Id { get; set; }
        public string Name { get; set; }

        // Note: this is important so the MudSelect can compare pizzas
        public override bool Equals(object o)
        {
            var other = o as CategoryDto;
            return other?.Name == Name;
        }

        // Note: this is important too!
        public override int GetHashCode() => Name?.GetHashCode() ?? 0;

        // Implement this for the Pizza to display correctly in MudSelect
        public override string ToString() => Name;
    }
}
