using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IIdentityService _identityService;
        public CreateUserCommandValidator(IIdentityService identityService)
        {
            _identityService = identityService;

            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
                                    .NotEmpty().WithMessage("UserName is required")
                                    .MustAsync(BeUniqueUserName)
                                    .WithMessage("This Username is already used")
                                    .WithErrorCode("UniqueUserName");

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                                 .NotEmpty().WithMessage("Email is required")
                                 .MustAsync(BeUniqueUserEmail)
                                 .WithMessage("This Email is already used")
                                 .WithErrorCode("UniqueEmail");
        }



        private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
        {
            bool userPresent = await _identityService.IsUserPresentAsync(userName);

            if (userPresent)
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

        private async Task<bool> BeUniqueUserEmail(string email, CancellationToken cancellationToken)
        {
            bool emailPresent = await _identityService.IsEmailAlreadyExistAsync(email);

            if (emailPresent)
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
