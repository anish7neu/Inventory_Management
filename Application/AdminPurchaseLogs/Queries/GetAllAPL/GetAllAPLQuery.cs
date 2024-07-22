using Application.Common.Interfaces;
using InventoryManagementSystem.Application.Vendors.Queries.GetAllVendors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AdminPurchaseLogs.Queries.GetAllAPL
{
    public class GetAllAPLQuery : IRequest<List<AdminPurchaseLogVm>>
    {

    }
    public class GetAllAPLQueryHandler : IRequestHandler<GetAllAPLQuery, List<AdminPurchaseLogVm>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllAPLQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<AdminPurchaseLogVm>> Handle(GetAllAPLQuery request, CancellationToken cancellationToken)
        {
            return await _context.AdminPurchaseLogs
                                 .Select(x => new AdminPurchaseLogVm
                                 {
                                     Quantity = x.Quantity,
                                     Price = x.Price,
                                     VendorName = x.Vendor.Name,
                                     ProductName = x.Product.ProductName,
                                     IsAddedToStock = x.IsAddedToStock,
                                 }).ToListAsync(cancellationToken);
        }
    }
}
