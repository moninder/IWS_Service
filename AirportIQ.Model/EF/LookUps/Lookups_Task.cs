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
    public partial class Lookups_Task
    {
        public int TaskID { get; set; }
        public short TaskTypeID { get; set; }
        public string TaskStatusCode { get; set; }
        public int StaffID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> StartedDate { get; set; }
        public Nullable<System.DateTime> EndedDate { get; set; }
        public Nullable<bool> Successful { get; set; }
        public string Notes { get; set; }
        public int UserID { get; set; }
        public int C_DataChanges_RowID { get; set; }
    
        public virtual Lookups_TaskStatus TaskStatus { get; set; }
        public virtual Lookups_TaskType TaskType { get; set; }
        public virtual Lookups_Security_Users Security_Users { get; set; }
        public virtual Lookups_Facility_Staff Facility_Staff { get; set; }
    }
    
}
