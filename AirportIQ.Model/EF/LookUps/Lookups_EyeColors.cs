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
    public partial class Lookups_EyeColors
    {
        public Lookups_EyeColors()
        {
            this.Person_PersonBiographics = new HashSet<Lookups_Person_PersonBiographics>();
        }
    
        public string EyeColorCode { get; set; }
        public string EyeColorDescription { get; set; }
        public bool IsActive { get; set; }
        public short SortOrder { get; set; }
        public int C_DataChanges_RowID { get; set; }
    
        public virtual ICollection<Lookups_Person_PersonBiographics> Person_PersonBiographics { get; set; }
    }
    
}