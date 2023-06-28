using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net7Basic.Dtos;
using Net7Basic.Models;
using Net7Basic.Repositories.Abstract;
using Serilog;
using System.Data;

namespace Net7Basic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IValidator<PostCreateDto> _validator;
        public PostsController(IPostRepository postRepository, UserManager<ApplicationUser> userManager, IMapper mapper, IValidator<PostCreateDto> validator)
        {
            _postRepository = postRepository;
            _userManager = userManager;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            try
            {

                var posts = await _postRepository.GetAll(includeProperties: "User,Blog");
                var postsDto = _mapper.Map<List<PostDto>>(posts);


                return Ok(postsDto);
            }
            catch (Exception e)
            {

                Log.Fatal(e.Message);
                return BadRequest(e.Message);

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var post = await _postRepository.Get(p => p.Id == id, includeProperties: "User,Blog");
                var postDto = _mapper.Map<PostDto>(post);
                return Ok(postDto);
            }
            catch (Exception e)
            {

                Log.Fatal(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin,human")]
        public async Task<ActionResult<ResponseDto<Post>>> AddPost([FromBody] PostCreateDto postCreateDto)
        {
            try
            {
                var validationResult = _validator.Validate(postCreateDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                Post post = _mapper.Map<Post>(postCreateDto);
                post.UserId = user.Id;
                post.CreatedAt = DateTime.Now;

                await _postRepository.Create(post);

                return Ok(post);
            }
            catch (Exception e)
            {

                Log.Fatal(e.Message);
                return BadRequest();
            }
        }

    }
}
