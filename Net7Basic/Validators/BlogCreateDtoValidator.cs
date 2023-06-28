using FluentValidation;
using Net7Basic.Dtos;

namespace Net7Basic.Validators
{
    public class BlogCreateDtoValidator:AbstractValidator<BlogCreateDto>
    {
        public BlogCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
        }
    }
}
