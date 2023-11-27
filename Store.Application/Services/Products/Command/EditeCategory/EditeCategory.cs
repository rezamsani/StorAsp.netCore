using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Command.EditeCategory
{
    public class EditeCategory : IEditeCategory
    {
        private readonly IDataBaseContext _context;
        public EditeCategory(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long categoryId, string categoryName, long ParentsId)
        {
            var categroy = _context.Categroys.Find(categoryId);
            if(categroy == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "دسته بندی مورد نظر یافت نشد"
                };
            }
            categroy.Name = categoryName;
            categroy.ParentCategoryId = ParentsId;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت ویرایش شد"
            };
        }
    }
}
