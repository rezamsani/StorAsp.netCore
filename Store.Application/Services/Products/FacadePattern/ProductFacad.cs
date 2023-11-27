using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPatterns;
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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.FacadePattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public ProductFacad(IDataBaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;

        }
        private AddNewCategory _addNewCategory;
        public AddNewCategory AddNewCategory
        {
            get
            {
                return _addNewCategory = _addNewCategory ?? new AddNewCategory(_context);

            }
        }

        private IGetCategory _getCategory;
        public IGetCategory GetCategory
        {
            get
            {
                return _getCategory = _getCategory ?? new GetCategory(_context);
            }
        }
        private IRemovCategory _removCategory;
        public IRemovCategory RemovCategory
        {
            get
            {
                return _removCategory = _removCategory ?? new RemovCategory(_context);
            }
        }
        private IEditeCategory _editeCategory;
        public IEditeCategory EditeCategory
        {
            get
            {
                return _editeCategory = _editeCategory ?? new EditeCategory(_context);
            }
        }
        private IGetParents _getParents;
        public IGetParents GetParents
        {
            get
            {
                return _getParents = _getParents ?? new GetParents(_context);
            }
        }

        private AddNewProduct _addNewProduct;
        public AddNewProduct AddNewProduct
        {
            get
            {
                return _addNewProduct = _addNewProduct ?? new AddNewProduct(_context, _environment);
            }
        }
        private IGetAllCategory _getAllCategory;
        public IGetAllCategory GetAllCategory
        {
            get
            {
                return _getAllCategory = _getAllCategory ?? new GetAllCategory(_context);
            }
        }
        private IGetProductForAdmin _getProductForAdmin;
        public IGetProductForAdmin GetProductForAdmin
        {
            get
            {
                return _getProductForAdmin = _getProductForAdmin ?? new GetProductForAdmin(_context);
            }
        }
        private IGetProductDetailForAdmin _getProductDetailForAdmin;
        public IGetProductDetailForAdmin GetProductDetailForAdmin
        {
            get
            {
                return _getProductDetailForAdmin = _getProductDetailForAdmin ?? new GetProductDetailForAdmin(_context);
            }
        }
    }
}
