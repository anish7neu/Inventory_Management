using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.AdminPurchaseLogs.Commands.AddAPL
{
    public class AddAPLCommand : IRequest<Guid>
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Guid VendorId { get; set; }
        public Guid ProductId { get; set; }
        public bool IsAddedToStock { get; set; }
    }
    public class AddAPLCommandHandler : IRequestHandler<AddAPLCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public AddAPLCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(AddAPLCommand request, CancellationToken cancellationToken)
        {
            var isProductPresent = _context.Products
                                         .Any(x => x.Id == request.ProductId);
            if (!isProductPresent) 
            {
                throw new NotFoundException($"Product with id '{request.ProductId}' not present.");
            }
            var entity = new AdminPurchaseLog
            {
                Quantity = request.Quantity,
                Price = request.Price,
                VendorId = request.VendorId,
                ProductId = request.ProductId,
                IsAddedToStock = false
            };
            _context.AdminPurchaseLogs.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
