using InventoryManagementSystem.Application.AdminPurchaseLogs.Commands.AddAPL;
using InventoryManagementSystem.Application.AvailableStocks.Commands.AddToStock;
using InventoryManagementSystem.Application.AvailableStocks.Commands.ApproveOrderRequest.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.WebUI.Controllers
{

    [Authorize]
    public class StockManagerController : ApiControllerBase
    {
        [HttpPost]
        [Route("addToStock")]
        public async Task<ActionResult<Guid>> AddToStock(AddToStockCommand addToStockCommand)
        {
            return await Mediator.Send(addToStockCommand);
        }

        [HttpPost]
        [Route("approveRequest")]
        public async Task<ActionResult<Guid>> ApproveOrderRequest(ApproveOrderRequestCommand approveOrderRequestCommand)
        {
            return await Mediator.Send(approveOrderRequestCommand);
        }
    }
}
