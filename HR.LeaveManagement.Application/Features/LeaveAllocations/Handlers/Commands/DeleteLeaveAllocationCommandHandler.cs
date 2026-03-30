using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Domain;
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

            //if it is null, then it means that the record is not found in the database.
            //Delete is idempotent, so we can just return without throwing an exception.
            if (leaveAllocation is not null)
                await leaveAllocationRepository.DeleteAsync(leaveAllocation);
        }
    }
}
