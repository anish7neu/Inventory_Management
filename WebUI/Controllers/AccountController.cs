

using Application.Common.Models;
using Application.Users.Commands.CreateRole;
using Application.Users.Commands.CreateUser;
using Infrastructure.Identity;
using InventoryManagementSystem.Application.Users.Query.AuthenticationSetup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventoryManagementSystem.WebUI.Controllers
{
    [Authorize]
    public class AccountController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator; 

        private const string Key = "dfg756!@@)(*";
        public AccountController(
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationRole> roleManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IConfiguration configuration,
                                 IMediator mediator) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpPost("/api/authenticate")]
        public async Task<ActionResult<AuthenticationSetupViewModel>> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var identityUser = await _userManager.FindByNameAsync(authenticationRequest.UserName);

            if (identityUser == null || identityUser.IsActive == false)
            {
                if (identityUser == null)
                {
                    return Unauthorized("Invalid User");
                }
                if (!identityUser.IsActive)
                {
                    return Unauthorized("Your account has been deactivated, Please contact your Admin");
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(identityUser, authenticationRequest.Password, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    identityUser.IsActive = false;
                    return Unauthorized("Account has been lockout, Please contact your Admin");
                }
                identityUser.AccessFailedCount++;
                return Unauthorized("Invalid Credentials");
            }

            var userSecretKey = string.Concat(identityUser.Id.ToString().Replace("-", "").AsSpan(0, 10), Key);
            identityUser.AccessFailedCount = 0;
            AuthenticationSetupViewModel authenticateSetupView = new AuthenticationSetupViewModel
            {
                IsEmailConfirmed = identityUser.EmailConfirmed,
                Email = identityUser.Email,
                IsValidUser = true
            };

            return Ok(authenticateSetupView);
        }
        [AllowAnonymous]
        [HttpPost("/api/users/{userId}/refreshToken")]
        public async Task<ActionResult<TokenResult>> RefreshToken(string userId)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);

            List<Claim> userClaims = await ConstructUserClaimsAsync(identityUser);

            JwtSecurityToken token = GenerateJwtToken(userClaims);

            var tokenResult = new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };

            //_logger.LogInformation("Refresh Token Generated.");

            return Ok(tokenResult);
        }

        [HttpPost("/api/createUsers")]
        public async Task<ActionResult<Result>> CreateUser(CreateUserCommand createUserCommand)
        {
            Result result = await Mediator.Send(createUserCommand);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost("/api/createRole")]
        public async Task<ActionResult<Result>> CreatRole(CreateRoleCommand createRoleCommand)
        {
            Result result = await Mediator.Send(createRoleCommand);

            if(!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        private JwtSecurityToken GenerateJwtToken(List<Claim> userClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Tokens:JwtIssuer"],
                audience: _configuration["Tokens:JwtAudience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Tokens:JwtValidMinutes"])),
                signingCredentials: creds
                );
            return token;
        }
        public class AuthenticationRequest
        {
            [Required]
            public string? UserName { get; set; }
            [Required]
            public string? Password { get; set; }
        }
        public class TokenResult
        {
            public string? Token { get; set; }
            public DateTime Expiration { get; set; }
        }
        private async Task<List<Claim>> ConstructUserClaimsAsync(ApplicationUser identityUser)
        {
            var roles = await _userManager.GetRolesAsync(identityUser);

            List<Claim> roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

            List<Claim> userClaims = (await _userManager.GetClaimsAsync(identityUser)).ToList();

            userClaims = userClaims.Union(roleClaims).ToList();

            foreach (var role in roles)
            {
                ApplicationRole appRole = await _roleManager.FindByNameAsync(role);
                IList<Claim> permissionsOfRoles = await _roleManager.GetClaimsAsync(appRole);
                userClaims = userClaims.Union(permissionsOfRoles).ToList();
            }


            userClaims = new List<Claim>(userClaims)
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, identityUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
            };

            return userClaims;
        }
    }
}
