using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AvailableStocks.Queries.ViewAllAvailableStock
{
    public class AvailableStockVm
    {
        public ActionTypes Action { get; set; }
        public int TotalQuantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid ChangerId { get; set; }
        //Self-referencing navigation
        public Guid? SRId { get; set; }
    }
}
