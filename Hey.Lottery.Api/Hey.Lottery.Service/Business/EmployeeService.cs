using System;
using Hey.Lottery.Models;
using Hey.Lottery.Repository.Business;

namespace Hey.Lottery.Service.Business
{
    public class EmployeeService : BaseService<Employee>
    {
        public EmployeeService() 
        {
            _repository = new EmployeeRepository();
        }
    }
}
