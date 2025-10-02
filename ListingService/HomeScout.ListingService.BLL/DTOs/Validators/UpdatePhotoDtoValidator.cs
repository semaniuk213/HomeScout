using FluentValidation;

namespace HomeScout.ListingService.BLL.DTOs.Validators
{
    public class UpdatePhotoDtoValidator : AbstractValidator<UpdatePhotoDto>
    {
        public UpdatePhotoDtoValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .Matches(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format.");
        }
    }
}
