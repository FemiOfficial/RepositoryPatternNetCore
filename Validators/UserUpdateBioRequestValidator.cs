using System.Collections.Generic;
using FluentValidation;
using repopractise.Domain.Dtos.User;

namespace repopractise.Validators
{
    public class UserUpdateBioRequestValidator : AbstractValidator<UserUpdateBioRequest>
    {
        public UserUpdateBioRequestValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.ProfileImage).NotNull();
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Lastname).NotEmpty();

        }
    }
}