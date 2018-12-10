using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Badging
{
    public class DocumentType : ReferenceItem
    {
        public bool RequiresIssuingAuthority;
        public bool RequiresIdentificationNumber;
        public bool RequiresExpirationDate;
    }
}
