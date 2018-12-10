using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AirportIQ.Data;
using System.Configuration;

using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{
    public class AdminRepository : IAdminRepository
    {

        #region Private Variables

        private string adminSchema = ConfigurationManager.AppSettings["SecuritySchema"].ToString();

        #endregion

        //-----------------------------------
        DataSet IAdminRepository.loadUserGroups(int userId, int applicationId)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.GroupsForm.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ApplicationId", ParameterType.DBString, applicationId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userId));
            ret = storedProcedure.ExecuteResourceDataSet();
            return ret;
        }
        DataSet IAdminRepository.loadUserGroups(int userId, int groupId, int applicationId)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.GroupsForm.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ApplicationId", ParameterType.DBString, applicationId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupID", ParameterType.DBString, groupId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userId));
            ret = storedProcedure.ExecuteResourceDataSet();
            return ret;
        }
        DataSet IAdminRepository.loadUserGroups(int userId, int groupId, int applicationId, int currentUserId)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.GroupsForm.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ApplicationId", ParameterType.DBString, applicationId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupID", ParameterType.DBString, groupId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CurrentUserID", ParameterType.DBString, currentUserId));
            ret = storedProcedure.ExecuteResourceDataSet();
            return ret;
        }
        DataSet IAdminRepository.loadGroupObjects(string groupList, int applicationId)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.GroupObjectsLoad]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ApplicationId", ParameterType.DBString, applicationId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupList", ParameterType.DBString, groupList));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        void IAdminRepository.saveGroupForm(Int32? groupID, String groupName, String groupDescription, Int32 applicationID, Int32? copyGroupID, Int32 userID, String action)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.GroupsForm.Save]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupID", ParameterType.DBString, groupID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupName", ParameterType.DBString, groupName));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupDescription", ParameterType.DBString, groupDescription));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ApplicationID", ParameterType.DBString, applicationID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CopyGroupID", ParameterType.DBString, copyGroupID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@_Action", ParameterType.DBString, action));
            ret = storedProcedure.ExecuteMultipleDataSet();
        }
        //------------------------------------
        void IAdminRepository.SaveGroupResources(DataTable resourcesToSave, Int32 userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.GroupsResource.Save]";
            //rguidi 3/4/2013 #20202
            //storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupsResourceTypes", ParameterType.DBString, resourcesToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupsResourceTypes", ParameterType.Structured, resourcesToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteResourceDataSet();
        }
        DataSet IAdminRepository.LoadSecurityTokens(Int32 userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.UserPermissionTokens.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserId", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        DataSet IAdminRepository.LoadResourceTokens()
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.ApplicationResourceTokens.Load]";
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        DataSet IAdminRepository.LoadGroupsOfUsers(Int32 userID, Int32? groupId, int applicationId, Int32? dataUserID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.UserGroupsForm.Load]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ApplicationId", ParameterType.DBString, applicationId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupID", ParameterType.DBString, groupId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userID));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@DataUserID", ParameterType.DBString, dataUserID));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }
        void IAdminRepository.SaveUsersToGroups(DataTable usersToSave, Int32 userID)
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.UsersToGroups.Save]";
            //rguidi 3/4/2013 #20202
            //storedProcedure.Parameters.Add(new StoredProcedureParameter("@UsersToGroupsTypes", ParameterType.DBString, usersToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UsersToGroupsTypes", ParameterType.Structured, usersToSave));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userID));
            ret = storedProcedure.ExecuteResourceDataSet();
        }



        DataSet IAdminRepository.loadSingleUserGroupHeader()
        {
            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.GroupsForm.LoadSingleUserGroup]";
            ret = storedProcedure.ExecuteResourceDataSet();
            return ret;
        }

        DataSet IAdminRepository.loadSingleResourceSet(Int32 userId, Int32 groupId, int applicationId, Int32 currentUserId)
        {
            if (currentUserId == null || currentUserId == -1)
                return null;

            DataSet ret = null;
            var storedProcedure = new StoredProcedure();
            storedProcedure.StoredProcedureName = adminSchema + ".[Security.UserGroupsForm.LoadSingleResourceSet]";
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ApplicationId", ParameterType.DBString, applicationId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@GroupID", ParameterType.DBString, groupId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@currentUserID", ParameterType.DBString, currentUserId));
            ret = storedProcedure.ExecuteMultipleDataSet();
            return ret;
        }

        DataSet IAdminRepository.loadUsersGroups(Int32 userId, int applicationId, Int32 currentUserId)
        {
            if (currentUserId == null || currentUserId == -1)
                return null;

            DataSet ret = null;

            var storedProcedure = new StoredProcedure();

            storedProcedure.StoredProcedureName = adminSchema + ".[Security.UserGroupsForm.LoadUsersGroups]";

            storedProcedure.Parameters.Add(new StoredProcedureParameter("@ApplicationID", ParameterType.DBString, applicationId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@UserID", ParameterType.DBString, userId));
            storedProcedure.Parameters.Add(new StoredProcedureParameter("@CurrentUserID", ParameterType.DBString, currentUserId));
            
            ret = storedProcedure.ExecuteMultipleDataSet();
            
            return ret;
        }
    }
}

