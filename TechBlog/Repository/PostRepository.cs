using Dapper;
using TechBlog.Context;
using TechBlog.Contracts;
using TechBlog.Models;

namespace TechBlog.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DapperContext _dapperContext;

        public PostRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            var query = "SELECT u.Username, p.Id, p.Title, p.[Description], p.Date FROM Post p JOIN UserPost up ON p.Id = up.PostId JOIN [User] u ON u.Id = up.UserId";
            using (var connection = _dapperContext.CreateConnection())
            {
                var posts = await connection.QueryAsync<Post>(query);
                return posts.ToList();
            }
        }

        public async Task<Post> GetPostById(int id)
        {
            var query = $"SELECT u.Username, p.Id, p.Title, p.[Description], p.Date FROM Post p JOIN UserPost up ON p.Id = up.PostId JOIN [User] u ON u.Id = up.UserId WHERE p.Id = {id}";
            using (var connection = _dapperContext.CreateConnection())
            {
                var post = await connection.QueryFirstAsync<Post>(query);
                return post;
            }
        }

        public async Task<int> DeletePostById(int id)
        {
            var query = $"DELETE FROM UserPost WHERE PostId = {id} DELETE FROM Post Where Id = {id}";
            using (var connection = _dapperContext.CreateConnection())
            {
                var post = await connection.ExecuteAsync(query);
                return post; 
            }
        }

        public async Task<Post> UpdatePostById(int id)
        {
            var query = $"SELECT u.Username, p.Id, p.Title, p.[Description], p.Date FROM Post p JOIN UserPost up ON p.Id = up.PostId JOIN [User] u ON u.Id = up.UserId WHERE p.Id = {id}";
            using (var connection = _dapperContext.CreateConnection())
            {
                var post = await connection.QueryFirstAsync<Post>(query);
                return post;
            }
        }

        public async Task<int> CreatePost(Post post)
        {
            string formattedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var query = $"INSERT INTO TechBlog.dbo.Post (Title, [Description], [Date]) VALUES ('{post.Title}', '{post.Description}', '{formattedDate}'); INSERT INTO TechBlog.dbo.[User] (Username, [Password]) VALUES ('{post.Username}', 'password2'); INSERT INTO TechBlog.dbo.UserPost (UserId, PostId) VALUES (IDENT_CURRENT('[User]'), IDENT_CURRENT('Post'))";
            using (var connection = _dapperContext.CreateConnection())
            {
                var newPost = await connection.ExecuteAsync(query);
                return newPost;
            }
        }
    }
}
