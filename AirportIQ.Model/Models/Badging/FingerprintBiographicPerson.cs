using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Xml.Serialization;
using System.IO;


namespace AirportIQ.Model.Models.Badging
{
    public class FingerprintBiographicPerson
    {
        #region -- Constructor / Destructor --
  		    public FingerprintBiographicPerson()
		    {
		    }

            public FingerprintBiographicPerson(DataSet ds)
		    {
			    LoadPerson(ds);
		    }
        #endregion

        #region -- Properties --
            public Person Person;
        #endregion

        #region -- Methods --
            /// <summary>
            /// Load data from existing DataSet
            /// </summary>
            /// <param name="ds"></param>
            private void LoadPerson(DataSet ds)
            {
                const int TBL_GENERAL = 0;
                const int TBL_GOVERNMENT_IDS = 1;
                const int TBL_ALIASES = 2;

                DataTable dtPerson = ds.Tables[TBL_GENERAL];
                if (dtPerson.Rows.Count > 0)
                {
                    DataRow dr = dtPerson.Rows[0];

                    // contact info
                    Person = new Person();
                    Person.PersonID = (int)dr["PersonID"];
                    Person.FirstName = (string)dr["FirstName"];
                    Person.MiddleName = (string)dr["MiddleName"];
                    Person.LastName = (string)dr["LastName"];
                    Person.SocialSecurityNumber = (string)dr["SocialSecurityNumber"];
                    Person.Address = LoadAddress(dr);
                    Person.PhoneHome = (string)dr["HomePhoneNumber"];
                    Person.PhoneWork = (string)dr["WorkPhoneNumber"];

                    Person.BirthDate = (DateTime)dr["DateOfBirth"];
                    Person.BirthCountry = (string)dr["CountryCode_Birth"];
                    Person.BirthStateCode = (string)dr["CountrySubdivisionCode_Birth"];
                    Person.CitizenshipCountry = (string)dr["CountryCode_Citizenship"];

                    Person.HeightInInches = (byte)dr["HeightInInches"];
                    Person.WeightInPounds = (short)dr["WeightInPounds"];
                    Person.Gender = (string)dr["SexCode"];
                    Person.EyeColor = (string)dr["EyeColorCode"];
                    Person.HairColor = (string)dr["HairColorCode"];
                    Person.Race = (string)dr["RaceCode"];

                }

                DataTable dtGovernmentIds = ds.Tables[TBL_GOVERNMENT_IDS];
                if (dtGovernmentIds.Rows.Count > 0)
                {
                    Person.GovernmentId1 = LoadGovernmentId(dtGovernmentIds.Rows[0]);
                }
                if (dtGovernmentIds.Rows.Count > 1)
                {
                    Person.GovernmentId2 = LoadGovernmentId(dtGovernmentIds.Rows[1]);
                }
                if (dtGovernmentIds.Rows.Count > 2)
                {
#warning What to do for more than 2 ids?
                    //throw new Exception("More than two government Ids for person");
                }

                DataTable dtAliases = ds.Tables[TBL_ALIASES];
                if (dtAliases.Rows.Count > 0)
                {
                    List<Alias> aliasList = new List<Alias>();
                    foreach (DataRow row in dtAliases.Rows)
                    {
                        aliasList.Add(LoadAlias(row));
                    }
                    Person.Aliases = aliasList.ToArray();
                }
             }

            /// <summary>
            /// Load Address from datarow
            /// </summary>
            /// <param name="dr"></param>
            /// <returns></returns>
            private Address LoadAddress(DataRow dr)
            {
                Address address = new Address();
                address.Country = (string)dr["CountryCode"];
                address.Address1 = (string)dr["Address"];
                address.Address2 = (string)dr["ApartmentNumber"];
                address.City = (string)dr["City"];
                address.StateCode = (string)dr["CountrySubdivisionCode"];
                address.PostalCode = (string)dr["PostalCode"];
                return address;
            }

            /// <summary>
            /// Load GovernmentId from datarow
            /// </summary>
            /// <param name="dr"></param>
            /// <returns></returns>
            private GovernmentId LoadGovernmentId(DataRow dr)
            {
                GovernmentId governmentId = new GovernmentId();
                governmentId.DocumentId = (int)dr["DocumentID"];
                governmentId.DocumentType = dr["DocumentTypeNumber"].ToString();
                governmentId.IdNumber = (string)dr["IdentificationNumber"];
                governmentId.ExpirationDate = (DateTime)dr["ExpirationDate"];
                governmentId.IssuingCountry = (string)dr["IssuingAuthority_CountryCode"];
                return governmentId;
            }

            /// <summary>
            /// Load Alias from datarow
            /// </summary>
            /// <param name="dr"></param>
            /// <returns></returns>
            private Alias LoadAlias(DataRow dr)
            {
                Alias alias = new Alias();
                alias.AliasId = (int)dr["PersonAliasID"];
                alias.Prefix = (string)dr["NamePrefixCode"];
                alias.FirstName = (string)dr["FirstName"];
                alias.MiddleName = (string)dr["MiddleName"];
                alias.LastName = (string)dr["LastName"];
                alias.Suffix = (string)dr["NameSuffixCode"];

                return alias;
            }

            public DataTable GetPersonDataTable()
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PersonID");
                dt.Columns.Add("FirstName");
                dt.Columns.Add("MiddleName");
                dt.Columns.Add("LastName");
                dt.Columns.Add("SocialSecurityNumber");

                dt.Columns.Add("CountryCode");
                dt.Columns.Add("Address");
                dt.Columns.Add("ApartmentNumber");
                dt.Columns.Add("City");
                dt.Columns.Add("CountrySubdivisionCode");
                dt.Columns.Add("PostalCode");

                dt.Columns.Add("HomePhoneNumber");
                dt.Columns.Add("WorkPhoneNumber");

                dt.Columns.Add("DateOfBirth");
                dt.Columns.Add("CountryCode_Birth");
                dt.Columns.Add("CountrySubdivisionCode_Birth");
                dt.Columns.Add("CountryCode_Citizenship");

                dt.Columns.Add("HeightInInches");
                dt.Columns.Add("WeightInPounds");
                dt.Columns.Add("SexCode");
                dt.Columns.Add("EyeColorCode");
                dt.Columns.Add("HairColorCode");
                dt.Columns.Add("RaceCode");

                DataRow dr = dt.NewRow();
                dr["PersonID"] = Person.PersonID;
                dr["FirstName"] = Person.FirstName;
                dr["MiddleName"] = Person.MiddleName;
                dr["LastName"] = Person.LastName;
                dr["SocialSecurityNumber"] = Person.SocialSecurityNumber;

                dr["CountryCode"] = Person.Address.Country;
                dr["Address"] = Person.Address.Address1;
                dr["ApartmentNumber"] = Person.Address.Address2;
                dr["City"] = Person.Address.City;
                dr["CountrySubdivisionCode"] = Person.Address.StateCode;
                dr["PostalCode"] = Person.Address.PostalCode;

                dr["HomePhoneNumber"] = Person.PhoneHome;
                dr["WorkPhoneNumber"] = Person.PhoneWork;

                dr["DateOfBirth"] = Person.BirthDate;
                dr["CountryCode_Birth"] = Person.BirthCountry;
                dr["CountrySubdivisionCode_Birth"] = Person.BirthStateCode;
                dr["CountryCode_Citizenship"] = Person.CitizenshipCountry;

                dr["HeightInInches"] = Person.HeightInInches;
                dr["WeightInPounds"] = Person.WeightInPounds;
                dr["SexCode"] = Person.Gender;
                dr["EyeColorCode"] = Person.EyeColor;
                dr["HairColorCode"] = Person.HairColor;
                dr["RaceCode"] = Person.Race;

                dt.Rows.Add(dr);

                return dt;
            }

            public DataTable GetGovernmentIdDataTable()
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("DocumentID");
                dt.Columns.Add("DocumentTypeNumber");
                dt.Columns.Add("IssuingAuthority_CountryCode");
                dt.Columns.Add("IdentificationNumber");
                dt.Columns.Add("ExpirationDate");

                LoadGovernmentIdIntoDataTable(dt, Person.GovernmentId1);
                LoadGovernmentIdIntoDataTable(dt, Person.GovernmentId2);

                return dt;
            }

            private void LoadGovernmentIdIntoDataTable(DataTable dt, GovernmentId govId)
            {
                if (govId != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["DocumentID"] = govId.DocumentId;
                    dr["DocumentTypeNumber"] = Int16.Parse(govId.DocumentType);
                    dr["IssuingAuthority_CountryCode"] = govId.IssuingCountry;
                    dr["IdentificationNumber"] = govId.IdNumber;
                    dr["ExpirationDate"] = govId.ExpirationDate;
                    dt.Rows.Add(dr);
                }
            }

            public DataTable GetAliasDataTable()
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PersonAliasID");
                dt.Columns.Add("NamePrefixCode");
                dt.Columns.Add("FirstName");
                dt.Columns.Add("MiddleName");
                dt.Columns.Add("LastName");
                dt.Columns.Add("NameSuffixCode");

                foreach (Alias alias in Person.Aliases)
                {
                    DataRow dr = dt.NewRow();
                    dr["PersonAliasID"] = alias.AliasId;
                    dr["NamePrefixCode"] = alias.Prefix;
                    dr["FirstName"] = alias.FirstName;
                    dr["MiddleName"] = alias.MiddleName;
                    dr["LastName"] = alias.LastName;
                    dr["NameSuffixCode"] = alias.Suffix;

                    dt.Rows.Add(dr);
                }

                return dt;
            }


            public string ToXml()
            {
                XmlSerializer serializer = new XmlSerializer(typeof(FingerprintBiographicPerson));

                MemoryStream ms = new MemoryStream();
                serializer.Serialize(ms, this);
                ms.Position = 0;

                StreamReader reader = new StreamReader(ms);
                return reader.ReadToEnd();
            }

            /// <summary>
            /// Convert to json
            /// </summary>
            /// <returns></returns>
            public string ToJson()
            {
                var idtc = new IsoDateTimeConverter { DateTimeFormat = "MM/dd/yyyy" };
                return JsonConvert.SerializeObject(this, idtc);
            }

        #endregion
    }
}
