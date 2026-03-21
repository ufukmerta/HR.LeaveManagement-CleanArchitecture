using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper) : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = mapper.Map<LeaveType>(request.LeaveTypeDto);
            leaveType = await leaveTypeRepository.AddAsync(leaveType);
            return leaveType.Id;
        }
    }
}
