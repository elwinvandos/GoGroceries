namespace Elwin.GoGroceries.Contracts
{
    public record GroceryListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<GroceryItemDto>? ShoppingItems { get; set; }
    }
}
