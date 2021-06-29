using Glomad.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models.Abstract
{
    public class AuditableEntity : IAuditableEntity
    {
        [MaxLength(256)]
        public int CreatedBy { get; set; }
        [MaxLength(256)]
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
