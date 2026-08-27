using EmpManagementSystem.Controllers;
using EmpManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmpManagementSystem.Tests
{
    public class EmployeeControllerTests
    {
        // Each test gets its own isolated in-memory database, so tests
        // never interfere with each other and never touch a real DB.
        private static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsAllEmployees()
        {
            // Arrange
            using var context = CreateContext();
            context.Employees.Add(new Employee
            {
                EmployeeCode = "EMP001",
                FullName = "Hassan Abbas",
                Gender = "Male",
                Age = 21,
                Department = "IT",
                LineManager = "Bilal Iqbal",
                JoiningDate = new DateTime(2026, 6, 1)
            });
            await context.SaveChangesAsync();
            var controller = new EmployeeController(context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Employee>>(viewResult.Model);
            Assert.Single(model);
            Assert.Equal("Hassan Abbas", model.First().FullName);
        }

        [Fact]
        public async Task Create_Post_AddsNewEmployeeAndRedirects()
        {
            // Arrange
            using var context = CreateContext();
            var controller = new EmployeeController(context);
            var newEmployee = new Employee
            {
                EmployeeCode = "EMP002",
                FullName = "Ali Khan",
                Gender = "Male",
                Age = 25,
                Department = "Finance",
                LineManager = "Hassan Abbas",
                JoiningDate = new DateTime(2026, 5, 15)
            };

            // Act
            var result = await controller.Create(newEmployee);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Equal(1, await context.Employees.CountAsync());
        }

        [Fact]
        public async Task Delete_RemovesEmployeeFromDatabase()
        {
            // Arrange
            using var context = CreateContext();
            context.Employees.Add(new Employee
            {
                EmployeeCode = "EMP003",
                FullName = "Sara Ahmed",
                Gender = "Female",
                Age = 23,
                Department = "HR",
                LineManager = "Bilal Iqbal",
                JoiningDate = new DateTime(2026, 4, 10)
            });
            await context.SaveChangesAsync();
            var controller = new EmployeeController(context);

            // Act
            await controller.Delete("EMP003");

            // Assert
            Assert.Equal(0, await context.Employees.CountAsync());
        }
    }
}
