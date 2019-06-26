using AutoMapper;

namespace Employee.ServiceLayer.Model
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<DataLayer.Model.Employee, EmployeeDto>();
            CreateMap<EmployeeDto, DataLayer.Model.Employee>();
            CreateMap<DataLayer.Model.Employee, DataLayer.Model.Employee>();
        }
    }
}
