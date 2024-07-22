using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AdminPurchaseLogs.Queries.GetAllAPL
{
    public class AdminPurchaseLogVm
    {
         public int Quantity{ get; set; }
        public double Price { get; set; }
        public string VendorName { get; set; }
        public string ProductName { get; set; }
        public bool IsAddedToStock { get; set; }
    }
}
