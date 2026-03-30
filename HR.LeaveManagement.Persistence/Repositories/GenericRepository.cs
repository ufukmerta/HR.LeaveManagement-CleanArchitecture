using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T>(LeaveManagementDbContext dbContext) : IGenericRepository<T> where T : BaseDomainEntity
    {
        public async Task<T?> GetAsync(int id)
        {
            return await dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var isExist = await dbContext.Set<T>().AnyAsync(x => x.Id == id);
            return isExist;

        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
