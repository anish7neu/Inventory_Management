using InventoryManagementSystem.Application.Products.Commands.AddProduct;
using InventoryManagementSystem.Application.Products.Commands.UpdateProduct;
using InventoryManagementSystem.Application.Products.Queries.GetAllProducts;
using InventoryManagementSystem.Application.Products.Queries.GetProductById;
using InventoryManagementSystem.Application.Requests.Command.AddRequest;
using InventoryManagementSystem.Application.Requests.Command.DeclineRequest;
using InventoryManagementSystem.Application.Requests.Command.UpdateRequest;
using InventoryManagementSystem.Application.Requests.Queries.GetAllRequests;
using InventoryManagementSystem.Application.Requests.Queries.GetRequestById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestVm = InventoryManagementSystem.Application.Requests.Queries.GetAllRequests.RequestVm;

namespace InventoryManagementSystem.WebUI.Controllers
{
    [Authorize]
    public class RequestController: ApiControllerBase
    {
        [HttpPost]
        [Route("addRequest")]
        public async Task<ActionResult<Guid>> Add(AddRequestCommand addRequestCommand)
        {
            return await Mediator.Send(addRequestCommand);
        }
        [Authorize]
        [HttpGet]
        [Route("getAllRequests")]
        public async Task<ActionResult<List<RequestVm>>> GetAll()
        {
            GetAllRequestsQuery query = new();
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("request/{id}")]
        public async Task<ActionResult<RequestVm>> GetById(Guid id)
        {
            GetRequestByIdQuery query = new(id);
            return await Mediator.Send(query);
        }

        [HttpPut]
        [Route("editRequest/{id}")]
        public async Task<ActionResult<Unit>> UpdateById(UpdateRequestCommand updateRequestCommand, Guid id)
        {
            if (id != updateRequestCommand.RequestId)
            {
                return BadRequest();
            }

            await Mediator.Send(updateRequestCommand);

            return NoContent();
        }
        [HttpPut]
        [Route("declineRequest/{id}")]
        public async Task<ActionResult<Unit>> DeclineRequest(DeclineRequestCommand declineRequestCommand, Guid id)
        {
            if (id != declineRequestCommand.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(declineRequestCommand);
            return NoContent();
        }
    }
}
