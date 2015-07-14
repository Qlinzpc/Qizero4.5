
namespace Qz.Core.Infrastructure.Interface.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Qz.Core.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IRepository<T> : IDisposable
            where T : class
    {
        bool IsOwnContext { get; set; }

        #region CURD

        IQueryable<T> All();
        Task<IQueryable<T>> AllAsync();

        Page.Response<T> All(Page.Request<T> req);
        Task<Page.Response<T>> AllAsync(Page.Request<T> req);

        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> FilterAsync(Expression<Func<T, bool>> predicate);

        bool Contains(Expression<Func<T, bool>> predicate);
        Task<bool> ContainsAsync(Expression<Func<T, bool>> predicate);

        T Find(Expression<Func<T, bool>> predicate);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        Task<T> AddAsync(T entity);

        IEnumerable<T> Add(IEnumerable<T> items);
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> items);

        bool Delete(T entity);
        Task<bool> DeleteAsync(T entity);

        int Delete(Expression<Func<T, bool>> predicate);
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);

        T Update(T entity);
        T Update(T entity, Action<DbEntityEntry<T>> modify);
        T Update(T entity, Action<T> modify);
        Task<T> UpdateAsync(T entity);

        int Count();
        Task<int> CountAsync();

        int Count(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        int Submit();
        Task<int> SubmitAsync();

        #endregion

    }
}
