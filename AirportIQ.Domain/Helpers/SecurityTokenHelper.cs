using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AirportIQ.Domain.Contracts;
using AirportIQ.Domain.Services;

namespace AirportIQ.Domain.Helpers
{
    public class SecurityTokenHelper
    {
        #region Private constants.....
            private const int _READ_TOKEN_TABLE = 0;
            private const int _WRITE_TOKEN_TABLE = 1;
            private const int _GROUP_TOKEN_TABLE = 4;
            private const int _GROUP_READ_TOKEN_TABLE = 5;
            private const int _GROUP_WRITE_TOKEN_TABLE = 6;
        #endregion
        #region Public Properties.....
        public List<long> ReadTokens
            {
                get;
                set;
            }
            public List<long> GroupTokens
            {
                get;
                set;
            }
            public List<long> GroupReadTokens
            {
                get;
                set;
            }
            public List<long> GroupWriteTokens
            {
                get;
                set;
            }
            public List<long> WriteTokens
            {
                get;
                set;
            }
        #endregion
        #region Public Methods.....
            /// <summary>
            /// call the functions that will populate the three public properties with
            /// the security tokens for this passed user
            /// </summary>
            /// <param name="userID">THe id of the user passed in from the application that you want to 
            /// load the security tokens for</param>
            public void initialize(int userID)
            {
                //SQL call to generate the three data tables from the admin service
                DataSet dsSecurity = (DataSet)LoadSecurityTokens(userID);   // TODO: redo this to go through adminservices instead...call to it did not work.

                //-------------------------------------------------------------------
                //call generate methods and assign return values to public properties
                ReadTokens = GenerateReadTokens(dsSecurity.Tables[_READ_TOKEN_TABLE]);
                WriteTokens = GenerateWriteTokens(dsSecurity.Tables[_WRITE_TOKEN_TABLE]);
                //GroupTokens = GenerateGroupTokens(dsSecurity.Tables[_GROUP_TOKEN_TABLE]);    // all tokens in group
                GroupReadTokens = GenerateGroupReadTokens(dsSecurity.Tables[_GROUP_READ_TOKEN_TABLE]);
                GroupWriteTokens = GenerateGroupWriteTokens(dsSecurity.Tables[_GROUP_WRITE_TOKEN_TABLE]);
            }
        #endregion
        #region Private Methods.....
            private List<long> GenerateReadTokens(DataTable dt)
            {
                var rArray = (from row in dt.AsEnumerable() select row.Field<long>("ReadAccessToken")).ToList();  // type is defined in call
                return rArray;
            }

            private List<long> GenerateWriteTokens(DataTable dt)
            {
                var rArray = (from row in dt.AsEnumerable() select row.Field<long>("WriteAccessToken")).ToList();  // type is defined in call
                return rArray;
            }

            //private List<long> GenerateGroupTokens(DataTable dt)
            //{
            //    var rArray = (from row in dt.AsEnumerable() select row.Field<long>("ByGroupAccessToken")).ToList();  // type is defined in call
            //    return rArray;
            //}
            private List<long> GenerateGroupReadTokens(DataTable dt)
            {
                var rArray = (from row in dt.AsEnumerable() select row.Field<long>("ByGroupAccessToken")).ToList();  // type is defined in call   
                return rArray;
            }
            private List<long> GenerateGroupWriteTokens(DataTable dt)
            {
                var rArray = (from row in dt.AsEnumerable() select row.Field<long>("ByGroupAccessToken")).ToList();  // type is defined in call   
                return rArray;
            }

            private DataSet LoadSecurityTokens(Int32 userID)
            {
                IAdmin ds = new AirportIQ.Domain.Services.AdminServices();
                DataSet ret = ds.LoadSecurityTokens(userID);
                return ret;
            }
        #endregion
    }
}
