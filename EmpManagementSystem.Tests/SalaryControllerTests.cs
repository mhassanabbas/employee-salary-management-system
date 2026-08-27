using EmpManagementSystem.Controllers;
using EmpManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmpManagementSystem.Tests
{
    public class SalaryControllerTests
    {
        private static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        private static async Task<Employee> SeedEmployee(AppDbContext context)
        {
            var employee = new Employee
            {
                EmployeeCode = "EMP001",
                FullName = "Hassan Abbas",
                Gender = "Male",
                Age = 21,
                Department = "IT",
                LineManager = "Bilal Iqbal",
                JoiningDate = new DateTime(2026, 6, 1)
            };
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
            return employee;
        }

        [Fact]
        public async Task Index_ReturnsSalariesWithLinkedEmployee()
        {
            // Arrange
            using var context = CreateContext();
            var employee = await SeedEmployee(context);
            context.Salaries.Add(new Salary
            {
                EmployeeCode = employee.EmployeeCode,
                SalaryType = "Monthly",
                Amount = 50000,
                Month = "July",
                Year = 2026
            });
            await context.SaveChangesAsync();
            var controller = new SalaryController(context);

            // Act
            var result = await controller.Index();

            // Assert — confirms the foreign-key relationship actually resolves
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Salary>>(viewResult.Model);
            var salary = Assert.Single(model);
            Assert.NotNull(salary.Employee);
            Assert.Equal("Hassan Abbas", salary.Employee.FullName);
        }

        [Fact]
        public async Task Create_Post_AddsSalaryLinkedToEmployee()
        {
            // Arrange
            using var context = CreateContext();
            var employee = await SeedEmployee(context);
            var controller = new SalaryController(context);
            var newSalary = new Salary
            {
                EmployeeCode = employee.EmployeeCode,
                SalaryType = "Monthly",
                Amount = 50000,
                Month = "July",
                Year = 2026
            };

            // Act
            var result = await controller.Create(newSalary);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Equal(1, await context.Salaries.CountAsync());
        }
    }
}
