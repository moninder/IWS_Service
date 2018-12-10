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
    public partial class Lookups_Common_Requirement_DocumentTypes
    {
        public Lookups_Common_Requirement_DocumentTypes()
        {
            this.Common_Requirement_DocumentsRequired = new HashSet<Lookups_Common_Requirement_DocumentsRequired>();
        }
    
        public short DocumentTypeNumber { get; set; }
        public string DocumentTypeDescription { get; set; }
        public string EntityTypeCode { get; set; }
        public bool RequiresIssuingAuthority { get; set; }
        public bool RequiresIdentificationNumber { get; set; }
        public bool RequiresExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public short SortOrder { get; set; }
        public short C_DataChanges_RowID { get; set; }
        public bool I9Compliant { get; set; }
        public bool Requires_CountrySubdivisionCode { get; set; }
        public string Force_CountryCode { get; set; }
        public string LegalStatusCode { get; set; }
        public Nullable<bool> ShortDate { get; set; }
        public Nullable<bool> RequiresExpirationDate_Optional { get; set; }
        public Nullable<bool> Requires_CountrySubdivisionCode_Optional { get; set; }
        public Nullable<bool> RequiresIssuingAuthority_School { get; set; }
    
        public virtual Lookups_Common_EntityTypes Common_EntityTypes { get; set; }
        public virtual ICollection<Lookups_Common_Requirement_DocumentsRequired> Common_Requirement_DocumentsRequired { get; set; }
        public virtual Lookups_Common_Requirement_LegalStatusTypes Common_Requirement_LegalStatusTypes { get; set; }
    }
    
}