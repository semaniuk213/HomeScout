using FluentValidation;

namespace HomeScout.ListingService.BLL.DTOs.Validators
{
    public class UpdateListingDtoValidator : AbstractValidator<UpdateListingDto>
    {
        public UpdateListingDtoValidator()
        {
            When(x => x.Title != null, () =>
            {
                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Title is required.")
                    .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");
            });

            When(x => x.Description != null, () =>
            {
                RuleFor(x => x.Description)
                    .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
            });

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("Address is required.")
                    .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");
            });

            When(x => x.City != null, () =>
            {
                RuleFor(x => x.City)
                    .NotEmpty().WithMessage("City is required.")
                    .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");
            });

            When(x => x.Price.HasValue, () =>
            {
                RuleFor(x => x.Price!.Value)
                    .GreaterThan(0).WithMessage("Price must be greater than 0.");
            });

            When(x => x.Area.HasValue, () =>
            {
                RuleFor(x => x.Area!.Value)
                    .GreaterThan(0).WithMessage("Area must be greater than 0.");
            });

            When(x => x.Type != null, () =>
            {
                RuleFor(x => x.Type)
                    .Must(t => t == "Rent" || t == "Sale")
                    .WithMessage("Type must be either 'Rent' or 'Sale'.");
            });

            When(x => x.OwnerId.HasValue, () =>
            {
                RuleFor(x => x.OwnerId!.Value)
                    .NotEmpty().WithMessage("OwnerId is required.");
            });
        }
    }
}