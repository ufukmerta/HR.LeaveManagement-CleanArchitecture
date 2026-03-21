using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper) : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await leaveAllocationRepository.GetAsync(request.LeaveAllocationDto.Id);
            mapper.Map(request.LeaveAllocationDto, leaveAllocation);
            await leaveAllocationRepository.UpdateAsync(leaveAllocation);
            return Unit.Value;
        }
    }
}
