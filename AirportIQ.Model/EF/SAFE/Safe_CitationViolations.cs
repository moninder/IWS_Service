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
    public partial class Safe_CitationViolations
    {
        public int SafeCitationID { get; set; }
        public int ViolationID { get; set; }
        public short ViolationTypeID { get; set; }
        public bool IsMovingViolation { get; set; }
        public byte ViolationPoints { get; set; }
        public int C_DataChanges_RowID { get; set; }
        public string Conditions { get; set; }
    
        public virtual Safe_Violations Safe_Violations { get; set; }
        public virtual Safe_ViolationTypes Safe_ViolationTypes { get; set; }
    }
    
}