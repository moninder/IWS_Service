using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
	public interface IWorkflow
	{
		int GetWorkID(int iD, int facilityWorkflowID);
	}
}
