using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
       
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);

    }
}
