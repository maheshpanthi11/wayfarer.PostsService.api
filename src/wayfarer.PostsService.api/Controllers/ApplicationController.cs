using Microsoft.AspNetCore.Mvc;
using wayfarer.PostsService.api.Model;
using wayfarer.PostsService.api.Service;

namespace wayfarer.PostsService.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IPostService _postService;

        public ApplicationController(IPostService postService)
        {
            this._postService = postService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await this._postService.GetPosts();
            result = result.ToList();
            return Ok(result);
        }
    }
}
