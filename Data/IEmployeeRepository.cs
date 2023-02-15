using CorporateNewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Data
{
   public interface IEmployeeRepository
    {


        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetAllNotApprovedEmployees();
        Task<IEnumerable<Employee>> GetAllApprovedEmployees();
        Task<Employee> FindEmployeeById(int employeeId);
        Task<Employee> FindEmployeeByName(string employeeName);
        Task<bool> CreateEmployee(Employee edt);
        Task<bool> CreateAdmin(Employee edt);
        Task<bool> UpdateEmployee(Employee edt);
        Task<bool> DeleteEmployee(Employee edt);
        Task<bool> ApproveEmployee(Employee edt);
    }
}
