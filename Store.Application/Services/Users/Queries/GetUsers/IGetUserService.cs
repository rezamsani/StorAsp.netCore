﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUserService
    {
        ResultGetUserDto Execute(RequestGetUserDto request);
    }
}
