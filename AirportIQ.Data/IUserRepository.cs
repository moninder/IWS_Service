using System;
using System.Data;

namespace AirportIQ.Data
{
	public interface IUserRepository
	{
		DataSet loadAllUsers(Int16 applicationID);

		DataSet loadUserNameList(Int16 applicationID);

		DataSet loadUserName(Int16 applicationID, Int32? groupId, string loginName, Int32? userId);

		void saveUserForm(DataTable userToSave, Int32 userID);
        void saveUserForm(DataTable userToSave, Int32 userID, Int32  userIDToDeactivate);

		DataSet UserLoad(string loginName);
		
		DataSet UserLoad(int applicationID, string loginName);

        // -----Manage User----------------------
        DataSet loadManageUserInfo(String personNameCriteria, Int32 userID, String LAWA_OAS, Boolean isNonUser);
        DataSet loadUserFormInfo(Int32? personUserID, Int32 userID, String LAWA_OAS);
        // --------------------------------------
        void saveUserResources(DataTable resourcesToSave, Int32 userID);
        // --------------------------------------
        DataSet LoadUserData(Int16 applicationID, string userLoginName, string userEncryptedPassword, string facilityCode);
        DataSet LoadUserDataOnly(Int16 applicationID, string userLoginName, string facilityCode);

	    string GetSecurityToken(int userId);
	}
}