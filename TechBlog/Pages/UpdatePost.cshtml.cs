using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using TechBlog.Models;
namespace TechBlog.Pages
{
    public class UpdatePostModel : PageModel
    {
        private readonly ILogger<UpdatePostModel> _logger;
        public Post UserPost { get; set; } = new Post();

        public UpdatePostModel(ILogger<UpdatePostModel> logger)
        {
            _logger = logger;
        }

        //This needs to be a unique post not ALL posts
        public async Task<IActionResult> OnGet(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7262/api/posts/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    // Process the data here
                    UserPost = JsonSerializer.Deserialize<Post>(data);
                    return Page();
                }
                else
                {
                    // Handle the error here
                    return NotFound();
                }
            }
        }
    }
}
