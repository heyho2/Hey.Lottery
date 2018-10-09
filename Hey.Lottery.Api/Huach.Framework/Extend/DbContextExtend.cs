using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Huach.Framework.Extend
{
    public static class DbContextExtend
    {
        private static readonly ConcurrentDictionary<string, string[]> _cacheEntityKeyNames = new ConcurrentDictionary<string, string[]>();
        public static string[] GetEntityKeyNames<TEntity>(this DbContext dbContext) where TEntity : class
        {
            Type typeFromHandle = typeof(TEntity);
            if (_cacheEntityKeyNames.TryGetValue(typeFromHandle.FullName, out string[] value))
            {
                return value;
            }
            ObjectContext objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;

            dynamic val = typeof(ObjectContext).GetMethod("CreateObjectSet", Type.EmptyTypes).MakeGenericMethod(typeFromHandle).Invoke(objectContext, null);
            value = ((IEnumerable<object>)val.EntitySet.ElementType.KeyMembers).Select(a => (string)(((dynamic)a).Name)).ToArray();
            _cacheEntityKeyNames.TryAdd(typeFromHandle.FullName, value);
            return value;
        }
        public static Dictionary<string, object> GetEntityKeys<TEntity>(this DbContext dbContext, TEntity entity) where TEntity : class
        {
            string[] entityKeyNames = dbContext.GetEntityKeyNames<TEntity>();
            Type typeFromHandle = typeof(TEntity);
            var dictionary = new Dictionary<string, object>();
            foreach (var item in entityKeyNames)
            {
                dictionary[item] = typeFromHandle.GetProperty(item).GetValue(entity);
            }
            return dictionary;
        }
    }
}
