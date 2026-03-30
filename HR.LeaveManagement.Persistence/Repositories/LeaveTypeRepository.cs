using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository(LeaveManagementDbContext context) : GenericRepository<LeaveType>(context), ILeaveTypeRepository
    {
    }
}
