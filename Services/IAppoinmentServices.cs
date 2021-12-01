﻿using AppoinmentScudeler.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppoinmentScudeler.Services
{
    public interface IAppoinmentServices
    {
        public List<DocterVM> GetDocterList();
        public List<PataintVM> GetPataintList();
        public Task<int> AddUpdate(AppointmentVM model);
    }
}
