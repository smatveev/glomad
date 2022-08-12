using Glomad.Models;
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
    }

    enum DocumentType : ushort
    {
        Passport,
        Photo, 
        Ticket,
        FinanceProof,
        ApplicationForm
    }
}
