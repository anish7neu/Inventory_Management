using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AvailableStocks.Queries.ViewAllAvailableStock
{
    public class ViewAllAvailableStockQuery : IRequest<List<AvailableStockVm>>
    {
    }

    public class ViewAllAvailableStockQueryHandler : IRequestHandler<ViewAllAvailableStockQuery, List<AvailableStockVm>>
    {
        private readonly IApplicationDbContext _context;
        public ViewAllAvailableStockQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<AvailableStockVm>> Handle(ViewAllAvailableStockQuery request, CancellationToken cancellationToken)
        {
            return await _context.AvailableStocks.Select(x => new AvailableStockVm
            {
                Action = x.Action,
                TotalQuantity = x.TotalQuantity,
                ProductId = x.ProductId,
                SRId = x.SRId,
                ChangerId = x.ChangerId
            }).ToListAsync(cancellationToken);
        }
    }
}
