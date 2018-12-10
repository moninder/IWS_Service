using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AirportIQ.Data
{
    /// <summary>
    /// Door Maintenance Repository Interface
    /// </summary>
    public interface IDoorMaintenanceRepository
    {
        DataSet Load(int userId, int? locationId, int? departmentId);

        void Save(int doorId, int departmentId, int tsaMaximumBadgeCount);

        int GetMaximumBadgeCount(int doorID);
    }
}
