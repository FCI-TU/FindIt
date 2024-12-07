using FindIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Domain.ProductEntities
{
    public class Size : BaseEntity
    {
        public string SizeName { get; set; } = null!;
        public virtual ICollection<ProductSize> ProductSizes { get; set; } = null!;
    }
}
