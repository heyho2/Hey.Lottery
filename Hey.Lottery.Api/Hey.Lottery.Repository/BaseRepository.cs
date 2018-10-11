using Hey.Lottery.Models;
using Huach.Framework.Extend;
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
        private DbContext db = DBContextFactory.GetCurrentDbContext;

        public IQueryable<TResult> LoadPaging<TResult>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TResult>> selector, out int total, int pageIndex, int pageSize, string order, string sort)
        {
            var temp = db.Set<T>().Where<T>(whereLambda);
            total = temp.Count();
            temp = temp.SetQueryableOrder(sort, order).Skip(pageSize * (pageIndex - 1))
                     .Take(pageSize);
            return temp.Select(selector).AsQueryable();
        }

        public int Add(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Added;
            return db.SaveChanges();
        }

        public int Update(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Update(T entity, params string[] proNames)
        {
            var entry = db.Entry<T>(entity);
            entry.State = EntityState.Unchanged;
            foreach (var item in proNames)
            {
                entry.Property(item).IsModified = true;
            }
            return db.SaveChanges();
        }

        public int Update(T entity, Expression<Func<T, bool>> whereLambda, params string[] proNames)
        {
            var listModifes = db.Set<T>().Where(whereLambda).ToList();
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
            return db.SaveChanges();
        }

        public int Delete(T entity)
        {
            //EF5.0的写法
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Deleted;
            return db.SaveChanges();
        }

        public int Delete(Expression<Func<T, bool>> delLambda)
        {
            var ListDel = db.Set<T>().Where(delLambda).ToList();
            ListDel.ForEach(a =>
            {
                db.Set<T>().Attach(a);
                db.Entry<T>(a).State = EntityState.Deleted;
            });
            //EF5.0的写法
            return db.SaveChanges();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().AsNoTracking().Where<T>(whereLambda).AsQueryable();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().AsNoTracking().FirstOrDefault<T>(whereLambda);
        }

        public T Find(params object[] keyValues)
        {
            return db.Set<T>().Find(keyValues);
        }
    }
}
