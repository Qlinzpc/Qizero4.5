
namespace Qz.Core.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    using Qz.Core.Entity;

    public static class QueryExtensions
    {
        /// <summary>
        /// 排序 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortExpression">排序字段名</param>
        /// <param name="sort">默认为1升序 2降序</param>
        /// <returns></returns>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string sortExpression, int sort = 1) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            string sortDirection = String.Empty;
            string propertyName = String.Empty;

            propertyName = sortExpression.Trim();
            sortDirection = sort == 1 ? "ASC" : "DESC";

            if (String.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            ParameterExpression parameter = Expression.Parameter(source.ElementType, String.Empty);
            MemberExpression property = Expression.Property(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = (sortDirection == "ASC") ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { source.ElementType, property.Type }, source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

        /// <summary>
        /// 获得分页数据 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> source, Page.Request<T> req) where T : class
        {
            return source.SortBy(req.SortName, (int)req.Sort).Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize);
        }

    }
}
