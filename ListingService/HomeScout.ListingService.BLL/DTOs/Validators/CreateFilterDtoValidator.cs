using FluentValidation;

namespace HomeScout.ListingService.BLL.DTOs.Validators
{
    public class CreateFilterDtoValidator : AbstractValidator<CreateFilterDto>
    {
        public CreateFilterDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(4, 100).WithMessage("Name must be between 4 and 100 characters.");
        }
    }
}
