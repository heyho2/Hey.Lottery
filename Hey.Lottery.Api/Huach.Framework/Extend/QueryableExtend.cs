using Huach.Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Huach.Framework.Extend
{
    public static class QueryableExtend
    {
        /// <summary>
        /// 根据字段名排序
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="query">IQueryable集合</param>
        /// <param name="sort">字段名</param>
        /// <param name="order">排序方向</param>
        /// <returns></returns>
        public static IQueryable<T> SetQueryableOrder<T>(this IQueryable<T> query, string sort, string order)
        {
            if (string.IsNullOrEmpty(sort))
                throw new Exception("必须指定排序字段!");

            PropertyInfo sortProperty = typeof(T).GetProperty(sort, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (sortProperty == null)
                throw new Exception("查询对象中不存在排序字段" + sort + "！");

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression body = param;
            if (Nullable.GetUnderlyingType(body.Type) != null)
                body = Expression.Property(body, "Value");
            body = Expression.MakeMemberAccess(body, sortProperty);
            LambdaExpression keySelectorLambda = Expression.Lambda(body, param);

            if (string.IsNullOrEmpty(order))
                order = "ASC";
            string queryMethod = order.ToUpper() == "DESC" ? "OrderByDescending" : "OrderBy";
            query = query.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), queryMethod,
                                                               new Type[] { typeof(T), body.Type },
                                                               query.Expression,
                                                               Expression.Quote(keySelectorLambda)));
            return query;
        }
        public static async Task<PagingResult<TResult>> PagingAsync<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> selector, int pageIndex, int pageSize, string order, string sort)
        {
            var count = await query.CountAsync();
            query = query.SetQueryableOrder(sort, order).Skip(pageSize * (pageIndex - 1))
                     .Take(pageSize);
            var list = await query.Select(selector).ToListAsync();
            return new PagingResult<TResult>
            {
                Count = count,
                Items = list,
                Index = pageIndex,
                Size = pageSize,
                Total = count % pageSize == 0 ? count / pageSize : count / pageSize + 1
            };
        }
        public static PagingResult<TResult> Paging<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> selector, int pageIndex, int pageSize, string order, string sort)
        {
            var count = query.Count();
            query = query.SetQueryableOrder(sort, order).Skip(pageSize * (pageIndex - 1))
                     .Take(pageSize);
            var list = query.Select(selector).ToList();
            return new PagingResult<TResult>
            {
                Count = count,
                Items = list,
                Index = pageIndex,
                Size = pageSize,
                Total = count % pageSize == 0 ? count / pageSize : count / pageSize + 1
            };
        }
    }
}
