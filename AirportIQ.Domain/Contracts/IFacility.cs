using System;
using System.Data;

namespace AirportIQ.Domain.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// History:
    /// JBienvenu 2013-01-10 19303: Added 'Code' and 'staff'.
    /// </remarks>
    public interface IFacility
    {
        /// <summary>
        /// Lambda function to get the facility code.
        /// </summary>
        /// <remarks>
        /// Declared 'Func(string)' rather than 'string' because the global variable that has the string may not be set when the property is set.
        /// </remarks>
        Func<string> Code { get; set; }

        DataTable workLocationsFormLoad();

        /// <summary>
        /// Returns list of staff members (ID and name) for the facility indicated by this.Code().
        /// </summary>
        /// <returns></returns>
        DataTable staffListLoad(); 
    }
}
