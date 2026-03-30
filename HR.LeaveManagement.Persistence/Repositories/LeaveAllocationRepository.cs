using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository(LeaveManagementDbContext dbContext) : GenericRepository<LeaveAllocation>(dbContext), ILeaveAllocationRepository
    {
        private readonly LeaveManagementDbContext _dbContext = dbContext;

        public Task<LeaveAllocation?> GetLeaveAllocationWithDetailsAsync(int id)
        {
            var leaveAllocation = _dbContext.LeaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);
            return leaveAllocation;
        }
        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync()
        {
            var leaveAllocations = await _dbContext.LeaveAllocations
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }
    }
}
