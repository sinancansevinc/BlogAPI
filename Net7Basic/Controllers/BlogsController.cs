using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net7Basic.Dtos;
using Net7Basic.Models;
using Net7Basic.Repositories.Abstract;
using Serilog;
using System.Security.Claims;

namespace Net7Basic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public BlogsController(IBlogRepository blogRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles ="admin,customer,human")]
        public async Task<IActionResult> GetBlogs()
        {
            try
            {

                var blogs = await _blogRepository.GetAll(includeProperties: "User");
                var blogsDto = _mapper.Map<List<BlogDto>>(blogs);


                return Ok(blogsDto);
            }
            catch (Exception e)
            {

                Log.Fatal(e.Message);
                return BadRequest(e.Message);
               
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<Blog>>> AddBlog([FromBody]BlogCreateDto blogCreateDto)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            blogCreateDto.CreatedAt = DateTime.Now;
            blogCreateDto.UserId = user.Id;

            try
            {

                Blog blog = _mapper.Map<Blog>(blogCreateDto);

                await _blogRepository.Create(blog);

                return Ok(blog);
            }
            catch (Exception e)
            {

                Log.Fatal(e.Message);
                return BadRequest();
            }
        }


    }
}
