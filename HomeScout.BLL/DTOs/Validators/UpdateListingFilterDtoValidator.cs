using FluentValidation;

namespace HomeScout.BLL.DTOs.Validators
{
    public class UpdateListingFilterDtoValidator : AbstractValidator<UpdateListingFilterDto>
    {
        public UpdateListingFilterDtoValidator()
        {
            RuleFor(x => x.FilterId)
                .GreaterThan(0).WithMessage("FilterId must be greater than 0.");
        }
    }
}
