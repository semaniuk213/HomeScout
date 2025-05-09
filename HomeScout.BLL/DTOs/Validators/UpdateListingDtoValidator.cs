using FluentValidation;
using HomeScout.DAL.Entities;

namespace HomeScout.BLL.DTOs.Validators
{
    public class UpdateListingDtoValidator : AbstractValidator<UpdateListingDto>
    {
        public UpdateListingDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Area)
                .GreaterThan(0).WithMessage("Area must be greater than 0.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .Must(t => t == "Rent" || t == "Sale")
                .WithMessage("Type must be either 'Rent' or 'Sale'.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
