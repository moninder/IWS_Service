//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace AirportIQ.Model.EF.LookUps
{
    public partial class Lookups_Division_Divisions
    {
        public Lookups_Division_Divisions()
        {
            this.Division_Contacts = new HashSet<Lookups_Division_Contacts>();
            this.Division_DivisionNotes = new HashSet<Lookups_Division_DivisionNotes>();
        }
    
        public int DivisionID { get; set; }
        public int CompanyID { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public Nullable<short> DivisionTypeID { get; set; }
        public string DivisionStatusCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string CountrySubdivisionCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<System.DateTime> WhenEnrolled { get; set; }
        public bool FingerprintRequired { get; set; }
        public bool BackgroundCheckRequired { get; set; }
        public System.DateTime WhenLastUpdated { get; set; }
        public int StaffID_LastUpdated { get; set; }
        public string CategoryCode { get; set; }
        public string ProviderNumber { get; set; }
        public string FacilityCode { get; set; }
        public decimal NewBadgeCost { get; set; }
        public decimal ReplaceBadgeCost { get; set; }
        public decimal BadgeBillingCost { get; set; }
        public decimal BadgeCreditCost { get; set; }
        public string C_Legacy_DatabaseName { get; set; }
        public string C_Legacy_TableName { get; set; }
        public Nullable<int> C_Legacy_DIV_ID { get; set; }
        public int C_DataChanges_RowID { get; set; }
        public string RAMSAgreementNumber { get; set; }
        public Nullable<System.DateTime> RAMSExpirationDate { get; set; }
    
        public virtual Miscellaneous_CountrySubdivisions Miscellaneous_CountrySubdivisions { get; set; }
        public virtual Lookups_Company_Companies Company_Companies { get; set; }
        public virtual ICollection<Lookups_Division_Contacts> Division_Contacts { get; set; }
        public virtual ICollection<Lookups_Division_DivisionNotes> Division_DivisionNotes { get; set; }
    }
    
}
