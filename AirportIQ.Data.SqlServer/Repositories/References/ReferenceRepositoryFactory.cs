using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Data.SqlServer.Repositories.References
{
    public static class ReferenceRepositoryFactory
    {
        public static IReferenceRepository Create(string key)
        {
            switch (key)
            {
                case "Audit.AuditStatuses":
                    return new ReferenceRepository("Data", "Audit.AuditStatuses", "AuditStatusCode", "AuditStatusDescription", "AuditStatusCode");
                
                case "Company.IndustryTypes":
                    return new ReferenceRepository("Data", "Company.IndustryTypes", "IndustryTypeID", "IndustryTypeDescription", "IndustryTypeAbbreviation");
                
                case "Division.AgreementTypes":
                    return new ReferenceRepository("Data", "Division.AgreementTypes", "AgreementTypeID", "AgreementTypeDescription", "AgreementTypeAbbreviation");
                
                case "Division.ContactTypes":
                    return new ReferenceRepository("Data", "Division.ContactTypes", "ContactTypeID", "ContactTypeDescription", "ContactTypeAbbreviation");
                
                case "Division.DivisionCategories":
                    return new ReferenceRepository("Data", "Division.DivisionCategories", "CategoryCode", "CategoryDescription", "CategoryCode");
                
                case "Division.DivisionStatuses":
                    return new ReferenceRepository("Data", "Division.DivisionStatuses", "DivisionStatusCode", "DivisionStatusDescription", "DivisionStatusCode");
                
                case "Division.Services":
                    return new ReferenceRepository("Data", "Division.Services", "ServiceID", "ServiceDescription", "ServiceAbbreviation");
                
                case "Icon.Icons":
                    return new ReferenceRepository("Data", "Icon.Icons", "IconID", "IconDescription", "IconAbbreviation");
                
                case "Miscellaneous.Countries":
                    return new ReferenceRepository("Data", "Miscellaneous.Countries", "CountryCode", "CountryDescription", "CountryCode");
                
                case "Miscellaneous.EyeColors":
                    return new ReferenceRepository("Data", "Miscellaneous.EyeColors", "EyeColorCode", "EyeColorDescription", "EyeColorCode");
                
                case "Miscellaneous.HairColors":
                    return new ReferenceRepository("Data", "Miscellaneous.HairColors", "HairColorCode", "HairColorDescription", "HairColorCode");
                
                case "Miscellaneous.NamePrefixes":
                    return new ReferenceRepository("Data", "Miscellaneous.NamePrefixes", "NamePrefixCode", "NamePrefixDescription", "NamePrefixCode");
                
                case "Miscellaneous.Races":
                    return new ReferenceRepository("Data", "Miscellaneous.Races", "RaceCode", "RaceDescription", "RaceCode");
                
                case "Miscellaneous.Sexes":
                    return new ReferenceRepository("Data", "Miscellaneous.Sexes", "SexCode", "SexDescription", "SexCode");
                
                case "Person.BadgeStatuses":
                    return new ReferenceRepository("Data", "Person.BadgeStatuses", "BadgeStatusCode", "BadgeStatusDescription", "BadgeStatusCode");
                
                case "Person.JobRoles":
                    return new ReferenceRepository("Data", "Person.JobRoles", "JobRoleID", "JobRoleDescription", "JobRoleAbbreviation");
                
                case "Safe.VehicleBodyTypes":
                    return new ReferenceRepository("Data", "Safe.VehicleBodyTypes", "VehicleBodyID", "VehicleBodyDescription", "VehicleBodyAbbreviation");

                case "Safe.VehicleMakes":
                    return new ReferenceRepository("Data", "Safe.VehicleMakes", "VehicleMakeID", "VehicleMakeDescription", "VehicleMakeAbbreviation");

                default:
                    throw new ArgumentOutOfRangeException("key");
            }
        }
    }
}
