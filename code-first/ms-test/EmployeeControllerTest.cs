using code_first.Controllers;
using code_first.Infrastructure;
using code_first.Models;
using code_first.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ms_test
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private AppDbContext _dbContext;
        private EmployeeController _controller;
        [TestInitialize]
        public void Setup()
        {
            _dbContext = MockDbContext.GetAppDbContext("EmployeeControllerTest");
            _controller = new EmployeeController(null, _dbContext);
        }

        [TestCleanup]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [TestMethod]
        public async Task Can_get_all_employees()
        {
            //using (var _dbContext = MockDbContext.GetAppDbContext("EmployeeControllerTest"))
            //{ }
            //    var _controller = new EmployeeController(null, _dbContext);
                var result = await _controller.GetAll() as OkObjectResult;
                var employees = result.Value as List<Employee>;

             

                Assert.AreEqual(200, result.StatusCode);
                Assert.AreEqual(3, employees.Count);
        }


        [TestMethod]
        public async Task Can_get_an_employee()
        {
            var employeeId = 1;

            var result = await _controller.Get(employeeId) as OkObjectResult;
            var employee = result.Value as Employee;

            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Pham", employee.LastName);
            Assert.AreEqual("Hoang", employee.FirstName);
        }

        [TestMethod]
        public async Task Can_add_employee()
        {
            var employee = new EmployeePostDto()
            {
                LastName = "Le",
                FirstName = "Thien",
            };

            var result = await _controller.Post(employee) as CreatedResult;
            var addedEmployee = result.Value as Employee;
            //var employee = result.Value as Employee;

            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual("Thien", addedEmployee.FirstName);
            Assert.AreEqual("Le", addedEmployee.LastName);
           // Assert.AreEqual("Pham", employee.LastName);
          //  Assert.AreEqual("Hoang", employee.FirstName);
        }


    }
}
