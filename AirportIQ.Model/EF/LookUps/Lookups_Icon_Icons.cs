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
    public partial class Lookups_Icon_Icons
    {
        public Lookups_Icon_Icons()
        {
            this.Icon_PersonDivisionIconXref = new HashSet<Lookups_Icon_PersonDivisionIconXref>();
            this.Division_AgreementBadgeIcons = new HashSet<Lookups_Division_AgreementBadgeIcons>();
            this.Person_BadgeIconPeriods = new HashSet<Lookups_Person_BadgeIconPeriods>();
        }
    
        public short IconID { get; set; }
        public string IconDescription { get; set; }
        public string IconAbbreviation { get; set; }
        public bool IsActive { get; set; }
        public short SortOrder { get; set; }
        public bool IsAllowedOnFirstBadge { get; set; }
        public short C_DataChanges_RowID { get; set; }
    
        public virtual ICollection<Lookups_Icon_PersonDivisionIconXref> Icon_PersonDivisionIconXref { get; set; }
        public virtual ICollection<Lookups_Division_AgreementBadgeIcons> Division_AgreementBadgeIcons { get; set; }
        public virtual ICollection<Lookups_Person_BadgeIconPeriods> Person_BadgeIconPeriods { get; set; }
    }
    
}
