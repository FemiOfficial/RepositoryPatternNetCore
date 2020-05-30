using FluentValidation;
using repopractise.Domain.Dtos.User;

namespace repopractise.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginRequestValidator() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}