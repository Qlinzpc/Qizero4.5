
namespace Qz.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    using Qz.Common;
    using Qz.Core.Entity;
    using Qz.Core.Infrastructure;
    using Qz.Core.Infrastructure.Interface.Repository;

    public class EntityRepository<TContext, TObject> : IRepository<TObject>
        where TContext : DbContext
        where TObject : class
    {

        public bool IsOwnContext { get; set; }

        // 当前上下文对象 
        protected TContext context;
        // 当前数据集对象
        protected DbSet<TObject> dbSet;

        public EntityRepository(TContext dbContext)
        {
            context = dbContext;
            dbSet = this.Context.Set<TObject>();
            IsOwnContext = false;
        }

        /// <summary>
        /// 获得当前 上下文对象 
        /// </summary>
        protected virtual TContext Context { get { return context; } }

        /// <summary>
        /// 获得当前 数据集对象 
        /// </summary>
        protected virtual DbSet<TObject> DbSet { get { return dbSet; } }

        /// <summary>
        /// 获得所有 数据集对象 
        /// </summary>
        /// <returns>数据集对象 集合</returns>
        public virtual IQueryable<TObject> All()
        {
            return DbSet.AsQueryable<TObject>();
        }
        /// <summary>
        /// 异步 获得所有 数据集对象 
        /// </summary>
        /// <returns>数据集对象 集合</returns>
        public virtual Task<IQueryable<TObject>> AllAsync()
        {
            return Task.Run(() =>
            {
                return DbSet.AsQueryable<TObject>();
            });
        }

        /// <summary>
        /// 异步 获得所有 数据集对象 
        /// </summary>
        /// <returns>数据集对象 集合</returns>
        public virtual void AllAsync1(Action<IQueryable<TObject>> callback)
        {
            Func<IQueryable<TObject>> func = (() =>
            {
                return DbSet.AsQueryable<TObject>();
            });

            func.RunAsync(callback);
        }

        /// <summary>
        /// 获得所有 数据集对象 
        /// </summary>
        /// <returns>数据集对象 集合</returns>
        public virtual Page.Response<TObject> All(Page.Request<TObject> req)
        {
            IQueryable<TObject> list = req.Filter == null ? All() : Filter(req.Filter);

            Page.Response<TObject> res = new Page.Response<TObject>
            {
                TotalCount = list.Count(),
                PageSize = req.PageSize,
                PageIndex = req.PageIndex
            };

            list = list.Page(req);

            res.Datas = list;
            res.Times = (DateTime.Now.Subtract(req.BeginTime).TotalMilliseconds / 1000.0) + " s";

            return res;
        }

        /// <summary>
        /// 异步 获得所有 数据集对象 
        /// </summary>
        /// <returns>数据集对象 集合</returns>
        public virtual Task<Page.Response<TObject>> AllAsync(Page.Request<TObject> req)
        {
            return Task.Run(() =>
            {
                return All(req);
            });
        }

        /// <summary>
        /// 根据条件对 数据集进行筛选 , 获得数据集对象 集合 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>数据集对象 集合</returns>
        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TObject>();
        }

        /// <summary>
        /// 异步根据条件对 数据集进行筛选 , 获得数据集对象 集合 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>数据集对象 集合</returns>
        public virtual Task<IQueryable<TObject>> FilterAsync(Expression<Func<TObject, bool>> predicate)
        {
            return Task.Run(() =>
            {
                return DbSet.Where(predicate).AsQueryable<TObject>();
            });
        }

        /// <summary>
        /// 根据条件判断数据集是否存在 该对象 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>true or false</returns>
        public virtual bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        /// <summary>
        /// 异步根据条件判断数据集是否存在 该实体对象 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>true or false</returns>
        public virtual Task<bool> ContainsAsync(Expression<Func<TObject, bool>> predicate)
        {
            return Task.Run(() => { return DbSet.Count(predicate) > 0; });
        }

        /// <summary>
        /// 根据条件查找数据集 实体对象 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体对象</returns>
        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 异步根据条件查找数据集 实体对象 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体对象</returns>
        public virtual Task<TObject> FindAsync(Expression<Func<TObject, bool>> predicate)
        {
            return Task.Run(() => { return DbSet.FirstOrDefault(predicate); });
        }

        /// <summary>
        /// 创建实体对象 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体对象</returns>
        public virtual TObject Add(TObject entity)
        {
            var _entity = DbSet.Add(entity);

            if (IsOwnContext) context.SaveChanges();

            return _entity;
        }

        /// <summary>
        /// 异步创建实体对象 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体对象</returns>
        public virtual Task<TObject> AddAsync(TObject entity)
        {
            return Task.Run(() =>
            {
                var _entity = DbSet.Add(entity);

                if (IsOwnContext) context.SaveChanges();

                return _entity;
            });
        }

        /// <summary>
        /// 创建实体对象 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体对象</returns>
        public virtual IEnumerable<TObject> Add(IEnumerable<TObject> entities)
        {
            var _entities = DbSet.AddRange(entities);

            if (IsOwnContext) context.SaveChanges();

            return _entities;
        }

        /// <summary>
        /// 异步创建实体对象 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体对象</returns>
        public virtual Task<IEnumerable<TObject>> AddAsync(IEnumerable<TObject> entities)
        {
            return Task.Run(() =>
            {
                var _entities = DbSet.AddRange(entities);

                if (IsOwnContext) context.SaveChanges();

                return _entities;
            });
        }

        /// <summary>
        /// 删除实体对象 
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体对象</returns>
        public virtual bool Delete(TObject entity)
        {
            DbSet.Remove(entity);

            if (IsOwnContext) return Context.SaveChanges() > 0;

            return false;
        }

        public virtual Task<bool> DeleteAsync(TObject entity)
        {
            return Task.Run(() =>
            {
                DbSet.Remove(entity);

                if (IsOwnContext) return Context.SaveChanges() > 0;

                return false;
            });
        }

        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var _objects = DbSet.Where(predicate).ToList();

            DbSet.RemoveRange(_objects);

            if (IsOwnContext)
            {
                if (Context.SaveChanges() > 0)
                {
                    return _objects.Count;
                }
            }

            return 0;

        }

        public virtual Task<int> DeleteAsync(Expression<Func<TObject, bool>> predicate)
        {
            return Task.Run(() =>
            {
                var _objects = DbSet.Where(predicate).ToList();

                DbSet.RemoveRange(_objects);

                if (IsOwnContext)
                {
                    if (Context.SaveChanges() > 0)
                    {
                        return _objects.Count;
                    }
                }

                return 0;
            });
        }

        public virtual TObject Update(TObject entity)
        {
            var _entity = Context.Entry(entity);

            DbSet.Attach(entity);

            _entity.State = EntityState.Modified;

            if (IsOwnContext) Context.SaveChanges();

            return entity;
        }

        public virtual TObject Update(TObject entity, Action<DbEntityEntry<TObject>> modify)
        {
			var _entity = Context.Entry(entity);
			
            DbSet.Attach(entity);

            _entity.State = EntityState.Modified;

            modify(_entity);

            if (IsOwnContext) Context.SaveChanges();

            return entity;
        }

        public virtual Task<TObject> UpdateAsync(TObject entity)
        {
            return Task.Run(() =>
            {
                var _entity = Context.Entry(entity);

                DbSet.Attach(entity);

                _entity.State = EntityState.Modified;

                if (IsOwnContext) Context.SaveChanges();

                return entity;
            });
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.Run(() =>
            {
                return DbSet.Count();
            });
        }

        public virtual int Count(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public virtual Task<int> CountAsync(Expression<Func<TObject, bool>> predicate)
        {
            return Task.Run(() =>
            {
                return DbSet.Count(predicate);
            });

        }

        public virtual int Submit()
        {
            return Context.SaveChanges();
        }

        public virtual Task<int> SubmitAsync()
        {
            return Task.Run(() =>
            {
                return Context.SaveChanges();
            });
        }


        /// <summary>
        /// 释放资源 
        /// </summary>
        public void Dispose()
        {
            if ((IsOwnContext) && context != null)
            {
                context.Dispose();
            }

            GC.SuppressFinalize(this);
        }


        public TObject Update(TObject entity, Action<TObject> modify)
        {
            throw new NotImplementedException();
        }
    }
}
