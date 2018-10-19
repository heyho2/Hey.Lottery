using Hey.Lottery.Models;
using Huach.Framework.Extend;
using Huach.Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Repository
{
    /// <summary>
    /// 实现对数据库的操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> where T : BaseModel
    {
        //获取的是当前线程内部的上下文实例，而且保证了线程内上下文唯一
        internal protected DbContext DbContext = DBContextFactory.GetCurrentDbContext;

        public IQueryable<T> Where(Expression<Func<T, bool>> whereLambda = null)
        {
            return DbContext.Set<T>().Where<T>(whereLambda).AsNoTracking();
        }

        #region 同步
        public int Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return DbContext.SaveChanges();
        }


        public int Update(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Modified;
            return DbContext.SaveChanges();
        }

        public int Update(T entity, params string[] proNames)
        {
            var entry = DbContext.Entry<T>(entity);
            entry.State = EntityState.Unchanged;
            foreach (var item in proNames)
            {
                entry.Property(item).IsModified = true;
            }
            return DbContext.SaveChanges();
        }
        public int Update(T entity, Expression<Func<T, bool>> whereLambda, params string[] proNames)
        {
            var listModifes = DbContext.Set<T>().Where(whereLambda).ToList();
            Type t = typeof(T);
            var proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            Dictionary<string, PropertyInfo> dicPros = new Dictionary<string, PropertyInfo>();
            proInfos.ForEach(a =>
            {
                if (proNames.Contains(a.Name))
                {
                    dicPros.Add(a.Name, a);
                }
            });
            foreach (var item in proNames)
            {
                if (dicPros.ContainsKey(item))
                {
                    PropertyInfo proInfo = dicPros[item];
                    object newValue = proInfo.GetValue(entity, null);
                    foreach (var m in listModifes)
                    {
                        proInfo.SetValue(m, newValue, null);
                    }
                }
            }
            return DbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Deleted;
            return DbContext.SaveChanges();
        }

        public int Delete(Expression<Func<T, bool>> delLambda)
        {
            var ListDel = DbContext.Set<T>().Where(delLambda).ToList();
            ListDel.ForEach(a =>
            {
                DbContext.Set<T>().Attach(a);
                DbContext.Entry<T>(a).State = EntityState.Deleted;
            });
            return DbContext.SaveChanges();
        }


        public T FirstOrDefault(Expression<Func<T, bool>> whereLambda)
        {
            return DbContext.Set<T>().AsNoTracking().FirstOrDefault<T>(whereLambda);
        }

        public T Find(params object[] keyValues)
        {
            return DbContext.Set<T>().Find(keyValues);
        }

        public PagingResult<TResult> GetListPaging<TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selector, int pageIndex, int pageSize, string order, string sort)
        {
            var temp = DbContext.Set<T>().Where<T>(whereLambda);
            var count = temp.Count();
            temp = temp.SetQueryableOrder(sort, order).Skip(pageSize * (pageIndex - 1))
                     .Take(pageSize);
            var list = temp.Select(selector).ToList();
            return new PagingResult<TResult>
            {
                Count = count,
                Items = list,
                Index = pageIndex,
                Size = pageSize,
                Total = count % pageSize == 0 ? count / pageSize : count / pageSize + 1
            };
        }

        public List<TResult> List<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereLambda)
        {
            return DbContext.Set<T>().Where(whereLambda).Select(selector).ToList();
        }

        public TResult FirstOrDefault<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereLambda)
        {
            return DbContext.Set<T>().Where(whereLambda).Select(selector).FirstOrDefault();
        }
        #endregion

        #region 异步

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await DbContext.Set<T>().Where<T>(whereLambda).AsNoTracking().ToListAsync();
        }


        public async Task<int> AddAsync(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return await DbContext.SaveChangesAsync();
        }
        public async Task<int> AddAsync(IEnumerable<T> entitys)
        {
            foreach (var entity in entitys)
            {
                DbContext.Set<T>().Add(entity);
            }
            return await DbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry<T>(entity).State = EntityState.Modified;
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity, params string[] proNames)
        {
            var entry = DbContext.Entry<T>(entity);
            entry.State = EntityState.Unchanged;
            foreach (var item in proNames)
            {
                entry.Property(item).IsModified = true;
            }
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Deleted;
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> delLambda)
        {
            var ListDel = await DbContext.Set<T>().Where(delLambda).ToListAsync();
            ListDel.ForEach(a =>
            {
                DbContext.Set<T>().Attach(a);
                DbContext.Entry<T>(a).State = EntityState.Deleted;
            });
            return await DbContext.SaveChangesAsync();
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await DbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync<T>(whereLambda);
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            return await DbContext.Set<T>().FindAsync(keyValues);
        }

        public async Task<PagingResult<TResult>> GetListPagingAsync<TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selector, int pageIndex, int pageSize, string order, string sort)
        {
            var temp = DbContext.Set<T>().Where(whereLambda);
            var count = await temp.CountAsync();
            temp = temp.SetQueryableOrder(sort ?? nameof(BaseModel.Id), order ?? "desc").Skip(pageSize * (pageIndex - 1))
                     .Take(pageSize);
            var list = await temp.Select(selector).ToListAsync();
            return new PagingResult<TResult>
            {
                Count = count,
                Items = list,
                Index = pageIndex,
                Size = pageSize,
                Total = count % pageSize == 0 ? count / pageSize : count / pageSize + 1
            };
        }

        public async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereLambda)
        {
            return await DbContext.Set<T>().Where(whereLambda).Select(selector).ToListAsync();
        }

        public async Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereLambda)
        {
            return await DbContext.Set<T>().Where(whereLambda).Select(selector).FirstOrDefaultAsync();
        }


        public async Task<int> CountAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await DbContext.Set<T>().CountAsync(whereLambda);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await DbContext.Set<T>().AnyAsync(whereLambda);
        }

        #endregion
    }
}
