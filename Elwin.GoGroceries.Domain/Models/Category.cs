using Elwin.GoGroceries.Domain.Models.Base;

namespace Elwin.GoGroceries.Domain.Models
{
    public class Category : WeightedEntity
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
