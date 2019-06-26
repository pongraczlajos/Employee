using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Employee.DataLayer.Repositories;
using Employee.ServiceLayer.Model;

namespace Employee.ServiceLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;

        private readonly IMapper mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public EmployeeDto ChangeEmployeeStatus(string id)
        {
            var employee = repository.GetById(id);
            employee.Status = !employee.Status;
            repository.Update(employee);
            repository.SaveChanges();

            return mapper.Map<EmployeeDto>(employee);
        }

        public EmployeeDto Create(EmployeeDto employeeDto)
        {
            var employee = mapper.Map<DataLayer.Model.Employee>(employeeDto);
            repository.Create(employee);
            repository.SaveChanges();

            return mapper.Map<EmployeeDto>(employee);
        }

        public IEnumerable<EmployeeDto> GetAllActiveEmployees()
        {
            return repository.GetEntities().Where(e => e.Status == true).ProjectTo<EmployeeDto>(mapper.ConfigurationProvider).ToList();
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return repository.GetEntities().ProjectTo<EmployeeDto>(mapper.ConfigurationProvider).ToList();
        }

        public EmployeeDto GetEmployee(string id)
        {
            return mapper.Map<EmployeeDto>(repository.GetById(id));
        }
    }
}
