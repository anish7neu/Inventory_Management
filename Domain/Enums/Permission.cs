using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum Permission : byte
    {
        AdminUser = 1,
        AdminView = 2,
        UserView = 3
    }
}
