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
    public partial class Lookups_Common_Requirement_DocumentsRequired
    {
        public Lookups_Common_Requirement_DocumentsRequired()
        {
            this.Common_Requirement_Documents = new HashSet<Lookups_Common_Requirement_Documents>();
        }
    
        public string RequirementCode { get; set; }
        public short DocumentTypeNumber { get; set; }
        public bool IsActive { get; set; }
        public short SortOrder { get; set; }
        public int C_DataChanges_RowID { get; set; }
        public string DropdownName { get; set; }
    
        public virtual Lookups_Common_Requirement_Requirements Common_Requirement_Requirements { get; set; }
        public virtual ICollection<Lookups_Common_Requirement_Documents> Common_Requirement_Documents { get; set; }
        public virtual Lookups_Common_Requirement_DocumentTypes Common_Requirement_DocumentTypes { get; set; }
    }
    
}