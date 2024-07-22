using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery: IRequest<List<ProductVm>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductVm>>
    {

        private readonly IApplicationDbContext _context;
        public GetAllProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProductVm>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                                 .Select(x => new ProductVm
                                 {
                                     Name = x.ProductName,
                                     Description=x.ProductDescription,
                                     ProductTypes=x.ProductCategory
                                 }).ToListAsync(cancellationToken);
        }
    }
}
