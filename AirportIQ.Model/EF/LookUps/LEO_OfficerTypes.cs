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
    public partial class LEO_OfficerTypes
    {
        public LEO_OfficerTypes()
        {
            this.LEO_OfficerRanksAndTitles = new HashSet<LEO_OfficerRanksAndTitles>();
        }
    
        public byte OfficerTypeID { get; set; }
        public string OfficerTypeDescription { get; set; }
        public string OfficerTypeAbbreviation { get; set; }
        public bool IsActive { get; set; }
        public short SortOrder { get; set; }
        public byte C_DataChanges_RowID { get; set; }
    
        public virtual ICollection<LEO_OfficerRanksAndTitles> LEO_OfficerRanksAndTitles { get; set; }
    }
    
}
