using FluentValidation;

namespace HomeScout.ListingService.BLL.DTOs.Validators
{
    public class CreateOwnerDtoValidator : AbstractValidator<CreateOwnerDto>
    {
        public CreateOwnerDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Phone)
                .Matches(@"^[\d\s\+\-]+$").When(x => !string.IsNullOrWhiteSpace(x.Phone))
                .WithMessage("Phone number contains invalid characters.");
        }
    }
}