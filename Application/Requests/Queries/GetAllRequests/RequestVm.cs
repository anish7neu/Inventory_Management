using Domain.Enums;

namespace InventoryManagementSystem.Application.Requests.Queries.GetAllRequests
{
    public class RequestVm
    {
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public RequestStatus Status { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set;}
    }
}