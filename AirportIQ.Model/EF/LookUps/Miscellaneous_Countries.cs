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
    public partial class Miscellaneous_Countries
    {
        public Miscellaneous_Countries()
        {
            this.Miscellaneous_CountrySubdivisions = new HashSet<Miscellaneous_CountrySubdivisions>();
            this.Person_PersonBiographics = new HashSet<Lookups_Person_PersonBiographics>();
            this.LEO_PoliceDepartments = new HashSet<LEO_PoliceDepartments>();
        }
    
        public string CountryCode { get; set; }
        public string CountryDescription { get; set; }
        public string CountrySubdivisionTypeName { get; set; }
        public short InternationalCallPrefix { get; set; }
        public bool IsActive { get; set; }
        public short SortOrder { get; set; }
        public int C_DataChanges_RowID { get; set; }
        public string NCICCountryCode { get; set; }
    
        public virtual ICollection<Miscellaneous_CountrySubdivisions> Miscellaneous_CountrySubdivisions { get; set; }
        public virtual ICollection<Lookups_Person_PersonBiographics> Person_PersonBiographics { get; set; }
        public virtual ICollection<LEO_PoliceDepartments> LEO_PoliceDepartments { get; set; }
    }
    
}
