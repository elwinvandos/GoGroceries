namespace Elwin.GoGroceries.Domain.Models
{
    public class GroceryListProduct : Entity
    {
        public Guid GroceryListId { get; private set; }
        public GroceryList GroceryList { get; private set; }

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        public Guid CategoryId { get; private set; }
        public int? Quantity { get; private set; }
        public string? Measurement { get; private set; }
        public int? MeasurementQuantity { get; private set; }
        public bool IsCheckedOff { get; private set; } = false;

        private GroceryListProduct()
        { 
        }

        public GroceryListProduct(GroceryList groceryList, Product product, Guid categoryId, int? quantity = null, string? measurement = null, int? measurementQuantity = null)
        {
            GroceryList = groceryList ?? throw new ArgumentNullException(nameof(groceryList));
            Product = product ?? throw new ArgumentNullException(nameof(product));
            CategoryId = categoryId;
            Quantity = quantity;
            Measurement = measurement;
            MeasurementQuantity = measurementQuantity;
        }

        public void ToggleIsCheckedOff()
        {
            IsCheckedOff = !IsCheckedOff;
        }

        public void Update(bool isCheckedOff)
        {
            IsCheckedOff = isCheckedOff;
        }

        public void Update(Guid categoryId, int? quantity = null, string? measurement = null, int? measureMentQuantity = null)
        {
            CategoryId = categoryId;
            Quantity = quantity;
            Measurement = measurement;
            MeasurementQuantity = measureMentQuantity;
        }
    }
}
