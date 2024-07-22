using Application.Common.Models;
using Application.Users.Commands.CreateRole;
using Application.Users.Commands.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result> CreateRoleAsync(string role, string description);
        Task<Result> CreateUserAsync(CreateUserCommand request);
        Task<string> GetUserNameAsync(Guid userId);
        Task<bool> IsInRoleAsync(Guid userId, string role);
        Task<Result> CreatRoleWithPermissionAsync(CreateRoleCommand request);
        Task<bool> IsRolePresentAsync(string roleName);
        Task<bool> IsUserPresentAsync(string userName);
        Task<bool> IsEmailAlreadyExistAsync(string email);
        Task<bool> AuthorizeAsync(Guid userId, string policyName);
    }
}
