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
    public partial class Lookups_Access_Categories
    {
        public Lookups_Access_Categories()
        {
            this.Division_AgreementAccessDefaults = new HashSet<Lookups_Division_AgreementAccessDefaults>();
            this.Access_PersonDivisionCategoryXref = new HashSet<Lookups_Access_PersonDivisionCategoryXref>();
            this.Access_CategoryAreaMapping = new HashSet<Lookups_Access_CategoryAreaMapping>();
            this.Person_BadgeAccessPeriods = new HashSet<Lookups_Person_BadgeAccessPeriods>();
        }
    
        public int CategoryID { get; set; }
        public string FacilityCode { get; set; }
        public string CategoryName { get; set; }
        public int SourceCategoryPKID { get; set; }
        public bool IsActive { get; set; }
        public short SortOrder { get; set; }
        public string C_Legacy_DatabaseName { get; set; }
        public string C_Legacy_TableName { get; set; }
        public int C_DataChanges_RowID { get; set; }
        public Nullable<int> C_Legacy_PrimaryKeyValue { get; set; }
    
        public virtual ICollection<Lookups_Division_AgreementAccessDefaults> Division_AgreementAccessDefaults { get; set; }
        public virtual ICollection<Lookups_Access_PersonDivisionCategoryXref> Access_PersonDivisionCategoryXref { get; set; }
        public virtual ICollection<Lookups_Access_CategoryAreaMapping> Access_CategoryAreaMapping { get; set; }
        public virtual ICollection<Lookups_Person_BadgeAccessPeriods> Person_BadgeAccessPeriods { get; set; }
    }
    
}