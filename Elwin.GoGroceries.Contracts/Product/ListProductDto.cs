namespace Elwin.GoGroceries.Contracts.Product
{
    public record ListProductDto : ProductDto
    {
        public bool IsCheckedOff { get; set; } = false;
    }
}
