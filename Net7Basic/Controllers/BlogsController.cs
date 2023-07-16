using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private const string blogListCacheKey = "blogList";
        private IMemoryCache _memoryCache;
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IValidator<BlogCreateDto> _validator;

        public BlogsController(IBlogRepository blogRepository, UserManager<ApplicationUser> userManager, IMapper mapper, IValidator<BlogCreateDto> validator, IMemoryCache memoryCache)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
            _mapper = mapper;
            _validator = validator;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs(int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                if (_memoryCache.TryGetValue(blogListCacheKey, out List<Blog> blogs))
                {
                    Log.Information("Blog list found in cache.");

                }
                else
                {
                    blogs = await _blogRepository.GetAll(includeProperties: "User", pageSize: pageSize, pageNumber: pageNumber);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal);

                    _memoryCache.Set(blogListCacheKey, blogs, cacheEntryOptions);

                }

                var blogsDto = _mapper.Map<List<BlogDto>>(blogs);

                return Ok(blogsDto);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            try
            {
                var blog = await _blogRepository.Get(p => p.Id == id, includeProperties: "User");
                var blogDto = _mapper.Map<BlogDto>(blog);
                return Ok(blogDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<Blog>>> AddBlog([FromBody] BlogCreateDto blogCreateDto)
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

                throw new Exception(e.Message);

            }
        }


    }
}
