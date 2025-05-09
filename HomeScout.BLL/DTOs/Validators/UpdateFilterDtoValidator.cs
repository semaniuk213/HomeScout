using FluentValidation;

namespace HomeScout.BLL.DTOs.Validators
{
    public class UpdateFilterDtoValidator : AbstractValidator<UpdateFilterDto>
    {
        public UpdateFilterDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(4, 100).WithMessage("Name must be between 4 and 100 characters.");
        }
    }
}
