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
    public partial class Lookups_Division_DivisionNotes
    {
        public int DivisionNoteID { get; set; }
        public int DivisionID { get; set; }
        public int StaffID { get; set; }
        public string NoteText { get; set; }
        public bool IsHotNote { get; set; }
        public System.DateTime WhenBegins { get; set; }
        public System.DateTime WhenEnds { get; set; }
        public bool IsLocked { get; set; }
        public string C_Legacy_DatabaseName { get; set; }
        public string C_Legacy_TableName { get; set; }
        public int C_DataChanges_RowID { get; set; }
    
        public virtual Lookups_Division_Divisions Division_Divisions { get; set; }
        public virtual Lookups_Facility_Staff Facility_Staff { get; set; }
    }
    
}