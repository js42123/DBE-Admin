//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBESearchAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompanyNAICSCode
    {
        public string NAICSCode { get; set; }
        public int Companyid { get; set; }
        public string Comments { get; set; }
    
        public virtual DBECompany DBECompany { get; set; }
        public virtual NAICSCode NAICSCode1 { get; set; }
    }
}