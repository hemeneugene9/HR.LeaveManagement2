using AutoMapper;
using HR.LeaveManagement.Application.DTOs;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, LeaveAllocationDto>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepsoitory;
        private readonly IMapper _mapper;

        public GetLeaveAllocationDetailRequestHandler(ILeaveAllocationRepository leaveAllocationRepsoitory, IMapper mapper)
        {
            _leaveAllocationRepsoitory = leaveAllocationRepsoitory;
            _mapper = mapper;
        }
        public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepsoitory.GetLeaveAllocationWithDetails(request.Id);
            return _mapper.Map<LeaveAllocationDto>(leaveAllocation);
        }
    }
}
