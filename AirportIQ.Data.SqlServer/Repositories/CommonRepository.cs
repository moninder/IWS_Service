using System.Configuration;
using System.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class CommonRepository : ICommonRepository
	{
		private readonly string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];


		public System.Data.DataTable UserCompanies(int userId)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Common.User.Companies]" };

			StoredProcedureParameter paramUserIdCode = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userId);
			storedProcedure.Parameters.Add(paramUserIdCode);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public System.Data.DataTable UserCompanyDivisions(int userId, int companyId)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Common.User.Company.Divisions]" };

			StoredProcedureParameter paramUserIdCode = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userId);
			StoredProcedureParameter paramCompanyIdCode = new StoredProcedureParameter("@CompanyID", ParameterType.DBInteger, companyId);
			storedProcedure.Parameters.Add(paramUserIdCode);
			storedProcedure.Parameters.Add(paramCompanyIdCode);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

		public DataTable CompaniesWithActiveBadges(int userId)
		{
			DataTable result = null;
			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Common.Companies.WithActiveBadges] 	" };

			StoredProcedureParameter paramUserIdCode = new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userId);
			storedProcedure.Parameters.Add(paramUserIdCode);

			result = storedProcedure.ExecuteDataSet();

			return result;
		}

        public void CreateAlert(int userId_Sender, int userId_Recipient, string subject, string message)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Common.Alerts.CreateAlert]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID_Sender", ParameterType.DBInteger, userId_Sender));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID_Recipient", ParameterType.DBInteger, userId_Recipient));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Subject", ParameterType.DBInteger, subject));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@MessageBody", ParameterType.DBInteger, message));

            storedProcedure.ExecuteNonQuery();
        }

        public DataTable GetAlerts(int userId)
        {
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[Common.Alerts.GetAlerts]" };

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBInteger, userId));

            DataTable alerts = storedProcedure.ExecuteDataSet();

            return alerts;
        }
	}
}