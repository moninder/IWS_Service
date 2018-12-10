using System;
using System.Data;

namespace AirportIQ.Data
{
	public interface IWorkQueueRepository
	{
		DataSet WorkQueueLoad(int userID);
	}
}
