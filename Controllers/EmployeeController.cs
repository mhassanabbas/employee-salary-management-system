using EmpManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // Show all employees
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }

        // Show add form
        public IActionResult Create()
        {
            return View();
        }

        // Save new employee
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Show edit form
        public async Task<IActionResult> Edit(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return View(employee);
        }

        // Save edited employee
        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Delete employee
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}