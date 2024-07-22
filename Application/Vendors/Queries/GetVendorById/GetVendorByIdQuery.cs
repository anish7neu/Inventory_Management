using Application.Common.Exceptions;
using Application.Common.Interfaces;
using InventoryManagementSystem.Application.Vendors.Queries.GetAllVendors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Vendors.Queries.GetVendorById
{
    public class GetVendorByIdQuery : IRequest<VendorVm>
    {
        public Guid VendorId { get; }

        public GetVendorByIdQuery(Guid vendorId)
        {
            VendorId = vendorId;
        }
    }

    public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, VendorVm>
    {
        private readonly IApplicationDbContext _context;

        public GetVendorByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VendorVm> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            var vendor = await _context.Vendors
                                       .FirstOrDefaultAsync(x => x.Id == request.VendorId, cancellationToken);

            if (vendor == null)
            {
                throw new NotFoundException($"Vendor with ID {request.VendorId} not found.");
            }

            return new VendorVm
            {
                Name = vendor.Name,
                Address = vendor.Address,
                PAN = vendor.PAN,
                PhoneNumber = vendor.PhoneNumber,
                Email = vendor.Email
            };
        }
    }

}
