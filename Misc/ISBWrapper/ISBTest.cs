using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


using ImageWare.ISBLibrary;
 
namespace ISBLibTest
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
	[TestFixture("192.168.16.12:8080", "192.168.16.12:8080", "192.168.16.12:8080")]
	public class ISBTest
	{

		public ISBLibExtGCR testSvc;

		static string defaultIP = "iws03qa8:8080";
		string DOCM_IP = defaultIP;
		string IDMS_IP = defaultIP;
		string CMS_IP = defaultIP;
		string EBTS_IP = "192.168.16.250:8080";

		Guid testPerson = new Guid("3545EA74-700D-43F3-B78B-4562E818E46E");
		string TestPersonString = "3545EA74-700D-43F3-B78B-4562E818E46E";
		int docType = 1;
		int TestBOAA_BadgeID = 23259; // 99805;   // 23259,   99757
		//int TestCMS_CardID = 3992113; // 3992112;   // 3992113, 3992111

		public ISBTest(string docm_ip, string idms_ip, string cms_ip, string edts_ip)   
		{
			DOCM_IP = docm_ip;
			IDMS_IP = idms_ip;
			CMS_IP = cms_ip;
			EBTS_IP = edts_ip;
		}

		[SetUp]
		public void Init()
		{
			testSvc = new ISBLibExtGCR(DOCM_IP, IDMS_IP, CMS_IP,EBTS_IP);
			testSvc.log("\nUsing IP addresses  DOCM:" + DOCM_IP + " IDMS:" + IDMS_IP + " CMS:" + CMS_IP + " EBTS:" + EBTS_IP) ;
			// to debug from an NUnit invocation, uncomment the following line
			System.Diagnostics.Debugger.Launch();
		}


		[Test, Description("item 6")]
		public void GetPicture()
		{
			GetPicture("7EC7B093-9103-311A-14E8-B308DF3FC15D");
			GetPicture("7D62FF11-6047-B20D-3CB4-F3523864FA84");
			GetPicture("C833777D-1BD0-5273-AAB3-990D99D89D8E");
		}

		public void GetPicture(string personString)
		{
			testSvc.log("\n\n\n\nTesting item 6 GetPicture(" + personString + ")");

			List<ImageWare.ISBLibrary.DMDocument> docs = testSvc.GetPicture(personString);

			testSvc.log(personString + " has " + docs.Count() + " pictures" + (docs.Count() > 0 ? ":" : "."));

			if (docs.Count() > 0)
			{
				ImageWare.ISBLibrary.DMDocumentInfo d;
				testSvc.log("    Location, Type, Version, Created, CreatedBy ");
				for (int x = 0; x < docs.Count(); x++)
				{
					d = docs[x].DocumentInfo;
					testSvc.log("    " + d.Location + ", " + d.Type + ", " + d.Version + "," + d.Created + "," + d.CreatedBy + ".");
				}
			}
		}

		[Test, Description("item 6")]
		public void GetIdentityDocument()
		{
			GetIdentityDocument("3CD97F5B-FD5E-4C88-A743-D9090CD111B6");
			GetIdentityDocument("A860B747-5B32-4370-8D9D-863A3FDAF206");
			GetIdentityDocument("1FADE394-05ED-4514-96D5-C8024DB69A8F");

			// no docs
			GetIdentityDocument("C833777D-1BD0-5273-AAB3-990D99D89D8E");
		}

		public void GetIdentityDocument(string personString)
		{
			testSvc.log("\n\n\n\nTesting item 6 GetIdentityDocument(" + personString + ")");

			ImageWare.ISBLibrary.DMDocument doc = testSvc.GetIdentityDocument(personString);

			if (doc == null)
			{
				testSvc.log(personString + " has no pictures in DOCM.");
				return;
			}

			ImageWare.ISBLibrary.DMDocumentInfo d = doc.DocumentInfo;
			testSvc.log("    Location, Type, Version, Created, CreatedBy ");
			testSvc.log("    " + d.Location + ", " + d.Type + ", " + d.Version + "," + d.Created + "," + d.CreatedBy + ".");
		}


		[Test, Description("item 6")]
		public void GetIdentityDocumentVersion()
		{
			//GetIdentityDocumentVersion("7EC7B093-9103-311A-14E8-B308DF3FC15D");
			//GetIdentityDocumentVersion("7D62FF11-6047-B20D-3CB4-F3523864FA84");
			//GetIdentityDocumentVersion("C833777D-1BD0-5273-AAB3-990D99D89D8E");
			GetIdentityDocumentVersion("3CD97F5B-FD5E-4C88-A743-D9090CD111B6");
			GetIdentityDocumentVersion("A860B747-5B32-4370-8D9D-863A3FDAF206");
			GetIdentityDocumentVersion("1FADE394-05ED-4514-96D5-C8024DB69A8F");
		}

		public void GetIdentityDocumentVersion(string personString)
		{
			testSvc.log("\n\n\n\nTesting item 6 GetIdentityDocumentVersion(" + personString + ")");
			int v = testSvc.GetIdentityDocumentVersion(personString);
			if (v == -1)
			{
				testSvc.log(personString + " has no pictures in DOCM.");
				return;
			}
			testSvc.log("Version = " + v + ", IWS_PersonGUID = " + personString );

			int v2 = testSvc.GetIdentityDocumentVersion(personString);
			if (v2 == -1)
			{
				testSvc.log(personString + " has no pictures in DOCM.");
				Assert.IsTrue(false);
			}
			Assert.AreEqual(v, v2);
		}

		[Test, Description("item 10")]
		public void RevokeCardByBOAABadgeID()
		{
			RevokeCardByBOAABadgeID(TestBOAA_BadgeID, "revoked for unit testing");
			RevokeCardByBOAABadgeID(99805, "revoked for unit testing");
			RevokeCardByBOAABadgeID(99757, "revoked for unit testing");
		}

		public void RevokeCardByBOAABadgeID(int BOAA_BadgeID, string reason)
		{
			testSvc.log("\n\n\n\nTesting item 10 RevokeCardByBOAABadgeID(" + BOAA_BadgeID + ", " + reason + ")");

			//Assert.IsTrue(testSvc.CMSRevokeCardByBOAABadgeID(BOAA_BadgeID, reason));
			CMSRevokeCardResponse b = testSvc.CMSRevokeCardByBOAABadgeID(BOAA_BadgeID, reason);
		}


		[Test, Description("item 14")]
		public void GetProvisioningAuditData()
		{
			GetProvisioningAuditData(-1, 99757);
			GetProvisioningAuditData(-1, 99805);
			GetProvisioningAuditData(-1, 23259);
			GetProvisioningAuditData(-1, 74688);
		}

		public void GetProvisioningAuditData(int CMS_CardID, int BOAA_BadgeID)
		{
			testSvc.log("\n\n\n\nTesting item 14 GetProvisioningAuditData(" + CMS_CardID + ", " + BOAA_BadgeID + ")");

			List<CardProvisionData> provData = testSvc.GetProvisioningAuditData(CMS_CardID, BOAA_BadgeID);

			if (provData.Count == 0)
			{
				testSvc.log("no provisioning data returned.");
			}

			foreach (CardProvisionData cpd in provData)
			{
				testSvc.log("     Position:" + cpd.Position);
				testSvc.log("  Access Type:" + cpd.AccessType);
				testSvc.log("  Category ID:" + cpd.CategoryID);
				testSvc.log("Category Name:" + cpd.CategoryName);
				testSvc.log("       Active:" + cpd.ActiveDate);
				testSvc.log("      Expires:" + cpd.ExpireDate);
			}

			Assert.IsTrue(1 == 1);			
			// FIX THIS
		}

		[Test, Description("item 16")]
		public void GetPersonInfoForBadge()
		{
			int TestBOAA_BadgeID = 123;
			GetPersonInfoForBadge(TestBOAA_BadgeID);
		}
		public void GetPersonInfoForBadge(int BOAA_BadgeID)
		{
			testSvc.log("\n\n\n\nTesting item 16 GetPersonInfoForBadge(" + BOAA_BadgeID + ")");

			ISBLibTest.IBMAPerson libperson = testSvc.GetPersonInfoForBadge(BOAA_BadgeID);

			if (libperson == null)
			{
				testSvc.log("libperson is null error");
				return;
			}

			ImageWare.ISBLibrary.IDMSPerson person = libperson.Person;

			testSvc.log("last name is '" + person.LastName + "'.");
			testSvc.log("first name is '" + person.FirstName + "'.");
			testSvc.log("DOB is '" + person.DOB + "'.");
			testSvc.log("status is '" + person.Status + "'.");
			Assert.IsTrue(1 == 1);
			// FIX THIS
		}



		[Test, Description("item 17")]
		public void GetCardIDsForPerson()
		{
			GetCardIDsForPerson("1DDD9685-7DC6-0012-FE06-06146FCEE1A4");
			GetCardIDsForPerson("C833777D-1BD0-5273-AAB3-990D99D89D8E");

			GetCardIDsForPerson("7D62FF11-6047-B20D-3CB4-F3523864FA84");
			GetCardIDsForPerson("01E51989-75CD-D64D-5FB0-A3F88C11C25E");
			GetCardIDsForPerson("CAC9D393-4868-1BE7-0AEE-BEC30A3C4072");
			GetCardIDsForPerson("FE9164C5-8CCC-3D07-6C81-1B69A3AB6539");
		}

		public void GetCardIDsForPerson(string personString)
		{
			testSvc.log("\n\n\n\nTesting item 17 GetCardIDsForPerson(" + personString + ")");

			ISBLibTest.IBMAPerson libperson = testSvc.GetCardIDsForPerson(personString);

			if (libperson == null)
			{
				testSvc.log("libperson is null error");
				return;
			}

			Debug.Write("IBMACardIDs:");
			foreach (int i in libperson.IBMACardID)
			{
				Debug.Write(" " + i);
			}
			testSvc.log(" ");

			Debug.Write("BOAABadgeIDs:");
			foreach (int i in libperson.BOAACardID)
			{
				Debug.Write(" " + i);
			}
			testSvc.log(" ");

			Assert.IsTrue(1 == 1);
			// FIX THIS
		}


		[Test, Description("item 18")]
		public void GetPersonDocuments()
		{
			GetPersonDocuments(TestPersonString);
		}

		public void GetPersonDocuments(string personString)
		{
			testSvc.log("\n\n\n\nTesting item 18 GetPersonDocuments(" + personString + ")");

			List<ImageWare.ISBLibrary.DMDocumentInfo> personDocs = testSvc.GetPersonDocuments(personString);

			testSvc.log(personString + " has " + personDocs.Count() + " documents.");

		}

		[Test, Description("item 19")]
		public void GetPersonDemographic()
		{
			GetPersonDemographic(TestPersonString);
		}

		public void GetPersonDemographic(string personString)
		{

			testSvc.log("\n\n\n\nTesting item 19 GetPerson(" + personString + ")");
			ImageWare.ISBLibrary.IDMSPerson person = testSvc.GetPerson(personString);

			if (person == null)
			{
				testSvc.log("GetPerson error");
				return;
			}
			testSvc.log("person.ToString() output:" + person.ToString());
			testSvc.log("last name is '" + person.LastName + "'.");
			testSvc.log("first name is '" + person.FirstName + "'.");
			testSvc.log("DOB is '" + person.DOB + "'.");
			testSvc.log("status is '" + person.Status + "'.");
			Assert.IsTrue(1 == 1);

		}


		[Test, Description("item 20")]
		public void GetNonBiometricDocuments()
		{
			GetNonBiometricDocuments("1DDD9685-7DC6-0012-FE06-06146FCEE1A4");
			GetNonBiometricDocuments("FE9164C5-8CCC-3D07-6C81-1B69A3AB6539");
			GetNonBiometricDocuments("0384B853-3970-15B8-0835-431DD7ED95B0");
   		}

		public void GetNonBiometricDocuments(string personString)
		{
			testSvc.log("\n\n\n\nTesting item 20 GetNonBiometricDocuments(" + personString + ")");

			List<ImageWare.ISBLibrary.DMDocument> docs = testSvc.GetNonBiometricDocuments(personString);

			testSvc.log(personString + " has " + docs.Count() + " non-biometric documents" + (docs.Count() > 0 ? ":" : "."));

			if (docs.Count() > 0)
			{
				ImageWare.ISBLibrary.DMDocumentInfo d;
				testSvc.log("    Location, Type, Version, GUID, PersonGUID, Created, CreatedBy ");
				for (int x = 0; x < docs.Count(); x++)
				{
					d = docs[x].DocumentInfo;
					testSvc.log("    " + d.Location + ", " + d.Type + ", " + d.Version + "," + d.GUID + "," + d.PersonGUID + "," + d.Created + "," + d.CreatedBy + ".");
				}
			}
		}

		
		[Test, Description("generic for 6 and 20")]
		public void GetDocument()
		{
			GetDocument(TestPersonString, docType);
		}

		public void GetDocument(string personString, int docType)
		{
			testSvc.log("\nTesting GetDocument(" + personString + ", " + docType + ")");

			List<ImageWare.ISBLibrary.DMDocument> docs = testSvc.GetDocument(personString, docType);

			testSvc.log(personString + " has " + docs.Count() + " documents of type " + docType + (docs.Count() > 0 ? ":" : "."));

			if (docs.Count() > 0)
			{
				ImageWare.ISBLibrary.DMDocumentInfo d;
				testSvc.log("    name, type, version ");
				for (int x = 0; x < docs.Count(); x++)
				{
					d = docs[x].DocumentInfo;
					testSvc.log("    " + d.Location + ", " + d.Type + ", " + d.Version + ".");
				}
			}
		}

		[Test, Description("calls all other tests")]
		public void AllTests()
		{
			AllIDMSTests();

			AllDOCMTests();

			AllCMSTests();

			Audit();
		}



		[Test, Description("calls all CMS tests")]
		public void AllCMSTests()
		{

			Audit(ThresholdDays: 256);

			RevokeCardByBOAABadgeID();					// item 10
			GetProvisioningAuditData();					// item 14
			GetPersonInfoForBadge();					// item 16
			GetCardIDsForPerson();						// item 17
		}

		public void AllCMSTests(string personString, int CMS_CardID, int BOAA_BadgeID)
		{
			testSvc.log("\n\n\n\nAllCMSTests(" + personString + ")");


			Audit(ThresholdDays: 256);

			RevokeCardByBOAABadgeID(TestBOAA_BadgeID, "revoked for unit testing");	// item 10
			//GetProvisioningAuditData(3992112, 99805);		// item 14
			//GetProvisioningAuditData(3992111, 99757);		// item 14
			GetProvisioningAuditData(CMS_CardID, BOAA_BadgeID);		// item 14

			GetPersonInfoForBadge(BOAA_BadgeID);					// item 16
			GetCardIDsForPerson(personString);						// item 17

			Audit(ThresholdDays: 256);
		}

		[Test, Description("calls all DOCM tests")]
		public void AllDOCMTests()
		{
			GetIdentityDocumentVersion();	// item 6
			GetIdentityDocument();			// item 6
			GetPicture();					// item 6
			GetPersonDocuments();			// item 18
			GetNonBiometricDocuments();		// item 20
			GetDocument();					// item 6 and item 20
		}

		public void AllDOCMTests(string personString, int CMS_CardID, int BOAA_BadgeID)
		{
			testSvc.log("\n\n\n\nAllDOCMTests(" + personString + ")");


		}


		[Test, Description("calls all IDMS tests")]
		public void AllIDMSTests()
		{
				GetPersonDemographic();				// item 19
		}

		public void AllIDMSTests(string personString, int CMS_CardID, int BOAA_BadgeID)
		{
			testSvc.log("\n\n\n\nAllDOCMTests(" + personString + ")");


		}


		[Test, Description("Audit")]
		public void Audit()
		{
			Audit(365);
		}


		private List<int> GetBadgesToAudit(int threshold)
		{
			string name = "GetBadgesToAudit";
			List<int> badgeIDs = new List<int>();
			string sqlcmd = "[App.Sbo].[IWS.GetBadgesToAudit]";
			ConnectionStringSettingsCollection connections = System.Configuration.ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(sqlcmd, sqlConn))
					{
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

						SqlParameter param = new SqlParameter();
						param.ParameterName = "@THRESHOLD";
						param.Value = threshold;

						sqlCommand.Parameters.Add(param);

						SqlDataReader reader = sqlCommand.ExecuteReader();
						while (reader.Read())
						{
							badgeIDs.Add((int)reader[0]);
						}
					}
				}
				catch (Exception ex)
				{
					testSvc.log(name + " IWS.GetBadgesToAudit Exception.Message: " + ex.Message);
					testSvc.log(name + " IWS.GetBadgesToAudit Exception.InnerException: " + ex.InnerException);
					testSvc.log(name + " IWS.GetBadgesToAudit Exception.Source: " + ex.Source);
					testSvc.log(name + " IWS.GetBadgesToAudit Exception.StackTrace: " + ex.StackTrace);
					throw ex;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return badgeIDs;
		}

		private bool SetBadgeToWaiting(int badgeID)
		{
			string name = "SetBadgeToWaiting";
			string sqlcmd = "UPDATE [Data].[Audit.CMSBadges] SET Status = 0, LastTransmitted = GETDATE() WHERE BadgeID = " + badgeID;
			ConnectionStringSettingsCollection connections = System.Configuration.ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					int numrows;
					using (var sqlCommand = new SqlCommand(sqlcmd, sqlConn))
					{
						sqlCommand.CommandType = System.Data.CommandType.Text;
						if ((numrows = (int) sqlCommand.ExecuteNonQuery()) != 1)
						{
							testSvc.log(name + " ERROR: updating" + badgeID + " returned " + numrows + " rows.  IGNORING...");
							testSvc.log(name + "        There is a problem with badge '" + badgeID + "' in the [Data].[Audit.CMSBadges] table.");
							return false;
						}
					}
				}
				catch (Exception ex)
				{
					testSvc.log(name + " Exception.Message: " + ex.Message);
					testSvc.log(name + " Exception.InnerException: " + ex.InnerException);
					testSvc.log(name + " Exception.Source: " + ex.Source);
					testSvc.log(name + " Exception.StackTrace: " + ex.StackTrace);
					throw ex;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return true;
		}


		private bool DoUpdateCmd(string name, int badgeID, string sqlcmd, int rowsExpected)
		{
			ConnectionStringSettingsCollection connections = System.Configuration.ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];
			bool ret = true;

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					int numrows;
					using (var sqlCommand = new SqlCommand(sqlcmd, sqlConn))
					{
						sqlCommand.CommandType = System.Data.CommandType.Text;
						if ((numrows = (int)sqlCommand.ExecuteNonQuery()) != rowsExpected)
						{
							testSvc.log(name + " ERROR: updating" + badgeID + " returned " + numrows + " rows when " + rowsExpected + " were expected.  IGNORING...");
							testSvc.log(name + "        There is a problem with badge '" + badgeID + "' in the [Data].[Audit.CMSBadges] table.");
							ret = false;
						}
					}
				}
				catch (Exception ex)
				{
					testSvc.log(name + " Exception.Message: " + ex.Message);
					testSvc.log(name + " Exception.InnerException: " + ex.InnerException);
					testSvc.log(name + " Exception.Source: " + ex.Source);
					testSvc.log(name + " Exception.StackTrace: " + ex.StackTrace);
					throw ex;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return ret;
		}

		private void AuditFailed(int badgeID, string errorText)
		{
			string name = "AuditFailed";
			string sqlcmd = "UPDATE [Data].[Audit.CMSBadges] SET Status = -1, Note = '" + errorText + "', LastAudited = GETDATE() WHERE BadgeID = " + badgeID;
			
			DoUpdateCmd(name, badgeID, sqlcmd, 1);
		}


		private void AuditPassed(int badgeID)
		{
			string name = "AuditPassed";
			string sqlcmd = "UPDATE [Data].[Audit.CMSBadges] SET Status = 1, Note = '', LastAudited = GETDATE() WHERE BadgeID = " + badgeID;
	
			DoUpdateCmd(name, badgeID, sqlcmd, 1);
		}

		public class ProvCategoryData
		{
			public char AccessType;	// D for Default or S for Special
			public int CategoryID;
			public string CategoryName;
			public DateTime WhenBecomesActive;
			public DateTime WhenExpires;

			public ProvCategoryData(char accessType, int categoryID, string categoryName, DateTime whenBecomesActive, DateTime whenExpires)
			{
				AccessType = accessType;
				CategoryID = categoryID;
				CategoryName = categoryName;
				WhenBecomesActive = whenBecomesActive;
				WhenExpires = whenExpires;
			}
		}


		public class ProvisionData
		{
			public long IWS_CardID;
			public string PIN;
			public List<ProvCategoryData> ProvisionedCategories;

			public ProvisionData(int cardID, string pin)
			{
				IWS_CardID = cardID;
				PIN = pin;
				ProvisionedCategories = new List<ProvCategoryData>();
			}
			public ProvisionData(ProvisionData p)
			{
				IWS_CardID = p.IWS_CardID;
				PIN = p.PIN;
				ProvisionedCategories = p.ProvisionedCategories;
			}
		}

		public ProvisionData GetBOAAProvisioningData(int badgeID)
		{
			string name = "GetBOAAProvisioningData";
			ProvisionData pd = new ProvisionData(cardID: 0, pin: "0");
			string sqlcmd = "[App.Sbo].[IWS.GetProvisionedAccessByBadgeID]";
			ConnectionStringSettingsCollection connections = System.Configuration.ConfigurationManager.ConnectionStrings;
			ConnectionStringSettings constr = connections["ApplicationServices"];

			using (var sqlConn = new SqlConnection(constr.ConnectionString))
			{
				try
				{
					sqlConn.Open();
					using (var sqlCommand = new SqlCommand(sqlcmd, sqlConn))
					{
						sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

						SqlParameter param = new SqlParameter();
						param.ParameterName = "@BOAA_BadgeID";
						param.Value = badgeID;

						sqlCommand.Parameters.Add(param);

						SqlDataReader dr = sqlCommand.ExecuteReader();
						while (dr.Read())
						{
							if (dr["CategoryID"] != null && dr["IWS_CardID"] != null && dr["WhenBecomesActive"] != null && dr["WhenExpires"] != null)
							{
								pd.IWS_CardID = (long)dr["IWS_CardID"];
								pd.PIN = (string)dr["PIN"];
								pd.ProvisionedCategories.Add(new ProvCategoryData(
										accessType: (char)dr["AccessType"].ToString()[0],
										categoryID: (int)dr["CategoryID"],
										categoryName: (string)dr["CategoryName"],
										whenBecomesActive: (DateTime)dr["WhenBecomesActive"],
										whenExpires: (DateTime)dr["WhenExpires"]));
							}						
						}
					}
				}
				catch (Exception ex)
				{
					testSvc.log(name + " Exception.Message: " + ex.Message);
					testSvc.log(name + " Exception.InnerException: " + ex.InnerException);
					testSvc.log(name + " Exception.Source: " + ex.Source);
					testSvc.log(name + " Exception.StackTrace: " + ex.StackTrace);
					throw ex;
				}
				finally
				{
					sqlConn.Close();
				}
			}
			return pd;
		}


		public void pokeCMS(int boaa_badge_id)
		{
			bool bb = testSvc.CMSSyncProvisioningDataByBOAABadgeID(boaa_badge_id);
		}


		public void quickTest(int id)
		{
			bool bb = testSvc.CMSSyncProvisioningDataByBOAABadgeID(id);
			ProvisionData BOAAProvData = GetBOAAProvisioningData(badgeID: id);
			List<CardProvisionData> CMSProvData = testSvc.GetProvisioningAuditData(-1, id);
		}

		public void showCMSProvisioningDataByIWSCardID(int iwsCardID)
		{
			List<CardProvisionData> CMSProvData = testSvc.CMSGetProvisioningData(iwsCardID);
		}

		public void showCMSProvisioningDataByBOAABadgeID(int BOAABadgeID)
		{
			List<CardProvisionData> CMSProvData = testSvc.GetProvisioningAuditData(-1, BOAABadgeID);
		}

		public void SmallAuditTest(int BOAA_BadgeID)
		{

			string name = "SmallAuditTest";
			testSvc.log("\n\n\n\n " + name + ":");

			ProvisionData BOAAProvData = GetBOAAProvisioningData(badgeID: BOAA_BadgeID);

			List<CardProvisionData> CMSProvData = testSvc.GetProvisioningAuditData(-1, BOAA_BadgeID);

///			List<CardProvisionData> CMSProvData = testSvc.GetProvisioningAuditData(-1, BOAA_BadgeID);

			if ((BOAAProvData.ProvisionedCategories.Count == 0) && (CMSProvData.Count == 0))
			{
					testSvc.log("ERROR: No provisioning data in either BOAA or CMS.");
			}
			if ((BOAAProvData.ProvisionedCategories.Count > 0) && (CMSProvData.Count == 0))
			{
					testSvc.log("ERROR: No provisioning data returned from CMS. BOAA provisioning data includes " +
							BOAAProvData.ProvisionedCategories.Count + " categories for this badge.");
			}
			if ((BOAAProvData.ProvisionedCategories.Count == 0) && (CMSProvData.Count > 0))
			{
				testSvc.log("ERROR: No provisioning data in BOAA.  CMS provisioning data includes " +
						CMSProvData.Count + " categories for this card.");
			}

			// 2.4 - compare CMS prov data with BOAA prov data
			if (CMSProvData.Count() >= BOAAProvData.ProvisionedCategories.Count())
			{
				foreach (CardProvisionData cpd in CMSProvData)
				{
					bool found = false; 
					foreach (ProvCategoryData bcd in BOAAProvData.ProvisionedCategories)
					{
						if (cpd.CategoryName == bcd.CategoryName)
						{
							found = true;
							if (cpd.AccessType != bcd.AccessType)
							{
								testSvc.log("ERROR (" + bcd.CategoryName + "): AccessType is " + cpd.AccessType + " in CMS and " + bcd.AccessType + " in BOAA.");
							}
							if (cpd.ActiveDate != bcd.WhenBecomesActive)
							{
								testSvc.log("ERROR (" + bcd.CategoryName + "): ActiveDate is " + cpd.ActiveDate + " in CMS and " + bcd.WhenBecomesActive + " in BOAA.");
							}
							if (cpd.ExpireDate != bcd.WhenExpires)
							{
								testSvc.log("ERROR (" + bcd.CategoryName + "): ExpireDate is " + cpd.ActiveDate + " in CMS and " + bcd.WhenExpires + " in BOAA.");
							}
						}
					}
					if (!found)
					{
						testSvc.log("ERROR: category " + cpd.CategoryName + " is present in CMS provisioning data but not found in BOAA Provisioning data.");
					}
				}
			}  
			else 
			{
				foreach (ProvCategoryData bcd in BOAAProvData.ProvisionedCategories)
				{
					bool found = false;
					foreach (CardProvisionData cpd in CMSProvData)
					{
						if (bcd.CategoryName == cpd.CategoryName)
						{
							found = true;
							if (cpd.AccessType != bcd.AccessType)
							{
								testSvc.log("ERROR (" + bcd.CategoryName + "): AccessType is " + cpd.AccessType + " in CMS and " + bcd.AccessType + " in BOAA.");
							}
							if (cpd.ActiveDate != bcd.WhenBecomesActive)
							{
								testSvc.log("ERROR (" + bcd.CategoryName + "): ActiveDate is " + cpd.ActiveDate + " in CMS and " + bcd.WhenBecomesActive + " in BOAA.");
							}
							if (cpd.ExpireDate != bcd.WhenExpires)
							{
								testSvc.log("ERROR (" + bcd.CategoryName + "): ExpireDate is " + cpd.ActiveDate + " in CMS and " + bcd.WhenExpires + " in BOAA.");
							}
						}
					}
					if (!found)
					{
						testSvc.log("ERROR: category " + bcd.CategoryName + " is present in BOAA provisioning data but not found in CMS Provisioning data.");
					}
				}
			}
		}

		public void Audit(int ThresholdDays)
		{
			string name = "Audit";
			testSvc.log("\n\n\n\n " + name + ":");

			DateTime dt;
			bool b = DateTime.TryParse("2012-10-16T17:16:15.031-07:00", out dt);
			string s1 = dt.ToString();
			string s2 = dt.ToString("yyyy-MM-ddTHH:mm:ss.fff");

			//List<CardProvisionData> CMSProvData2 = testSvc.GetProvisioningAuditData(-1, 3828301); // BOAA_BadgeID);

			/*
			bool bb;
			bb = testSvc.CMSSyncProvisioningDataByBOAABadgeID(3828301);
			bb = testSvc.CMSSyncProvisioningDataByBOAABadgeID(3816244);
			bb = testSvc.CMSSyncProvisioningDataByBOAABadgeID(3938605);
			bb = testSvc.CMSSyncProvisioningDataByBOAABadgeID(3943144);
			bb = testSvc.CMSSyncProvisioningDataByBOAABadgeID(3961861);
			bb = testSvc.CMSSyncProvisioningDataByBOAABadgeID(3969133);
			 */

			//quickTest(3938605);

			//return;



#warning AUDIT: if db perfomance sucks, persist the db connection and pass it around
#warning AUDIT: if one-badge-at-a-time performance sucks, process badges in batches

			// 1 - get badges to audit
			List<int> badgeIDs = GetBadgesToAudit(threshold: ThresholdDays);

			// 2 - for each badge to audit
			foreach (int BOAA_BadgeID in badgeIDs)
			{
				// 2.3 - get BOAA provisioning data for badge
				ProvisionData BOAAProvData = GetBOAAProvisioningData(badgeID: BOAA_BadgeID);

				// 2.1 - get the CMS provisioning data for the badge
				List<CardProvisionData> CMSProvData = testSvc.GetProvisioningAuditData(-1, BOAA_BadgeID);

				// short-circuit the audit if either (or both!) sides don't have data
				if ((BOAAProvData.ProvisionedCategories.Count == 0) && (CMSProvData.Count == 0))
				{
					AuditFailed(badgeID: BOAA_BadgeID, errorText: "No provisioning data in either BOAA or CMS.");
					continue;
				}
				if ((BOAAProvData.ProvisionedCategories.Count > 0) && (CMSProvData.Count == 0))
				{
					AuditFailed(badgeID: BOAA_BadgeID, errorText: "No provisioning data returned from CMS. BOAA provisioning data includes " +
							BOAAProvData.ProvisionedCategories.Count + " categories for this card.");
					continue;
				}
				if ((BOAAProvData.ProvisionedCategories.Count == 0) && (CMSProvData.Count > 0))
				{
					AuditFailed(badgeID: BOAA_BadgeID, errorText: "No provisioning data in BOAA.  CMS provisioning data includes " +
							CMSProvData.Count + " categories for this card.");
					continue;
				}


				// 2.2 -  set badge to WAITING and LastTransmitted to today
				if (SetBadgeToWaiting(BOAA_BadgeID) == false)
				{
					testSvc.log(name + "ERROR: SetBadgeToWaiting returned false.  IGNORING (will retry automatically)");
				}

				// 2.4 - compare CMS prov data with BOAA prov data
				if (CMSProvData.Count() >= BOAAProvData.ProvisionedCategories.Count()) 
				{
					foreach (CardProvisionData cpd in CMSProvData)
					{
						bool found = false;
						foreach (ProvCategoryData bcd in BOAAProvData.ProvisionedCategories)
						{
							if (cpd.CategoryName == bcd.CategoryName)
							{
								found = true;
								if (cpd.AccessType != bcd.AccessType)
								{
									AuditFailed(badgeID: BOAA_BadgeID, errorText: "ERROR: AccessType is " + cpd.AccessType + " in CMS and " + bcd.AccessType + "in BOAA.");
								}
								if (cpd.ActiveDate != bcd.WhenBecomesActive)
								{
									AuditFailed(badgeID: BOAA_BadgeID, errorText: "ERROR: ActiveDate is " + cpd.ActiveDate + " in CMS and " + bcd.WhenBecomesActive + "in BOAA.");
								}
								if (cpd.ExpireDate != bcd.WhenExpires)
								{
									AuditFailed(badgeID: BOAA_BadgeID, errorText: "ERROR: ExpireDate is " + cpd.ActiveDate + " in CMS and " + bcd.WhenExpires + "in BOAA.");
								}

#warning FIX THIS: Audit(): not auditing Position
#warning FIX THIS: Audit(): not auditing PIN

							}
						}
						if (!found)
						{
							AuditFailed(badgeID: BOAA_BadgeID, errorText: "ERROR: category " + cpd.CategoryName + " is present in CMS provisioning data but not found in BOAA Provisioning data.");
						}
					}
				}
				else
				{
					foreach (ProvCategoryData bcd in BOAAProvData.ProvisionedCategories)
					{
						bool found = false;
						foreach (CardProvisionData cpd in CMSProvData)
						{
							if (bcd.CategoryName == cpd.CategoryName)
							{
								found = true;
								if (cpd.AccessType != bcd.AccessType)
								{
									AuditFailed(badgeID: BOAA_BadgeID, errorText: "ERROR: AccessType is " + cpd.AccessType + " in CMS and " + bcd.AccessType + "in BOAA.");
								}
								if (cpd.ActiveDate != bcd.WhenBecomesActive)
								{
									AuditFailed(badgeID: BOAA_BadgeID, errorText: "ERROR: ActiveDate is " + cpd.ActiveDate + " in CMS and " + bcd.WhenBecomesActive + "in BOAA.");
								}
								if (cpd.ExpireDate != bcd.WhenExpires)
								{
									AuditFailed(badgeID: BOAA_BadgeID, errorText: "ERROR: ExpireDate is " + cpd.ActiveDate + " in CMS and " + bcd.WhenExpires + "in BOAA.");
								}

#warning FIX THIS: Audit(): not auditing Position
#warning FIX THIS: Audit(): not auditing PIN

							}
						}
						if (!found)
						{
							AuditFailed(badgeID: BOAA_BadgeID, errorText: "ERROR: category " + bcd.CategoryName + " is present in BOAA provisioning data but not found in CMS Provisioning data.");
						}
					}

				}
				AuditPassed(badgeID: BOAA_BadgeID);
			}
		}

		public string RetransmitTSCTransaction(string TransactionControlNumber)
		{ 
			testSvc.log("\n\n\n\nTesting retransmitTSCTransaction(" + TransactionControlNumber + ")");

			string result = testSvc.RetransmitTSCTransaction(TransactionControlNumber);

			testSvc.log(TransactionControlNumber + " retransmit request result: " + result + ".");

			return result;
		}
		
	}
}

