using Application.Common.Interfaces;
using InventoryManagementSystem.Application.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Requests.Queries.GetAllRequests
{
    public class GetAllRequestsQuery : IRequest<List<RequestVm>>
    {
    }

    public class GetAllRequestsQueryHandler : IRequestHandler<GetAllRequestsQuery, List<RequestVm>>
    {

        private readonly IApplicationDbContext _context;
        public GetAllRequestsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<RequestVm>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
        {
            
            return await _context.OrderRequests
                                 .Select(x => new RequestVm
                                 {
                                     Quantity = x.Quantity,
                                     Remarks = x.Remarks,
                                     Status = x.Status,
                                     UserId = x.UserId,
                                     UserName = x.UserName,
                                 }).ToListAsync(cancellationToken);
        }
    }

}
