using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Users.Query.AuthenticationSetup
{
    public class AuthenticationSetupViewModel
    {
        public string? Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsValidUser { get; set; }
    }
}
