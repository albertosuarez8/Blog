using Microsoft.AspNetCore.Mvc;
using TechBlog.Contracts;
using TechBlog.Models;
using TechBlog.Repository;

namespace TechBlog.Controller
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await _postRepository.GetPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var post = await _postRepository.GetPostById(id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var post = await _postRepository.DeletePostById(id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("help/")]
        public async Task<IActionResult> CreatePost(Post newPost)
        {
            try
            {
                var post = await _postRepository.CreatePost(newPost);
                return Ok(post);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
