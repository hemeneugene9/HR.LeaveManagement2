using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveRequestRepository)
        {
            _leaveTypeRepository = leaveRequestRepository;
            Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
