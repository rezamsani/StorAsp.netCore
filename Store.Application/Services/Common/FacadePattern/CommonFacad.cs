using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Common.Query.GetCategorySear;
using Store.Application.Services.Common.Query.GetMenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Common.FacadePattern
{
    public class CommonFacad : ICommon
    {
        private readonly IDataBaseContext _context;
        public CommonFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IGetMenuItem _getMenuItem;
        public IGetMenuItem GetMenuItem
        {
            get
            {
                return _getMenuItem = _getMenuItem ?? new GetMenuItem(_context);
            }
        }
        private IGetCategorySear _getCategorySear;
        public IGetCategorySear GetCategorySear
        {
            get
            {
                return _getCategorySear = _getCategorySear ?? new GetCategorySear(_context);
            }
        }
    }
}
