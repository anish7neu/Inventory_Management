using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AvailableStocks.Commands.ApproveOrderRequest.Commands
{
    public class ApproveOrderRequestCommand : IRequest<Guid>
    {
        public Guid ChangerId { get; set; }
        public ApproveOrderRequestCommand(Guid changerId)
        {
            ChangerId = changerId;
        }
    }
    public class ApproveOrderRequestCommandHandler : IRequestHandler<ApproveOrderRequestCommand, Guid>
    {
        public  IApplicationDbContext _context { get; set; }
        public ApproveOrderRequestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(ApproveOrderRequestCommand request, CancellationToken cancellationToken)
        {
            Request? orderedRequest = await _context.OrderRequests
                                                   .FirstOrDefaultAsync(r => r.Id == request.ChangerId, cancellationToken) ??
                                                   throw new NotFoundException($"Request with Id {request.ChangerId} not present.");

            bool isRequestInPendingOrNot = orderedRequest.Status == RequestStatus.Pending;

            if (!isRequestInPendingOrNot)
                throw new NotFoundException($"Request is in {orderedRequest.Status} status.");

            bool isProductAlreadyPresentInProductTable = await _context.Products
                                                                .AnyAsync(x => x.Id == orderedRequest.ProductId, cancellationToken);

            if (!isProductAlreadyPresentInProductTable)
                throw new NotFoundException($"Product with ID {orderedRequest.ProductId} not found in product table.");

            bool isProductAlreadyPresentInStockTable = await _context.AvailableStocks
                                                              .AnyAsync(x => x.ProductId == orderedRequest.ProductId, cancellationToken);

            if (!isProductAlreadyPresentInStockTable)
                throw new NotFoundException($"Product with ID {orderedRequest.ProductId} not found in stock table.");


            AvailableStock? currentAvailableStock = await _context.AvailableStocks
                                                               .Where(x => x.ProductId == orderedRequest.ProductId)
                                                               .OrderByDescending(x => x.Created)
                                                               .FirstOrDefaultAsync(cancellationToken);

            AvailableStock decreasedAvailableStock = new()
            {
                Action = ActionTypes.Dispatched,
                ChangerId = orderedRequest.Id,
                TotalQuantity = currentAvailableStock.TotalQuantity <= 0 || currentAvailableStock.TotalQuantity < orderedRequest.Quantity 
                                                                        ? 
                                                                        throw new NotFoundException($"Requested quantity ({orderedRequest.Quantity}) is not avilable in stock.") 
                                                                        : 
                                                                        currentAvailableStock.TotalQuantity - orderedRequest.Quantity,
                ProductId = orderedRequest.ProductId,
                SRId = currentAvailableStock?.Id
            };

            orderedRequest.Status = RequestStatus.Approved;

            _context.AvailableStocks.Add(decreasedAvailableStock);
            await _context.SaveChangesAsync(cancellationToken);

            return decreasedAvailableStock.Id;
        }
    }
}
