namespace Elwin.GoGroceries.Contracts
{
    public record GroceryItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
