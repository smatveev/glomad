using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models
{
    public class Interest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IList<UserInterest> UserInterests { get; set; }
    }

    public class UserInterest
    {
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
