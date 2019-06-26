using AutoMapper;
using Employee.DataLayer.Repositories;
using Employee.ServiceLayer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Employee.ServiceLayer.Tests
{
    [TestClass]
    public class EmployeeServiceUnitTest
    {
        [TestMethod]
        public void ChangeEmployeeStatus_FromInactiveToActive_Success()
        {
            // Arrange
            (var repository, var mapper) = GetBaseSetup();
            repository.Setup(r => r.GetById("john.smith")).Returns(new DataLayer.Model.Employee()
            {
                EmployeeId = "john.smith",
                Status = false
            });
            var service = new EmployeeService(repository.Object, mapper);

            // Act
            var dto = service.ChangeEmployeeStatus("john.smith");

            // Assert
            Assert.IsTrue(dto.Status);
        }

        [TestMethod]
        public void GetAllEmployees_Success()
        {
            // Arrange
            (var repository, var mapper) = GetBaseSetup();
            repository.Setup(r => r.GetEntities()).Returns(GetEntities().AsQueryable());
            var service = new EmployeeService(repository.Object, mapper);

            // Act
            var employees = service.GetAllEmployees();

            // Assert
            Assert.AreEqual(GetEntities().Count(), employees.Count());
        }

        [TestMethod]
        public void GetAllActiveEmployees_Success()
        {
            // Arrange
            (var repository, var mapper) = GetBaseSetup();
            repository.Setup(r => r.GetEntities()).Returns(GetEntities().AsQueryable());
            var service = new EmployeeService(repository.Object, mapper);

            // Act
            var employees = service.GetAllActiveEmployees();

            // Assert
            Assert.AreEqual(3, employees.Count());
        }

        [TestMethod]
        public void GetEmployee_Success()
        {
            // Arrange
            (var repository, var mapper) = GetBaseSetup();
            var entity = new DataLayer.Model.Employee()
            {
                EmployeeId = "john.smith",
                Status = false
            };
            repository.Setup(r => r.GetById("john.smith")).Returns(entity);
            var service = new EmployeeService(repository.Object, mapper);

            // Act
            var dto = service.GetEmployee("john.smith");

            // Assert
            Assert.AreEqual("john.smith", dto.EmployeeId);
            Assert.AreEqual(false, dto.Status);
        }

        [TestMethod]
        public void CreateEmployee_Success()
        {
            // Arrange
            (var repository, var mapper) = GetBaseSetup();
            repository.Setup(r => r.GetEntities()).Returns(GetEntities().AsQueryable());
            var service = new EmployeeService(repository.Object, mapper);
            var oldDto = new EmployeeDto()
            {
                EmployeeId = "john.snow",
                FirstName = "John",
                LastName = "Snow",
                Status = true
            };

            // Act
            var newDto = service.Create(oldDto);

            // Assert
            Assert.AreEqual(oldDto.EmployeeId, newDto.EmployeeId);
            Assert.AreEqual(oldDto.FirstName, newDto.FirstName);
            Assert.AreEqual(oldDto.LastName, newDto.LastName);
            Assert.AreEqual(oldDto.Status, newDto.Status);
        }

        private (Mock<IEmployeeRepository> repository, IMapper mapper) GetBaseSetup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mappings());
            });
            var mapper = mappingConfig.CreateMapper();
            var repository = new Mock<IEmployeeRepository>();

            return (repository, mapper);
        }

        private IEnumerable<DataLayer.Model.Employee> GetEntities()
        {
            return new List<DataLayer.Model.Employee>()
            {
                new DataLayer.Model.Employee()
                {
                    EmployeeId = "john.smith",
                    Status = false
                },
                new DataLayer.Model.Employee()
                {
                    EmployeeId = "andrea.pirlo",
                    Status = true
                },
                new DataLayer.Model.Employee()
                {
                    EmployeeId = "johnny.depp",
                    Status = false
                },
                new DataLayer.Model.Employee()
                {
                    EmployeeId = "kate.winslet",
                    FirstName = "Kate",
                    LastName = "Winslet",
                    Status = true
                },
                new DataLayer.Model.Employee()
                {
                    EmployeeId = "ursula.drbubo",
                    Status = true
                },
            };
        }
    }
}
