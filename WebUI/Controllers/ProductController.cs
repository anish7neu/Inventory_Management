using InventoryManagementSystem.Application.Products.Queries.GetProductById;
using InventoryManagementSystem.Application.Products.Commands.AddProduct;
using InventoryManagementSystem.Application.Products.Commands.UpdateProduct;
using InventoryManagementSystem.Application.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.WebUI.Controllers
{
    [Authorize]
    public class ProductController: ApiControllerBase
    {
        [HttpPost]
        [Route("addProduct")]
        public async Task<ActionResult<Guid>> Add(AddProductCommand addProductCommand)
        {
            return await Mediator.Send(addProductCommand);
        }

        [HttpGet]
        [Route("getAllProducts")]
        public async Task<ActionResult<List<ProductVm>>> GetAll()
        {
            GetAllProductsQuery query = new();
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<ActionResult<ProductVm>> GetById(Guid id)
        {
            GetProductByIdQuery query = new(id);
            return await Mediator.Send(query);
        }

        [HttpPut]
        [Route("product/{id}")]
        public async Task<ActionResult<Unit>> UpdateById(UpdateProductCommand updateProductCommand, Guid id)
        {
            if (id != updateProductCommand.ProductId)
            {
                return BadRequest();
            }

            await Mediator.Send(updateProductCommand);

            return NoContent();
        }
    }
}
