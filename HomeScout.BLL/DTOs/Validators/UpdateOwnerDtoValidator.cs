using FluentValidation;

namespace HomeScout.BLL.DTOs.Validators
{
    public class UpdateOwnerDtoValidator : AbstractValidator<UpdateOwnerDto>
    {
        public UpdateOwnerDtoValidator()
        {
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MinimumLength(3).WithMessage("Name must be at least 3 characters.");
            });

            When(x => x.Email != null, () =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress().WithMessage("Invalid email format.");
            });

            When(x => x.Phone != null, () =>
            {
                RuleFor(x => x.Phone)
                    .Matches(@"^[\d\s\+\-]+$").WithMessage("Phone number contains invalid characters.");
            });
        }
    }
}
