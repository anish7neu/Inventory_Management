using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Vendors.Queries.GetAllVendors
{
    public class GetAllVendorsQuery : IRequest<List<VendorVm>>
    {
    }
    public class GetAllVendorsQueryHandler : IRequestHandler<GetAllVendorsQuery, List<VendorVm>> 
    {
        private readonly IApplicationDbContext _context;
        public GetAllVendorsQueryHandler(IApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<List<VendorVm>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Vendors
                                 .Select(x => new VendorVm
                                 {
                                     Name = x.Name,
                                     Address = x.Address,
                                     PAN = x.PAN,
                                     PhoneNumber = x.PhoneNumber,
                                     Email = x.Email,
                                 }).ToListAsync(cancellationToken);
        }
    }
}
