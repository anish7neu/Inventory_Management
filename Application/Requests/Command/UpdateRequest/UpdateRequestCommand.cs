using Application.Common.Exceptions;
using Application.Common.Interfaces;
using InventoryManagementSystem.Application.Products.Commands.UpdateProduct;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Requests.Command.UpdateRequest
{
    public class UpdateRequestCommand:IRequest
    {
        public Guid RequestId { get; }

        public UpdateRequestCommand(Guid requestId)
        {
            RequestId = requestId;
        }
        public int Quantity { get; set; }
        public string Remarks { get; set; }


    }

    public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand>
    {
        public IApplicationDbContext _context { get; set; }
        public UpdateRequestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            var requests = await _context.OrderRequests
                                        .FindAsync(new object[] { request.RequestId }, cancellationToken);
            if (requests == null)
            {
                throw new NotFoundException(nameof(Requests), request.RequestId);
            }
            requests.Quantity= request.Quantity;
            requests.Remarks = request.Remarks;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
