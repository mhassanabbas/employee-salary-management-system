using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpManagementSystem.Models
{
    public class Salary
    {
        [Key]
        public int SalaryID { get; set; }

        public string EmployeeCode { get; set; }

        [ForeignKey("EmployeeCode")]
        public Employee Employee { get; set; }

        public string SalaryType { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
    }
}