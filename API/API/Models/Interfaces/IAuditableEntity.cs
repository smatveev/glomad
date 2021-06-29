using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models.Interfaces
{
    public interface IAuditableEntity
    {
        int CreatedBy { get; set; }
        int UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
