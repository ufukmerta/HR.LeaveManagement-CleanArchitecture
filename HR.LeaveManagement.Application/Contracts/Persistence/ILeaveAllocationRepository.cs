using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation?> GetLeaveAllocationWithDetailsAsync(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync();
    }
}
