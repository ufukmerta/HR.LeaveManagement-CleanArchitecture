using AutoMapper;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IEmailSender emailSender,
        IMapper mapper) : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestDtoValidator(leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto, cancellationToken);

            if (validationResult.IsValid is false)
                return BaseCommandResponse.Fail([.. validationResult.Errors.Select(e => e.ErrorMessage)], "Leave Request creation failed.");

            var leaveRequest = mapper.Map<LeaveRequest>(request.LeaveRequestDto);

            leaveRequest = await leaveRequestRepository.AddAsync(leaveRequest);

            var email = new Email
            {
                To = "employee@org.com",
                Subject = "Leave Request Created",
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been created successfully."
            };

            try
            {
                await emailSender.SendEmailAsync(email);
            }
            catch (Exception ex)
            {

                throw;
            }

            return BaseCommandResponse.Success(leaveRequest.Id, "Leave Request created successfully.");
        }
    }
}
