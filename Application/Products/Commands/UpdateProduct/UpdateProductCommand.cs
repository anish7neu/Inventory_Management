using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using InventoryManagementSystem.Application.Vendors.Commands.UpdateVendor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand:IRequest
    {
        public Guid ProductId { get; }

        public UpdateProductCommand(Guid productId)
        {
            ProductId = productId;
        }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public ProductTypes ProductCategory{ get; set; }

    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        public IApplicationDbContext _context { get; set; }
        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                                        .FindAsync(new object[] { request.ProductId }, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException(nameof(Products), request.ProductId);
            }
            product.ProductName = request.ProductName;
            product.ProductDescription = request.ProductDescription;
            product.ProductCategory = request.ProductCategory;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
