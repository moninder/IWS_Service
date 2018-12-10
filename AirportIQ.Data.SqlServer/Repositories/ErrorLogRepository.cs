using System;
using log4net;

namespace AirportIQ.Data.SqlServer.Repositories
{
    /// <summary>
    /// Data access layer for error logging related functionality
    /// </summary>
    public class ErrorLogRepository : IErrorRepository
    {

        #region Private Variables

            private static readonly ILog log = LogManager.GetLogger(typeof(ErrorLogRepository));

        #endregion

        #region Public Methods
       
            public void logError(Exception Ex, string ErrorMessage)
            {
                log.Error(ErrorMessage, Ex);
            }

       #endregion

    }
}