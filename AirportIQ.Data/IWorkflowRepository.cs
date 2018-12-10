using System;
using System.Data;

namespace AirportIQ.Data
{
	public interface IWorkflowRepository
	{
		int GetWorkID(int iD, int facilityWorkflowID);
	}
}
