﻿namespace AppoinmentScudeler.Models.ViewModels
{
    public class AppointmentVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Duration { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public bool IsDoctorApproved { get; set; }
        public string AdminId { get; set; }

        public string DoctorName { get; set; }
        public string PatintName { get; set; }
        public string AdminName { get; set; }
    }
}
