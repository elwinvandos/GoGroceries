namespace Elwin.GoGroceries.Contracts.DtoBases
{
    public record NamedDtoBase : DtoBase
    {
        public string Name { get; set; } = null!;
    }
}
