
namespace Qz.Core.Infrastructure.Interface.UnitOfWorks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    using Qz.Core.Entity;
    using Qz.Core.Infrastructure.Interface.Configuration;
    using System.Data.Entity.Infrastructure;

    public interface IUnitOfWorks
    {
        DbRawSqlQuery<T> SqlQuery<T>(string sql, params object[] parameters) where T : class;
        DbRawSqlQuery SqlQuery(Type elementType,string sql, params object[] parameters);
        int SqlCommand(string sql, params object[] parameters);

        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<IQueryable<T>> WhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        Page.Response<T> All<T>(Page.Request<T> req) where T : class;
        Task<Page.Response<T>> AllAsync<T>(Page.Request<T> req) where T : class;

        int Count<T>() where T : class;
        Task<int> CountAsync<T>() where T : class;

        int Count<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        T Find<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        T Update<T>(T entity) where T : class;
        T Update<T>(T entity, Action<DbEntityEntry<T>> modify) where T : class;
        Task<T> UpdateAsync<T>(T entity) where T : class;

        T Add<T>(T entity) where T : class;
        Task<T> AddAsync<T>(T entity) where T : class;

        IEnumerable<T> Add<T>(IEnumerable<T> entities) where T : class;
        Task<IEnumerable<T>> AddAsync<T>(IEnumerable<T> entities) where T : class;

        int Delete<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        bool Delete<T>(T entity) where T : class;
        Task<bool> DeleteAsync<T>(T entity) where T : class;

        void Clear<T>() where T : class;
        Task ClearAsync<T>() where T : class;

        int SaveChange();

        void Config(IConfiguration settings);

        bool IsOwnContext { get; set; }

        IQueryable<T> All<T>() where T : class;
    }
}
