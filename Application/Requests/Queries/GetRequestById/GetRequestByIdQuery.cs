using Application.Common.Exceptions;
using Application.Common.Interfaces;
using InventoryManagementSystem.Application.Requests.Queries.GetAllRequests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Application.Requests.Queries.GetRequestById
{
    public class GetRequestByIdQuery : IRequest<RequestVm>
    {
        public Guid RequestId { get; }

        public GetRequestByIdQuery(Guid requestId)
        {
            RequestId = requestId;
        }
    }

    public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, RequestVm>
    {
        private readonly IApplicationDbContext _context;

        public GetRequestByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RequestVm> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.OrderRequests
                                .FirstOrDefaultAsync(x => x.Id == request.RequestId, cancellationToken);

            if (requests == null)
            {
                throw new NotFoundException($"Request with ID {request.RequestId} not found.");
            }

            return new RequestVm
            {
                Quantity = requests.Quantity,
                Remarks = requests.Remarks,
                UserId = requests.UserId,
                UserName = requests.UserName,
                Status = requests.Status
            };
        }


    }

}
