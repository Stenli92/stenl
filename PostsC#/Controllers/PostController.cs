using Microsoft.AspNetCore.Mvc;

namespace PostsC_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public PostController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost]
        public async Task<IActionResult> FetchAndSavePosts()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
            if (response.IsSuccessStatusCode)
            {
                var posts = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                _context.AddRange(posts);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
