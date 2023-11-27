using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Finance.Command.AddRequestPay
{
    public interface IAddRequestPay
    {
        ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId);
    }
}
