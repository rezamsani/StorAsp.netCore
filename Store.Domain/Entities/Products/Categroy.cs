using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    public class Categroy:BaseEntity
    {
        public string Name { get; set; }
        public virtual Categroy ParentCategory { get; set; }
        public long? ParentCategoryId { get; set; }
        public virtual ICollection<Categroy> SubCategorys { get; set; }
    }
}
