using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Helpers
{
    public interface IJsonDatasource
    {
        /// <summary>
        /// helper function to return the structure of the data object as a JSON string.  Should be implemented with the
        /// Newtonsoft.JSON object.
        /// </summary>
        /// <returns>Returns the current instance as a JSON string to be serialized to the client</returns>
        string ToJson();
    }
}
