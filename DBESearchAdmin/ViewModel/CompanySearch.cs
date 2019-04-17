﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBESearchAdmin.ViewModel
{
    using DBESearchAdmin.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using DBESearchAdmin.ViewModel;

    public partial class CompanySearch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompanySearch()
        {
            this.CompanyItemCodes = new HashSet<CompanyItemCode>();
            this.CompanyNAICSCodes = new HashSet<CompanyNAICSCode>();
        }

        [DisplayName("Company Id")]
        public int CompanyId { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("DBA Name")]
        public string DBAName { get; set; }
        [DisplayName("First Name")]
        public string OwnersFirstName { get; set; }
        [DisplayName("Last Name")]
        public string OwnersLastName { get; set; }
        [DisplayName("Address")]
        public string CompanyAddress { get; set; }
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        [DisplayName("Zip")]

        public string Zip { get; set; }
        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public string Phone { get; set; }
        [DisplayName("Fax")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public string Fax { get; set; }
        [DisplayName("District")]
        public string District { get; set; }

        public Nullable<bool> DBE { get; set; }

        public Nullable<bool> ACDBE { get; set; }

        public Nullable<bool> SBP { get; set; }
        public Nullable<bool> MBE { get; set; }
        public Nullable<bool> Certified { get; set; }
        [DisplayName("Date Certified")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CertificationDate { get; set; }
        [DisplayName("Date Decertified")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DecertificationDate { get; set; }
        [DisplayName("Reason Decertified")]
        public string DecertReason { get; set; }

        public Nullable<bool> Suspended { get; set; }
        [DisplayName("Suspension Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Suspension_Date { get; set; }

        public Nullable<bool> DWBE { get; set; }

        public Nullable<bool> WBE { get; set; }

        public Nullable<bool> ACDWBE { get; set; }
        [DisplayName("Small Buisness")]
        public Nullable<bool> SmallBusiness { get; set; }
        [DisplayName("On Site Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> OnSiteReviewDate { get; set; }
        [DisplayName("Type of Firm")]
        public string TypeofFirm { get; set; }
        [DisplayName("Date Requested Last On-Site")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateRequestedLastOnSite { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Desk Audit Review"), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string DeskAuditReview { get; set; }
        [DisplayName("Race")]
        public string Race { get; set; }
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [DisplayName("Annual Affidavit")]
        public string MonthofAnnualAffidavit { get; set; }
        [DisplayName("Renewal Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> RenewalDate { get; set; }


        public Nullable<bool> ACWBE { get; set; }
        public string County { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyItemCode> CompanyItemCodes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompanyNAICSCode> CompanyNAICSCodes { get; set; }

        public List<CompanyItemCode> CompanyItemCodesList { get; set; }
        public List<CompanyNAICSCode> CompanyNAICSCodesList { get; set; }
    }
}
