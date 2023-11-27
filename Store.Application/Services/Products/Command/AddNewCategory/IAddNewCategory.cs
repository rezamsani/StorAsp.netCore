using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Command.AddNewCategory
{
    public interface IAddNewCategory
    {
        ResultDto Execute(long? ParentId, String Name);
    }

    public class AddNewCategory : IAddNewCategory
    {
        private readonly IDataBaseContext _context;
        public AddNewCategory(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long? ParentId, string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام دسته بندی را وارد نمایید"
                };
            }
            Categroy categroy = new Categroy()
            {
                Name = Name,
                ParentCategory = GetParent(ParentId)
            };
            _context.Categroys.Add(categroy);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت اضافه شد."
            };

        }
        private Categroy GetParent(long? ParentId)
        {
            return _context.Categroys.Find(ParentId);
        }
    }

}
