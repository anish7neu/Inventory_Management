using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Application.Requests.Command.DeclineRequest
{
    public class DeclineRequestCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeclineRequestCommand(Guid id)
        {
            Id = id;
        }
    }
    public class DeclineRequestCommandHandler : IRequestHandler<DeclineRequestCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeclineRequestCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeclineRequestCommand request, CancellationToken cancellationToken)
        {
            Request? toBeDeclinedRequest = await _context.OrderRequests
                                                         .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken) 
                                                         ?? throw new NotFoundException($"No request with Id {request.Id}.");

            toBeDeclinedRequest.Status = toBeDeclinedRequest.Status == RequestStatus.Pending
                                                                     ? RequestStatus.Declined
                                                                     :
                                                                     throw new NotFoundException("Request is either already declined or approved.");
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
