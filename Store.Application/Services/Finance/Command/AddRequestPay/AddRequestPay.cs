using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Finance;

namespace Store.Application.Services.Finance.Command.AddRequestPay
{
    public class AddRequestPay : IAddRequestPay
    {
        private readonly IDataBaseContext _context;
        public AddRequestPay(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId)
        {
            var user = _context.Users.Find(UserId);
            RequestPay requestPay = new RequestPay()
            {
                Amount = Amount,
                Guid = Guid.NewGuid(),
                IsPay = false,
                User = user,
                //UserId = user.Id
            };
            _context.RequestPays.Add(requestPay);
            _context.SaveChanges();
            return new ResultDto<ResultRequestPayDto>()
            {
                Data = new ResultRequestPayDto()
                {
                    guid = requestPay.Guid,
                    Amount = requestPay.Amount,
                    Email = user.Email,
                    RequestPayId = requestPay.Id
                },
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
