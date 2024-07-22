using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AdminPurchaseLogs.Commands.AddAPL
{
    public class AddAPLCommandValidator : AbstractValidator<AddAPLCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddAPLCommandValidator(IApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;

            RuleFor(a => a.ProductId)
                .NotEmpty()
                .WithMessage("ProductId can not be empty.");
            RuleFor(a=>a.VendorId)
                .NotEmpty()
                .WithMessage("VendorId can not be empty.");
            RuleFor(a => a.Price)
                .NotEmpty()
                .WithMessage("Price can not be empty.");
            RuleFor(a => a.Quantity)
                .NotEmpty()
                .WithMessage("Quantity can not be empty.");
        }
    }
}
