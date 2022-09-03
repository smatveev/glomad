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

        public ushort Type { get; set; }
        public float CostOfProgramm { get;set; }

        public string CostCurrency { get; set; }
        public float Income { get; set; }
    }

    public enum VisaType : ushort
    {
        Startup,
        Work, 
        Tourist,
        Nomad,
        Student,
        Invest,
        Business
    }

    public class VisaSearch
    {
        public int PassportId { get; set; }
        public int CurrentCountryId { get; set; }
        public int DestinationId { get; set; }
    }

    public class VisaSearchResult
    {
        public int? Id { get; set; }
        public string VisaName { get; set; }
        public string Description { get; set; }
        public bool IsExdendable{ get; set; }
        public short Duration { get; set; }
        public List<Review> Reviews { get; set; }
        public string CountryName { get; set; }
        public string Type { get; set; }
        public ushort TypeId { get; set; }
        public float Income { get; set; }
        public string Cost{ get; set; }
        public float CostNum { get; set; }
    }

    public class VisaPage
    {
        public Visa Visa { get; set; }
        public List<Review> Reviews { get; set; }
        public List<VisaSearchResult> Visas { get; set; }
        public List<EmbassyVM> Embassies { get; set; }
        public List<VisaDoc> VisaDocs { get; set; }
    }
}
