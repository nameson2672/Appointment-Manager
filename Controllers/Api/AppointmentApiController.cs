using AppoinmentScudeler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppoinmentScudeler.Controllers.Api
{
    [Route("api/AddAppoinment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppoinmentServices _appointmentServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;

        public AppointmentApiController(IAppoinmentServices appoinmentServices, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentServices = appoinmentServices;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
