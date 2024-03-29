﻿using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class VisaDoc
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Visa Visa { get; set; }
        public ushort DocumentType { get; set; }
        public string TextRu { get; set; }
    }

    public enum DocumentType : ushort
    {
        Passport,
        Photo, 
        Ticket,
        FinanceProof,
        ApplicationForm,
        HealthInsurance,
        Contract, 
        Criminal,
        PlaceOfStay,
        PaymentProof,
        Copies,
        Diplom,
        CV,
        CheckList,
        BirthCertificate,
        Other
    }
}
