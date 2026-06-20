using Microsoft.AspNetCore.Mvc;
using wayfarer.PostsService.api.Service;
using wayfarer.PostsService.DataAccess.DataModels;

namespace wayfarer.PostsService.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            this._postService = postService;
        }

        [HttpGet(Name = "GetPostById")]
        public async Task<IActionResult> Get(long userId, int limit = 10)
        {
            var result = await this._postService.GetPostsByUserId(userId);
            result = result.ToList().Take(limit);
            return Ok(result);
        }

        [HttpPost(Name = "SavePost")]
        public IActionResult Post([FromBody] Post post)
        {
            return Ok("Hello World");
        }
    }
}
