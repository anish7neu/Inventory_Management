using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Products.Commands.AddProduct
{
    public class AddProductCommand: IRequest<Guid>
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public ProductTypes ProductCategory { get; set; }


    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public AddProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductCategory = request.ProductCategory,

            };

            _context.Products.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;

        }
    }



}
