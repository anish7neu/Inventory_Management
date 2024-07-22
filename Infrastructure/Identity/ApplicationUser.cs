using InventoryManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Address {  get; set; }
        public bool IsActive { get; set; }
        public bool IsQRScanned { get; set; }
        public UserType UserType { get; set; }
        public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set;}
        public virtual ICollection<IdentityUserClaim<Guid>> UserClaims { get; set;}
        public virtual ICollection<ApplicationRole> Roles { get; set;}  
    }
}
