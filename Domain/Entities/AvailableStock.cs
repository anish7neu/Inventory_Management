using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class AvailableStock : AuditableEntity
    {
        public Guid Id { get; set; }
        public ActionTypes Action { get; set; }
        public int TotalQuantity{ get; set; }
        public Product Product { get; set; }
        public Guid ProductId {  get; set; }
        public Guid ChangerId { get; set; }
        //Self-referencing navigation
        public AvailableStock UpdatedStock { get; set; }
        public Guid? SRId { get; set; }  

    }
}
