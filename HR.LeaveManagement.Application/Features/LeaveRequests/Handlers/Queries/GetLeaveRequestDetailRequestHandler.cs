using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepsoitory;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepsoitory,
            ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveRequestRepsoitory = leaveRequestRepsoitory;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepsoitory.GetLeaveRequestWithDetails(request.Id);
            return _mapper.Map<LeaveRequestDto>(leaveRequest);
        }
    }
}
