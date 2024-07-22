using Application.Common.Interfaces;
using Domain.Enums;
using FluentValidation;
using InventoryManagementSystem.Application.Products.Commands.UpdateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Requests.Command.UpdateRequest
{
    public class UpdateRequestCommandValidator: AbstractValidator<UpdateRequestCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateRequestCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.Remarks)
               .NotEmpty()
               .WithMessage("Remarks is required.")
               .MaximumLength(50);

            RuleFor(v => v.Quantity)
               .NotEmpty()
               .WithMessage("Quantity is required.")
               .GreaterThan(0)
               .WithMessage("Quantity must be greater than zero.");


        }
    }
}
