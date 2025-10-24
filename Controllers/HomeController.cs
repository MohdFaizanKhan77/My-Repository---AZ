using Microsoft.AspNetCore.Mvc;

namespace EmolyeePortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
