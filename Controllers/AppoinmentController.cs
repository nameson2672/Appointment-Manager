using AppoinmentScudeler.Services;
using AppoinmentScudeler.Utility;
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
            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.DocterList = _appionmentServices.GetDocterList();
            ViewBag.PataintList = _appionmentServices.GetPataintList();
            return View();
        }
    }
}
