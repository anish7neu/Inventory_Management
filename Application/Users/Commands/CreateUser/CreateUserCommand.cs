using Application.Common.Interfaces;
using Application.Common.Models;
using InventoryManagementSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string UserName { get; set;}
        public string Email { get; set;}
        public string Password { get; set;}
        public string ConfirmPassword { get; set;}
        public string PhoneNumber { get; set;}
        public string Address { get; set;}
        public UserType UserType { get; set;}
        public List<string> AssignedRoles { get; set;}
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IIdentityService _identityService;
        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }   
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.CreateUserAsync(request);
        }

    }
}
