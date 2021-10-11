using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;


        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
            ILeaveTypeRepository leaveTypeRepository, IMapper mapper,
            IEmailSender emailSender)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _emailSender = emailSender;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);

            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

                

            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
            leaveRequest = await _leaveRequestRepository.Add(leaveRequest);



            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveRequest.Id;
            var email = new Email
            {
                To = "employee@org.com",
                Body = $"Your leave request for {request.LeaveRequestDto.StartDate} to {request.LeaveRequestDto.EndDate}"+
                $"has been submitted succesfully.",
                Subject = "Leave Request Submitted"
            };
            try
            {
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                //Log or handle error, but dont throw....
                throw;
            }
            return response;

        }
    }
}
