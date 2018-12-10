using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class WorkQueueServices : IWorkQueue
	{
		#region "Private Variables"

		private readonly IWorkQueueRepository _WorkQueueRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public WorkQueueServices() : this(new WorkQueueRepository()) { }

		public WorkQueueServices(IWorkQueueRepository workQueueRepository)
		{
			if (workQueueRepository == null) throw new ArgumentNullException("WorkQueueRepository");
			_WorkQueueRepository = workQueueRepository;
		}

		#endregion "Constructors"

		#region Public Methods

		public DataSet WorkQueueLoad(int userID)
		{
			return _WorkQueueRepository.WorkQueueLoad(userID);
		}

		#endregion Public Methods
	}
}