using Employee.ServiceLayer.Model;
using System.Collections.Generic;

namespace Employee.ServiceLayer
{
    public interface IEmployeeService
    {
        EmployeeDto Create(EmployeeDto employee);

        IEnumerable<EmployeeDto> GetAllEmployees();

        IEnumerable<EmployeeDto> GetAllActiveEmployees();

        EmployeeDto GetEmployee(string id);

        EmployeeDto ChangeEmployeeStatus(string id);
    }
}
