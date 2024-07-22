using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Request : AuditableEntity
    {
        public Guid Id{ get; set; }
        public int Quantity{ get; set; }
        public string Remarks { get; set; }
        public RequestStatus Status { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
