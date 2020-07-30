using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);
        Task<T> Find(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<bool> SaveChangesAsync();
        IQueryable<T> Query();
    }
}