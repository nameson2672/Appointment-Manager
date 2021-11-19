using Microsoft.AspNetCore.Identity;

namespace AppoinmentScudeler.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
    }
}
