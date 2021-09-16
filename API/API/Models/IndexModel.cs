﻿using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class IndexModel
    {
        public IndexModel() { 
        }

        [BindProperty]
        public int CitizenshipId { get; set; }

        public string CovidInfo { get; set; }

        public IEnumerable<VisaSearchResult> Visas { get; set; }
        public IEnumerable<VisaSearchResult> VisasNonEntry { get; set; }
    }
}