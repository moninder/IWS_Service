using System;
using System.Configuration;
using System.Text;

namespace AirportIQ.Domain.Helpers
{
    public static class FileUploadHelper
    {

        #region Private Variables

        private static long maxFileUploadSize = Convert.ToInt64(ConfigurationManager.AppSettings["Maxfileuploadsize"]);

        #endregion

        #region Private Property

        public static long Maxfileuploadsize
        {
            get
            {
                return maxFileUploadSize;
            }
        }

        #endregion

    }
}
