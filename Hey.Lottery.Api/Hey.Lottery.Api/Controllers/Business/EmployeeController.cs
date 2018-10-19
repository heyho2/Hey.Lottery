using Hey.Lottery.Models;
using Hey.Lottery.Service.Business;
using Hey.Lottery.ViewModels.Base;
using Hey.Lottery.ViewModels.Business;
using Huach.Framework.Extend;
using Huach.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hey.Lottery.Api.Controllers
{
    public class EmployeeController : BaseApiController
    {
        readonly EmployeeService _employeeService = new EmployeeService();

        /// <summary>
        /// 员工列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<GetEmployeeListPagingResponse>)), HttpGet]
        public async Task<IHttpActionResult> GetListPagingAsync([FromUri][Required]GetEmployeeListPagingRequest request)
        {
            Expression<Func<Employee, bool>> where = a => a.Disable == (short)BaseModel.DisableEnum.Normal;
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                where = where.And(a => a.Name.Contains(request.Name));
            }
            if (!string.IsNullOrWhiteSpace(request.Department))
            {
                where = where.And(a => a.Department.Contains(request.Department));
            }
            var result = await _employeeService.GetListPagingAsync<GetEmployeeListPagingItem, GetEmployeeListPagingResponse>(a => new GetEmployeeListPagingItem
            {
                Id = a.Id,
                Name = a.Name,
                Department = a.Department,
                CreateDate = a.CreateDate,
            }, where, request);
            return Succeed(result, "加载成功");
        }
        /// <summary>
        /// 员工列表
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<BaseListResponse<GetEmployeeListPagingItem>>)), HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _employeeService.GetListAsync(a => new GetEmployeeListPagingItem
            {
                Id = a.Id,
                Name = a.Name,
                Department = a.Department,
                CreateDate = a.CreateDate,
            }, a => a.Disable == (short)BaseModel.DisableEnum.Normal);
            return Succeed(new BaseListResponse<GetEmployeeListPagingItem>
            {
                Items = result
            }, "加载成功");
        }
        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<AddEmployeeResponse>)), HttpPost]
        public async Task<IHttpActionResult> AddAsync(AddEmployeeRequest request)
        {
            var employee = new Employee
            {
                Name = request.Name,
                Department = request.Department
            };
            if (await _employeeService.AnyAsync(a => a.Name == request.Name && a.Department == request.Department))
            {
                return Fail("存在重名");
            }
            var result = await _employeeService.AddAsync(employee);

            if (result > 0)
            {
                return Succeed(new AddEmployeeResponse
                {
                    Id = employee.Id
                }, "添加成功");
            }
            else
            {
                return Fail("修改失败");
            }
        }
        /// <summary>
        /// 修改（注意：没有修改的也要将原来的数据传回）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult)), HttpPost]
        public virtual async Task<IHttpActionResult> UpdateAsync(UpdateEmployeeRequest request)
        {
            var entity = await _employeeService.FirstOrDefaultAsync(a => a.Id == request.Id);
            entity.Department = request.Department;
            entity.Name = request.Name;
            var result = await _employeeService.UpdateAsync(entity);
            if (result > 0)
            {
                return Succeed("修改成功");
            }
            else
            {
                return Fail("修改失败");
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<int>)), HttpGet]
        public virtual async Task<IHttpActionResult> DeleteAsync([FromUri]EmployeeDeleteRequest request)
        {
            var result = await _employeeService.DeleteAsync(a => a.Id == request.Id);
            if (result > 0)
            {
                return Succeed(result, "删除成功");
            }
            else
            {
                return Fail("删除失败");
            }
        }
        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<int>)), HttpPost]
        public async Task<IHttpActionResult> ImportAsync([Required]List<AddEmployeeRequest> requests)
        {
            requests = requests.Distinct().ToList();//去重
            if (requests.Count() == 0)
            {
                return Fail("数据空的");
            }
            var result = await _employeeService.AddAsync(requests.Select(a => new Employee
            {
                Name = a.Name,
                Department = a.Department,
            }).ToList());
            return Succeed(result, "导入成功");
        }
        /// <summary>
        /// 清除所有
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ActionResult<int>)), HttpPost]
        public async Task<IHttpActionResult> RemoveAllAsync()
        {
            //
            var result = await _employeeService.DeleteAsync(a => true);
            return Succeed(result, "清除成功");
        }
    }
}
