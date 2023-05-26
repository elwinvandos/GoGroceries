namespace Elwin.GoGroceries.Domain.Models
{
    public class Category : NamedEntity
    {
        public string? ColorCode { get; private set; }

        public Category(string name, string? colorCode) : base(name)
        {
            ColorCode = colorCode;
        }

        public void UpdateColorCode(string colorCode)
        {
            if (colorCode == ColorCode) return;
            ColorCode = colorCode;
        }
    }
}
