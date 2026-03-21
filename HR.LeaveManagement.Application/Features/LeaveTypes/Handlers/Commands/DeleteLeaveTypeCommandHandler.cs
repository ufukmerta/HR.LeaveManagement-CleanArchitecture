using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<DeleteLeaveTypeCommand>
    {
        public async Task Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await leaveTypeRepository.GetAsync(request.Id);
            await leaveTypeRepository.DeleteAsync(leaveType);
        }
    }
}
