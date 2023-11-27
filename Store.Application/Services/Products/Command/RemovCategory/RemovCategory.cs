using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Command.RemovCategory
{
    public class RemovCategory : IRemovCategory
    {
        private readonly IDataBaseContext _context;
        public RemovCategory(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long categoryId)
        {
            var categore = _context.Categroys
                .Include(ca => ca.SubCategorys)
                .FirstOrDefault(ca => ca.Id == categoryId);
            if (categore == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "چنین دسته بندی و کالایی وجود ندارد"
                };
            }
            foreach (var item in categore.SubCategorys.ToList())
            {
                item.IsRemoved = true;
                item.RemoveTime = DateTime.Now;
            }
            categore.RemoveTime = DateTime.Now;
            categore.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "اطلاعات با موفقیت حذف شد"
            };
        }
    }
}
