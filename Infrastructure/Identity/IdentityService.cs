using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Users.Commands.CreateRole;
using Application.Users.Commands.CreateUser;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        public IdentityService( 
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<Result> CreateRoleAsync(string role, string description)
        {
            var appRole = new ApplicationRole
            {
                Name = role,
                Description = description
            };
            IdentityResult result = await _roleManager.CreateAsync( appRole );
            return result.ToApplicationResult();
        }

        public async Task<Result> CreateUserAsync(CreateUserCommand request)
        {
            IdentityResult finalResult = new();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) 
            {
                var appUser = new ApplicationUser
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    UserType = request.UserType,    
                    IsActive = true
                };
                IdentityResult userCreatedResult = await _userManager.CreateAsync(appUser, request.Password);
                if (userCreatedResult.Succeeded) 
                {
                    finalResult = await _userManager.AddToRolesAsync(appUser, request.AssignedRoles);
                }
                else
                {
                    return finalResult.ToApplicationResult() ;
                }
                scope.Complete();
            }
            return finalResult.ToApplicationResult();
        }

        public async Task<Result> CreatRoleWithPermissionAsync(CreateRoleCommand request)
        {
            IdentityResult finalResult = new();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var appRole = new ApplicationRole
                {
                    Name = request.RoleName,
                    Description = request.Description
                };

                IdentityResult identityResult = await _roleManager.CreateAsync(appRole);

                if (identityResult.Succeeded)
                {
                    ApplicationRole role = await _roleManager.FindByNameAsync(request.RoleName);

                    foreach (var permission in request.Permissions)
                    {
                        if (Enum.IsDefined(typeof(Domain.Enums.Permission), permission.PermissionValue))
                        {
                            finalResult = await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission.PermissionValue.ToString()));
                        }
                    }
                }
                scope.Complete();
            }
            return (finalResult.ToApplicationResult());
        }

        public async Task<string> GetUserNameAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<bool> IsRolePresentAsync(string roleName)
        {
            ApplicationRole role = await _roleManager.FindByNameAsync(roleName);
            var rolePresent = (role != null) ? true : false;
            return rolePresent;
        }
        public async Task<bool> IsUserPresentAsync(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            var userPresent = (user != null) ? true : false;
            return userPresent;
        }
        public async Task<bool> IsEmailAlreadyExistAsync(string email)
        {
            ApplicationUser userId = await _userManager.FindByEmailAsync(email);
            var emailUsed = (userId != null) ? true : false;

            return emailUsed;
        }
        public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }
    }
}
