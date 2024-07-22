using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.AvailableStocks.Commands.AddToStock
{
    public class AddToStockCommand : IRequest<Guid>
    {
        public Guid ChangerId { get; set; }
        public AddToStockCommand(Guid changerId)
        {
            ChangerId = changerId;
        }
    }
    public class AddToStockCommandHandler : IRequestHandler<AddToStockCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public AddToStockCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(AddToStockCommand request, CancellationToken cancellationToken)
        {
            AdminPurchaseLog? adminPurchaseLog = await _context.AdminPurchaseLogs
                                                               .SingleOrDefaultAsync(x => x.Id == request.ChangerId, cancellationToken) ?? 
                                                             throw new NotFoundException($"Admin Purchase log with ID {request.ChangerId} not found.");
            
            bool isProductAlreadyPresentInProductTable = await _context.Products
                                                                .AnyAsync(x => x.Id == adminPurchaseLog.ProductId, cancellationToken);
           
            if (!isProductAlreadyPresentInProductTable)
                 throw new NotFoundException($"Product with ID {adminPurchaseLog.ProductId} not found in product table.");
            

            bool isProductAlreadyPresentInStockTable = await _context.AvailableStocks
                                                              .AnyAsync(x => x.ProductId == adminPurchaseLog.ProductId, cancellationToken);



            AvailableStock? currentAvailableStock = await _context.AvailableStocks
                                                               .Where(x => x.ProductId == adminPurchaseLog.ProductId)
                                                               .OrderByDescending(x => x.Created) // Order by 'Date' in descending order
                                                               .FirstOrDefaultAsync(); // Get the first (latest) result


            AvailableStock addAvailableStock = new()
            {
                Action = ActionTypes.Added,
                ChangerId = adminPurchaseLog.Id,
                TotalQuantity = currentAvailableStock == null ? adminPurchaseLog.Quantity: currentAvailableStock.TotalQuantity + adminPurchaseLog.Quantity,
                ProductId = adminPurchaseLog.ProductId,
                SRId = isProductAlreadyPresentInStockTable ? currentAvailableStock?.Id : null,
            };

            adminPurchaseLog.IsAddedToStock = true;

            _context.AvailableStocks.Add(addAvailableStock);
            await _context.SaveChangesAsync(cancellationToken);

            return addAvailableStock.Id;
        }
    }
}
