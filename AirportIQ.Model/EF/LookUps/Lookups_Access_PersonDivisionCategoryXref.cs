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
    public partial class Lookups_Access_PersonDivisionCategoryXref
    {
        public int PersonDivisionXrefID { get; set; }
        public int CategoryID { get; set; }
        public System.DateTime WhenBegins { get; set; }
        public System.DateTime WhenEnds { get; set; }
        public int StaffID_Begin { get; set; }
        public int StaffID_End { get; set; }
        public bool IsSpecialAccess { get; set; }
        public string C_Legacy_DatabaseName { get; set; }
        public string C_Legacy_TableName { get; set; }
        public int C_DataChanges_RowID { get; set; }
    
        public virtual Lookups_Access_Categories Access_Categories { get; set; }
        public virtual Lookups_Facility_Staff Facility_Staff { get; set; }
        public virtual Lookups_Facility_Staff Facility_Staff1 { get; set; }
        public virtual Person_PersonDivisionXref Person_PersonDivisionXref { get; set; }
    }
    
}
