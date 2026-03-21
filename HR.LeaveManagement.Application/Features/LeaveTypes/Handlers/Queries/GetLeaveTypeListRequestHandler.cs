using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
    {
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await leaveTypeRepository.GetAllAsync();
            return mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        }
    }
}
