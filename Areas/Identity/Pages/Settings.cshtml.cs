using System.ComponentModel.DataAnnotations;
using EmolyeePortal.Models;
using EmolyeePortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmolyeePortal.Areas.Identity.Pages
{
    [Authorize]
    public class SettingsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly EmployeeService _employeeService;
        private readonly ILogger<SettingsModel> _logger;

        public SettingsModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            EmployeeService employeeService,
            ILogger<SettingsModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _employeeService = employeeService;
        }

        public class InputModel
        {
            [Required]
            [Display(Name = "Display name")]
            [StringLength(100)]
            public string UserName { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            [Phone]
            [Display(Name = "Phone number")]
            public string? PhoneNumber { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public IList<Employee>? MyEmployees { get; private set; }

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            Input.UserName = await _userManager.GetUserNameAsync(user) ?? string.Empty;
            Input.Email = await _userManager.GetEmailAsync(user) ?? string.Empty;
            Input.PhoneNumber = await _userManager.GetPhoneNumberAsync(user);

            // Try to find employee(s) by email to show quick links
            try
            {
                var (employees, _) = await _employeeService.GetEmployees(Input.Email, null, null, 1, 10);
                MyEmployees = employees;
            }
            catch
            {
                MyEmployees = new List<Employee>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var setNameResult = await _userManager.SetUserNameAsync(user, Input.UserName);
            if (!setNameResult.Succeeded)
            {
                foreach (var err in setNameResult.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
                return Page();
            }

            if (Input.Email != await _userManager.GetEmailAsync(user))
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    foreach (var err in setEmailResult.Errors)
                        ModelState.AddModelError(string.Empty, err.Description);
                    return Page();
                }
            }

            var currentPhone = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != currentPhone)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    foreach (var err in setPhoneResult.Errors)
                        ModelState.AddModelError(string.Empty, err.Description);
                    return Page();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated.";

            return RedirectToPage();
        }
    }
}