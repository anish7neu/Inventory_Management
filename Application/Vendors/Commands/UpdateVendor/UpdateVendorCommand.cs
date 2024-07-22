using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Vendors.Commands.UpdateVendor
{
    public class UpdateVendorCommand : IRequest
    {
        public Guid VendorId { get; }

        public UpdateVendorCommand(Guid vendorId)
        {
            VendorId = vendorId;
        }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand>
    {
        public IApplicationDbContext _context { get; set; }
        public UpdateVendorCommandHandler(IApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _context.Vendors
                                        .FindAsync(new object[] { request.VendorId }, cancellationToken);
            if (vendor == null)
            {
                throw new NotFoundException(nameof(Vendors), request.VendorId);
            }
            vendor.Address = request.Address;
            vendor.Email = request.Email;
            vendor.PhoneNumber = request.PhoneNumber;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
