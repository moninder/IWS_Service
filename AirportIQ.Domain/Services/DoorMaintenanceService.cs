using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Data;
using System.Data;

namespace AirportIQ.Domain.Services
{
    public class DoorMaintenanceService : Contracts.IDoorMaintenance
    {
        private readonly IDoorMaintenanceRepository _repository;

        public DoorMaintenanceService() : this(new Data.SqlServer.Repositories.DoorMaintenanceRepository())
        {
        }

        public DoorMaintenanceService(IDoorMaintenanceRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            _repository = repository;
        }

        public DataSet Load(int userId, int? locationId, int? departmentId)
        {
            return _repository.Load(userId, locationId, departmentId);
        }

        public void Save(int doorId, int departmentId, int tsaMaximumBadgeCount)
        {
            _repository.Save(doorId, departmentId, tsaMaximumBadgeCount);
        }

        public int GetMaximumBadgeCount(int doorID)
        {
            return _repository.GetMaximumBadgeCount(doorID);
        }
    }
}
