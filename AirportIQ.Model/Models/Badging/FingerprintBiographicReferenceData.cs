using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AirportIQ.Model.Models.Helpers;

namespace AirportIQ.Model.Models.Badging
{
    public class FingerprintBiographicReferenceData
    {
        #region -- Constructor / Destructor --
            private FingerprintBiographicReferenceData()
            {
            }

            public FingerprintBiographicReferenceData(DataSet ds)
            {
                LoadReferenceData(ds);
            }
        #endregion

        #region -- Properties --
            public Country[] Countries;
            public CountrySubdivision[] CountrySubdivisions;
            public ReferenceItem[] Genders;
            public ReferenceItem[] EyeColors;
            public ReferenceItem[] HairColors;
            public ReferenceItem[] Races;
            public DocumentType[] DocumentTypes;
        #endregion

        #region -- Methods --
            /// <summary>
            /// Load data from existing DataSet
            /// </summary>
            /// <param name="ds"></param>
            private void LoadReferenceData(DataSet ds)
            {
                const int TBL_COUNTRIES = 0;
                const int TBL_COUNTRY_SUBDIVISIONS = 1;
                const int TBL_GENDERS = 2;
                const int TBL_EYE_COLORS = 3;
                const int TBL_HAIR_COLORS = 4;
                const int TBL_RACES = 5;
                const int TBL_DOCUMENT_TYPES = 6;

                Genders = LoadReferenceItems(ds.Tables[TBL_GENDERS], "SexCode", "SexDescription");
                EyeColors = LoadReferenceItems(ds.Tables[TBL_EYE_COLORS], "EyeColorCode", "EyeColorDescription");
                HairColors = LoadReferenceItems(ds.Tables[TBL_HAIR_COLORS], "HairColorCode", "HairColorDescription");
                Races = LoadReferenceItems(ds.Tables[TBL_RACES], "RaceCode", "RaceDescription");

                DataTable dt = ds.Tables[TBL_COUNTRIES];
                if (dt.Rows.Count > 0)
                {
                    List<Country> countryList = new List<Country>();
                    foreach (DataRow row in dt.Rows)
                    {
                        countryList.Add(LoadCountry(row));
                    }
                    Countries = countryList.ToArray();
                }

                dt = ds.Tables[TBL_COUNTRY_SUBDIVISIONS];
                if (dt.Rows.Count > 0)
                {
                    List<CountrySubdivision> subdivisionList = new List<CountrySubdivision>();
                    foreach (DataRow row in dt.Rows)
                    {
                        subdivisionList.Add(LoadCountrySubdivision(row));
                    }
                    CountrySubdivisions = subdivisionList.ToArray();
                }

                dt = ds.Tables[TBL_DOCUMENT_TYPES];
                if (dt.Rows.Count > 0)
                {
                    List<DocumentType> documentTypeList = new List<DocumentType>();
                    foreach (DataRow row in dt.Rows)
                    {
                        documentTypeList.Add(LoadDocumentType(row));
                    }
                    DocumentTypes = documentTypeList.ToArray();
                }
            }

            /// <summary>
            /// Load reference item from datarow
            /// </summary>
            /// <param name="dr"></param>
            /// <returns></returns>
            private ReferenceItem LoadReferenceItem(DataRow dr, string codeColumnName, string descriptionColumnName)
            {
                ReferenceItem item = new ReferenceItem();
                item.Code = (string)dr[codeColumnName];
                item.Description = (string)dr[descriptionColumnName];

                return item;
            }

            /// <summary>
            /// Load reference items from data table
            /// </summary>
            /// <param name="dr"></param>
            /// <returns></returns>
            private ReferenceItem[] LoadReferenceItems(DataTable dt, string codeColumnName, string descriptionColumnName)
            {
                List<ReferenceItem> referenceList = new List<ReferenceItem>();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        referenceList.Add(LoadReferenceItem(row, codeColumnName, descriptionColumnName));
                    }
                }
                return referenceList.ToArray();
            }

            /// <summary>
            /// Load Country from datarow
            /// </summary>
            /// <param name="dr"></param>
            /// <returns></returns>
            private Country LoadCountry(DataRow dr)
            {
                Country country = new Country();
                country.Code = (string)dr["CountryCode"];
                country.Description = (string)dr["CountryDescription"];
                country.CountrySubdivisionTypeName = (string)dr["CountrySubdivisionTypeName"];

                return country;
            }

            /// <summary>
            /// Load Country subdivision
            /// </summary>
            /// <param name="dr"></param>
            /// <returns></returns>
            private CountrySubdivision LoadCountrySubdivision(DataRow dr)
            {
                CountrySubdivision countrySubivision = new CountrySubdivision();
                countrySubivision.Code = (string)dr["CountrySubdivisionCode"];
                countrySubivision.Description = (string)dr["CountrySubdivisionName"];
                countrySubivision.CountryCode = (string)dr["CountryCode"];

                return countrySubivision;
            }

            /// <summary>
            /// Load document type
            /// </summary>
            /// <param name="dr"></param>
            /// <returns></returns>
            private DocumentType LoadDocumentType(DataRow dr)
            {
                DocumentType documentType = new DocumentType();
                documentType.Code =  dr["DocumentTypeNumber"].ToString();
                documentType.Description = (string)dr["DocumentTypeDescription"];
                documentType.RequiresIssuingAuthority = (bool)dr["RequiresIssuingAuthority"];
                documentType.RequiresIdentificationNumber = (bool)dr["RequiresIdentificationNumber"];
                documentType.RequiresExpirationDate = (bool)dr["RequiresExpirationDate"];

                return documentType;
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
