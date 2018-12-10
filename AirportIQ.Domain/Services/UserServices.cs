using System;
using System.Data;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class UserServices : IUser
	{
		#region "Private Variables"

		private readonly IUserRepository userRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public UserServices() : this(new UserRepository()) { }

		public UserServices(IUserRepository userRepository)
		{
			if (userRepository == null) throw new ArgumentNullException("userRepository");
			this.userRepository = userRepository;
		}

		#endregion "Constructors"

		#region "Public Methods"

		DataSet IUser.loadUserList(short ApplicationID)
		{
			return this.userRepository.loadAllUsers(ApplicationID);
		}

		DataSet IUser.loadUserNameList(short ApplicationID)
		{
			return this.userRepository.loadUserNameList(ApplicationID);
		}

		DataSet IUser.loadUserName(short applicationID, Int32? groupId, string loginName, Int32? userId)
		{
			return this.userRepository.loadUserName(applicationID, groupId, loginName, userId);
		}

        // -----Manage User---------------
        DataSet IUser.loadManageUserInfo(String personNameCriteria, Int32 userID, String LAWA_OAS, Boolean isNonUser)
        {
            return this.userRepository.loadManageUserInfo(personNameCriteria, userID, LAWA_OAS, isNonUser);
        }
        DataSet IUser.loadUserFormInfo(Int32? personUserID, Int32 userID, String LAWA_OAS)
        {
            return this.userRepository.loadUserFormInfo(personUserID, userID, LAWA_OAS);
        }
        // ---------------------------------loadUserFormInfo

		void IUser.saveUserForm(DataTable userToSave, Int32 userID)
		{
			this.userRepository.saveUserForm(userToSave, userID);
		}
        void IUser.saveUserForm(DataTable userToSave, Int32 userID, Int32 userIDToDeactivate)
        {
            this.userRepository.saveUserForm(userToSave, userID, userIDToDeactivate);
        }
		public DataSet UserLoad(string loginName)
		{
			return userRepository.UserLoad(loginName);
		}

		public DataSet UserLoad(int applicationID, string loginName)
		{
			return userRepository.UserLoad(applicationID, loginName);
		}
        // ---------------------------------user resources
        void IUser.saveUserResources(DataTable resourcesToSave, Int32 userID)
        {
            this.userRepository.saveUserResources(resourcesToSave, userID);
        }
        public DataSet LoadUserData(Int16 applicationID, string userLoginName, string userEncryptedPassword, string facilityCode)
        {
            return userRepository.LoadUserData(applicationID, userLoginName, userEncryptedPassword, facilityCode);
        }
        public DataSet LoadUserDataOnly(Int16 applicationID, string userLoginName, string facilityCode)
        {
            return userRepository.LoadUserDataOnly(applicationID, userLoginName, facilityCode);
        }
		#endregion "Public Methods"
	}
}