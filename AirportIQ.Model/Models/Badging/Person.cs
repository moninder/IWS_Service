using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Badging
{
    public class Person
    {
        public int PersonID;
        public string LastName;
        public string FirstName;
        public string MiddleName;
        public string SocialSecurityNumber;
        public Address Address;
        public string PhoneHome;
        public string PhoneWork;

        public DateTime BirthDate;
        public string BirthCountry;
        public string BirthStateCode;
        public string CitizenshipCountry;

        public int HeightInInches;
        public int WeightInPounds;
        public string Gender;
        public string EyeColor;
        public string HairColor;
        public string Race;

        public GovernmentId GovernmentId1;
        public GovernmentId GovernmentId2;

        public Alias[] Aliases;
    }
}
