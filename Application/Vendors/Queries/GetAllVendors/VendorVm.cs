﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Vendors.Queries.GetAllVendors
{
    public class VendorVm
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PAN { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
