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
    public partial class Lookups_Common_Requirement_Documents
    {
        public int DocumentID { get; set; }
        public int EntityID { get; set; }
        public string RequirementCode { get; set; }
        public short DocumentTypeNumber { get; set; }
        public string DocumentFilename { get; set; }
        public string DocumentDescription { get; set; }
        public string ContentTypeCode { get; set; }
        public byte[] DocumentImage { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public string IssuingAuthority_CountryCode { get; set; }
        public string IssuingAuthority_CountrySubdivisionCode { get; set; }
        public string IdentificationNumber { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public string C_Legacy_DatabaseName { get; set; }
        public string C_Legacy_TableName { get; set; }
        public int C_DataChanges_RowID { get; set; }
        public System.DateTime WhenSubmitted { get; set; }
        public string IssuingAuthority_School { get; set; }
    
        public virtual Lookups_Common_Entities Common_Entities { get; set; }
        public virtual Lookups_Common_Requirement_DocumentsRequired Common_Requirement_DocumentsRequired { get; set; }
        public virtual Miscellaneous_CountrySubdivisions Miscellaneous_CountrySubdivisions { get; set; }
    }
    
}