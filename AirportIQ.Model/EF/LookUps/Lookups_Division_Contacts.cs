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
    public partial class Lookups_Division_Contacts
    {
        public int ContactID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> DivisionID { get; set; }
        public short ContactTypeID { get; set; }
        public string NamePrefixCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameSuffixCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string CountrySubdivisionCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string Work1PhonePrefix { get; set; }
        public string Work1PhoneNumber { get; set; }
        public string Work1PhoneExtension { get; set; }
        public string Work2PhonePrefix { get; set; }
        public string Work2PhoneNumber { get; set; }
        public string Work2PhoneExtension { get; set; }
        public string MobilePhonePrefix { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string FaxPhonePrefix { get; set; }
        public string FaxPhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string AlternateEmailAddress { get; set; }
        public System.DateTime WhenContactBegins { get; set; }
        public System.DateTime WhenContactEnds { get; set; }
        public string C_Legacy_DatabaseName { get; set; }
        public string C_Legacy_TableName { get; set; }
        public int C_DataChanges_RowID { get; set; }
    }
    
}