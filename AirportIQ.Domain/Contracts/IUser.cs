using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
	public interface IUser
	{
		DataSet loadUserList(Int16 ApplicationID); // to load user information

		DataSet loadUserNameList(Int16 ApplicationID); // to load user name & id list

		DataSet loadUserName(Int16 applicationID, Int32? groupId, string loginName, Int32? userId); // to load user name

        // --------Manage User----------
        DataSet loadManageUserInfo(String personNameCriteria, Int32 userID, String LAWA_OAS,Boolean isNonUser);
        DataSet loadUserFormInfo(Int32? personUserID, Int32 userID, String LAWA_OAS);
        // -----------------------------loadUserFormInfo(Int32? personUserID, Int32 userID, String LAWA_OAS)


		void saveUserForm(DataTable userToSave, Int32 userID);
        void saveUserForm(DataTable userToSave, Int32 userID, Int32 userIDToDeactivate);
        
		DataSet UserLoad(string loginName);

		DataSet UserLoad(int applicationID, string loginName);
        // --------Manage User----------
        void saveUserResources(DataTable resourcesToSave, Int32 userID);

        DataSet LoadUserData(Int16 applicationID, string userLoginName, string userEncryptedPassword, string facilityCode);
        DataSet LoadUserDataOnly(Int16 applicationID, string userLoginName, string facilityCode);

    }
}