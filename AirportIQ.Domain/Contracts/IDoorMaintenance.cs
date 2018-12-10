using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
    /// <summary>
    /// Interface for the door maintenance service
    /// </summary>
    public interface IDoorMaintenance
    {
        DataSet Load(int userId, int? locationId, int? departmentId);

        void Save(int doorId, int departmentId, int tsaMaximumBadgeCount);

        int GetMaximumBadgeCount(int doorID);
    }
}
