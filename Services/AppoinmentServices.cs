using AppoinmentScudeler.Models;
using AppoinmentScudeler.Models.ViewModels;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using AppoinmentScudeler.Utility;

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
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
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
            var patints = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                           select new PataintVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();
            return patints;
        }
    }
}
