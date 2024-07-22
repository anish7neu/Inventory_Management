using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Result>
    {
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public List<Permission> Permissions { get; set; }
    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result>
    {
        private readonly IIdentityService _identityService;
        public CreateRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.CreatRoleWithPermissionAsync(request);
        }
    }
    public class Permission
    {
        public byte PermissionValue { get; set; }
    }
}
