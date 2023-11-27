using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Orders.Query.GetUserOrder
{
    public class GetUserOrdersDto
    {
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
        public long RequestPayId { get; set; }
        public List<OrderDetailsDto> OrderDetails { get; set; }
    }

}
