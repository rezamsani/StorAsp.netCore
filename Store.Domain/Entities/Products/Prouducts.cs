using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Invertory { get; set; }
        public bool Displayed { get; set; }
        public int ViewCount { get; set; }
        public virtual Categroy Categroy { get; set; }
        public long CategoryId { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ProductFeaturs> ProductFeaturs { get; set; }
    }
}
