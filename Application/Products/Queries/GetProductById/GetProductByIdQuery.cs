using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using InventoryManagementSystem.Application.Products.Queries.GetAllProducts;
using InventoryManagementSystem.Application.Vendors.Queries.GetAllVendors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery:  IRequest<ProductVm>
    {

        public Guid ProductId { get; }

        public GetProductByIdQuery(Guid productId)
        {
            ProductId = productId;
        }
    }


    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVm>
    {
        private readonly IApplicationDbContext _context;

        public GetProductByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                                       .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                throw new NotFoundException($"Product with ID {request.ProductId} not found.");
            }

            return new ProductVm
            {
                Name = product.ProductName,
                Description = product.ProductDescription,
                ProductTypes = product.ProductCategory
            };
        }

       
    }

}
