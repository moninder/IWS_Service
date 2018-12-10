using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
    public interface IAdmin
    {
        //--------------------
        DataSet loadUserGroups(int userId, int applicationId); // to load user group info including current groups
        DataSet loadUserGroups(int userId,int groupId, int applicationId); // to load user group info including current groups
        DataSet loadUserGroups(int userId, int groupId, int applicationId, int currentUserId); // to load user group info including current groups
        //---------------------
        DataSet loadGroupObjects(string groupList, int applicationId); // to load user group info including current groups
        void saveGroupForm(Int32? groupID, String groupName, String groupDescription, Int32 applicationID, Int32? copyGroupID, Int32 userID, String action);

        DataSet loadSingleUserGroupHeader();
        DataSet loadSingleResourceSet(int userId, int groupId, int applicationId, int currentUserId);
        DataSet loadUsersGroups(int userId, int applicationId, int currentUserId);

        void SaveGroupResources(DataTable resourcesToSave, Int32 userID);

        DataSet LoadSecurityTokens(Int32 userID);
        DataSet LoadResourceTokens();
        DataSet LoadGroupsOfUsers(Int32 userID, Int32? groupId, int applicationId, Int32? dataUserID);
        void SaveUsersToGroups(DataTable usersToSave, Int32 userID);
    }
}
