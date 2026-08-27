using EmpManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpManagementSystem.Controllers
{
    public class SalaryController : Controller
    {
        private readonly AppDbContext _context;

        public SalaryController(AppDbContext context)
        {
            _context = context;
        }

        // Show all salaries
        public async Task<IActionResult> Index()
        {
            var salaries = await _context.Salaries.Include(s => s.Employee).ToListAsync();
            return View(salaries);
        }

        // Show add form
        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }

        // Save new salary
        [HttpPost]
        public async Task<IActionResult> Create(Salary salary)
        {
            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Show edit form
        public async Task<IActionResult> Edit(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            ViewBag.Employees = _context.Employees.ToList();
            return View(salary);
        }

        // Save edited salary
        [HttpPost]
        public async Task<IActionResult> Edit(Salary salary)
        {
            _context.Salaries.Update(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Delete salary
        public async Task<IActionResult> Delete(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}