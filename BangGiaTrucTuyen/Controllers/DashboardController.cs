using Microsoft.AspNetCore.Mvc;

namespace BangGiaTrucTuyen.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
