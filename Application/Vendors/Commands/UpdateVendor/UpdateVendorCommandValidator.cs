using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace InventoryManagementSystem.Application.Vendors.Commands.UpdateVendor
{
    public class UpdateVendorCommandValidator : AbstractValidator<UpdateVendorCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateVendorCommandValidator( IApplicationDbContext context) 
        {
            _context = context;

            RuleFor(v => v.PhoneNumber)
               .NotEmpty()
               .Matches(@"^(98|97)\d{8}$|^0\d{8}$").WithMessage("Phone number must start with '98' or '97' and be exactly 10 digits.")
               .WithMessage("Phone number is required.");


            RuleFor(v => v.Email)
           .NotEmpty().WithMessage("Email address is required.")
           .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
               .WithMessage("Invalid email address format.")
           .When(v => !string.IsNullOrEmpty(v.Email)); // Conditionally apply the rule if email address is not empty


            //RuleFor(v => v.PhoneNumber)
            //    .NotEmpty()
            //    .WithMessage("Phone number is required.");
            //RuleFor(v => v.Address)
            //    .NotEmpty()
            //    .WithMessage("Address is required.");
            //RuleFor(v => v.Email)
            //    .NotEmpty()
            //    .WithMessage("Email is required.");
        }
    }
}
