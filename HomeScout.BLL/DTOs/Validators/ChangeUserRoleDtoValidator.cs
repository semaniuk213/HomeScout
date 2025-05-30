using System.Data;
using FluentValidation;

namespace HomeScout.BLL.DTOs.Validators
{
    public class ChangeUserRoleDtoValidator : AbstractValidator<ChangeUserRoleDto>
    {
        public ChangeUserRoleDtoValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(t => t == "User" || t == "Admin")
                .WithMessage("Role must be either 'User' or 'Admin'.");
        }
    }
}
