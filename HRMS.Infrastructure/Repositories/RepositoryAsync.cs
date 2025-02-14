using Microsoft.EntityFrameworkCore;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Contract;
using HRMS.Infrastructure.Contexts;

namespace HRMS.Infrastructure.Repositories
{
    public class RepositoryAsync<T, TId> : IRepositoryAsync<T, TId> where T : AuditableEntity<TId>
    {
        private readonly ApplicationDbContext _dbContext;

        public RepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            _ = await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<bool> FindForiegnExists(int Id)
        {
            return false;
        }

        public Task DeleteAsync(T entity)
        {
            _ = _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .AsNoTracking().ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>()!.Find(entity.Id)!;
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }
    }
}
