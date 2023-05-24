using Dapper;
using TechBlog.Context;
using TechBlog.Contracts;
using TechBlog.Models;

namespace TechBlog.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;

        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var query = "SELECT * FROM [TechBlog].[dbo].[User]";
            using (var connection = _dapperContext.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }
        }
    }
}
