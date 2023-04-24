namespace Elwin.GoGroceries.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            Name = name;
        }
    }
}
