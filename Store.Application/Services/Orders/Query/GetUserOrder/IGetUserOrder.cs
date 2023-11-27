using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Orders.Query.GetUserOrder
{
    public interface IGetUserOrder
    {
        ResultDto<List<GetUserOrdersDto>> Execute(long UserId);
    }
}
