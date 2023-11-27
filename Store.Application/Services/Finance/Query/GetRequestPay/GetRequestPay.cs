using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Finance.Query.GetRequestPay
{
    public class GetRequestPay: IGetRequestPay
    {
        private readonly IDataBaseContext _context;
        public GetRequestPay(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<RequestPayDto> Execute(Guid guid)
        {
            var requestPay = _context.RequestPays.Where(p => p.Guid == guid).FirstOrDefault();
            if(requestPay != null)
            {
                return new ResultDto<RequestPayDto>()
                {
                    Data = new RequestPayDto()
                    {
                        Id=requestPay.Id,
                        Amount = requestPay.Amount
                    },
                    IsSuccess = true,
                    Message = ""
                };
            }
            else
            {
                throw new Exception("request pay not found");
            }
        }
    }
}
