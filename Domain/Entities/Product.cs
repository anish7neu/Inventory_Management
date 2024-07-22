using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public ProductTypes ProductCategory { get; set; }
        public virtual ICollection<Request>? Requests { get; set; }
        public virtual ICollection<AdminPurchaseLog>? AdminPurchaseLogs { get; set; }
        public virtual ICollection<AvailableStock>? AvailableStocks { get; set; }


    }
}
