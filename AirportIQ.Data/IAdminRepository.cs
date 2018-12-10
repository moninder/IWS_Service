using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AirportIQ.Data
{
    public interface IAdminRepository
    {
        //------------
        DataSet loadUserGroups(int userId,int applicationId);
        DataSet loadUserGroups(int userId, int groupId, int applicationId);
        DataSet loadUserGroups(int userId, int groupId, int applicationId, int currentUserId);
        //-------------------
        DataSet loadGroupObjects(string groupList, int applicationId);

        void saveGroupForm(Int32? groupID, String groupName, String groupDescription, Int32 applicationID, Int32? copyGroupID, Int32 userID, String action);

        void SaveGroupResources(DataTable resourcesToSave, Int32 userID);

        DataSet LoadSecurityTokens(Int32 userID);
        DataSet LoadResourceTokens();
        DataSet LoadGroupsOfUsers(Int32 userID, Int32? groupId, int applicationId, Int32? dataUserID);

        void SaveUsersToGroups(DataTable usersToSave, Int32 userID);

        DataSet loadSingleUserGroupHeader();
        DataSet loadSingleResourceSet(int userId, int groupId, int applicationId, int currentUserId);
        DataSet loadUsersGroups(int userId, int applicationId, int currentUserId);
    }
}