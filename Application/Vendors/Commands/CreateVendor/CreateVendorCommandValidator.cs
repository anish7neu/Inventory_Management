using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace InventoryManagementSystem.Application.Vendors.Commands.CreateVendor
{
    public class CreateVendorCommandValidator : AbstractValidator<CreateVendorCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public CreateVendorCommandValidator(IApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;

            RuleFor(v => v.PAN)
                .NotEmpty()
                .WithMessage("PAN is required.")
                .Length(10).WithMessage("PAN must be of length is 10");
            RuleFor(v => v.PhoneNumber)
                .NotEmpty()
                .Matches(@"^(98|97)\d{8}$|^0\d{8}$").WithMessage("Phone number must start with '98' or '97' and be exactly 10 digits.")
                .WithMessage("Phone number is required.");
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
            RuleFor(v => v.Address)
                .NotEmpty()
                .WithMessage("Address is required.");

            RuleFor(v => v.Email)
           .NotEmpty().WithMessage("Email address is required.")
           .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
               .WithMessage("Invalid email address format.")
           .When(v => !string.IsNullOrEmpty(v.Email)); // Conditionally apply the rule if email address is not empty

        }

    }
}
