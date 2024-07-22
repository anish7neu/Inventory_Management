using InventoryManagementSystem.Application.AvailableStocks.Queries.ViewAllAvailableStock;
using InventoryManagementSystem.Application.AvailableStocks.Queries.ViewAvailableStockById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.WebUI.Controllers
{
    [Authorize]
    public class AvailableStockController: ApiControllerBase
    {
        [HttpGet]
        [Route("allAvailableStocks")]
        public async Task<ActionResult<List<AvailableStockVm>>> GetAll()
        {
            ViewAllAvailableStockQuery query = new();
            return await Mediator.Send(query);
        }
        [HttpGet]
        [Route("availableStock/{id}")]
        public async Task<ActionResult<AvailableStockVm>> GetById(Guid id)
        {
            ViewAvailableStockByIdQuery query = new(id);
            return await Mediator.Send(query);
        }
    }
}
