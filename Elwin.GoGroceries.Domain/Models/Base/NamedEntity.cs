namespace Elwin.GoGroceries.Domain.Models.Base
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; private set; }

        protected NamedEntity(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void UpdateName(string name)
        {
            if (name == Name) return;
            Name = name;
        }
    }
}
