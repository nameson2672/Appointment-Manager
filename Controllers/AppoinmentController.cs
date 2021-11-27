using Microsoft.AspNetCore.Mvc;

namespace AppoinmentScudeler.Controllers
{
    public class AppoinmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
