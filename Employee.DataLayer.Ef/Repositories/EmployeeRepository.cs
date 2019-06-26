using AutoMapper;
using Employee.DataLayer.Repositories;

namespace Employee.DataLayerEf.Repositories
{
    public class EmployeeRepository : RepositoryBase<DataLayer.Model.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override DataLayer.Model.Employee GetEntity(DataLayer.Model.Employee entity)
        {
            return GetById(entity.EmployeeId);
        }
    }
}
