using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.EF.SAFE
{
    public partial class Division_Divisions
    {
        public string FullAddress
        {
            get
            {
                string address = string.Empty;

                address = this.Address1;

                if (!string.IsNullOrWhiteSpace(this.Address2))
                    address += "\n" + this.Address2;

                if (!string.IsNullOrWhiteSpace(this.Address3))
                    address += "\n" + this.Address3;

                address += "\n" + this.City + ", " + this.CountrySubdivisionCode + " " + this.PostalCode;

                return address;
            }
        }

        public string FullAddressAsHtml
        {
            get { return this.FullAddress.Replace("\n", @"</br>"); }
        }
    }
}
