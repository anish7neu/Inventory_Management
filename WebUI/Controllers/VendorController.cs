using Application.Common.Interfaces;
using Domain.Entities;
using InventoryManagementSystem.Application.Vendors.Commands.CreateVendor;
using InventoryManagementSystem.Application.Vendors.Commands.UpdateVendor;
using InventoryManagementSystem.Application.Vendors.Queries.GetAllVendors;
using InventoryManagementSystem.Application.Vendors.Queries.GetVendorById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventoryManagementSystem.WebUI.Controllers
{

    [Authorize]
    public class VendorController : ApiControllerBase
    {
        [HttpPost]
        [Route("addVendor")]
        public async Task<ActionResult<Guid>> Create(CreateVendorCommand createVendorCommand)
        {
            return await Mediator.Send(createVendorCommand);
        }

        [HttpGet]
        [Route("getAllVendors")]
        public async Task<ActionResult<List<VendorVm>>> GetAll()
        {
            GetAllVendorsQuery query = new();
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("vendor/{id}")]
        public async Task<ActionResult<VendorVm>> GetById(Guid id)
        {
            GetVendorByIdQuery query = new(id);
            return await Mediator.Send(query);
        }

        [HttpPut]
        [Route("vendor/{id}")]
        public async Task<ActionResult<Unit>> UpdateById(UpdateVendorCommand updateVendorCommand, Guid id)
        {
            if (id != updateVendorCommand.VendorId)
            {
                return BadRequest();
            }

            await Mediator.Send(updateVendorCommand);

            return NoContent();
        }

    }
}
