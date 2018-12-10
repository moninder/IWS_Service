using System;
using System.Configuration;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.Helper;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class AgreementRepository : IAgreementRepository
	{
		#region Private Variables

		private readonly string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];
        private readonly string _shadowSchema = ConfigurationManager.AppSettings["Shadow.ApplicationSchema"];

		#endregion Private Variables

		public DataTable LoadDivisionAgreements(int divisionID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Agreements.Lists.ByDivisionID]" };

			StoredProcedureParameter paramDivisionID = new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID);
			storedProcedure.Parameters.Add(paramDivisionID);

			result = storedProcedure.ExecuteDataSet();

			return result;		
		}

        public DataSet LoadAgreement(int agreementID)
		{
			DataSet result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Agreements.Details.Load]" };

			var paramAgreementID = new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID);
			storedProcedure.Parameters.Add(paramAgreementID);

			result = storedProcedure.ExecuteMultipleDataSet();

			return result;		
		}

		public DataTable LoadAgreementTypes()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.Lists.AgreementTypes]" };

			result = storedProcedure.ExecuteDataSet();

			return result;		
		}

		public DataTable LoadCompanyAndDivsion(int agreementID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.CompanyAndDivsion.ByAgreementID]" };

			var paramAgreementID = new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID);
			storedProcedure.Parameters.Add(paramAgreementID);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadDefaultAccess(int agreementID, string facilityCode)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.DefaultAccess.Load]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadAccessCategories(int locationID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.AccessCategories.Load]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@LocationID", ParameterType.DBString, locationID));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadLocationsForAgreement(int agreementID, int jobroleID, string facilityCode)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.LocationsForAgreement.Load]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@JobRoleID", ParameterType.DBInteger, jobroleID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

        public DataTable ApprovedIconsByBadgeColor(string facilityCode)
        {
            DataTable result = null;
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.ApprovedIconsByBadgeColor]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode));

            result = storedProcedure.ExecuteDataSet();

            return result;
        }

		public DataTable LoadLocationsWithAgreementIndicator(int agreementID, int divisionTypeId, string facilityCode)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.LocationsWithAgreementIndicator.Load]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));

            if (divisionTypeId != -1)
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypeID", ParameterType.DBInteger, divisionTypeId));

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadLocationsForFacitity(string facilityCode)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.LocationsForFacility.Load]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadJobRolesForAgreement(int agreementID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.JobRolesForAgreement.Load]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

        public DataTable LoadBadgeColorsForAgreement(int agreementID, int divisionTypeId, string facilityCode)
        {
            DataTable result = null;
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.BadgeColorsForAgreement.Load]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));

            if (divisionTypeId != -1)
                storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionTypeID", ParameterType.DBInteger, divisionTypeId));

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@FacilityCode", ParameterType.DBString, facilityCode));

            result = storedProcedure.ExecuteDataSet();

            return result;
        }

		public DataTable LoadJobRoles()
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.JobRoles.Load]" };

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable LoadJobRolesWithAgreementIndicator(int agreementID)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.JobRolesWithAgreementIndicator.Load]" };

			storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

        public void UpdateBadgeAccess(int agreementID, int jobRoleID, int locationID, int staffID, int UserID)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[CorporateMaintenance.JobRoles.UpdateBadgeAccess]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@JobRoleID", ParameterType.DBInteger, jobRoleID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@LocationID", ParameterType.DBInteger, locationID));
            //storedProcedure.Parameters.Add(new StoredProcedureParameter("@CategoryID", ParameterType.DBInteger, categoryID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@StaffID", ParameterType.DBInteger, staffID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

            storedProcedure.ExecuteNonQuery();
        }

        //JBienvenu 2013-01-11 19304 new
        /// <summary>
        /// Return the earliest and latest dates at which the specified agreement can start and end.
        /// </summary>
        /// <param name="agreementID"></param>
        public AgreementHelper.DateLimits DateLimits(int? agreementID, int? newPrimeAgreementID)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Agreements.DateLimits]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID ?? 0));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@NewPrimeAgreementID", ParameterType.DBInteger, newPrimeAgreementID ?? 0));

            DataTable tableReturned = storedProcedure.ExecuteDataSet();

            if (tableReturned == null)
            {
                return new AgreementHelper.DateLimits();
            }
            else if (tableReturned.Rows.Count == 0)
            {
                return new AgreementHelper.DateLimits();
            }
            else if (tableReturned.Rows.Count == 1)
            {
                return new AgreementHelper.DateLimits(tableReturned.Rows[0]);
            }
            else
                throw new Exception(storedProcedure.StoredProcedureName+" returned more than 1 row.");
            
        }

        public void Save(int agreementID, int divisionID, string agreementNumber, short agreementTypeID, string directAuthorityNumber, string otherAgreementType, DateTime whenAgreementBegins, DateTime whenAgreementEnds, int? agreementID_Prime, DataTable services, int UserID)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Agreements.Details.Save]" };
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DivisionID", ParameterType.DBInteger, divisionID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementNumber", ParameterType.DBString, agreementNumber));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementTypeID", ParameterType.DBInteger, agreementTypeID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DirectAuthorityNumber", ParameterType.DBString, directAuthorityNumber));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@OtherAgreementType", ParameterType.DBString, otherAgreementType));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@WhenAgreementBegins", ParameterType.DBDateTime, whenAgreementBegins));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@WhenAgreementEnds", ParameterType.DBDateTime, whenAgreementEnds));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID_Prime", ParameterType.DBInteger, agreementID_Prime));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Services", ParameterType.Structured, services));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, UserID));

            storedProcedure.ExecuteDataSet();
        }

        public void DeleteDefaultAccessBadgeColorsAndIcons(int agreementId)
        {
            this.DeleteDefaultAccessBadgeColorsAndIcons(agreementId, false);
        }

        public void DeleteDefaultAccessBadgeColorsAndIcons(int agreementId, bool useShadowTables)
        {
            var schema = useShadowTables ? _shadowSchema : _Schema;

            var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[CorporateMaintenance.DeleteDefaultAccessBadgeColorsAndIcons]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@AgreementID", ParameterType.DBInteger, agreementId));

            storedProcedure.ExecuteNonQuery();
        }

        public void LoadIndustryTypes(DataTable newIndustryTypes)
        {
            var schema = _shadowSchema;

            var storedProcedure = new StoredProcedure() { StoredProcedureName = schema + ".[Data.DefaultAccessForm.LoadIndustryTypes]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CompanyIndustryTypes", ParameterType.Structured, newIndustryTypes));

            storedProcedure.ExecuteNonQuery();
        }
	}
}

