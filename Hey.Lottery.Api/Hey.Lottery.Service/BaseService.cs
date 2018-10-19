using Hey.Lottery.Models;
using Hey.Lottery.Repository;
using Hey.Lottery.ViewModels;
using Hey.Lottery.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hey.Lottery.Service
{
    public class BaseService<T> where T : BaseModel
    {
        protected BaseRepository<T> _repository;
        public async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereLambda)
        {
            return await _repository.GetListAsync(selector, whereLambda);
        }
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereLambda)
        {

            return await _repository.GetListAsync(whereLambda);
        }



        public async Task<int> CountAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await _repository.CountAsync(whereLambda);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await _repository.AnyAsync(whereLambda);
        }
        /// <summary>
        /// 实现对数据库的查询  --简单单行查询
        /// </summary> 
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> whereLambda)
        {
            return await _repository.FirstOrDefaultAsync(selector, whereLambda);
        }
        /// <summary>
        /// 实现对数据库的查询  --简单单行查询
        /// </summary> 
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await _repository.FirstOrDefaultAsync(whereLambda);
        }
        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="total">总条数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">一页显示多少条数据</param>
        /// <param name="order">DESC/ASC</param>
        /// <param name="sort">排序字段</param>
        /// <returns></returns>
        public async Task<TResult> GetListPagingAsync<TSelect, TResult>(Expression<Func<T, TSelect>> selector, Expression<Func<T, bool>> whereLambda, BasePagingRequest request) where TSelect : BaseItemResponse where TResult : BasePagingResponse<TSelect>, new()
        {
            var result = await _repository.GetListPagingAsync(whereLambda, selector, request.PageIndex, request.PageSize, request.Direction, request.SortField);
            return new TResult
            {
                Count = result.Count,
                Total = result.Total,
                Items = result.Items
            };
        }
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public async Task<T> FindAsync(params object[] keyValues)
        {
            return await _repository.FindAsync(keyValues);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="delLambda"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> delLambda)
        {
            return await _repository.DeleteAsync(delLambda);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public async Task<int> AddAsync(T entity)
        {
            entity.CreateBy = CurrentUser.Info.Id;
            entity.CreateDate = DateTime.Now;
            entity.ModifyBy = 0;
            entity.Disable = (short)BaseModel.DisableEnum.Normal;
            return await _repository.AddAsync(entity);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public async Task<int> AddAsync(List<T> entitys)
        {
            foreach (var entity in entitys)
            {
                entity.CreateBy = CurrentUser.Info.Id;
                entity.CreateDate = DateTime.Now;
                entity.ModifyBy = 0;
                entity.Disable = (short)BaseModel.DisableEnum.Normal;
            }
            return await _repository.AddAsync(entitys);
        }
        /// <summary>
        /// 修改
        /// 注意：主键必须传回
        /// </summary>
        /// <param name="delLambda"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T entity)
        {
            entity.ModifyBy = CurrentUser.Info.Id;
            entity.ModifyDate = DateTime.Now;
            entity.Disable = (short)BaseModel.DisableEnum.Normal;
            return await _repository.UpdateAsync(entity);
        }
        /// <summary>
        /// 禁用
        /// 注意：主键必须传回
        /// </summary>
        /// <param name="delLambda"></param>
        /// <returns></returns>
        public async Task<int> DisableAsync(int id)
        {
            var entity = _repository.Find(id);
            entity.ModifyBy = CurrentUser.Info.Id;
            entity.ModifyDate = DateTime.Now;
            entity.Disable = (short)BaseModel.DisableEnum.Disable;
            return await _repository.UpdateAsync(entity);
        }
    }
}
