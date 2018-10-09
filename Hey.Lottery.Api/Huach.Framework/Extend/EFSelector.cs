using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Huach.Framework.Extend
{
    /// <summary>
    /// EF select 表达式目录树生成
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class EFSelector<TEntity, TResult> where TEntity : class, new() where TResult : class, new()
    {
        /// <summary>
        /// 缓存表达式
        /// </summary>
        private static Expression<Func<TEntity, TResult>> _selectorLambda = null;
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<TEntity, TResult>> Get()
        {
            if (_selectorLambda != null)
            {
                return _selectorLambda;
            }
            Type entityType = typeof(TEntity);
            Type resultType = typeof(TResult);
            var parameterExpression = Expression.Parameter(entityType, "p");
            var memberBindingList = new List<MemberBinding>();
            foreach (var item in resultType.GetProperties())
            {
                if (entityType.GetProperties().Any(ep => ep.Name == item.Name))
                {
                    var property = Expression.Property(parameterExpression, entityType.GetProperty(item.Name));
                    var memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
            }
            foreach (var item in resultType.GetFields())
            {
                if (entityType.GetProperties().Any(ep => ep.Name == item.Name))
                {
                    var property = Expression.Field(parameterExpression, entityType.GetField(item.Name));
                    var memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
            }
            var memberInitExpression = Expression.MemberInit(Expression.New(resultType), memberBindingList.ToArray());
            _selectorLambda = Expression.Lambda<Func<TEntity, TResult>>(memberInitExpression, new ParameterExpression[]
           {
                parameterExpression
           });
            return _selectorLambda;
        }
    }
}
