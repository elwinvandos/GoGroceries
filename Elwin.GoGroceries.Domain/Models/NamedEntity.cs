namespace Elwin.GoGroceries.Domain.Models
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }

        protected NamedEntity(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
