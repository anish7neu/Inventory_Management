using Application.Common.Exceptions;
using Application.Common.Interfaces;
using InventoryManagementSystem.Application.AdminPurchaseLogs.Queries.GetAllAPL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AdminPurchaseLogs.Queries.GetSpecificAPL
{
    public class APLByIdQuery : IRequest<AdminPurchaseLogVm>
    {
        public Guid APLId { get; }
        public APLByIdQuery(Guid aPLId) 
        {
            APLId = aPLId;
        }
    }
    public class APLByIdHandler : IRequestHandler<APLByIdQuery, AdminPurchaseLogVm>
    {
        public IApplicationDbContext _context;
        public APLByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AdminPurchaseLogVm> Handle(APLByIdQuery request, CancellationToken cancellationToken)
        {
            var adminPurchaseLog = await _context.AdminPurchaseLogs
                                                    .Include(a => a.Product)
                                                    .Include(a => a.Vendor)
                                                    .FirstOrDefaultAsync(a => a.Id == request.APLId, cancellationToken);
            if (adminPurchaseLog == null) 
            {
                throw new NotFoundException($"AdmkinPurchaseLog with ID {request.APLId} not found.");
            }
            return new AdminPurchaseLogVm
            {
                Quantity = adminPurchaseLog.Quantity,
                Price = adminPurchaseLog.Price,
                ProductName = adminPurchaseLog.Product.ProductName,
                VendorName  = adminPurchaseLog.Vendor.Name,
                IsAddedToStock = adminPurchaseLog.IsAddedToStock
            };

        }
    }
}
