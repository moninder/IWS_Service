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
    public partial class Lookups_Person_BadgeIconPeriods
    {
        public int BadgeID { get; set; }
        public short IconID { get; set; }
        public System.DateTime WhenBecomesActive { get; set; }
        public System.DateTime WhenExpires { get; set; }
        public int StaffID { get; set; }
        public string C_Legacy_DatabaseName { get; set; }
        public string C_Legacy_TableName { get; set; }
        public int C_DataChanges_RowID { get; set; }
    
        public virtual Lookups_Icon_Icons Icon_Icons { get; set; }
        public virtual Lookups_Person_Badges Person_Badges { get; set; }
        public virtual Lookups_Facility_Staff Facility_Staff { get; set; }
    }
    
}
