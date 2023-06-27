using AutoMapper;
using Net7Basic.Dtos;
using Net7Basic.Models;

namespace Net7Basic.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<Blog, BlogCreateDto>().ReverseMap();
            CreateMap<Post, PostCreateDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
        }

    }
}
