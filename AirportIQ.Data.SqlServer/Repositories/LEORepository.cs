using System;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.Helpers;
using AirportIQ.Data.SqlServer.Initializers;
using AirportIQ.Model.Models.Badging.Results;
using AirportIQ.Model.Models;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class LEORepository : ILEORepository
	{
		private string schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"].ToString();

		public DataTable CardAppointmentGetPeople(string serialNumber, string SSN, string lastName, string OAS_Name, string yearOfBirth)
		{
			DataTable ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[LEO.Appointment.GetPeople]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@serialNumber", ParameterType.DBString, serialNumber));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@SSN", ParameterType.DBString, SSN));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@LastName", ParameterType.DBString, lastName));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@OAS_Name", ParameterType.DBString, OAS_Name));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@YearOfBirth", ParameterType.DBString, yearOfBirth));

			ret = storedProcedure.ExecuteDataSet();
			return ret;
		}

        public bool CardAppointmentSaveLEOSection(LEOResults leoResults)
        {
            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = schema + ".[LEO.Appointment.SaveLEOSection]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@OfficerBadgeID", ParameterType.DBInteger, leoResults.OfficerBadgeID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@PoliceDepartmentID", ParameterType.DBInteger, leoResults.PoliceDepartmentID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@OfficerRankAndTitleID", ParameterType.DBInteger, leoResults.OfficerRankAndTitleID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@SerialNumber", ParameterType.DBString, leoResults.SerialNumber));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@WhenExpires", ParameterType.DBDateTime, leoResults.WhenExpires));

            storedProcedure.ExecuteDataSet();

            return true;
        }

        ////JBienvenu 2013-01-09 Disabled try-catch block so that exceptions can be thrown up the call stack.
        //public bool SaveBadgingResults(BadgingResults badgingResults, int userID)
        //{
        //    bool result = false;

        //    //try
        //    //{
        //        StoredProcedure storedProcedure = new StoredProcedure()
        //        {
        //            StoredProcedureName = schema + ".[Badging.Appointment.Save]"
        //        };

        //        string countryCode = badgingResults.BiographicModel.CountryCode;
        //        if (countryCode == null || countryCode == string.Empty)
        //        {
        //            badgingResults.BiographicModel.CountryCode = "USA";
        //        }

        //        string countrySubdivisionCode = badgingResults.BiographicModel.CountrySubdivisionCode;
        //        if (countrySubdivisionCode == null)
        //        {
        //            badgingResults.BiographicModel.CountrySubdivisionCode = string.Empty;
        //        }

        //        string countryCode_Birth = badgingResults.BiographicModel.CountryCode_Birth;
        //        if (countryCode_Birth == null || countryCode_Birth == string.Empty)
        //        {
        //            badgingResults.BiographicModel.CountryCode_Birth = "USA";
        //        }

        //        //JBienvenu 2013-01-09 new block
        //        string countrySubdivisionCode_Birth = badgingResults.BiographicModel.CountrySubdivisionCode_Birth;
        //        if (countrySubdivisionCode_Birth == null)
        //        {
        //            badgingResults.BiographicModel.CountrySubdivisionCode_Birth = string.Empty;
        //        }

        //        string strBadgingResults = XmlHelper.Serialize(badgingResults);
        //        storedProcedure.Parameters.Add(new StoredProcedureParameter("@BadgingResults", ParameterType.DBString, strBadgingResults));
        //        storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userID));

        //        storedProcedure.ExecuteDataSet();

        //        result = true;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    result = false;
        //    //    Debug.WriteLine("SaveBadgingResults() Exception.Message: " + ex.Message);
        //    //    Debug.WriteLine("SaveBadgingResults()  Exception.InnerException: " + ex.InnerException);
        //    //    Debug.WriteLine("SaveBadgingResults()  Exception.Source: " + ex.Source);
        //    //    Debug.WriteLine("SaveBadgingResults()  Exception.StackTrace: " + ex.StackTrace);
        //    //}

        //    return result;
        //}
	}
}