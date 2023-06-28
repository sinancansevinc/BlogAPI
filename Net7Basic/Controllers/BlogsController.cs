using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<BlogCreateDto> _validator;

        public BlogsController(IBlogRepository blogRepository, UserManager<ApplicationUser> userManager, IMapper mapper, IValidator<BlogCreateDto> validator)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            try
            {
                var blog = await _blogRepository.Get(p=>p.Id==id, includeProperties: "User");
                var blogDto = _mapper.Map<BlogDto>(blog);
                return Ok(blogDto);
            }
            catch (Exception e)
            {

                Log.Fatal(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<Blog>>> AddBlog([FromBody]BlogCreateDto blogCreateDto)
        {
            try
            {
                var validationResult = _validator.Validate(blogCreateDto);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.First());
                }
                
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                Blog blog = _mapper.Map<Blog>(blogCreateDto);
                blog.UserId = user.Id;
                blog.CreatedAt = DateTime.Now;

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
