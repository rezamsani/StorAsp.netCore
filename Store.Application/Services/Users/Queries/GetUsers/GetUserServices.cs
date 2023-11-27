using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUserServices : IGetUserService
    {
        private readonly IDataBaseContext _context;
        public GetUserServices(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultGetUserDto Execute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                
                users = users.Where(p => p.FullName.Contains(request.SearchKey) || p.Email.Contains(request.SearchKey));
            }
            int rowCount = 0;
            var userList = users.ToPaged(request.Page, 20, out rowCount).Select(p => new GetUsersDTo
            {
                FullName = p.FullName,
                Email = p.Email,
                Id = p.Id,
                IsActive = p.IsActive
            }).ToList();
            return new ResultGetUserDto
            {
                Rows = rowCount,
                Users = userList
            };
        }
    }
}
