using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName) : base(roleName)
        {

        }

        public ApplicationRole(string roleName, string description) : base(roleName)
        {
            Description = description;
        }
        public string? Description { get; set; }
        public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }

        public virtual ICollection<IdentityRoleClaim<Guid>> Claims { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
