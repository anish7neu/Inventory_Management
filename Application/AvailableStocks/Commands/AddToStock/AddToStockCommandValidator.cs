using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AvailableStocks.Commands.AddToStock
{
    public class AddToStockCommandValidator : AbstractValidator<AddToStockCommand>
    {
        public AddToStockCommandValidator() 
        {
            RuleFor(s => s.ChangerId).NotEmpty()
                                     .WithMessage("ChangerId is required.");
        }
    }
}
