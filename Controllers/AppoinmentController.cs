using AppoinmentScudeler.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppoinmentScudeler.Controllers
{
    public class AppoinmentController : Controller
    {
        private readonly IAppoinmentServices _appionmentServices;

        public AppoinmentController(IAppoinmentServices appoinmentServices)
        {
            _appionmentServices = appoinmentServices;
        }
        public IActionResult Index()
        {
            ViewBag.DocterList =  _appionmentServices.GetDocterList();
            return View();
        }
    }
}
