using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;

namespace AirportIQ.IWS.Service.Web
{
	using NUnit.Framework;
	/******************
	 * 
	 *	NOTE:	to use NUnit, do the following:
	 *	
	 *				1 - Create a new NUnit project (within the NUnit app)  and use Project | Add VS Project to add your VS project with the test code you have added 
	 *				2 - from NUNit|Tools|Settings|Test Loader|Assembly Isolation
	 *						set 'Default Domain Usage' to "Use a single AppDomain for all tests'
	 *				3 - from Project | Edit
	 *						set 'Configuration File Name' to web.config (or whatever you are using)
	 * 
	 * ****************/
	[TestFixture]
	public class IwsServiceTest
	{

		static IwsService testSvc;

		Guid goodPerson = new Guid("6E6BCBA4-5F5C-FB94-6C53-550DB5A482D3");
		Guid badPerson = new Guid("99999999-1234-1234-1234-123456789012");
		Guid nullPerson = new Guid("00000000-0000-0000-0000-000000000000");
		byte[] testImage = { 1, 2, 3, 4, 5 };

        // these are for testing [Initiate,Update]BackgroundCheck
		Guid BC_PersonID = new Guid("127B866D-A040-9CC7-8A27-92E260DE295E");
		int TSCTransactionTypeID = 4;  // Operator modification
		string TransactionControlNumber = "Test12345";


		public IwsServiceTest()
		{
			testSvc = new IwsService();
		}

		[SetUp]
		public void Init()
		{
			// to debug from an NUnit invocation, uncomment the following line
			System.Diagnostics.Debugger.Launch();
		}
		
		[Test]
		public void IsDbConnected()
		{
			Assert.IsTrue(testSvc.IsDbConnected());
		}

		/*
		[TestFixture, Description("Biometrics group")]
		public class BiometricTest : IwsServiceTest
		{
			 
		}
		*/


		[Test, Description("T02")]
		public void BiometricUpdate()
		{
			Assert.IsTrue(testSvc.BiometricUpdate(goodPerson, testImage));
			Assert.IsFalse(testSvc.BiometricUpdate(badPerson, testImage));
			Assert.IsFalse(testSvc.BiometricUpdate(nullPerson, testImage));
			Assert.IsFalse(testSvc.BiometricUpdate(goodPerson, null));
		}


		[Test, Description("T05")]
		public void UpdateBadgeID()
		{
			int goodBadgeID = 900008;
			//int badBadgeID = 0;
			int cardID = 123456;

			Assert.IsTrue(testSvc.UpdateBadgeID(cardID, goodBadgeID));
			//Assert.IsFalse(testSvc.UpdateBadgeID(cardID, badBadgeID));
		}

		// this is db code to clean up after tests so that they can be re-run
		enum ExecuteType
		{
				  Scalar,
				  NonQuery
		}

		static bool sendExpectSql(string sqlcmd, int expected, ExecuteType xtype)
		{
			int ret;
			ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(sqlcmd, sqlConn))
					{
						sqlCommand.CommandType = System.Data.CommandType.Text;

						switch (xtype)
						{
							case ExecuteType.NonQuery:
								ret = (int) sqlCommand.ExecuteNonQuery();
								break;
								
							default:
							case ExecuteType.Scalar:
								ret = (int) sqlCommand.ExecuteScalar();
								break;								

						}
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
			return (ret == expected);
		}

		static bool isExpired(Guid PersonID, int iwsCardID)
		{
			string sqlcmd = "SELECT COUNT (*) FROM [Data].[Person.BadgeStatusPeriods] as PBSP ";
			sqlcmd += "INNER JOIN [Data].[Person.Persons] AS DPP ON DPP.PersonID_IWS = '" + PersonID.ToString() + "' ";
			sqlcmd += "INNER JOIN 	[Data].[Person.PersonDivisionXref] AS DPPDX ON DPPDX.PersonID = DPP.PersonID ";
			sqlcmd += "INNER JOIN [Data].[Person.Badges] AS DPB ON DPPDX.PersonDivisionXrefID = DPB.PersonDivisionXrefID AND PBSP.BadgeID = DPB.BadgeID ";
			sqlcmd += "AND DPB.BadgeID_IWS = '" + iwsCardID + "' ";
			sqlcmd += "WHERE PBSP.BadgeStatusCode = 'EXPR' AND PBSP.Whenexpires = '12/31/9999'";

			return sendExpectSql(sqlcmd, 1, ExecuteType.Scalar);
		}


		static bool activateForTesting(Guid PersonID, int iwsCardID)
		{
			string sqlcmd = "UPDATE [Data].[Person.BadgeStatusPeriods] set BadgeStatusCode = 'ACTV' FROM [Data].[Person.BadgeStatusPeriods] as PBSP ";
			sqlcmd += "INNER JOIN [Data].[Person.Persons] AS DPP ON DPP.PersonID_IWS = '" +	PersonID.ToString() + "' ";
			sqlcmd += "INNER JOIN 	[Data].[Person.PersonDivisionXref] AS DPPDX ON DPPDX.PersonID = DPP.PersonID ";
			sqlcmd += "INNER JOIN [Data].[Person.Badges] AS DPB ON DPPDX.PersonDivisionXrefID = DPB.PersonDivisionXrefID AND PBSP.BadgeID = DPB.BadgeID ";
			sqlcmd += "AND DPB.BadgeID_IWS = '" + iwsCardID + "' ";
			sqlcmd += "WHERE PBSP.BadgeStatusCode = 'EXPR' AND PBSP.Whenexpires = '12/31/9999'";

			return sendExpectSql(sqlcmd, 1, ExecuteType.NonQuery);
		}


		
		[Test, Description("T11 - expire a badge using PersonGUID and IWS_CardID")]
		public void ExpireBadge()
		{
			int iwsCardID = 123456;
			Guid PersonID = new Guid("89C29FFE-FFC3-F10A-42B6-D3BEAD98A923");
			
			// ensure that this badge is active for our test
			if (isExpired(PersonID, iwsCardID))
			{
				activateForTesting(PersonID, iwsCardID);
			}

			Assert.IsTrue(testSvc.ExpireBadge(PersonID, iwsCardID));
			Assert.IsTrue(isExpired(PersonID, iwsCardID));

			// try to deactivate a badge that is already deactivated
			Assert.IsTrue(testSvc.ExpireBadge(PersonID, iwsCardID));
			
			// clean up after our test
			Assert.IsTrue(activateForTesting(PersonID, iwsCardID));

		}



		[Test]
		public void InitiateBackgroundCheck()
		{
			DateTime TransactionDate = DateTime.Now;
			string ProgramIdentification = "tp";
			string ResponseIdentification = "tr";
			string Status = "most excellent";
			string StatusText = "this is the most excellent status text";
			string XMLdata = "<foo/>";
			string Direction = "ToTSC";
			
			Assert.IsTrue(testSvc.InitiateBackgroundCheck(BC_PersonID, TSCTransactionTypeID, TransactionControlNumber, TransactionDate, ProgramIdentification, 
											ResponseIdentification, Status, StatusText, XMLdata, Direction));
		}

		[Test]
		public void UpdateBackgroundCheck()
		{

			string AgencyCode = "TSA";
			string CheckTypeCode = "STA";
			string TransactionTypeCode = "NTFY";
			
			DateTime TransactionDate = DateTime.Now.AddMinutes(-2);
			string Result = "this is a result for testing";
			DateTime ResultDate = DateTime.Now;
			string ResultDetails = "these are the result details for testing";
			DateTime ResultDetailDate = DateTime.Now.AddMinutes(-1);

			Assert.IsTrue(testSvc.UpdateBackgroundCheck(BC_PersonID, AgencyCode, CheckTypeCode, TransactionTypeCode, TransactionControlNumber,
											TransactionDate, Result, ResultDate, ResultDetails, ResultDetailDate));
		}
		[Test]
		public void SetFBICaseNumber()
		{
			Guid iwsPersonID = new Guid("89C29FFE-FFC3-F10A-42B6-D3BEAD98A923");
			string caseNumber = "this is the case number";
			Assert.IsFalse(testSvc.SetFBICaseNumber(iwsPersonID, caseNumber, "PASS"));
		}

		[Test]
		public void ProvisionedByBOAABadgeID()
		{
			int BOAABadgeID = 4069676;


			ProvisionData tpd = testSvc.ProvisionedByBOAABadgeID(BOAABadgeID);
			Assert.IsTrue(true);

		}

		[Test]
		public void ProvisionedByCard()
		{
			int IWS_CardID = 1932169;
			string expectedPIN = "2887";

			ProvisionData pd = testSvc.ProvisionedByCard(IWS_CardID);

			Assert.IsTrue(pd.PIN == expectedPIN);
		}

		[Test]
		public void ProvisioningComplete()
		{
			int IWS_CardID = 1932169;

			Assert.IsTrue(testSvc.ProvisioningComplete(IWS_CardID));
			Assert.IsTrue(testSvc.ProvisioningComplete(3816244));		// insert
			Assert.IsTrue(testSvc.ProvisioningComplete(3816244));		// update
			Assert.IsTrue(testSvc.ProvisioningComplete(3961861));
			Assert.IsTrue(testSvc.ProvisioningComplete(3961861));

		}

	}
}
