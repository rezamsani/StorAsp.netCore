namespace Store.Application.Services.Users.Queries.GetUsers
{
    public class ResultGetUserDto
    {
        public List<GetUsersDTo> Users { get; set; }
        public int Rows { get; set; }
    }
}
