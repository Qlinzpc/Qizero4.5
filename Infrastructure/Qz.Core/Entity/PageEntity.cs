
namespace Qz.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 公共分页实体 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Page
    {
        /// <summary>
        /// 请求 
        /// </summary>
        public class Request<TEntity> where TEntity : class
        {
            public DateTime BeginTime { get; set; }

            private int pageIndex;
            public int PageIndex
            {
                get
                {
                    return pageIndex <= 0 ? 1 : pageIndex;
                }
                set
                {
                    pageIndex = value;
                }
            }

            private int pageSize;
            public int PageSize
            {
                get
                {
                    return pageSize <= 0 ? 1 : pageSize;
                }
                set
                {
                    pageSize = value;
                }
            }

            public string SortName { get; set; }
            public Sorting Sort { get; set; }

            public Expression<Func<TEntity, bool>> Filter { get; set; }
        }

        /// <summary>
        /// 响应 
        /// </summary>
        public class Response<TEntity> where TEntity : class
        {
            private int pageIndex;
            public int PageIndex
            {
                get
                {
                    return pageIndex <= 0 ? 1 : pageIndex;
                }
                set
                {
                    pageIndex = value;
                }
            }

            private int pageSize;
            public int PageSize
            {
                get
                {
                    return pageSize <= 0 ? 1 : pageSize;
                }
                set
                {
                    pageSize = value;
                }
            }

            private int totalCount;
            public int TotalCount
            {
                get
                {
                    return totalCount < 0 ? 0 : totalCount;
                }
                set
                {
                    totalCount = value;
                }
            }

            public int PageCount
            {
                get
                {
                    return (TotalCount + PageSize - 1) / PageSize;
                }
            }

            public string Sql { get; set; }
            public string Times { get; set; }
            public IQueryable<TEntity> Datas { get; set; }
        }
    }

    public enum Sorting
    {
        Desc = 1,
        Asc = 2
    }
}
