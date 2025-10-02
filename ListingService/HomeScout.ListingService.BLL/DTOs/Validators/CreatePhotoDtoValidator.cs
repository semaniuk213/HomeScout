using FluentValidation;

namespace HomeScout.ListingService.BLL.DTOs.Validators
{
    public class CreatePhotoDtoValidator : AbstractValidator<CreatePhotoDto>
    {
        public CreatePhotoDtoValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .Matches(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format.");

            RuleFor(x => x.ListingId)
                .GreaterThan(0).WithMessage("ListingId must be greater than 0.");
        }
    }
}
