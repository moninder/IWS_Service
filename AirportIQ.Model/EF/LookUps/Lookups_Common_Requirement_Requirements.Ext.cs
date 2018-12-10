using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.EF.LookUps
{
    public partial class Lookups_Common_Requirement_Requirements
    {
        public static string GetRequirementCodeFromDescription(string description)
        {
            string code = string.Empty;

            using (LookupEntities context = new LookupEntities())
            {
                var codes = from c in context.Lookups_Common_Requirement_Requirements
                            where c.RequirementDescription == description || c.RequirementCode == description
                            select c.RequirementCode;

                code = codes.FirstOrDefault();
            }

            return code;
        }
    }
}
