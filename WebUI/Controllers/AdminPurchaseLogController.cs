using Domain.Entities;
using InventoryManagementSystem.Application.AdminPurchaseLogs.Queries.GetSpecificAPL;
using InventoryManagementSystem.Application.AdminPurchaseLogs.Queries.GetAllAPL;
using InventoryManagementSystem.Application.Vendors.Queries.GetAllVendors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Application.AdminPurchaseLogs.Commands.AddAPL;

namespace InventoryManagementSystem.WebUI.Controllers
{
    [Authorize]
    public class AdminPurchaseLogController : ApiControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("allAdminPurchaseLog")]
        public async Task<List<AdminPurchaseLogVm>> GetAll()
        {
            GetAllAPLQuery query = new();
            return await Mediator.Send(query);
        }
        [Authorize]
        [HttpGet]
        [Route("adminPurchaseLog/{id}")]
        public async Task<AdminPurchaseLogVm> GetSpecific(Guid id)
        {
            APLByIdQuery query = new(id);
            return await Mediator.Send(query);
        }
        [HttpPost]
        [Route("addAdminPurchaseLog")]
        public async Task<ActionResult<Guid>> Add(AddAPLCommand addAPLCommand)
        {
            return await Mediator.Send(addAPLCommand);
        }
    }
}
