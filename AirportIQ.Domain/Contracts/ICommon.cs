using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
	public interface ICommon
	{
		DataTable UserCompanies(int userId);
		DataTable UserCompanyDivisions(int userId, int companyId);
		DataTable CompaniesWithActiveBadges(int userId);
        void CreateAlert(int userId_Sender, int userId_Recipient, string subject, string message);
        DataTable GetAlerts(int userId);
	}
}
