using Store.Application.Services.Common.Query.GetCategorySear;
using Store.Application.Services.Common.Query.GetMenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadPatterns
{
    public interface ICommon
    {
        IGetMenuItem GetMenuItem { get; }
        IGetCategorySear GetCategorySear { get; }
    }

}
