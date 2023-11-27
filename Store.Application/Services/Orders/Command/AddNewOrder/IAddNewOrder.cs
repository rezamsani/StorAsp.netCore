﻿using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Orders.Command.AddNewOrder
{
    public interface IAddNewOrder
    {
        ResultDto Execute(RequestAddNewOrderSericeDto request);
    }
}
