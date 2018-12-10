using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class CommonServices : ICommon
	{

		#region "Private Variables"

		private readonly ICommonRepository _CommonRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public CommonServices() : this(new CommonRepository()) { }

		public CommonServices(ICommonRepository commonRepository)
		{
			if (commonRepository == null) throw new ArgumentNullException("CommonRepository");
			_CommonRepository = commonRepository;
		}

		#endregion "Constructors"



		public DataTable UserCompanies(int userId)
		{
			return _CommonRepository.UserCompanies(userId);
		}

		public DataTable UserCompanyDivisions(int userId, int companyId)
		{
			return _CommonRepository.UserCompanyDivisions(userId, companyId);
		}

		public DataTable CompaniesWithActiveBadges(int userId)
		{
			return _CommonRepository.CompaniesWithActiveBadges(userId);
		}

        public void CreateAlert(int userId_Sender, int userId_Recipient, string subject, string message)
        {
            _CommonRepository.CreateAlert(userId_Sender, userId_Recipient, subject, message);
        }

        public DataTable GetAlerts(int userId)
        {
            return _CommonRepository.GetAlerts(userId);
        }
	}
}