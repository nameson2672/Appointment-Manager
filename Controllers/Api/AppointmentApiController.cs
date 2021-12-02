using AppoinmentScudeler.Models.ViewModels;
using AppoinmentScudeler.Services;
using AppoinmentScudeler.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AppoinmentScudeler.Controllers.Api
{
    [Route("api/Appointment")]
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
        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalenderData(AppointmentVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                int Code = _appointmentServices.AddUpdate(data).Result;
                Console.WriteLine(Code);
                commonResponse.Status = Code;
                if (commonResponse.Status == 1)
                {
                    commonResponse.Message = Helper.appointmentUpdated;
                }
                if (commonResponse.Status == 2)
                {
                    commonResponse.Message = Helper.appointmentAdded;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string doctorId)
        {
            CommonResponse<List<AppointmentVM>> commonResponse = new CommonResponse<List<AppointmentVM>>();
            try
            {
                if (role == Helper.Patient)
                {
                    commonResponse.Dataenum = _appointmentServices.PatientsEventsById(loginUserId);
                    commonResponse.Status = Helper.success_code;
                }
                else if (role == Helper.Doctor)
                {
                    commonResponse.Dataenum = _appointmentServices.DoctorsEventsById(loginUserId);
                    commonResponse.Status = Helper.success_code;
                }
                else
                {
                    commonResponse.Dataenum = _appointmentServices.DoctorsEventsById(doctorId);
                    commonResponse.Status = Helper.success_code;
                }
            }
            catch (Exception e)
            {
                commonResponse.Message = e.Message;
                commonResponse.Status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

    }
}
