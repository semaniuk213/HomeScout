using FluentValidation;

namespace HomeScout.BLL.DTOs.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserMame is required.")
            .MinimumLength(3).WithMessage("UseName must be at least 3 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == "User" || role == "Admin")
                .WithMessage("Role must be either 'User' or 'Admin'.");
        }
    }
}
