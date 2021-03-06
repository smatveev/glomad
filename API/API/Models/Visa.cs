﻿using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Visa
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public Country Country { get; set; }
        public short Duration { get; set; }
        public bool IsExtendable { get; set; }
        public string Description { get; set; }
    }

    public class VisaSearch
    {
        public int PassportId { get; set; }
        public int CurrentCountryId { get; set; }
        public int DestinationId { get; set; }
    }

    public class VisaSearchResult
    {
        public int Id { get; set; }
        public string VisaName { get; set; }
        public string Description { get; set; }
        public bool IsExdendable{ get; set; }
        public short Duration { get; set; }
    }
}
