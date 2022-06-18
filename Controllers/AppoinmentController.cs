using System.Linq;
using AppoinmentScudeler.Models.ViewModels;
using AppoinmentScudeler.Services;
using AppoinmentScudeler.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


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
            var currentLoged = User.Identities.ToList()[0].Claims.First().Value;
            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.DocterList = _appionmentServices.GetDocterList();
            ViewBag.PataintList = _appionmentServices.GetPataintList();
            ViewBag.CurrentLogedIn = _appionmentServices.GetPataintByid(currentLoged);
            return View();
        }
    }
}
