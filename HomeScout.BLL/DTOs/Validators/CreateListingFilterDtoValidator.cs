using FluentValidation;

namespace HomeScout.BLL.DTOs.Validators
{
    public class CreateListingFilterDtoValidator : AbstractValidator<CreateListingFilterDto>
    {
        public CreateListingFilterDtoValidator()
        {
            RuleFor(x => x.ListingId)
                .GreaterThan(0).WithMessage("ListingId must be greater than 0.");

            RuleFor(x => x.FilterId)
                .GreaterThan(0).WithMessage("FilterId must be greater than 0.");
        }
    }
}
