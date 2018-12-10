using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Domain.Contracts
{
	public interface IWorkQueue
	{
		DataSet WorkQueueLoad(int userID);
	}
}
