using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Persistence.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();
        Task ChangeApprovalStatusAsync(LeaveRequest leaveRequest, bool? approvalStatus);
    }
}
