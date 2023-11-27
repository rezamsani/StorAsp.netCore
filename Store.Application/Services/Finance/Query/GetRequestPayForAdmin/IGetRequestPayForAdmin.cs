using Store.Common.Dto;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Finance.Query.GetRequestPayForAdmin
{
    public interface IGetRequestPayForAdmin
    {
        ResultDto<List<RequestPayDto>> Execute();
    }
}
