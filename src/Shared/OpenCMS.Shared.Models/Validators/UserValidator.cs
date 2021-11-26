using FluentValidation;
using OpenCMS.Shared.Models.Models;

namespace OpenCMS.Shared.Validators
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
           
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Email is required")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Not valid Email");
            RuleFor(x => x.UserName)
                .NotNull()
                .WithMessage("Username is required");

            RuleFor(x => x.UserRoles)
                .NotEmpty()
                .WithMessage("User should have atleast one role");
            When(x => !string.IsNullOrEmpty(x.Password), () =>
            {
                RuleFor(x => x.Password)
                    .MinimumLength(3)
                    .WithMessage("Minimum of 3 characters");
            });
            When(x => x.Id == null, () =>
            {
                RuleFor(x => x.Password)
                    .NotNull()
                    .WithMessage("Password is required");
            });
        }
     
    }

    public class RoleValidator : BaseAbstractValidator<RoleModel>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Role)
                .NotNull()
                .WithMessage("Role is required");
        }
    }
}
