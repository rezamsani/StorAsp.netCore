using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Orders.Query.GetUserOrder
{
    public class GetUserOrder: IGetUserOrder
    {
        private readonly IDataBaseContext _context;
        public GetUserOrder(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetUserOrdersDto>> Execute(long UserId)
        {
            var orders = _context.Orders.Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product).Where(p => p.UserId == UserId).ToList()
                .Select(p => new GetUserOrdersDto
                {
                    OrderId = p.Id,
                    OrderState = p.OrderState,
                    RequestPayId = p.RequestPayId,
                    OrderDetails = p.OrderDetails.Select(o => new OrderDetailsDto
                    {
                        Count = o.Count,
                        OrderDetailId = o.Id,
                        Price = o.Price,
                        ProductId = o.ProductId,
                        ProductName = o.Product.Name
                    }).ToList()
                }).ToList();
            return new ResultDto<List<GetUserOrdersDto>>()
            {
                Data = orders,
                IsSuccess = true,
                Message=""
            };
        }
    }
}
