using Application.Common.Exceptions;
using Application.Common.Interfaces;
using InventoryManagementSystem.Application.AvailableStocks.Queries.ViewAllAvailableStock;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AvailableStocks.Queries.ViewAvailableStockById
{
    public class ViewAvailableStockByIdQuery : IRequest<AvailableStockVm>
    {
        public Guid AvailableStockId { get; }

        public ViewAvailableStockByIdQuery (Guid id)
        {
            AvailableStockId = id;
        }
    }
    public class ViewAvailableStockByIdQueryHandler : IRequestHandler<ViewAvailableStockByIdQuery, AvailableStockVm>
    {
        private readonly IApplicationDbContext _context;
        public ViewAvailableStockByIdQueryHandler( IApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<AvailableStockVm> Handle(ViewAvailableStockByIdQuery request, CancellationToken cancellationToken)
        {
            var availableStock = await _context.AvailableStocks.FirstOrDefaultAsync(x => x.Id == request.AvailableStockId);

            if (availableStock == null)
            {
                throw new NotFoundException($"Vendor with ID {request.AvailableStockId} not found.");
            }
            return new AvailableStockVm()
            {
                Action = availableStock.Action,
                TotalQuantity = availableStock.TotalQuantity,
                ProductId = availableStock.ProductId,
                SRId = availableStock.SRId
            };
        }
    }
}
