namespace Elwin.GoGroceries.Contracts
{
    public record GroceryListDto : NamedDtoBase
    {
        public ICollection<GroceryItemDto>? GroceryItems { get; set; }
    }
}
