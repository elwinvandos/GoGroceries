namespace Elwin.GoGroceries.Contracts
{
    public record NamedDtoBase : DtoBase
    {
        public string Name { get; set; } = null!;
    }
}
