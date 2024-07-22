using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unipluss.Sign.ExternalContract.Entities;

namespace Domain.Entities
{
    public class AdminPurchaseLog :AuditableEntity
    {
        public Guid Id { get; set; }
        public int Quantity{ get; set; }
        public double Price { get; set; }
        public Guid VendorId{ get; set; }
        public Vendor Vendor { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public bool IsAddedToStock { get; set; }
    }
}
