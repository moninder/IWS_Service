using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
    /// <summary>
    /// Interface controlling all the DB access points for a CItationENtry entity
    /// </summary>
    public interface ICitation
    {
        DataSet LoadCitationEntry(int citationID);
    }
}
