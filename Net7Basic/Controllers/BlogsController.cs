using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net7Basic.Repositories.Abstract;
using Serilog;

namespace Net7Basic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogsController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            try
            {
                var blogs = await _blogRepository.GetAll(includeProperties:"Posts");
                return Ok(blogs);
            }
            catch (Exception e)
            {

                Log.Fatal(e.Message);
                return BadRequest(e.Message);
               
            }
        }


    }
}
