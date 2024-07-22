using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace InventoryManagementSystem.Application.Requests.Command.AddRequest
{
    public class AddRequestCommand : IRequest<Guid>
    {
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public RequestStatus Status { get; set; }
        public Guid ProductId { get; set; }
    }

    public class AddRequestCommandHandler : IRequestHandler<AddRequestCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public AddRequestCommandHandler(IApplicationDbContext context, 
                                        ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task<Guid> Handle(AddRequestCommand request, CancellationToken cancellationToken)
        {
            string? userId = _currentUserService.UserId;
            string? userName = _currentUserService.UserName;
            var entity = new Request
            {
                Quantity = request.Quantity,
                Remarks = request.Remarks,
                Status = request.Status,
                ProductId = request.ProductId,
                UserId = userId,
                UserName = userName,
            };

            _context.OrderRequests.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;

        }
    }




}
