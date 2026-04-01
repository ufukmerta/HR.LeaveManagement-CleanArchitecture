using AutoMapper;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : IRequestHandler<UpdateLeaveAllocationCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto, cancellationToken);

            if (validationResult.IsValid is false)
                return BaseCommandResponse.Fail([.. validationResult.Errors.Select(e => e.ErrorMessage)], "Leave Allocation update failed.");

            var leaveAllocation = await leaveAllocationRepository.GetAsync(request.LeaveAllocationDto.Id)
                ?? throw new NotFoundException(nameof(LeaveAllocation), request.LeaveAllocationDto.Id);

            mapper.Map(request.LeaveAllocationDto, leaveAllocation);

            await leaveAllocationRepository.UpdateAsync(leaveAllocation);

            return BaseCommandResponse.Success("Leave Allocation updated successfully.");
        }
    }
}
