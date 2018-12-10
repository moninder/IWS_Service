using AirportIQ.WF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AirportIQ.Tests.WF.Integration
{
    [DataContract]
    public class PayraiseWorkflow: Workflow
    {
        [DataMember]
        public string Employee { get; set; }
        [DataMember]
        public double RequestedSalaryAmount { get; set; }
    }
}
