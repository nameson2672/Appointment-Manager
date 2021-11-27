using AppoinmentScudeler.Models;
using AppoinmentScudeler.Models.ViewModels;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace AppoinmentScudeler.Services
{
    public class AppoinmentServices : IAppoinmentServices
    {
        private readonly ApplicationDbContext _db;

        public AppoinmentServices(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<DocterVM> GetDocterList()
        {
            var doctors = (from user in _db.Users
                           select new DocterVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();
            return doctors;
        }

        public List<PataintVM> GetPataintList()
        {
            throw new System.NotImplementedException();
        }
    }
}
