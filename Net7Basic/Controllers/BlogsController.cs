using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Net7Basic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class BlogsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            Log.Fatal("Test logs");
            return Ok("Blogs come !");
        }
    }
}
