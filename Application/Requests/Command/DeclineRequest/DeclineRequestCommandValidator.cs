using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Requests.Command.DeclineRequest
{
    public class DeclineRequestCommandValidator : AbstractValidator<DeclineRequestCommand>
    {
        public DeclineRequestCommandValidator() 
        {
            RuleFor(r => r.Id).NotEmpty()
                              .WithMessage("RequestId is required.");
        }
    }
}
