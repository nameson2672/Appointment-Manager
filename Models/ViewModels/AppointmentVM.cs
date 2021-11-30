using System;

namespace AppoinmentScudeler.Models.ViewModels
{
    public class AppointmentVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string DoctorId { get; set; }
        public string PataintId { get; set; }
        public bool IsDoctorApproved { get; set; }
        public string AdminId { get; set; }

        public string DoctorName { get; set; }
        public string PataintName { get; set; }
        public string AdminName { get; set; }
    }
}
