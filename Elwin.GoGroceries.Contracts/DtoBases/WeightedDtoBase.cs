using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elwin.GoGroceries.Contracts.DtoBases
{
    public record WeightedDtoBase : NamedDtoBase
    {
        public int UserWeight { get; set; }
    }
}
