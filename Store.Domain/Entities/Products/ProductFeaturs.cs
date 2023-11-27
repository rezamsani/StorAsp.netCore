using Store.Domain.Entities.Commons;

namespace Store.Domain.Entities.Products
{
    public class ProductFeaturs: BaseEntity
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
