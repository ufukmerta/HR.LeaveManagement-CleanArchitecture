using AutoMapper;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : IRequestHandler<UpdateLeaveTypeCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto, cancellationToken);

            if (validationResult.IsValid is false)
                return BaseCommandResponse.Fail([.. validationResult.Errors.Select(e => e.ErrorMessage)], "Leave type update failed.");

            var leaveType = await leaveTypeRepository.GetAsync(request.LeaveTypeDto.Id)
                ?? throw new NotFoundException(nameof(LeaveType), request.LeaveTypeDto.Id);

            mapper.Map(request.LeaveTypeDto, leaveType);

            await leaveTypeRepository.UpdateAsync(leaveType);

            return BaseCommandResponse.Success(leaveType.Id, "Leave type updated successfully.");
        }
    }
}
