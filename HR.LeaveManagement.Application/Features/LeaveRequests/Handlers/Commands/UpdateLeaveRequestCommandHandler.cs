using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : IRequestHandler<UpdateLeaveRequestCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await leaveRequestRepository.GetAsync(request.Id)
                ?? throw new NotFoundException(nameof(LeaveRequest), request.Id);

            if (request.UpdateLeaveRequestDto is not null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(leaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto, cancellationToken);

                if (validationResult.IsValid is false)
                    return BaseCommandResponse.Fail([.. validationResult.Errors.Select(e => e.ErrorMessage)], "Leave request update failed.");

                if (request.Id != request.UpdateLeaveRequestDto.Id)
                    return BaseCommandResponse.Fail("Leave request ID mismatch.", "Leave request update failed.");

                mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);

                await leaveRequestRepository.UpdateAsync(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDto is not null)
            {
                if (request.Id != request.ChangeLeaveRequestApprovalDto.Id)
                    return BaseCommandResponse.Fail("Leave request ID mismatch.", "Leave request approval update failed.");

                await leaveRequestRepository.ChangeApprovalStatusAsync(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
            }

            return BaseCommandResponse.Success(request.Id, "Leave request updated successfully.");
        }
    }
}
