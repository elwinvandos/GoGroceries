namespace Elwin.GoGroceries.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public string? ColorCode { get; private set; }

        public Category(string name, string? colorCode)
        {
            Name = name;
            ColorCode = colorCode;
        }
    }
}
