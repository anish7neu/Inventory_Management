using Application.Common.Interfaces;
using Domain.Enums;
using FluentValidation;
using InventoryManagementSystem.Application.Products.Commands.AddProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Requests.Command.AddRequest
{
    public class AddRequestCommandValidator : AbstractValidator<AddRequestCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddRequestCommandValidator(IApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;

           RuleFor(v => v.Remarks)
               .NotEmpty()
               .MaximumLength(50);
            RuleFor(v => v.Status)
                .NotEmpty()
                .Equals(RequestStatus.Pending);
            RuleFor(v => v.Quantity)
                .NotEmpty()
                .GreaterThan(0);

        }
    }
}
