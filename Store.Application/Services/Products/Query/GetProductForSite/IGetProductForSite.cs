using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Query.GetProductForSite
{
    public interface IGetProductForSite
    {
        ResultDto<ResultProductForSiteDto> Execute(Ordering ordering, string SearchKey, int pageNumber, int pageSize, long? CatId=null);
    }
    public enum Ordering
    {
        NotOrder=0,
        MostVisited=1,
        Bestselling=2,
        MostPopular=3,
        theNewest=4,
        Cheapest=5,
        theMostExpensive=6
    }
}
