using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper) : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetailsAsync();
            return mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
        }
    }
}
