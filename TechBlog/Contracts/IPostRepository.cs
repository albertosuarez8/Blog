using TechBlog.Models;

namespace TechBlog.Contracts
{
    public interface IPostRepository
    {
        public Task<IEnumerable<Post>> GetPostsAsync();
        public Task<Post> GetPostById(int id);
        public Task<int> DeletePostById(int id);
        public Task<Post> UpdatePostById(int id);
        public Task<int> CreatePost(Post post);


    }
}
