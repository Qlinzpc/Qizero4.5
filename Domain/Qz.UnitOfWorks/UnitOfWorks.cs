
namespace Qz.UnitOfWorks
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    using Qz.Core.Infrastructure.Interface.Configuration;
    using Qz.Core.Infrastructure.Interface.UnitOfWorks;
    using Qz.Core.Infrastructure.Interface.Repository;
    using Qz.Core.Infrastructure.Configuration;
    using Qz.Core.Entity;
    using Qz.Repository;
    using Qz.Logging;
    using Qz.Common;
    using System.Data.Entity.Infrastructure;

    public class UnitOfWorks<TDbContext> : IUnitOfWorks
        where TDbContext : DbContext
    {
        protected TDbContext dbContext;
        public bool IsOwnContext { get; set; }

        public UnitOfWorks(TDbContext context)
        {
            dbContext = context;
        }

        private IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        public void Register<T>(IRepository<T> repository) where T : class
        {
            var key = typeof(T);

            if (!repositories.ContainsKey(key)) repositories.Add(key, repository);

        }

        private IRepository<T> GetRepository<T>() where T : class
        {

            IRepository<T> repository = null;

            var key = typeof(T);

            if (repositories.ContainsKey(key))
            {
                repository = (IRepository<T>)repositories[key];
            }
            else
            {
                repository = GenericRepository<T>();

                repositories.Add(key, repository);
            }

            repository.IsOwnContext = this.IsOwnContext;

            return repository;
        }

        protected virtual IRepository<T> GenericRepository<T>() where T : class
        {
            return new EntityRepository<TDbContext, T>(dbContext);
        }

        public DbRawSqlQuery<T> SqlQuery<T>(string sql, params object[] parameters) where T : class
        {
            return dbContext.Database.SqlQuery<T>(sql, parameters);
        }

        public DbRawSqlQuery SqlQuery(Type elementType,string sql, params object[] parameters) 
        {
            return dbContext.Database.SqlQuery(elementType, sql, parameters);
        }

        public int SqlCommand(string sql, params object[] parameters)
        {
            var rows = 0;

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    rows = dbContext.Database.ExecuteSqlCommand(sql, parameters);

                    transaction.Commit();

                    return rows;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    Log.ErrorAsync(ex.Message);
                    return rows;
                }
            }

        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.GetRepository<T>().Filter(predicate);
        }

        public Task<IQueryable<T>> WhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.GetRepository<T>().FilterAsync(predicate);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return this.GetRepository<T>().All();
        }

        public Page.Response<T> All<T>(Page.Request<T> req) where T : class
        {
            return this.GetRepository<T>().All(req);
        }

        public Task<Page.Response<T>> AllAsync<T>(Page.Request<T> req) where T : class
        {
            return this.GetRepository<T>().AllAsync(req);
        }

        public int Count<T>() where T : class
        {
            return this.GetRepository<T>().Count();
        }

        public Task<int> CountAsync<T>() where T : class
        {
            return this.GetRepository<T>().CountAsync();
        }

        public int Count<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.GetRepository<T>().Count(predicate);
        }

        public Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.GetRepository<T>().CountAsync(predicate);
        }

        public T Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.GetRepository<T>().Find(predicate);
        }

        public Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.GetRepository<T>().FindAsync(predicate);
        }

        public T Update<T>(T entity) where T : class
        {
            return this.GetRepository<T>().Update(entity);
        }

        public T Update<T>(T entity, Action<DbEntityEntry<T>> modify) where T : class
        {
            return this.GetRepository<T>().Update(entity, modify);
        }

        public Task<T> UpdateAsync<T>(T entity) where T : class
        {
            return this.GetRepository<T>().UpdateAsync(entity);
        }

        public T Add<T>(T entity) where T : class
        {
            return this.GetRepository<T>().Add(entity);
        }

        public Task<T> AddAsync<T>(T entity) where T : class
        {
            return this.GetRepository<T>().AddAsync(entity);
        }

        public IEnumerable<T> Add<T>(IEnumerable<T> entities) where T : class
        {
            return this.GetRepository<T>().Add(entities);
        }

        public Task<IEnumerable<T>> AddAsync<T>(IEnumerable<T> entities) where T : class
        {
            return this.GetRepository<T>().AddAsync(entities);
        }

        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.GetRepository<T>().Delete(predicate);
        }

        public Task<int> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return this.GetRepository<T>().DeleteAsync(predicate);
        }

        public bool Delete<T>(T entity) where T : class
        {
            return this.GetRepository<T>().Delete(entity);
        }

        public Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            return this.GetRepository<T>().DeleteAsync(entity);
        }

        public void Clear<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public int SaveChange(bool validateOnSave = true)
        {
            dbContext.Configuration.ValidateOnSaveEnabled = validateOnSave;

            return dbContext.SaveChanges();
        }

        int IUnitOfWorks.SaveChange()
        {
            return this.SaveChange();
        }

        public void Config(IConfiguration settings)
        {
            var config = settings as DBConfiguration;

            if (config != null)
            {

                this.dbContext.Configuration.AutoDetectChangesEnabled = config.AutoDetectChangesEnabled;
                this.dbContext.Configuration.LazyLoadingEnabled = config.LazyLoadingEnabled;
                this.dbContext.Configuration.ProxyCreationEnabled = config.ProxyCreationEnabled;
                this.dbContext.Configuration.ValidateOnSaveEnabled = config.ValidateOnSaveEnabled;

            }
            else
            {
                // 启用自动检测更改 
                this.dbContext.Configuration.AutoDetectChangesEnabled = false;
                // 启用保存验证 
                this.dbContext.Configuration.ValidateOnSaveEnabled = false;
                // 启用预加载 
                this.dbContext.Configuration.LazyLoadingEnabled = false;
                // 启用代理创建 
                this.dbContext.Configuration.ProxyCreationEnabled = false;
            }
        }

        public void Dispose()
        {
            if (dbContext != null) dbContext.Dispose();

            GC.SuppressFinalize(this);
        }

    }
}
