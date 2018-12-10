using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
	public class UserRepository : IUserRepository
	{
		#region Private Variables

		// private string xschema = ConfigurationManager.AppSettings["SecuritySchema"].ToString();
		private string schema = ConfigurationManager.AppSettings["SecuritySchema"].ToString();

		#endregion Private Variables

        private string ReadConnectionString()
        {
            ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
            ConnectionStringSettings constr = connections["ApplicationServices"];
            return constr.ConnectionString;
        }

	    string IUserRepository.GetSecurityToken(int userId)
	    {
	        string retVal = "";

	        using (var sqlConn = new SqlConnection(ReadConnectionString()))
            {
                sqlConn.Open();

                using (var sqlCommand = new SqlCommand("[App.Oas].[Reports.GetSecurityToken]", sqlConn))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@userID", userId));
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    retVal = (string)sqlCommand.ExecuteScalar();
                }

                sqlConn.Close();
            }

            return retVal;
	    }

		DataSet IUserRepository.loadAllUsers(Int16 applicationID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Security.Users.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@applicationID", ParameterType.DBString, applicationID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		DataSet IUserRepository.loadUserNameList(Int16 applicationID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Security.Users.LoadNameList]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@applicationID", ParameterType.DBString, applicationID));
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}

		DataSet IUserRepository.loadUserName(Int16 applicationID, Int32? groupId, string loginName, Int32? userId)
		{
			DataSet ret = null;
			string dc = ((char)1).ToString();
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Security.Users.LoadNameList]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@applicationID", ParameterType.DBString, applicationID));
			if (!(groupId == null))
			{
				storedProcedure.Parameters.Add(new StoredProcedureParameter("@groupID", ParameterType.DBString, groupId));
			}
			if (!(loginName == dc))
			{
				storedProcedure.Parameters.Add(new StoredProcedureParameter("@userLoginName", ParameterType.DBString, loginName));
			}
			if (!(userId == null))
			{
				storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userId));
			}
			ret = storedProcedure.ExecuteMultipleDataSet();
			return ret;
		}
		void IUserRepository.saveUserForm(DataTable userToSave, Int32 userID)
		{
			DataSet ret = null;
			var storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Security.UsersForm.Save]";
            //rguidi 3/4/2013 #20202
			//storedProcedure.Parameters.Add(new StoredProcedureParameter("@Users", ParameterType.DBString, userToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Users", ParameterType.Structured, userToSave));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
			ret = storedProcedure.ExecuteResourceDataSet();
		}
        void IUserRepository.saveUserForm(DataTable userToSave, Int32 userID, Int32 userIDToDeactivate)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.UsersForm.Save]";
            //rguidi 3/4/2013 #20202
            //storedProcedure.Parameters.Add(new StoredProcedureParameter("@Users", ParameterType.DBString, userToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Users", ParameterType.Structured, userToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserToInactivateID", ParameterType.DBString, userIDToDeactivate));
            ret = storedProcedure.ExecuteResourceDataSet();
        }
		public DataSet UserLoad(string loginName)
		{
			StoredProcedure storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Security.User.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@LoginName", ParameterType.DBString, loginName));

			return storedProcedure.ExecuteMultipleDataSet();
		}


		public DataSet UserLoad(int applicationID, string loginName)
		{
			StoredProcedure storedProcedure = new StoredProcedure();
			storedProcedure.StoredProcedureName = schema + ".[Security.User.Load]";
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@applicationID", ParameterType.DBInteger, applicationID));
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@LoginName", ParameterType.DBString, loginName));

			// return storedProcedure.ExecuteMultipleDataSet();
			return new DataSet();
		}

        // ---------Manage Users Info -----------------------
        DataSet IUserRepository.loadManageUserInfo(String personNameCriteria, Int32 userID,String LAWA_OAS, Boolean isNonUser)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.ManageUserInfo.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@personNameCriteria", ParameterType.DBString, personNameCriteria));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@LAWA_OAS",ParameterType.DBString, LAWA_OAS));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@IsNonUser", ParameterType.DBString, isNonUser));

            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        DataSet IUserRepository.loadUserFormInfo(Int32? personUserID, Int32 userID, String LAWA_OAS)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.DisplayUserInfo.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@personUserID", ParameterType.DBString, personUserID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@LAWA_OAS", ParameterType.DBString, LAWA_OAS));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        // --------------------------------
        void IUserRepository.saveUserResources(DataTable resourcesToSave, Int32 userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.ResourceForm.Save]";
            //rguidi 3/4/2013 #20202
            //storedProcedure.Parameters.Add(new StoredProcedureParameter("@Resources", ParameterType.DBString, resourcesToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@Resources", ParameterType.Structured, resourcesToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteResourceDataSet();
        }
        DataSet IUserRepository.LoadUserData(Int16 applicationID, string userLoginName, string userEncryptedPassword, string facilityCode)
        {
            DataSet ret = null;
            string dc = ((char)1).ToString();
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.Login.LoadUserData]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@applicationID", ParameterType.DBString, applicationID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userLoginName", ParameterType.DBString, userLoginName ));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userEncryptedPassword", ParameterType.DBString, userEncryptedPassword ));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@facilityCode", ParameterType.DBString, facilityCode));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        DataSet IUserRepository.LoadUserDataOnly(Int16 applicationID, string userLoginName, string facilityCode)
        {
            DataSet ret = null;
            string dc = ((char)1).ToString();
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = schema + ".[Security.Login.LoadUserDataOnly]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@applicationID", ParameterType.DBString, applicationID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@userLoginName", ParameterType.DBString, userLoginName));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@facilityCode", ParameterType.DBString, facilityCode));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        // --------------------------------
	}
}