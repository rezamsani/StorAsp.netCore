﻿using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Common.Query.GetCategorySear
{
    public interface IGetCategorySear
    {
        ResultDto<List<GetCategorySearDto>> Execute();
    }
}
