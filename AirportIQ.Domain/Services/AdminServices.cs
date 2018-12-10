using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;

namespace AirportIQ.Domain.Services
{
    public class AdminServices : IAdmin
    {
         #region "Private Variables"

            private readonly IAdminRepository adminRepository;

        #endregion

        #region "Constructors"

            public AdminServices() : this(new AdminRepository()) { }

            public AdminServices(IAdminRepository adminRepository)
            {
                if (adminRepository == null) throw new ArgumentNullException("adminRepository");
                this.adminRepository = adminRepository;
            }
            
        #endregion

        #region "Public Methods"

            //---------------------
            DataSet IAdmin.loadUserGroups(int userId, int applicationId)
            {
                return this.adminRepository.loadUserGroups(userId, applicationId);
            }
            DataSet IAdmin.loadUserGroups(int userId, int groupId, int applicationId)
            {
                return this.adminRepository.loadUserGroups(userId, groupId, applicationId);
            }
            DataSet IAdmin.loadUserGroups(int userId, int groupId, int applicationId, int currentUserId)
            {
                return this.adminRepository.loadUserGroups(userId, groupId, applicationId, currentUserId);
            }

            //--------------------

            DataSet IAdmin.loadGroupObjects(String groupList, int applicationId)
            {
                return this.adminRepository.loadGroupObjects(groupList, applicationId);
            }
            void IAdmin.saveGroupForm(Int32? groupID, String groupName, String groupDescription, Int32 applicationID, Int32? copyGroupID, Int32 userID, String action)
            {
                this.adminRepository.saveGroupForm(groupID, groupName, groupDescription, applicationID, copyGroupID, userID, action );
            }

            void IAdmin.SaveGroupResources(DataTable resourcesToSave, Int32 userID)
            {
                this.adminRepository.SaveGroupResources(resourcesToSave, userID);
            }

            DataSet IAdmin.LoadSecurityTokens(Int32 userID)
            {
                return this.adminRepository.LoadSecurityTokens(userID);
            }
            DataSet IAdmin.LoadResourceTokens()
            {
                return this.adminRepository.LoadResourceTokens();
            }
            DataSet IAdmin.LoadGroupsOfUsers(Int32 userID, Int32? groupId, int applicationId, Int32? dataUserID)
            {
                return this.adminRepository.LoadGroupsOfUsers(userID, groupId, applicationId, dataUserID);
            }
            void IAdmin.SaveUsersToGroups(DataTable usersToSave, Int32 userID)
            {
                this.adminRepository.SaveUsersToGroups(usersToSave, userID);
            }


        /// <summary>
        /// MAV Returns a single header line for the user admin to allow for chagnes to the data but not to the entire page.
        /// </summary>
        /// <returns></returns>
        DataSet IAdmin.loadSingleUserGroupHeader()
        {
            return this.adminRepository.loadSingleUserGroupHeader();
        }

        /// <summary>
        /// MAV Returns a single header line for the user admin to allow for chagnes to the data but not to the entire page.
        /// </summary>
        /// <returns></returns>
        DataSet IAdmin.loadSingleResourceSet(int userId, int groupId, int applicationId, int currentUserId)
        {
            return this.adminRepository.loadSingleResourceSet(userId, groupId, applicationId, currentUserId);
        }

        DataSet IAdmin.loadUsersGroups(int userId, int applicationId, int currentUserId)
        {
            return this.adminRepository.loadUsersGroups(userId, applicationId, currentUserId);
        }

       #endregion
       
    }
}
