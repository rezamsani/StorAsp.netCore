using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Finance.Query.GetRequestPayForAdmin
{
    public class GetRequestPayForAdmin: IGetRequestPayForAdmin
    {
        private readonly IDataBaseContext _context;
        public GetRequestPayForAdmin(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<RequestPayDto>> Execute()
        {
            var requestPay = _context.RequestPays.Include(p => p.User).ToList()
                .Select(p => new RequestPayDto
                {
                    Amount = p.Amount,
                    Authority = p.Authority,
                    Guid = p.Guid,
                    IsPay = p.IsPay,
                    PayDate = p.PayDate,
                    RefId = p.RefId,
                    UserId = p.UserId,
                    UserName = p.User.FullName,
                    Id = p.Id
                }).ToList();
            return new ResultDto<List<RequestPayDto>>()
            {
                Data = requestPay,
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
