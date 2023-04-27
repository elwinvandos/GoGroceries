namespace Elwin.GoGroceries.Domain.Models
{
    public class GroceryListProduct : Entity
    {
        public Guid GroceryListId { get; private set; }
        public GroceryList GroceryList { get; private set; }

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        public bool IsCheckedOff { get; private set; } = false;

        private GroceryListProduct()
        {

        }

        public GroceryListProduct(GroceryList groceryList, Product product)
        {
            GroceryList = groceryList ?? throw new ArgumentNullException(nameof(groceryList));
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }
    }
}
