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
    public partial class Lookups_Person_Trainers
    {
        public int CompanyID { get; set; }
        public int PersonID { get; set; }
        public short TrainingSiteID { get; set; }
        public string PersonTypeStatusCode { get; set; }
        public bool IsCertified { get; set; }
        public bool IsFingerPrinted { get; set; }
        public bool IsLEO { get; set; }
        public System.DateTime WhenTrained { get; set; }
        public System.DateTime WhenExpires { get; set; }
        public Nullable<System.DateTime> WhenPrinted { get; set; }
        public string Instructor { get; set; }
        public string Note { get; set; }
        public string C_Legacy_DatabaseName { get; set; }
        public string C_Legacy_TableName { get; set; }
        public int C_DataChanges_RowID { get; set; }
    
        public virtual Lookups_Company_Companies Company_Companies { get; set; }
        public virtual Lookups_Person_PersonBiographics Person_PersonBiographics { get; set; }
        public virtual Lookups_Person_PersonTypeStatuses Person_PersonTypeStatuses { get; set; }
        public virtual Lookups_Person_TrainingSites Person_TrainingSites { get; set; }
    }
    
}
