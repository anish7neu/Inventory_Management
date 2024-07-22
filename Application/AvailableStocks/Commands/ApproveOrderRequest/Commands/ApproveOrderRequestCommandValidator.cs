using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AvailableStocks.Commands.ApproveOrderRequest.Commands
{
    public class ApproveOrderRequestCommandValidator : AbstractValidator<ApproveOrderRequestCommand>
    {
        public ApproveOrderRequestCommandValidator()
        {
            RuleFor(a => a.ChangerId).NotEmpty()
                                     .WithMessage("Changer Id can't be empty.");
        }
    }
}
