using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.Data;


namespace InventoryManagementSystem.Application.Products.Commands.AddProduct
{
    public class AddProductCommandValidator:AbstractValidator<AddProductCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddProductCommandValidator(IApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;

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
