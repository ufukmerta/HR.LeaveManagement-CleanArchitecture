using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper) : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await leaveAllocationRepository.GetLeaveAllocationsWithDetailsAsync();
            return mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
        }
    }
}
