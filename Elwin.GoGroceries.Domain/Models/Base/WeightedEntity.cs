using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elwin.GoGroceries.Domain.Models.Base
{
    public abstract class WeightedEntity : NamedEntity
    {
        public int UserWeight { get; private set; }

        protected WeightedEntity(string name) : base(name)
        {
        }

        public void IncreaseUserWeight()
        {
            UserWeight++;
        }
    }
}
