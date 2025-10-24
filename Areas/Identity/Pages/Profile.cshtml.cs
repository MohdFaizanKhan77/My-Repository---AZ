using System.Collections.Generic;
using System.Threading.Tasks;
using EmolyeePortal.Models;
using EmolyeePortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmolyeePortal.Areas.Identity.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EmployeeService _employeeService;

        public ProfileModel(UserManager<IdentityUser> userManager, EmployeeService employeeService)
        {
            _userManager = userManager;
            _employeeService = employeeService;
        }

        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string? PhoneNumber { get; private set; }

        public IList<Employee>? MyEmployees { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            UserName = await _userManager.GetUserNameAsync(user) ?? string.Empty;
            Email = await _userManager.GetEmailAsync(user) ?? string.Empty;
            PhoneNumber = await _userManager.GetPhoneNumberAsync(user);

            try
            {
                var (employees, _) = await _employeeService.GetEmployees(Email, null, null, 1, 10);
                MyEmployees = employees;
            }
            catch
            {
                MyEmployees = new List<Employee>();
            }

            return Page();
        }
    }
}
