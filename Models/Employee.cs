using System.ComponentModel.DataAnnotations;

namespace EmpManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string LineManager { get; set; }
        public string Department { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}