using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Query.GetCategorys
{
    public interface IGetCategory
    {
        ResultDto<List<CategoriesDto>> Execute(long? parentId);
    }

    public class GetCategory : IGetCategory
    {
        private readonly IDataBaseContext _context;
        public GetCategory(IDataBaseContext context)
        {
            _context = context;
            
        }
        public ResultDto<List<CategoriesDto>> Execute(long? parentId)
        {           
            var categores = _context.Categroys
                .Include(ca => ca.ParentCategory)
                .Include(ca => ca.SubCategorys)
                .Where(ca => ca.ParentCategoryId == parentId)
                .ToList()
                .Select(ca => new CategoriesDto
                {
                    Id = ca.Id,
                    Name = ca.Name,
                    Parent = ca.ParentCategory != null ? new ParentCategoryDto
                    {
                        Id = ca.ParentCategory.Id,
                        Name = ca.ParentCategory.Name,
                    }
                    : null,
                    HasChild = ca.SubCategorys.Count() > 0 ? true : false
                }).ToList();
            return new ResultDto<List<CategoriesDto>>()
            {
                Data = categores,
                IsSuccess = true,
                Message = "لیست با موفقیت برگشت داده شد"
            };
        }
    }
    public class CategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasChild { get; set; }
        public ParentCategoryDto Parent { get; set; }
    }
    public class ParentCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

}
