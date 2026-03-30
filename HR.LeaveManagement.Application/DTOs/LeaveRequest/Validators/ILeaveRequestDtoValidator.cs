using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("{PropertyName} cannot be in the past.")
                .LessThan(x => x.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(x => x.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}.");

            RuleFor(x => x.LeaveTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(
                    async (id, cancellationToken) =>
                    {
                        var leaveTypeExists = await leaveTypeRepository.ExistsAsync(id);
                        return leaveTypeExists;
                    }).WithMessage("{PropertyName} does not exist.");

            RuleFor(x => x.RequestComments)
               .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
        }
    }
}
