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
    public partial class Lookups_Access_CategoryAreaMapping
    {
        public int CategoryID { get; set; }
        public int AreaID { get; set; }
        public bool IsActive { get; set; }
        public int C_DataChanges_RowID { get; set; }
    
        public virtual Lookups_Access_Areas Access_Areas { get; set; }
        public virtual Lookups_Access_Categories Access_Categories { get; set; }
    }
    
}
