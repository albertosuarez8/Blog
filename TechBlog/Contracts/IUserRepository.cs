using TechBlog.Models;

namespace TechBlog.Contracts
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsers();
    }
}
