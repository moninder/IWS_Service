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

namespace AirportIQ.Model.EF.SAFE
{
    public partial class Badges_Info
    {
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public int BadgeID { get; set; }
        public string BadgeNumber { get; set; }
        public int CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CorporationName { get; set; }
        public int DivisionID { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string CountrySubdivisionCode { get; set; }
        public string PostalCode { get; set; }
        public System.DateTime WhenBecomesActive { get; set; }
        public System.DateTime WhenExpires { get; set; }
        public int PersonDivisionXrefID { get; set; }
    }
    
}
