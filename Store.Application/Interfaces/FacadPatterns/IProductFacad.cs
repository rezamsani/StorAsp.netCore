using Store.Application.Services.Products.Command.AddNewCategory;
using Store.Application.Services.Products.Command.AddNewProduct;
using Store.Application.Services.Products.Command.EditeCategory;
using Store.Application.Services.Products.Command.RemovCategory;
using Store.Application.Services.Products.Query.GetAllCategory;
using Store.Application.Services.Products.Query.GetCategorys;
using Store.Application.Services.Products.Query.GetParents;
using Store.Application.Services.Products.Query.GetProductDetailForAdmin;
using Store.Application.Services.Products.Query.GetProductForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        AddNewCategory AddNewCategory { get; }
        IGetCategory GetCategory { get; }
        IRemovCategory RemovCategory { get; }
        IEditeCategory EditeCategory { get; }
        IGetParents GetParents { get; }
        AddNewProduct AddNewProduct { get; }
        IGetAllCategory GetAllCategory { get; }
        IGetProductForAdmin GetProductForAdmin { get; }
        IGetProductDetailForAdmin GetProductDetailForAdmin { get; }
    }
}
