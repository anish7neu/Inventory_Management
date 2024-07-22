using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Vendors.Commands.CreateVendor
{
    public class CreateVendorCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PAN { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateVendorCommandHandler(IApplicationDbContext context) 
        {  
            _context = context; 
        }
        public async Task<Guid> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var entity = new Vendor
            {
                Name = request.Name,
                Address = request.Address,
                PAN = request.PAN,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
            };
            
            _context.Vendors.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
            
        }
    }
}
