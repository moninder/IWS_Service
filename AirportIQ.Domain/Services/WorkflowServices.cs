using System;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
	public class WorkflowServices : IWorkflow
	{
		#region "Private Variables"

		private readonly IWorkflowRepository _WorkflowRepository;

		#endregion "Private Variables"

		#region "Constructors"

		public WorkflowServices() : this(new WorkflowRepository()) { }

		public WorkflowServices(IWorkflowRepository workflowRepository)
		{
			if (workflowRepository == null) throw new ArgumentNullException("WorkflowRepository");
			_WorkflowRepository = workflowRepository;
		}

		#endregion "Constructors"


		#region Public Methods

		public int GetWorkID(int iD, int facilityWorkflowID)
		{
			return _WorkflowRepository.GetWorkID(iD, facilityWorkflowID);
		}

		

		#endregion
	}
}
