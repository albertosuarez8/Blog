using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using TechBlog.Models;

namespace TechBlog.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ILogger<DashboardModel> _logger;
        public List<Post> Posts { get; set; } = new List<Post>();

        public DashboardModel(ILogger<DashboardModel> logger)
        {
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7262/api/posts");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    // Process the data here
                    Posts = JsonSerializer.Deserialize<List<Post>>(data);
                }
                else
                {
                    // Handle the error here
                }
            }
        }
    }
}
