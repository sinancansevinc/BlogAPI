using FluentValidation;
using Net7Basic.Dtos;

namespace Net7Basic.Validators
{
    public class PostCreateDtoValidator:AbstractValidator<PostCreateDto>
    {
        public PostCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.BlogId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
