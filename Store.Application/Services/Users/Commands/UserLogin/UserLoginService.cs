using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Commands.UserLogin
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IDataBaseContext _context;
        public UserLoginService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultUserloginDto> Execute(string Username, string Password)
        {
            #region validation
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = "نام کاربری و رمز عبور را وارد نمایید"
                };
            }
            var user = _context.Users.Include(p => p.UserInRoles)
                .ThenInclude(p => p.Role)
                .Where(p => p.Email == Username && p.IsActive == true).
                FirstOrDefault();
            if (user == null)
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto()
                    {
                    },
                    IsSuccess = false,
                    Message = "کاربری با این ایمیل در سایت فروشگاه  ثبت نام نکرده است",
                };
            }
            if (Password != user.Password)
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = "رمز وارد شده اشتباه است!"
                };
            }
            #endregion
            List<string> roles = new List<string>();
            foreach (var item in user.UserInRoles)
            {
                roles.Add(item.Role.Name);
            }
            return new ResultDto<ResultUserloginDto>()
            {
                Data = new ResultUserloginDto()
                {
                    Name = user.FullName,
                    UserId = user.Id,
                    Roles = roles
                },
                IsSuccess = true,
                Message = "ورود به سایت با موفقیت انجام شد"
            };
        }
    }
}
