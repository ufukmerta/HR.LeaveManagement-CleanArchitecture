using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper) : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetAsync(request.Id);

            if (request.UpdateLeaveRequestDto != null)
            {
                mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
                await leaveRequestRepository.UpdateAsync(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDto != null)
            {
                mapper.Map(request.ChangeLeaveRequestApprovalDto, leaveRequest);
                await leaveRequestRepository.ChangeApprovalStatusAsync(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
            }

            return Unit.Value;
        }
    }
}
