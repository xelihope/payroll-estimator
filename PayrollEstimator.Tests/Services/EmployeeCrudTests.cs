using Moq;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using PayrollEstimator.Data.Context;
using PayrollEstimator.Data.Entities;
using PayrollEstimator.Services;

namespace PayrollEstimator.Tests.Services
{
    [TestFixture]
    public class EmployeeCrudTests
    {
        [Test]
        public void Get_ReturnsAllOrderedByName()
        {
            // Arrange
            var dbContextMock = new Mock<IPayrollEstimatorContext>();
            var employees = new List<Employee> {
                new Employee { EmployeeId = 1, FirstName = "Able", LastName = "Gable" },
                new Employee { EmployeeId = 2, FirstName = "Zelda", LastName = "Link" },
                new Employee { EmployeeId = 3, FirstName = "Leroy", LastName = "Jenkins" },
                new Employee { EmployeeId = 4, FirstName = "Zoo", LastName = "Zebra" },
                new Employee { EmployeeId = 5, FirstName = "Alice", LastName = "Wonderland" },
            }.AsQueryable();
            var dbSetMock = EntityFrameworkHelper.GetMockDbSet(employees);
            dbContextMock.Setup(x => x.Employees).Returns(dbSetMock.Object);

            // Act
            var results = new EmployeeCrud(dbContextMock.Object).Get()
                .Result;

            // Assert
            CollectionAssert.AreEqual(employees.Select(e => e.FirstName).OrderBy(x => x), 
                results.Select(x => x.FirstName));
        }

        [Test]
        public void Create_SimpleEmployee_SavesNewEmployee()
        {
            // Arrange
            var dbContextMock = new Mock<IPayrollEstimatorContext>();
            var employees = EntityFrameworkHelper.GetMockDbSet(new List<Employee>().AsQueryable());
            dbContextMock.Setup(x => x.Employees).Returns(employees.Object);

            var employeeData = new Mock<IEmployeeData>();
            employeeData.Setup(d => d.Dependents).Returns(new List<IDependentData> {
                GetDependent(), GetDependent(), GetDependent()
            });

            // Act
            var result = new EmployeeCrud(dbContextMock.Object).Create(employeeData.Object);

            // Assert
            dbContextMock.Verify(d => d.SaveChangesAsync(), Times.Once);
            employees.Verify(x => x.Add(It.Is<Employee>(p => p.Dependents.Count == 3)), 
                Times.Once);
        }

        private IDependentData GetDependent()
        {
            var dependent = new Mock<IDependentData>();
            return dependent.Object;
        }

        [Test]
        public void Create_DoesNotThrowOnNulls()
        {
            // Arrange
            var dbContextMock = new Mock<IPayrollEstimatorContext>();
            var employees = EntityFrameworkHelper.GetMockDbSet(new List<Employee>().AsQueryable());
            dbContextMock.Setup(x => x.Employees).Returns(employees.Object);

            var employeeData = new Mock<IEmployeeData>();
            employeeData.Setup(d => d.Dependents).Returns((List<IDependentData>)null);

            // Act
            var result = new EmployeeCrud(dbContextMock.Object).Create(employeeData.Object);

            // Assert
            dbContextMock.Verify(d => d.SaveChangesAsync(), Times.Once);
            employees.Verify(x => x.Add(It.Is<Employee>(p => p.Dependents.Count == 0)), Times.Once);
        }
    }
}
