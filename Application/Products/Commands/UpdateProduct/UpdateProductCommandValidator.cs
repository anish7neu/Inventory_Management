using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
    { 
    private readonly IApplicationDbContext _context;
        public UpdateProductCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.ProductName)
              .NotEmpty()
              .WithMessage("Name is required.")
              .MaximumLength(30);
            RuleFor(v => v.ProductDescription)
               .NotEmpty()
               .MaximumLength(50);

        }
    }
}
