using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OpenCMS.Web.Infrastructure.Models;

namespace OpenCMS.Web.Infrastructure.Validators
{
    public class UserValidator: BaseAbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("Firstname is required");
            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("Lastname is required");
            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Password is required")
                .MinimumLength(3)
                .WithMessage("Minimum of 3 characters"); 
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Email is required")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Not valid Email");
            RuleFor(x => x.UserName)
                .NotNull()
                .WithMessage("Username is required");


        }
     
    }
}
