using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository(LeaveManagementDbContext dbContext) : GenericRepository<LeaveRequest>(dbContext), ILeaveRequestRepository
    {
        private readonly LeaveManagementDbContext _dbContext = dbContext;

        public async Task ChangeApprovalStatusAsync(LeaveRequest leaveRequest, bool? approvalStatus)
        {
            leaveRequest.Approved = approvalStatus;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync()
        {
            var leaveRequests = _dbContext.LeaveRequests
                .Include(lr => lr.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest?> GetLeaveRequestWithDetailsAsync(int id)
        {
            var leaveRequests = await _dbContext.LeaveRequests
                .Include(lr => lr.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);
            return leaveRequests;
        }
    }
}
