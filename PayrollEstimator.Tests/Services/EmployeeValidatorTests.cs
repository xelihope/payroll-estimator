using Moq;
using NUnit.Framework;
using PayrollEstimator.Services;
using System.Collections.Generic;

namespace PayrollEstimator.Tests.Services
{
    [TestFixture]
    public class EmployeeValidatorTests
    {
        [Test]
        public void GetErrors_ReturnsNoErrors()
        {
            // Arrange
            var employee = CreateTestEmployee("Firstname", "Lastname", 
                new List<string> { "Firstname" }, new List<string> { "Lastname" });

            // Act
            var result = new EmployeeValidator().GetErrors(employee);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestCase("", "Lastname")]
        [TestCase("Firstname", "")]
        [TestCase("Firstname", null)]
        [TestCase(null, null)]
        [TestCase("    \n", "Lastname")]
        public void GetErrors_ReturnsNameRequiredError(string firstName, string lastName)
        {
            // Arrange
            var employee = CreateTestEmployee(firstName, lastName);

            // Act
            var result = new EmployeeValidator().GetErrors(employee);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(Resources.Employee.NameRequiredError, result[0]);
        }

        [TestCase("", "Lastname")]
        [TestCase("Firstname", "")]
        [TestCase("Firstname", null)]
        [TestCase(null, null)]
        [TestCase("    \n", "Lastname")]
        public void GetErrors_ReturnsDependentNameRequiredError(string firstName, string lastName)
        {
            // Arrange
            var employee = CreateTestEmployee("Firstname", "Lastname", 
                new List<string> { firstName }, new List<string> { lastName });

            // Act
            var result = new EmployeeValidator().GetErrors(employee);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(Resources.Employee.DependentNameRequiredError, result[0]);
        }

        private IEmployeeData CreateTestEmployee(string firstName, string lastName,
            IList<string> dependentFirstNames = null, IList<string> dependentLastNames = null)
        {
            var mockEmployee = new Mock<IEmployeeData>();
            mockEmployee.Setup(x => x.FirstName).Returns(firstName);
            mockEmployee.Setup(x => x.LastName).Returns(lastName);
            var dependents = new List<IDependentData>();
            if (dependentFirstNames != null) {
                for (int i = 0; i < dependentFirstNames.Count; i++) {
                    var mockDependent = new Mock<IDependentData>();
                    mockDependent.Setup(x => x.FirstName).Returns(dependentFirstNames[i]);
                    mockDependent.Setup(x => x.LastName).Returns(dependentLastNames[i]);
                    dependents.Add(mockDependent.Object);
                }
                mockEmployee.Setup(x => x.Dependents).Returns(dependents);
            }

            return mockEmployee.Object;
        }
    }
}
