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
    public class GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper) : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
    {
        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.Id);
            return mapper.Map<LeaveRequestDto>(leaveRequest);
        }
    }
}
