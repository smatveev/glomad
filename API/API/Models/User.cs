using Glomad.Models.Abstract;
using Glomad.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models
{
    public class User : IdentityUser<int>, IAuditableEntity
    {
        public City CurrentCity { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IList<UserInterest> Interests { get; set; } 
    }

    public class UserProfileData
    {
        public IEnumerable<Interest> UserInterests { get; set; }
        public IEnumerable<Interest> Interests { get; set; }
        public IEnumerable<Survey> Surveys { get; set; }
    }
}
