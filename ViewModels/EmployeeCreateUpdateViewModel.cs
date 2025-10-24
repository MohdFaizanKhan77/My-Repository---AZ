using EmolyeePortal.Models;
using System.ComponentModel.DataAnnotations;

namespace EmolyeePortal.ViewModels
{
    public class EmployeeCreateUpdateViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Full Name")]

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        public string FullName { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "Department")]

        [Required(ErrorMessage ="Department is required")]
        public int DepartmentId { get; set; }

        [Display(Name = "Designation")]

        [Required(ErrorMessage ="Designation is required")]
        public int DesignationId { get; set; }

        [Display(Name = "Hire Date")]
        [Required(ErrorMessage = "Hire Date is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(EmployeeCreateUpdateViewModel), nameof(ValidationHireDate))]
        public DateTime HireDate { get; set; } = DateTime.Today;

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(EmployeeCreateUpdateViewModel), nameof(ValidationDateOfBirth))]
        public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-60);

        [Display(Name = "Employee type")]

        [Required(ErrorMessage ="Employee type is required")]
        public int EmployeeTypeId { get; set; }

        [Required(ErrorMessage = "Gender is required")]

        public string Gender { get; set; } = null!;

        [Required(ErrorMessage ="Salary is required")]
        [Range(0,double.MaxValue)]

        public decimal Salary { get; set; }

        //for drop down lists
        public List<Department>? Departments { get; set; }
        public List<Designation>? Designations { get; set; }
        public List<EmployeeType>? EmployeeTypes { get; set; }

        //validation methods
        public static ValidationResult? ValidationHireDate(DateTime? hireDate, ValidationContext context)
        {
            if (!hireDate.HasValue)
                return new ValidationResult("Hire Date is required");

            if (hireDate.Value.Date > DateTime.Today)
                return new ValidationResult("Hire Date cannot be in future.");

            return ValidationResult.Success;
        }

        public static ValidationResult? ValidationDateOfBirth(DateTime? dob, ValidationContext context)
        {
            if (!dob.HasValue)
                return new ValidationResult("Date of Birth is required.");
            var mindob = DateTime.Today.AddYears(-60);  //max age is 60 years(adjust as needed)
            var maxdob = DateTime.Today.AddYears(-18);   //min working age is 18 years

            if (dob.Value.Date < mindob || dob.Value.Date > maxdob)
                return new ValidationResult($"Date of birth must in between {mindob:yyy-MM-dd} and {maxdob:yyy-MM-dd}.");

            return ValidationResult.Success;
        }

    }
}
