using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        private readonly IIdentityService _identityService;
        public CreateRoleCommandValidator( IIdentityService identityService ) 
        {
            _identityService = identityService;

            RuleFor(x=>x.RoleName).Cascade(CascadeMode.Stop)
                                  .NotEmpty().WithMessage("Username is required.")
                                  .MustAsync(BeUniqueRoleName)
                                  .WithMessage(errorMessage: "This RoleName is already used")
                                  .WithErrorCode("UniqueRoleName");
        }

        private async Task<bool> BeUniqueRoleName(string userRole, CancellationToken cancellationToken)
        {
            bool rolePresent = await _identityService.IsRolePresentAsync(userRole);

            if (rolePresent)
            {
                // validation fails
                return false;
            }
            else
            {
                // validation passess
                return true;
            }
        }
    }
}
