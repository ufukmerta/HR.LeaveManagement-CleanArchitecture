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
    public class CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationDtoValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto, cancellationToken);

            if (validationResult.IsValid is false)
                return BaseCommandResponse.Fail([.. validationResult.Errors.Select(e => e.ErrorMessage)], "Leave Allocation creation failed.");

            var leaveAllocation = mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);

            leaveAllocation = await leaveAllocationRepository.AddAsync(leaveAllocation);

            return BaseCommandResponse.Success(leaveAllocation.Id, "Leave Allocation created successfully.");
        }
    }
}
