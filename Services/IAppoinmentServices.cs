using AppoinmentScudeler.Models.ViewModels;
using System.Collections.Generic;

namespace AppoinmentScudeler.Services
{
    public interface IAppoinmentServices
    {
        public List<DocterVM> GetDocterList();
        public List<PataintVM> GetPataintList();
    }
}
