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
    public partial class IWS_Badge_Badge
    {
        public IWS_Badge_Badge()
        {
            this.Person_PersonBadgeXref = new HashSet<IWS_Person_PersonBadgeXref>();
        }
    
        public long BadgeID_IWS { get; set; }
        public int BadgeNumber { get; set; }
        public int EMP_ID { get; set; }
        public string Prox { get; set; }
        public string Match { get; set; }
        public string Exception { get; set; }
        public string BID { get; set; }
        public Nullable<int> BadgeColorID { get; set; }
        public string BadgeStatusCode { get; set; }
        public string BadgeIssuanceReasonCode { get; set; }
        public string Gates { get; set; }
        public int CompanyID { get; set; }
        public int DivisionID { get; set; }
        public int JobRoleID { get; set; }
        public Nullable<System.DateTime> BadgeIssueDate { get; set; }
        public Nullable<System.DateTime> BadgeExpiryDate { get; set; }
        public string PIN { get; set; }
        public int StaffID { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<int> CardNo { get; set; }
        public string C_Legacy_DatabaseName { get; set; }
        public string C_Legacy_TableName { get; set; }
    
        public virtual ICollection<IWS_Person_PersonBadgeXref> Person_PersonBadgeXref { get; set; }
    }
    
}
