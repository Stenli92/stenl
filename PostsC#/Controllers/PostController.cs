using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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
            string url = "https://jsonplaceholder.typicode.com/posts";

            HttpClient client = new HttpClient();

            string response = await client.GetStringAsync(url);


            if (!response.IsNullOrEmpty())
            {
                var posts =  JsonConvert.DeserializeObject<Post[]>(response);
                _context.AddRange(posts);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
