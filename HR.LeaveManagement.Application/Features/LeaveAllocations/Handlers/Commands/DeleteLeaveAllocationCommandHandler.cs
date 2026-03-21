using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository) : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        public async Task Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await leaveAllocationRepository.GetAsync(request.Id);
            await leaveAllocationRepository.DeleteAsync(leaveAllocation);
        }
    }
}
