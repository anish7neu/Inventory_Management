using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vendor : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PAN {get; set; }
        public string PhoneNumber { get; set;}
        public string Email { get; set; }
        public virtual ICollection<AdminPurchaseLog> AdminPurchaseLogs { get; set; }

        
    }
}
