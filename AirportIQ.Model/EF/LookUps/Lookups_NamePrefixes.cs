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
    public partial class Lookups_NamePrefixes
    {
        public Lookups_NamePrefixes()
        {
            this.Person_PersonBiographics = new HashSet<Lookups_Person_PersonBiographics>();
            this.Person_Aliases = new HashSet<Lookups_Person_Aliases>();
            this.Division_Contacts = new HashSet<Lookups_Division_Contacts>();
        }
    
        public string NamePrefixCode { get; set; }
        public string NamePrefixDescription { get; set; }
        public bool IsActive { get; set; }
        public byte SortOrder { get; set; }
        public int C_DataChanges_RowID { get; set; }
    
        public virtual ICollection<Lookups_Person_PersonBiographics> Person_PersonBiographics { get; set; }
        public virtual ICollection<Lookups_Person_Aliases> Person_Aliases { get; set; }
        public virtual ICollection<Lookups_Division_Contacts> Division_Contacts { get; set; }
    }
    
}
