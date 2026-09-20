using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Medexia.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int age => DateTime.Now.Year - DateOfBirth.Year -
                  (DateTime.Today.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
        public bool IsProfileSetupCompleted { get; set; }
        public string ProfilePictureURL { get; set; }
       




    }

}
