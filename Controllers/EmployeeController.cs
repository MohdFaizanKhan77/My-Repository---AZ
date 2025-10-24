using EmolyeePortal.Models;
using EmolyeePortal.Services;
using EmolyeePortal.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmolyeePortal.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly IEmailSender _emailSender;


        public EmployeeController(EmployeeService employeeService, IEmailSender emailSender)
        {
            _employeeService = employeeService;
            _emailSender = emailSender;
        }
        [HttpGet]
        public async Task<IActionResult> List(
            string? searchTerm,
            int? SelectedDepartmentId,
            int SelectedEmployeeTypeId,
            int pageNumber = 1,
            int pageSize = 5)
        {
            var (employees, totalCount) = await _employeeService.GetEmployees(searchTerm, SelectedDepartmentId, SelectedEmployeeTypeId, pageNumber, pageSize);

            var viewModel = new EmolyeeListViewModel
            {
                Employees = employees,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Total = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                SearchTerm = searchTerm,
                SelectedDepartmentId = SelectedDepartmentId,
                SelectedEmployeeTypeId = SelectedEmployeeTypeId,
                Departments = await _employeeService.GetDepartmentsAsync(),
                EmployeeTypes = await _employeeService.GetEmployeeTypesAsync()

            };
            return View(viewModel);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new EmployeeCreateUpdateViewModel
            {
                Departments = await _employeeService.GetDepartmentsAsync(),
                EmployeeTypes = await _employeeService.GetEmployeeTypesAsync(),
                Designations = new List<Designation>()

            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    FullName = vm.FullName,
                    Email = vm.Email,
                    DepartmentId = vm.DepartmentId,
                    DesignationId = vm.DesignationId,
                    HireDate = vm.HireDate,
                    DateOfBirth = vm.DateOfBirth,
                    EmployeeTypeId = vm.EmployeeTypeId,
                    Gender = vm.Gender,
                    Salary = vm.Salary

                };
                await _employeeService.CreateEmployeeAsync(employee);
                return RedirectToAction("Success", new { id = employee.Id });

            }
            vm.Departments = await _employeeService.GetDepartmentsAsync();
            vm.EmployeeTypes = await _employeeService.GetEmployeeTypesAsync();
            vm.Designations = await _employeeService.GetDesignationsByDepartmentAsync(vm.DepartmentId);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            var vm = new EmployeeCreateUpdateViewModel
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                DesignationId = employee.DesignationId,
                HireDate = employee.HireDate,
                DateOfBirth = employee.HireDate,
                EmployeeTypeId = employee.EmployeeTypeId,
                Gender = employee.Gender,
                Salary = employee.Salary,
                Departments = await _employeeService.GetDepartmentsAsync(),
                EmployeeTypes = await _employeeService.GetEmployeeTypesAsync(),
                Designations = await _employeeService.GetDesignationsByDepartmentAsync(employee.DepartmentId)
                

            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = vm.Id,
                    FullName = vm.FullName,
                    Email = vm.Email,
                    DepartmentId = vm.DepartmentId,
                    DesignationId = vm.DesignationId,
                    HireDate = vm.HireDate,
                    DateOfBirth = vm.HireDate,
                    EmployeeTypeId = vm.EmployeeTypeId,
                    Gender = vm.Gender,
                    Salary = vm.Salary

                };
                await _employeeService.UpdateEmployeeAsync(employee);
                TempData["Message"] = $"Employee with Id {employee.Id} and Name {employee.FullName} has been updated";
                return RedirectToAction("List");
            }
            vm.Departments = await _employeeService.GetDepartmentsAsync();
            vm.EmployeeTypes = await _employeeService.GetEmployeeTypesAsync();
            vm.Designations = await _employeeService.GetDesignationsByDepartmentAsync(vm.DesignationId);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            return View(employee);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            await _employeeService.DeleteEmployeeAsync(id);
            TempData["Message"] = $"Employee with Id {id} and Name {employee.FullName} has been deleted";

            return RedirectToAction("List");

        }
        [HttpGet]
        public async Task<JsonResult> GetDesignations(int departmentId)
        {
            var designations = await _employeeService.GetDesignationsByDepartmentAsync(departmentId);

            var result = designations.Select(d => new { id = d.Id, name = d.Name }).ToList();

            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> Success(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return View(employee);

        }
        // Add the SendEmail action here
        [HttpPost]
        public async Task<IActionResult> SendEmail(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null || string.IsNullOrEmpty(employee.Email))
            {
                return NotFound();
            }

                    await _emailSender.SendEmailAsync(
            employee.Email,  // send to that specific employee
            "📢 Important Update from Employee Portal",
            $@"
                <p>Hi <b>{employee.FullName}</b>,</p>
                <p><b>We’re excited to inform you that the Employee Portal is live 🎉.</b></p>
                <p>You can now:</p>
                <ul>
                    <li>Update your personal details</li>
                    <li>Check announcements</li>
                    <li>Collaborate with your team</li>
                </ul>
                <p><b>Stay tuned for more updates!</b></p>
                <br/>
                <p>Best Regards,<br/> <b>HR Team</b></p>
            "
);


            TempData["Message"] = $"Email sent to {employee.FullName} ({employee.Email})";
            return RedirectToAction(nameof(List));
        }
    }
}
         
    