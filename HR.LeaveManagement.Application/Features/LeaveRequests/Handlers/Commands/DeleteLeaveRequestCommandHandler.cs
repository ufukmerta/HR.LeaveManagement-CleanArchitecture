using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository) : IRequestHandler<DeleteLeaveRequestCommand>
    {
        public async Task Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetAsync(request.Id);
            await leaveRequestRepository.DeleteAsync(leaveRequest);
        }
    }
}
