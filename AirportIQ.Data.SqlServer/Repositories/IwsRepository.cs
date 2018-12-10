using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Initializers;

namespace AirportIQ.Data.SqlServer.Repositories
{

    public class IwsRepository : IIwsRepository    
    {
        #region Members

        private readonly string _Schema = ConfigurationManager.AppSettings["SBO.ApplicationSchema"];

      #endregion
      #region Fisc

      public DataTable GetFingerprintImages(Guid personID)
      {
         ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
         ConnectionStringSettings constr = connections["ApplicationServices"];
         DataTable result = null;

         using (var sqlConn = new SqlConnection(constr.ConnectionString))
         {
            try
            {
               sqlConn.Open();
               var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[FISC.GetFingerprintImages]" };
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@GUID", ParameterType.DBString, personID.ToString()));
               result = storedProcedure.ExecuteDataSet();
               result.TableName = "FingerprintImages";
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               sqlConn.Close();
            }
         }
         return result;
      }

      public DataTable GetDocuments(Guid personID)
      {
         ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
         ConnectionStringSettings constr = connections["ApplicationServices"];
         DataTable result = null;

         using (var sqlConn = new SqlConnection(constr.ConnectionString))
         {
            try
            {
               sqlConn.Open();
               var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[FISC.GetDocuments]" };
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@GUID", ParameterType.DBString, personID.ToString()));
               result = storedProcedure.ExecuteDataSet();
               result.TableName = "Documents";
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               sqlConn.Close();
            }
         }
         return result;
      }


      public DataTable GetDojStatus(Guid personID)
      {
         ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
         ConnectionStringSettings constr = connections["ApplicationServices"];
         DataTable result = null;

         using (var sqlConn = new SqlConnection(constr.ConnectionString))
         {
            try
            {
               sqlConn.Open();
               var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[FISC.GetDojStatus]" };
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@GUID", ParameterType.DBString, personID.ToString()));
               result = storedProcedure.ExecuteDataSet();
               result.TableName = "DojStatus";
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               sqlConn.Close();
            }
         }
         return result;
      }


      /// <summary>
      /// For an employee, get all of their active badges which confrom to the FISC badge model
      /// </summary>
      /// <param name="personID">Unique GUID identifier for an employee</param>
      /// <returns>A list of active badges</returns>
      public DataTable GetBadges(Guid personID)
      {
         ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
         ConnectionStringSettings constr = connections["ApplicationServices"];
         DataTable result = null;

         using (var sqlConn = new SqlConnection(constr.ConnectionString))
         {
            try
            {
               sqlConn.Open();
               var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[FISC.GetBadges]" };
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@GUID", ParameterType.DBString, personID.ToString()));
               result = storedProcedure.ExecuteDataSet();
               result.TableName = "Badges";
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               sqlConn.Close();
            }
         }
         return result;
      }

      /// <summary>
      /// For an employee, get all of the data relevant to the FISC person model
      /// </summary>
      /// <param name="personID">Unique GUID identifier for an employee</param>
      /// <returns>Person data elements</returns>
      public DataTable GetPerson(Guid personID) 
      {
         ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
         ConnectionStringSettings constr = connections["ApplicationServices"];
         DataTable result = null;

         using (var sqlConn = new SqlConnection(constr.ConnectionString))
         {
            try
            {
               sqlConn.Open();
               var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[FISC.GetPerson]" };
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@GUID", ParameterType.DBString, personID.ToString()));
               result = storedProcedure.ExecuteDataSet();
               
               //result.TableName = "Person";
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               sqlConn.Close();
            }
         }

         return result;
      }

      /// <summary>
      /// Get all of the GUID's for employees that were fingerprinted over a time range
      /// </summary>
      /// <param name="start">Begin time</param>
      /// <param name="end">End time</param>
      /// <returns>A datatable of guids which represent employees that were fingerprinted</returns>
      public DataTable GetFingerprints(DateTime start, DateTime end)
      {
         ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
         ConnectionStringSettings constr = connections["ApplicationServices"];
         DataTable result = null;

         using (var sqlConn = new SqlConnection(constr.ConnectionString))
         {
            try
            {
               sqlConn.Open();
               var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[FISC.GetFingerprinted]" };
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@StartTime", ParameterType.DBDateTime, start));
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@EndTime", ParameterType.DBDateTime, end));
               result = storedProcedure.ExecuteDataSet();
               result.TableName = "Fingerprints";
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               sqlConn.Close();
            }
         }
         return result;
      }

      /// <summary>
      /// Get the an EBTS binary data for an employee that was fingerprinted
      /// </summary>
      /// <param name="personID">Unique GUID identifier for an employee</param>
      /// <returns>Binary data of the employees EBTS file, for the LAST time they were fingerprinted</returns>
      public Byte[] GetEBTS(Guid personID)
      {
         ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
         ConnectionStringSettings constr = connections["ApplicationServices"];
         Byte[] ebtsBinary = null;

         using (var sqlConn = new SqlConnection(constr.ConnectionString))
         {
            try
            {
               sqlConn.Open();
               var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[FISC.GetEBTS]" };
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@GUID", ParameterType.DBString, personID.ToString()));
               DataTable result = storedProcedure.ExecuteDataSet();
               ebtsBinary = (Byte[])result.Rows[0][0];
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               sqlConn.Close();
            }
         }

         return ebtsBinary;
      }


      /// <summary>
      /// Given a note, add it to CS
      /// </summary>
      /// <param name="personID">Unique GUID identifier for an employee</param>
      /// <returns>If the action was successful</returns>
      public bool AddNote(Guid personID, string note)
      {
         ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
         ConnectionStringSettings constr = connections["ApplicationServices"];

         using (var sqlConn = new SqlConnection(constr.ConnectionString))
         {
            try
            {
               sqlConn.Open();
               var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[FISC.ADDtoCS.Note]" };
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@GUID", ParameterType.DBString, personID.ToString()));
               storedProcedure.Parameters.Add(new StoredProcedureParameter("@Note", ParameterType.DBString, note.ToString()));
               DataTable result = storedProcedure.ExecuteDataSet();
            }
            catch (Exception ex)
            {
               throw ex;
               //return false;
            }
            finally
            {
               sqlConn.Close();
            }
         }

         return true;
      }
      #endregion
      #region methods

      public bool BiometricUpdate(Guid personID, byte[] image)
        {
			if (personID == null)
				return false;
			if (image == null)
				return false;

			int x;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.BiometricUpdate]", sqlConn))
					{
						sqlCommand.Parameters.AddWithValue("@personGUID", personID);
						sqlCommand.Parameters.AddWithValue("@SignatureImage", image);
						sqlCommand.CommandTimeout = 120;
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
						
						x = sqlCommand.ExecuteNonQuery();
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return (x == 1);
        }

        public bool UpdateBadgeID(int cardID, int badgeID)
        {
			int x;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.UpdateBadgeID]", sqlConn))
					{
						sqlCommand.Parameters.AddWithValue("@BOAA_BadgeID", badgeID);
						sqlCommand.Parameters.AddWithValue("@IWS_CardID", cardID);
						sqlCommand.CommandTimeout = 120;
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

						x = sqlCommand.ExecuteNonQuery();
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return (x == 1);
		}

		public bool InitiateBackgroundCheck(Guid personID, int TSCTransactionTypeID, string TransactionControlNumber, DateTime TransactionDate, string ProgramIdentification,
											string ResponseIdentification, string Status, string StatusText, string XMLdata, string Direction)
        {

			if (personID == null)
				return false;

			int x;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.InitiateBackgroundCheck]", sqlConn))
					{
						sqlCommand.Parameters.AddWithValue("@IWS_PersonGUID", personID);
						sqlCommand.Parameters.AddWithValue("@TSCTransactionTypeID", TSCTransactionTypeID);
						sqlCommand.Parameters.AddWithValue("@TransactionControlNumber", TransactionControlNumber);
						sqlCommand.Parameters.AddWithValue("@TransactionDate", TransactionDate);
						sqlCommand.Parameters.AddWithValue("@ProgramIdentification", ProgramIdentification);
						sqlCommand.Parameters.AddWithValue("@ResponseIdentification", ResponseIdentification);
						sqlCommand.Parameters.AddWithValue("@Status", Status);
						sqlCommand.Parameters.AddWithValue("@StatusText", StatusText);
						sqlCommand.Parameters.AddWithValue("@xmlData", XMLdata);
						sqlCommand.Parameters.AddWithValue("@Direction", Direction);

						sqlCommand.CommandTimeout = 120;
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

						x = sqlCommand.ExecuteNonQuery();
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return (x == 1);
        }

		//Rguidi 6/6/2013 new process: replaces InitiateBackgroundCheck
		public bool UpdateBackgroundCheckStatus(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, DateTime TransactionDate,
										 string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, string XMLdata)
		{
			if (personID == null)
				return false;

			if (String.IsNullOrEmpty(ProgramIdentification)) { ProgramIdentification = string.Empty; }
			if (String.IsNullOrEmpty(TransactionControlNumber)) { TransactionControlNumber = string.Empty; }
			if (String.IsNullOrEmpty(ResponseIdentification)) { ResponseIdentification = ""; } //does not like string.Empty

			int x;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.UpdateBackgroundCheckStatus]", sqlConn))
					{
						sqlCommand.Parameters.AddWithValue("@IWS_PersonGUID", personID);
						sqlCommand.Parameters.AddWithValue("@TransactionName", TransactionName);
						sqlCommand.Parameters.AddWithValue("@AgencyCode", AgencyCode);
						sqlCommand.Parameters.AddWithValue("@TransactionControlNumber", TransactionControlNumber);
						sqlCommand.Parameters.AddWithValue("@TransactionDate", TransactionDate);
						sqlCommand.Parameters.AddWithValue("@ProgramIdentification", ProgramIdentification);
						sqlCommand.Parameters.AddWithValue("@ResponseIdentification", ResponseIdentification);
						sqlCommand.Parameters.AddWithValue("@TransmissionStatus", TransmissionStatus);
						sqlCommand.Parameters.AddWithValue("@TransmissionStatusText", TransmissionStatusText);
						sqlCommand.Parameters.AddWithValue("@xmlData", XMLdata);

						sqlCommand.CommandTimeout = 120;
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

						x = sqlCommand.ExecuteNonQuery();
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return (x >= 1);
		}

		public bool UpdateBackgroundCheck(Guid personID, string AgencyCode, string CheckTypeCode, string TransactionTypeCode, string TransactionControlNumber,
											DateTime TransactionDate, string Result, DateTime ResultDate, string ResultDetails, DateTime ResultDetailDate)
        {
			if (personID == null)
				return false;

			int x;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.UpdateBackgroundCheck]", sqlConn))
					{
						sqlCommand.Parameters.AddWithValue("@IWS_PersonGUID", personID);
						sqlCommand.Parameters.AddWithValue("@AgencyCode", AgencyCode);
						sqlCommand.Parameters.AddWithValue("@CheckTypeCode", CheckTypeCode);
						sqlCommand.Parameters.AddWithValue("@TransactionTypeCode", TransactionTypeCode);
						sqlCommand.Parameters.AddWithValue("@TransactionControlNumber", TransactionControlNumber);
						sqlCommand.Parameters.AddWithValue("@TransactionDate", TransactionDate);
						sqlCommand.Parameters.AddWithValue("@Result", Result);
						sqlCommand.Parameters.AddWithValue("@ResultDate", ResultDate);
						sqlCommand.Parameters.AddWithValue("@ResultDetails", ResultDetails);
						sqlCommand.Parameters.AddWithValue("@ResultDetailDate", ResultDetailDate);

						sqlCommand.CommandTimeout = 120;
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

						x = sqlCommand.ExecuteNonQuery();
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return (x == 1);
      }

		//Rguidi 6/6/2013 new process: replaces UpdateBackgroundCheck
		public bool UpdateBackgroundCheckResult(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, 
										DateTime TransactionDate, string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, 
										string XMLdata,
										string AgencyResult, DateTime AgencyResultDate, string AgencyResultDetails, DateTime AgencyResultDetailDate,
										string BackgroundCheckID, string BackgroundCheckType, string company)
    {
			if (personID == null)
				return false;

			int x;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.UpdateBackgroundCheckResult]", sqlConn))
					{
						sqlCommand.Parameters.AddWithValue("@IWS_PersonGUID", personID);
						sqlCommand.Parameters.AddWithValue("@TransactionName", TransactionName);
						sqlCommand.Parameters.AddWithValue("@AgencyCode", AgencyCode);
						sqlCommand.Parameters.AddWithValue("@TransactionControlNumber", TransactionControlNumber);
						sqlCommand.Parameters.AddWithValue("@TransactionDate", TransactionDate);
						sqlCommand.Parameters.AddWithValue("@ProgramIdentification", ProgramIdentification);
						sqlCommand.Parameters.AddWithValue("@ResponseIdentification", ResponseIdentification);
						sqlCommand.Parameters.AddWithValue("@TransmissionStatus", TransmissionStatus);
						sqlCommand.Parameters.AddWithValue("@TransmissionStatusText", TransmissionStatusText);
						sqlCommand.Parameters.AddWithValue("@xmlData", XMLdata);
						sqlCommand.Parameters.AddWithValue("@AgencyResult", AgencyResult);
						sqlCommand.Parameters.AddWithValue("@AgencyResultDate", AgencyResultDate);
						sqlCommand.Parameters.AddWithValue("@AgencyResultDetails", AgencyResultDetails);
						sqlCommand.Parameters.AddWithValue("@AgencyResultDetailDate", AgencyResultDetailDate);
						sqlCommand.Parameters.AddWithValue("@BackgroundCheckID", BackgroundCheckID);
						sqlCommand.Parameters.AddWithValue("@BackgroundCheckType", BackgroundCheckType);

						sqlCommand.CommandTimeout = 120;
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

						x = sqlCommand.ExecuteNonQuery();
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return (x >= 1);
      }


    public string RetreiveFbiHistoryUrl(Guid personID)
    {
        return ((null != personID) ? personID.ToString() : "");
        //throw new NotImplementedException();
    }



		public bool SetFBICaseNumber(Guid personID, string CaseNumber, string result, DateTime resultDate)
		{
			if (personID == null)
				return false;

			int x;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.SetFBICaseNumber]", sqlConn))
					{
						sqlCommand.Parameters.AddWithValue("@IWS_PersonGUID", personID);
						sqlCommand.Parameters.AddWithValue("@CaseNumber", CaseNumber);
                  sqlCommand.Parameters.AddWithValue("@Result", result);
                  sqlCommand.Parameters.AddWithValue("@ResultDate", resultDate);
                  sqlCommand.CommandTimeout = 120;
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

						x = sqlCommand.ExecuteNonQuery();
					}
				}
				catch
				{
               System.Threading.Thread.Sleep(1000); //p.capp retry one time for TCP Transport error timeout
               try
               {
                  using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.SetFBICaseNumber]", sqlConn))
                  {
                     sqlCommand.Parameters.AddWithValue("@IWS_PersonGUID", personID);
                     sqlCommand.Parameters.AddWithValue("@CaseNumber", CaseNumber);
                     sqlCommand.Parameters.AddWithValue("@Result", result);
                     sqlCommand.Parameters.AddWithValue("@ResultDate", resultDate);
                     sqlCommand.CommandTimeout = 120;
                     sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                     x = sqlCommand.ExecuteNonQuery();
                  }
               }
               catch
               {
                  throw;
               }
              
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return (x == 1);
		}

      public bool SetTSACaseNumber(Guid personID, string CaseNumber, string result, DateTime resultDate)
      {
         if (personID == null)
            return false;

         int x;
         ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
         ConnectionStringSettings constr = connections["ApplicationServices"];

         using (var sqlConn = new SqlConnection(constr.ConnectionString))
         {
            try
            {
               sqlConn.Open();
               using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.SetTSACaseNumber]", sqlConn))
               {
                  sqlCommand.Parameters.AddWithValue("@IWS_PersonGUID", personID);
                  sqlCommand.Parameters.AddWithValue("@CaseNumber", CaseNumber);
                  sqlCommand.Parameters.AddWithValue("@Result", result);
                  sqlCommand.Parameters.AddWithValue("@ResultDate", resultDate);
                  sqlCommand.CommandTimeout = 120;
                  sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                  x = sqlCommand.ExecuteNonQuery();
               }
            }
            catch
            {
               throw;
            }
            finally
            {
               sqlConn.Close();
            }
         }
         return (x == 1);
      }

      public bool ExpireBadge(Guid personID, int cardID)
		{
			int ret = 0;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.ExpireBadge]", sqlConn))
					{
						sqlCommand.Parameters.AddWithValue("@IWS_PersonGUID", personID);
						sqlCommand.Parameters.AddWithValue("@IWS_CardID", cardID);
						sqlCommand.CommandTimeout = 120;
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        ret = (int) sqlCommand.ExecuteScalar();
					}
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return ret > 0;
		}


		public DataTable ProvisionedByCard(int cardID)
		{
			DataTable result = null;

			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[IWS.GetProvisionedAccess]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@IWS_CardID", ParameterType.DBInteger, cardID));
			result = storedProcedure.ExecuteDataSet();

			return result;
		}


		public DataTable ProvisionedByBOAABadgeID(int BOAABadgeID)
		{
			DataTable result = null;

			var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[IWS.GetProvisionedAccessByBOAABadgeID]" };
			storedProcedure.Parameters.Add(new StoredProcedureParameter("@BOAABadgeID", ParameterType.DBInteger, BOAABadgeID));
			result = storedProcedure.ExecuteDataSet();

			return result;
		}



		public bool ProvisioningComplete(int cardID)
		{
			int x;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(_Schema + ".[IWS.ProvisioningComplete]", sqlConn))
					{
						sqlCommand.Parameters.AddWithValue("@IWS_CardID", cardID);
						sqlCommand.CommandTimeout = 120;
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

						x = sqlCommand.ExecuteNonQuery();
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return true;
		}


        /// <summary>
        /// Test method that attempts to connect to the DB and return a record.  Returns true if the record is present.
        /// This is used for testing the DB Connectivity and the Web Service Setup
        /// </summary>
        /// <returns>True if the call succeeds</returns>
        public bool IsDbAlive()
        {
            bool ret = false;

            DataTable result = null;
            var storedProcedure = new StoredProcedure() { StoredProcedureName = _Schema + ".[IWS.IsDbAlive]" };

			try
			{
				result = storedProcedure.ExecuteDataSet();
			}
			catch (Exception ex)
			{
				string s = ex.Message;
			}
            ret = (result.Rows.Count > 0);
            
            return ret;
        }


        public int GetLastTSCTransactionID(int PersonDivisionCheckID)
        {
            int ReturnValue = 0;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 TSCTransactionID FROM [Data].[BackgroundCheck.TSCTransactions] WHERE PersonDivisionCheckID = @PersonDivisionCheckID ORDER BY TSCTransactionID DESC", conn);
                cmd.Parameters.AddWithValue("@PersonDivisionCheckID", PersonDivisionCheckID);
                conn.Open();
                ReturnValue = (int) cmd.ExecuteScalar();
                conn.Close();
            }

            return ReturnValue;
        }

        public DataSet GetUpdatePersonCompleteCount(int TSCTransactionID)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[App.BCAT].[GetUpdatePersonCompleteCount]", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@TSCTransactionID", TSCTransactionID);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
            }

            return ds;
        }

        public void SaveDeletePersonSubmission(int PersonDivisionCheckID, string TransactionGUID, int UserID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[App.BCAT].[InsertTSCTransaction]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TransactionType", "DELETE PERSON SUBMITTED"));
                cmd.Parameters.Add(new SqlParameter("@PersonDivisionCheckID", PersonDivisionCheckID));
                cmd.Parameters.Add(new SqlParameter("@Status", "SUBMITTED"));
                cmd.Parameters.Add(new SqlParameter("@StatusText", "Delete Person Request submitted to TSC"));
                cmd.Parameters.Add(new SqlParameter("@Direction", "TO_TSC"));
                cmd.Parameters.Add(new SqlParameter("@TransactionDate", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@TransactionControlNumber", TransactionGUID));
                cmd.Parameters.Add(new SqlParameter("@UserID", UserID));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void ArchiveFingerprints(string SSN, string CompanyCode, string DivisionCode)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Utility.ArchiveFingerprints", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SSN", SSN));
                cmd.Parameters.Add(new SqlParameter("@WhenFingerprintsTaken", new DateTime(1901, 1, 1)));
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", CompanyCode));
                cmd.Parameters.Add(new SqlParameter("@DivisionCode", DivisionCode));
                cmd.Parameters.Add(new SqlParameter("@DoArchive", true));
                cmd.Parameters.Add(new SqlParameter("@DoDelete", true));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void SaveBadgeCancellationTransaction(int PersonDivisionCheckID, int UserID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[App.BCAT].[InsertTSCTransaction]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TransactionType", "ACTIVE BADGES CANCELED"));
                cmd.Parameters.Add(new SqlParameter("@PersonDivisionCheckID", PersonDivisionCheckID));
                cmd.Parameters.Add(new SqlParameter("@Status", "BADGES CANCELED"));
                cmd.Parameters.Add(new SqlParameter("@StatusText", "All Active Badges Canceled"));
                cmd.Parameters.Add(new SqlParameter("@Direction", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@TransactionDate", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@TransactionControlNumber", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@UserID", UserID));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void CancelBadge(int BadgeID, int UserID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[BL].[Badge.SetStatus]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BadgeID", BadgeID);
                cmd.Parameters.AddWithValue("@StatusCode", "CANC");
                cmd.Parameters.AddWithValue("@ExpirationDate", new DateTime(9999, 12, 31));
                cmd.Parameters.AddWithValue("@UserID", UserID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet GetPersonsForBatchUpdate()
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[App.Sbo].[IWS.GetPersonsForBatchUpdate]", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
            }
            return ds;
        }

        public DataSet GetAllPersonUpdates()
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[App.Sbo].[IWS.GetAllPersonUpdates]", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 300;
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);
            }
            return ds;
        }

        public DataSet PopulatePersonUpdate(List<int> BadgeIDs)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[App.Sbo].[IWS.PopulatePersonUpdate]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;                
                cmd.Parameters.AddWithValue("@PersonIDs", AirportIQ.Data.Helpers.XmlHelper.Serialize(BadgeIDs));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);                
            }

            return ds;
        }

        public DataSet GetPersonUpdate(int PersonUpdateID)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[App.Sbo].[IWS.GetPersonUpdate]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonUpdateID", PersonUpdateID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }

            return ds;
        }


        public bool MarkPersonUpdateDetailAsSent(int PersonUpdateID, int BadgeID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[App.Sbo].[IWS.MarkPersonUpdateDetailAsSent]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonUpdateID", PersonUpdateID);
                cmd.Parameters.AddWithValue("@PersonID", BadgeID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return true;
        }

    #endregion

    }
}
