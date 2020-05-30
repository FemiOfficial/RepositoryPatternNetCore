using System;
using System.Collections.Generic;
using FluentValidation;
using repopractise.Domain.Dtos.User;

namespace repopractise.Validators
{
    public class UserRegisterRequestValidiator : AbstractValidator<UserRegisterDto>
    {
        List<string> conditions = new List<string> {"staff", "client"};

        public UserRegisterRequestValidiator()
        {
    
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Lastname).NotEmpty();
            RuleFor(x => x.Type)
                    .Must(x => conditions.Contains(x))
                    .WithMessage("user type must be staff or client");

        }
    }

}
