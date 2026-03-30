using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Domain;
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

            //if it is null, then it means that the record is not found in the database.
            //Delete is idempotent, so we can just return without throwing an exception.
            if (leaveRequest is not null)
                await leaveRequestRepository.DeleteAsync(leaveRequest);
        }
    }
}
