using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Finance.Query.GetRequestPay
{
    public interface IGetRequestPay
    {
        ResultDto<RequestPayDto> Execute(Guid guid);
    }
}
