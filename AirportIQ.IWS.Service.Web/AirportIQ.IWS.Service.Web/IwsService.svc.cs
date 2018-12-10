using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ISBWrapper;
using ISBLibTest;
using ImageWare.ISBLibrary;

using log4net;
using log4net.Config;
using log4net.Util;

using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Helpers;
using System.Threading.Tasks;
using System.Threading;

namespace AirportIQ.IWS.Service.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ErrorLogging]
    public class IwsService : IIwsService
    {
        #region "Members"

		private static readonly ILog llog = LogManager.GetLogger("AirportIQ.IWS.Service.Web");
        private static CancellationTokenSource PersonUpdate_CancelTokenSource = new CancellationTokenSource();
        private static CancellationToken PersonUpdate_CancelToken = PersonUpdate_CancelTokenSource.Token;
        private static PersonUpdate CurrentUpdate = null;

        #endregion
        
        #region "Methods"

		public IwsService()   
		{
			XmlConfigurator.Configure();
			BasicConfigurator.Configure();

            if (OperationContext.Current.Host.Description.Behaviors.Find<ErrorServiceBehavior>() == null)
                OperationContext.Current.Host.Description.Behaviors.Add(new ErrorServiceBehavior());
		}

		
		public void log(string ss)
		{
			llog.Debug(ss);
		}

      #region FiscServices

      /// <summary>
      /// Get the status of DOJ for the persons last fingerprint
      /// </summary>
      /// <param name="personID"></param>
      /// <returns></returns>
      public Doj GetDojStatus(Guid personID)
      {
         log("GetDojStatus(): PersonGUID: " + personID);
         try
         {
            DataTable dt = new IwsRepository().GetDojStatus(personID);

            return new Doj() { DojStatus = dt.Rows[0][0].ToString() };

         }
         catch (Exception e)
         {
            log(e.Message);
         }
         return new Doj() { DojStatus = "FAILED TO RETREIEVE STATUS" }; 
      }

      /// <summary>
      /// Get all of a persons fingerprint images from the EBTS server
      /// </summary>
      /// <param name="personID"></param>
      /// <returns>a list of fingeprint images/returns>
      public FingerprintImages GetFingerprintImages(Guid personID)
      {
         log("FingerprintImages(): PersonGUID: " + personID);
         FingerprintImages fingerprintImages = new FingerprintImages();
         fingerprintImages.fingerprintImages = new List<FingerprintImage>();
         try
         {
            int version = 0;
            DataTable dt = new IwsRepository().GetFingerprintImages(personID);
            
            foreach (DataRow row in dt.Rows)
            {
               FingerprintImage fi = new FingerprintImage()
               {
                  sequence = row["Sequence"].ToString(),
                  image = (Byte[])row["FingerprintImage"]
               };
               fingerprintImages.fingerprintImages.Add(fi);
            }
            if(dt.Rows.Count > 0)
               fingerprintImages.version = version = int.Parse(dt.Rows[0]["Version"].ToString()); ;
         }
         catch (Exception e)
         {
            log(e.Message);
         }
         return fingerprintImages;
      }


      /// <summary>
      /// Get all documents captured when the person was fingerprinted
      /// </summary>
      /// <param name="personID"></param>
      /// <returns>a list of I9 documents and I94 and visa information if included</returns>
      public Documents GetDocuments(Guid personID)
      {
         log("GetDocuments(): PersonGUID: " + personID);
         Documents documents = new Documents();
         try
         {
            DataTable dt = new IwsRepository().GetDocuments(personID);
            foreach (DataRow row in dt.Rows)
            {
               Document d = new Document()
               {
                  Description = row["DocumentDescription"].ToString(),
                  CountryCode = row["CountryCode"].ToString(),
                   School = row["School"].ToString(),
                    ExpirationDate = DateTime.Parse(row["ExpirationDate"].ToString()),
                     StateCode = row["StateCode"].ToString(),
                      WhenCreated = DateTime.Parse(row["WhenCreated"].ToString())
               };
               documents.documents.Add(d);

               //TODO: IF DOCUMENT IS TYPE A DO NOT ADD TYPE B or C
            }
         }
         catch (Exception e)
         {
            log(e.Message);
         }
         return documents;
      }

      public Person GetPerson(Guid personID)
      {
         log("GetPerson(): personID=" + personID);
         Person person = null;
         try
         {
           DataTable dt = new IwsRepository().GetPerson(personID);

            foreach (DataRow row in dt.Rows ) //not necessary, only one person
            {
               int result = 0;
               bool hasApt = int.TryParse(row["addressAptNo"].ToString(), out result);
               person = new Person()
               {
                  LastName = row["LastName"].ToString()
                  , MiddleName = row["MiddleName"].ToString()
                  , FirstName = row["FirstName"].ToString()
                  , EmployeeID = int.Parse(row["EmployeeID"].ToString())
                  , citizenship = row["citizenship"].ToString()
                  , DateOfBirth = DateTime.Parse(row["DateOfBirth"].ToString())
                  , SexCode = row["SexCode"].ToString()
                  , HeightInInches = int.Parse(row["HeightInInches"].ToString())
                  , WeightInPounds = int.Parse(row["WeightInPounds"].ToString())
                  , addressCity = row["addressCity"].ToString()
                  , addressZip = int.Parse(row["addressZip"].ToString())
                  , cardPayloadLpublic = row["cardPayloadLpublic"].ToString()
                  , cardPayloadStatus = row["cardPayloadStatus"].ToString()
                  , cardPayloadType = row["cardPayloadType"].ToString()
                  , addressState = row["addressState"].ToString()
                  , CountrySubdivisionCode = row["CountrySubdivisionCode"].ToString()
                  , countryOfBirth = row["countryOfBirth"].ToString()
                  , addressCountry = row["addressCountry"].ToString()
                  , airportCode = "LAX"
                  , contactPhone = row["contactPhone"].ToString()
                  , contactPhone2 = row["contactPhone2"].ToString()
                  , email = row["email"].ToString()
                  , EmailAddress_Alternate = row["EmailAddress_Alternate"].ToString()
                  , addressAptNo =  result
                  , eyeColor = row["eyeColor"].ToString()
                  , fax = ""
                  , hairColor = row["hairColor"].ToString()
                  , prefix = row["prefix"].ToString()
                  , residenceAddress = row["residenceAddress"].ToString()
                  , SocialSecurityNumber = row["SocialSecurityNumber"].ToString()
                  , suffix = row["prefix"].ToString()
                  , occupation = row["occupation"].ToString()
                  , division = row["division"].ToString()
                  , orgCode = row["orgCode"].ToString()
                  , employer = row["employer"].ToString()
                  , race = row["race"].ToString()
                  ,
                  certNumberDs1350 = row["certNumberDs1350"].ToString()
                  ,
                  formNumberI94 = row["formNumberI94"].ToString()
                  ,
                  nonImmigrantVisaNumber = row["nonImmigrantVisaNumber"].ToString()
                  ,
                  passportIssuingCountry = row["passportIssuingCountry"].ToString()
                  ,
                  pasportNumber = row["pasportNumber"].ToString()
                  ,
                  alienRegistrationNumber = row["alienRegistrationNumber"].ToString()

                  ,Alias1FirstName = row["Alias1FirstName"].ToString()
                  ,Alias1MiddleName = row["Alias1MiddleName"].ToString()
                  ,Alias1LastName = row["Alias1LastName"].ToString()

                  ,Alias2FirstName = row["Alias2FirstName"].ToString()
                  ,Alias2LastName = row["Alias2LastName"].ToString()
                  ,Alias2MiddleName = row["Alias2MiddleName"].ToString()

                  ,Alias3FirstName = row["Alias3FirstName"].ToString()
                  ,Alias3LastName = row["Alias3LastName"].ToString()
                  ,Alias3MiddleName = row["Alias3MiddleName"].ToString()
                  ,DojStatus = row["DojStatus"].ToString()
               };
            }
         }
         catch (Exception e)
         {
            log(e.Message);
         }

         return person;
      }

      public Badges GetBadges(Guid personID)
      {
         log("GetBadges(): personID=" + personID);
         Badges badges = new Badges();
         try
         {
            DataTable dt = new IwsRepository().GetBadges(personID);

            foreach (DataRow row in dt.Rows)
            {
               BadgeDetails bd = new BadgeDetails()
               {
                  PersonGUID = System.Guid.Parse(row["PersonGUID"].ToString())
                  , BadgeColor = row["BadgeColor"].ToString()
                  , BadgeNumber = int.Parse(row["BadgeNumber"].ToString())
                  , DivisionID = int.Parse(row["DivisionID"].ToString())
                  , CompanyID = int.Parse(row["CompanyID"].ToString())
                  , jobTitle = row["jobTitle"].ToString()
                  , JobRoleDescription = row["JobRoleDescription"].ToString()
                  , BadgeStatusCode = row["BadgeStatusCode"].ToString()
                  , customsType = row["customsType"].ToString()
                  , BadgeIssuanceReasonCode = row["BadgeIssuanceReasonCode"].ToString()
                  , originalBadgeDate = DateTime.Parse(row["originalBadgeDate"].ToString())
                  , issueDate = DateTime.Parse(row["issueDate"].ToString())
                  , expiryDate = DateTime.Parse(row["expiryDate"].ToString())
                  , lastUpdateDateTime = DateTime.Parse(row["lastUpdateDateTime"].ToString())
                   ,
                  Employer = row["Employer"].ToString()
                   ,
                 DvisionName = row["DvisionName"].ToString()
               };
               badges.badges.Add(bd);
            }
         }
         catch (Exception e)
         {
            log(e.Message);
         }

         return badges;
      }

      public Fingerprinted GetFingerprints(DateTime start, DateTime end)
      {
         log("GetFingerprints(): start: " +start + " end: " + end);
         Fingerprinted fingerprinted = new Fingerprinted();
         try
         {
            DataTable dt =  new IwsRepository().GetFingerprints(start, end);

            foreach (DataRow row in dt.Rows)
            {
               Fingerprint f = new Fingerprint() {
                  Created = DateTime.Parse(row["Created"].ToString()),
                  CreatedBy = row["CreatedBy"].ToString(),
                  PersonGUID = System.Guid.Parse(row["PersonGUID"].ToString()),
                  FPType = row["FPType"].ToString()
               };
               fingerprinted.fingerprinted.Add(f);
            }
         }
         catch (Exception e)
         {
            log(e.Message);
         }

         return fingerprinted;
      }

      public Byte [] GetEBTS(Guid personID)
      {
         log("GetEBTS(): personID=" + personID);
         try
         {
            return new IwsRepository().GetEBTS(personID);
         }
         catch (Exception e)
         {
            log(e.Message);
         }
         return new Byte[0];

      }

      public bool AddNote(Guid personID, string note)
      {
         log("AddNote(): personID=" + personID + "note=" + note);
         try
         {
             new IwsRepository().AddNote(personID, note);
            return true;
         }
         catch (Exception e)
         {
            log(e.Message);
            return false;
         }
      }
      #endregion

      // item 02
      // FIX THIS: our db will change in the near future and we will need the Division as a parameter. 
      public bool BiometricUpdate(Guid personID, byte[] signatureImage)
        {
			log("BiometricUpdate(): personID=" + personID);

            bool b = false;

            try
            {
                b = new IwsRepository().BiometricUpdate(personID, signatureImage);
            }
            catch (Exception e)
            {
                log(e.Message);
            }

            return b;
        }

		// item 05
        public bool UpdateBadgeID(int iwsCardID, int gcrBadgeID)
        {
			log("UpdateBadgeID(): iwsCardID=" + iwsCardID + ", gcrBadgeID=" + gcrBadgeID);

            bool b = false;

            try
            {
                b = new IwsRepository().UpdateBadgeID(iwsCardID, gcrBadgeID);
            }
            catch (Exception e)
            {
                log(e.Message);
            }

            return b;

        }

		// item 07
		public bool InitiateBackgroundCheck(Guid iwsPersonID, int TSCTransactionTypeID, string TransactionControlNumber, DateTime TransactionDate, string ProgramIdentification, 
											string ResponseIdentification, string Status, string StatusText, string XMLdata, string Direction, bool fromFisc)
        {

         if (fromFisc == null || fromFisc == false)
            return false;

			log("InitiateBackgroundCheck(): iwsPersonID=" + iwsPersonID + ", TSCTransactionTypeID=" + TSCTransactionTypeID + ", TransactionControlNumber=" + TransactionControlNumber);
			log("                           TransactionDate=" + TransactionDate + ", ProgramIdentification=" + ProgramIdentification + ", ResponseIdentification=" + ResponseIdentification);
			log("                           Status=" + Status + ", StatusText=" + StatusText + ", Direction=" + Direction);
			log("                           XMLdata=" + XMLdata);
			
            bool b = false;

            try
            {
                b = new IwsRepository().InitiateBackgroundCheck(iwsPersonID, TSCTransactionTypeID, TransactionControlNumber, TransactionDate, ProgramIdentification,
                                            ResponseIdentification, Status, StatusText, XMLdata, Direction);
            }
            catch (Exception e)
            {
                log(e.Message);
            }

            return b;
        }

		// item 08
		public bool UpdateBackgroundCheck(Guid iwsPersonID, string AgencyCode, string CheckTypeCode, string TransactionTypeCode, string TransactionControlNumber,
											DateTime TransactionDate, string Result, DateTime ResultDate, string ResultDetails, DateTime ResultDetailDate,bool fromFisc)
     {

         if (fromFisc == null || fromFisc == false)
            return false;
         log("UpdateBackgroundCheck(): iwsPersonID=" + iwsPersonID + ", AgencyCode=" + AgencyCode + ", CheckTypeCode=" + CheckTypeCode);
			log("                           TransactionTypeCode=" + TransactionTypeCode + ", TransactionControlNumber=" + TransactionControlNumber + ", TransactionDate=" + TransactionDate);
			log("                           Result=" + Result + ", ResultDate=" + ResultDate);
			log("                           ResultDetails=" + ResultDetails + ", ResultDetailDate=" + ResultDetailDate);

			bool b = false;

			try
			{
				b = new IwsRepository().UpdateBackgroundCheck(iwsPersonID, AgencyCode, CheckTypeCode, TransactionTypeCode, TransactionControlNumber,
											 TransactionDate, Result, ResultDate, ResultDetails, ResultDetailDate);
			}
			catch (Exception e)
			{
				log(e.Message);
			}

			return b;
    }


		//Rguidi 6/7/2013 : NEW process redesign: replaces InitiateBackgroundCheck
		public bool UpdateBackgroundCheckStatus(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, DateTime TransactionDate,
									string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, string XMLdata, bool fromFisc)
		{

         if (fromFisc == null || fromFisc == false)
            return false;
         log("UpdateBackgroundCheckStatus(): iwsPersonID=" + personID + ", TransactionNamee=" + TransactionName + ", AgencyCode=" + AgencyCode);
			log("                           TransactionControlNumber=" + TransactionControlNumber + ", TransactionDate=" + TransactionDate);
			log("                           ProgramIdentification=" + ProgramIdentification + ", ResponseIdentification=" + ResponseIdentification);
			log("                           TransmissionStatus=" + TransmissionStatus + ", TransmissionStatusText=" + TransmissionStatusText);

			bool b = false;

			try
			{
				b = new IwsRepository().UpdateBackgroundCheckStatus(personID, TransactionName, AgencyCode, TransactionControlNumber, TransactionDate,
											  ProgramIdentification, ResponseIdentification, TransmissionStatus, TransmissionStatusText, XMLdata);
			}
			catch (Exception e)
			{
				log(e.Message);
			}

			return b;
		}

		//Rguidi 6/7/2013 : NEW process redesign: replaces UpdateBackgroundCheck
		public bool UpdateBackgroundCheckResult(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, DateTime TransactionDate,
								 string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, string XMLdata,
								 string AgencyResult, DateTime AgencyResultDate, string AgencyResultDetails, DateTime AgencyResultDetailDate,
								 string BackgroundCheckID, string BackgroundCheckType, bool fromFisc, string company)
		{

         if (fromFisc == null || fromFisc == false)
            return false;
         log("UpdateBackgroundCheckResult(): iwsPersonID=" + personID + ", TransactionNamee=" + TransactionName + ", AgencyCode=" + AgencyCode);
			log("                           TransactionControlNumber=" + TransactionControlNumber + ", TransactionDate=" + TransactionDate);
			log("                           ProgramIdentification=" + ProgramIdentification + ", ResponseIdentification=" + ResponseIdentification);
			log("                           TransmissionStatus=" + TransmissionStatus + ", TransmissionStatusText=" + TransmissionStatusText);
			log("                           AgencyResult=" + AgencyResult + ", AgencyResultDate=" + AgencyResultDate);
			log("                           AgencyResultDetails=" + AgencyResultDetails + ", AgencyResultDetailDate=" + AgencyResultDetailDate);
			log("                           BackgroundCheckID=" + BackgroundCheckID + ", BackgroundCheckType=" + BackgroundCheckType);

			bool b = false;

			try
			{
				DateTime minSQLdate = DateTime.Parse("1/1/1753 12:00:00 AM"); //System.DateTime.MinValue = 1/1/0001 which fails in sql
				if (AgencyResultDate == null || AgencyResultDate.Year == 1) { AgencyResultDate = minSQLdate; }
				if (AgencyResultDetailDate == null || AgencyResultDetailDate.Year == 1) { AgencyResultDetailDate = minSQLdate; }
				if (AgencyResultDetails == null) { AgencyResultDetails = ""; }
				if (BackgroundCheckID == null) { BackgroundCheckID = ""; }
				if (BackgroundCheckType == null) { BackgroundCheckType = ""; }

				
				b = new IwsRepository().UpdateBackgroundCheckResult(personID, TransactionName, AgencyCode, TransactionControlNumber, 
											  TransactionDate, ProgramIdentification, ResponseIdentification, TransmissionStatus, TransmissionStatusText, XMLdata,
											  AgencyResult, AgencyResultDate, AgencyResultDetails, AgencyResultDetailDate, BackgroundCheckID, BackgroundCheckType,  company);
			}
			catch (Exception e)
			{
				log(e.Message);
			}

			return b;
		}

		// item 09
		// FIX THIS: no details yet
		public string RetreiveFbiHistoryUrl(Guid iwsPersonID)
		{
			log("RetreiveFbiHistoryUrl(): iwsPersonID=" + iwsPersonID);
			
            string s = "";

            try
            {
                s = new IwsRepository().RetreiveFbiHistoryUrl(iwsPersonID);
            }
            catch (Exception e)
            {
                log(e.Message);
            }

            return s;
		}

		// item 09
		//Rguidi 6/7/2013 : NEW process redesign: replaced UpdateBackgroundCheckResult
		public bool SetFBICaseNumber(Guid iwsPersonID, string caseNumber, string result = "PASS", DateTime resultDate = new DateTime())
		{
         if (resultDate == new DateTime())
            resultDate = DateTime.Now;

			log("SetFBICaseNumber(): iwsPersonID=" + iwsPersonID + ", caseNumber=" + caseNumber + ", result=" + result + ", resultDate=" + resultDate);
			
            bool b = false;

            try
            {
                b = new IwsRepository().SetFBICaseNumber(iwsPersonID, caseNumber, result, resultDate);
            }
            catch (Exception e)
            {
                log(e.Message);
            }

            return b;
		}

      //Rguidi 6/7/2013 : NEW process redesign: replaced UpdateBackgroundCheckResult
      public bool SetTSACaseNumber(Guid iwsPersonID, string caseNumber, string result , DateTime resultDate)
      {
         log("SetTSACaseNumber(): iwsPersonID=" + iwsPersonID + ", caseNumber=" + caseNumber + ", result=" + result + ", resultDate=" + resultDate);

         bool b = false;

         try
         {
            b = new IwsRepository().SetTSACaseNumber(iwsPersonID, caseNumber, result, resultDate);
         }
         catch (Exception e)
         {
            log(e.Message);
         }

         return b;
      }

      // item 11
      public bool ExpireBadge(Guid iwsPersonID, int iwsCardID)
		{
			log("ExpireBadge(): iwsPersonID=" + iwsPersonID + ", iwsCardID=" + iwsCardID);
			
            bool b = false;

            try
            {
                b = new IwsRepository().ExpireBadge(iwsPersonID, iwsCardID);
            }
            catch (Exception e)
            {
                log(e.Message);
            }

            return b;

		}

		// item 12
		public ProvisionData ProvisionedByCard(int cardID)
		{
			string name = "ProvisionedByCard(): ";
			
			log(name + "cardID=" + cardID.ToString());

            ProvisionData pd = new ProvisionData(cardID: 0, pin: "0", employeeid: "0", companydivision: "0", jobroleid: 0);

            try
            {
                DataTable dt = new IwsRepository().ProvisionedByCard(cardID);

                bool first = true;

                foreach (DataRow dr in dt.Rows)
                {
                    if (first)
                    {
                        first = false;
                        pd.IWS_CardID = (long)dr["IWS_CardID"];
                        pd.PIN = (string)dr["PIN"];

                        pd.EmployeeID = (string)dr["EmployeeID"].ToString();
												string CompanyCode = ((string)dr["CompanyCode"]).PadLeft(4, '0');
												string DivisionCode = ((string)dr["DivisionCode"]).PadLeft(2, '0');
												pd.CompanyDivision = (string)(CompanyCode + DivisionCode);
												pd.JobRoleID = int.Parse(dr["JobRoleID"].ToString());

                    }

										log(name + "IWS_CardID: " + pd.IWS_CardID.ToString() + ", PIN: " + pd.PIN + ", EmployeeID: " + pd.EmployeeID + ", CompanyDivision: " + pd.CompanyDivision + ", JobRoleID: " + pd.JobRoleID);

									  //RGuidi: 6/4/2013 Only add -if- record has a category value. This is in support of CMS requiring a minimal record (employee/codiv: with no categories) to revoke provisioning 
										if (dr["CategoryName"].ToString().Length > 0)
										{
											 pd.ProvisionedCategories.Add(new CategoryData(
                                                            accessType: (char)dr["AccessType"].ToString()[0],
                                                            categoryID: (int)dr["CategoryID"],
                                                            categoryName: (string)dr["CategoryName"],
                                                            whenBecomesActive: (DateTime)dr["WhenBecomesActive"],
                                                            whenExpires: (DateTime)dr["WhenExpires"]
																														));
										}
                }

                log(name + " ProvisionedCategories Count: " + pd.ProvisionedCategories.Count.ToString());

                foreach (CategoryData bcd in pd.ProvisionedCategories)
                {
                    log("           AccessType: " + bcd.AccessType);
                    log("           CategoryID: " + bcd.CategoryID);
                    log("         CategoryName: " + bcd.CategoryName);
                    log("    WhenBecomesActive: " + bcd.WhenBecomesActive);
                    log("          WhenExpires: " + bcd.WhenExpires);
                }

            }
            catch (Exception e)
            {
                log(e.Message);
            }
			return pd;
		}

		public ProvisionData ProvisionedByBOAABadgeID(int BOAABadgeID)
		{
			string name = "ProvisionedByBOAABadgeID(): ";

            log(name + "BOAABadgeID=" + BOAABadgeID.ToString());

						ProvisionData pd = new ProvisionData(cardID: 0, pin: "0", employeeid: "0", companydivision: "0", jobroleid: 0);

            try
            {
                DataTable dt = new IwsRepository().ProvisionedByBOAABadgeID(BOAABadgeID);
			    bool first = true;

			    foreach (DataRow dr in dt.Rows)
			    {
				    if (first)
				    {
					    first = false;

                        long lIWSCardID = 0;
                        Int64.TryParse(dr["IWS_CardID"].ToString(), out lIWSCardID);
                        pd.IWS_CardID = lIWSCardID;

												pd.PIN = (string)dr["PIN"];
												pd.EmployeeID = (string)dr["EmployeeID"].ToString();
												string CompanyCode = ((string)dr["CompanyCode"]).PadLeft(4, '0');
												string DivisionCode = ((string)dr["DivisionCode"]).PadLeft(2, '0');
												pd.CompanyDivision = (string)(CompanyCode + DivisionCode);
												pd.JobRoleID = int.Parse(dr["JobRoleID"].ToString());

												log(name + "IWS_CardID: " + pd.IWS_CardID + ", PIN: " + pd.PIN + ", EmployeeID: " + pd.EmployeeID + ", CompanyDivision: " + pd.CompanyDivision + ", JobRoleID: " + pd.JobRoleID);
				    }

						//RGuidi: 6/4/2013 Only add -if- record has a category value. This is in support of CMS requiring a minimal record (employee/codiv: with no categories) to revoke provisioning 
						if (dr["CategoryName"].ToString().Length > 0)
						{
							pd.ProvisionedCategories.Add(new CategoryData(
																	accessType: (char)dr["AccessType"].ToString()[0],
																	categoryID: (int)dr["CategoryID"],
																	categoryName: (string)dr["CategoryName"],
																	whenBecomesActive: (DateTime)dr["WhenBecomesActive"],
																	whenExpires: (DateTime)dr["WhenExpires"]
																	));
						}
			    }
                log(name + " ProvisionedCategories Count: " + pd.ProvisionedCategories.Count.ToString());

			    foreach (CategoryData bcd in pd.ProvisionedCategories)
			    {
				    log("           AccessType: " + bcd.AccessType);
				    log("           CategoryID: " + bcd.CategoryID);
				    log("         CategoryName: " + bcd.CategoryName);
				    log("    WhenBecomesActive: " + bcd.WhenBecomesActive);
				    log("          WhenExpires: " + bcd.WhenExpires);
			    }

            }
            catch (Exception e)
            {
                log(e.Message);
            }

			return pd;
		}


		// item 12 callback (no interface number - spec'd 2/27/2013)
		public bool ProvisioningComplete(int iwsCardID)
		{
			log("ProvisioningComplete(): iwsCardID=" + iwsCardID);
			
            bool b = false;

            try
            {
                b = new IwsRepository().ProvisioningComplete(iwsCardID);
            }
            catch (Exception e)
            {
                log(e.Message);
            }

            return b;
		}




        public bool IsAlive()
        {
			return true;
        }

        public bool IsDbConnected()
        {
			log("IsDbConnected()");

			bool ret = false;
            try
            {
                AirportIQ.Domain.Services.IwsService service = new AirportIQ.Domain.Services.IwsService();
                ret = service.IsDbAlive();
            }
            catch (Exception ex)
            {
				string s = ex.Message;  // FIX THIS - atchison MessageBox.Show("exception caught in IsDbConnected():" + ex.Message);	
                log("exception caught in IsDbConnected():" + ex.Message);
            }

            return ret;
        }

        public bool DoDeletePersonTasks(DeletePersonInfo info)
        {
            ISBLibTest.ISBLibExtGCR IWS_Service = new ISBLibExtGCR( System.Configuration.ConfigurationManager.AppSettings["DOCM_IP"],
                                                                    System.Configuration.ConfigurationManager.AppSettings["IDMS_IP"],
                                                                    System.Configuration.ConfigurationManager.AppSettings["CMS_IP"],
                                                                    System.Configuration.ConfigurationManager.AppSettings["EBTS_IP"]);

            string LogInfo = string.Format("EmployeeID={0}, PersonDivisionCheckID={1}, UserID={2}, PersonGUID={3}", 
                                            info.EmployeeID, info.PersonDivisionCheckID, info.UserID, info.PersonGUID);

            log("DoDeletePersonTasks():" + LogInfo);

            try
            {
                string EmployeeID = info.EmployeeID.ToString();
                int PersonDivisionCheckID = info.PersonDivisionCheckID;
                int UserID = info.UserID;
                string PersonGuid = info.PersonGUID;

                var IWSRepo = new IwsRepository();

                int LastTransactionID = IWSRepo.GetLastTSCTransactionID(info.PersonDivisionCheckID);

                IDMSPerson person = IWS_Service.GetPerson(info.PersonGUID);

                if (person == null)
                {
                    log("DoDeletePersonTasks():\tPerson does not exist in IDMS!");
                    return false;
                }

                IWS_Service.RevokePendingBadge(info.PersonGUID, false);

                foreach (DeletePersonBadgeInfo dr in info.BadgeInfo)
                {
                    LogInfo = string.Format("DoDeletePersonTasks():\tBadgeNumber={0}, CorporationName={1}, BadgeID={2}, BadgeID_IWS={3}, AccessLevel={4}", 
                                            dr.BadgeNumber, dr.CorporationName, dr.BadgeID, dr.BadgeID_IWS, dr.AccessLevel);
                    log(LogInfo);
                    List<string> BadgeData = new List<string>();
                    bool SendToTSC = (dr == info.BadgeInfo.Last());

                    BadgeData.Add(dr.BadgeNumber);
                    BadgeData.Add("Revoked");
                    BadgeData.Add("Delete Person From TSC");
                    BadgeData.Add(string.Empty);
                    BadgeData.Add(string.Empty);
                    BadgeData.Add(dr.CorporationName);

                    IWSRepo.CancelBadge(dr.BadgeID, info.UserID);
                    IWS_Service.CMSRevokeCard(dr.BadgeID_IWS, "Delete/Inactivate, UserID=" + UserID);                    
                    IWS_Service.UpdatePerson(PersonGuid, new List<string>(), BadgeData, dr.AccessLevel, SendToTSC);
                }

                var DeletePersonTask = Task.Factory.StartNew(() =>
                {
                    log("DoDeletePersonTasks():\tContinue Tasks for " + info.PersonGUID);

                    foreach (DeletePersonCompanyInfo dr in info.CompanyInfo)
                    {
                        IWSRepo.ArchiveFingerprints(info.SSN, dr.CompanyCode, dr.DivisionCode);
                    }

                    if (info.BadgeInfo.Count > 0)
                    {
                        IWSRepo.SaveBadgeCancellationTransaction(PersonDivisionCheckID, UserID);
                    }

                    string TransactionGUID = IWS_Service.DeletePersonFromTSC(PersonGuid, EmployeeID);

                    IWSRepo.SaveDeletePersonSubmission(PersonDivisionCheckID, TransactionGUID, UserID);

                    try
                    {
                        List<DMDocumentInfo> Documents = IWS_Service.GetPersonDocuments(PersonGuid);

                        foreach (DMDocumentInfo doc in Documents)
                        {
                            IWS_Service.RemoveDocument(doc.GUID);
                        }

                        IWS_Service.DeletePersonFromIDMS(PersonGuid);
                    }
                    catch (ISBException ex)
                    {
                        //person alredy deleted, no sense popping up an error if -120
                        if (ex.ErrorCode != -120)
                        {
                            throw;
                        }
                    }
                    log("DoDeletePersonTasks Complete:" + LogInfo);
                });
            }
            catch (Exception ex)
            {
                log("Exception caught in DoDeletePersonTasks: " + ex.ToString());
                return false;
            }
            return true;
        }        

        public DataTable GetPersonsForBatchUpdate()
        {
            try
            {
                var ds = new IwsRepository().GetPersonsForBatchUpdate();
                if (ds == null || ds.Tables.Count == 0)
                    return null;
                else
                    return ds.Tables[0];           
            }
            catch (Exception ex)
            {
                log("Exception caught in GetPersonsForBatchUpdate: " + ex.ToString());
                return null;
            }
        }

        public List<PersonUpdate> GetAllPersonUpdates()
        {
            List<PersonUpdate> returnValue = null;
            try
            {
                var ds = new IwsRepository().GetAllPersonUpdates();
                returnValue = MapPersonUpdateDataSet(ds);
            }
            catch (Exception ex)
            {
                log("Exception caught in GetAllPersonUpdates: " + ex.ToString());
                return null;
            }
            return returnValue;
        }

        public PersonUpdate GetCurrentPersonUpdate()
        {
            if (CurrentUpdate == null)
                return null;

            var ds = new IwsRepository().GetPersonUpdate(CurrentUpdate.PersonUpdateID);
            var returnValue = MapPersonUpdateDataSet(ds).FirstOrDefault();
            return returnValue;
        }

        public PersonUpdate PopulatePersonUpdate(List<int> BadgeIDs)
        {
            var IwsRepo = new IwsRepository();
            var returnValue = new PersonUpdate();
            DataSet ds = IwsRepo.PopulatePersonUpdate(BadgeIDs);
            returnValue = MapPersonUpdateDataSet(ds).FirstOrDefault();
            return returnValue;
        }

        public PersonUpdate GetPersonUpdate(int PersonUpdateID)
        {
            var IwsRepo = new IwsRepository();
            var ds = IwsRepo.GetPersonUpdate(PersonUpdateID);
            return MapPersonUpdateDataSet(ds).FirstOrDefault();
        }

        public PersonUpdate BeginPersonUpdate(int PersonUpdateID)
        {
            var IwsRepo = new IwsRepository();
            if (CurrentUpdate != null && CurrentUpdate.Persons.Count > 0)
                return CurrentUpdate;

            int UpdatePersonCallsPerHour = 75;

            Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["UpdatePersonCallsPerHour"], out UpdatePersonCallsPerHour);
            TimeSpan timeBetweenCalls = new TimeSpan(0, 0, 3600 / (UpdatePersonCallsPerHour <= 0 ? 75 : UpdatePersonCallsPerHour));

            var ds = IwsRepo.GetPersonUpdate(PersonUpdateID);
            CurrentUpdate = MapPersonUpdateDataSet(ds).FirstOrDefault();

            if (CurrentUpdate != null)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {                        
                        DateTime lastCallTime = DateTime.UtcNow;
                        var pendingPersons = CurrentUpdate.Persons.Where(x => x.IsPending).OrderBy(x => x.OrderBy).ToList();
                        log("Person Update ID " + CurrentUpdate.PersonUpdateID + " beginning.");
                        foreach (PersonUpdateDetail detail in pendingPersons)
                        {
                            ISBLibTest.ISBLibExtGCR IWS_Service = new ISBLibExtGCR(System.Configuration.ConfigurationManager.AppSettings["DOCM_IP"],
                                                                System.Configuration.ConfigurationManager.AppSettings["IDMS_IP"],
                                                                System.Configuration.ConfigurationManager.AppSettings["CMS_IP"],
                                                                System.Configuration.ConfigurationManager.AppSettings["EBTS_IP"]);

                            List<string> PersonData = new List<string>(); // empty; Person Update only.                            

                            log("Update Person: EmployeeID = " + detail.EmployeeID + ", PersonGUID :" + detail.PersonGUID);
                            foreach (BadgeRecord badge in detail.Badges)
                            {
                                List<string> BadgeStatusData = new List<string>();
                                BadgeStatusData.Add(badge.BadgeNumber);
                                BadgeStatusData.Add(badge.BadgeStatusCode == "ACTV" ? "Active" : "Revoked");
                                BadgeStatusData.Add(badge.ReasonForDeactivation);
                                BadgeStatusData.Add(""); //Access Level (SIDA, Sterile): this can not be changed, so pass back their existing value if exists
                                BadgeStatusData.Add(""); //LocalBadgeType: 
                                BadgeStatusData.Add(badge.CorporationName); //EmployerName: 
                                log("\tUpdate Badge Number: + " + badge.BadgeNumber + "," + badge.CorporationName + "," + badge.BadgeStatusCode + "," + badge.ReasonForDeactivation);                                
                                IWS_Service.UpdatePerson(detail.PersonGUID.ToString(), PersonData, BadgeStatusData, detail.TypeCode, badge == detail.Badges.Last());
                                if (badge == detail.Badges.Last())
                                    log("Sent to TSC");
                                if (PersonUpdate_CancelToken.IsCancellationRequested)
                                {
                                    // Cancel Web service was called.
                                    log("BeginPersonUpdate cancelled");
                                    PersonUpdate_CancelTokenSource = new CancellationTokenSource();
                                    PersonUpdate_CancelToken = PersonUpdate_CancelTokenSource.Token;
                                    CurrentUpdate = null;
                                    break;
                                }
                            }
                            
                            IwsRepo.MarkPersonUpdateDetailAsSent(detail.PersonUpdateID, detail.PersonID);
                            log("Update Person on PersonID " + detail.PersonID + " Complete");

                            DateTime finishTime = DateTime.UtcNow;
                            DateTime nextCallTime = lastCallTime.Add(timeBetweenCalls);
                            if (finishTime < nextCallTime)
                            {
                                log("Sleeping for " + nextCallTime.Subtract(finishTime).TotalMilliseconds.ToString() + " milliseconds");
                                System.Threading.Thread.Sleep(Convert.ToInt32(nextCallTime.Subtract(finishTime).TotalMilliseconds));
                            }

                            lastCallTime = DateTime.UtcNow;
                        }
                        if (CurrentUpdate != null)
                        {
                            log("Person Update ID " + CurrentUpdate.PersonUpdateID + " complete.");
                        }
                        CurrentUpdate = null;
                    }
                    catch (Exception ex)
                    {
                        log(ex.ToString());
                    }
                    
                });
            }              
            return CurrentUpdate;
        }

        public void CancelPersonUpdate()
        {
            log("CancelPersonUpdate web service call");
            PersonUpdate_CancelTokenSource.Cancel();
        }

        private IList<T> GetDataFromDataTable<T>(DataTable table)
        {
            AutoMapper.Mapper.CreateMap<IDataReader, IList<T>>();
            using (var reader = table.CreateDataReader())
            {
                return AutoMapper.Mapper.Map<IList<T>>(reader).ToList();
            }
        }

        private List<PersonUpdate> MapPersonUpdateDataSet(DataSet ds)
        {
            var returnValue = new List<PersonUpdate>();
            ds.Relations.Clear();
            ds.Relations.Add("PersonUpdate", ds.Tables[0].Columns["PersonUpdateID"], ds.Tables[1].Columns["PersonUpdateID"], false);
            ds.Relations.Add("Person", ds.Tables[1].Columns["PersonID"], ds.Tables[2].Columns["PersonID"], false);            
            var PersonUpdates = GetDataFromDataTable<PersonUpdate>(ds.Tables[0]).ToList();
            var PersonUpdateDetails = GetDataFromDataTable<PersonUpdateDetail>(ds.Tables[1]).ToList();
            var PersonUpdateBadges = GetDataFromDataTable<BadgeRecord>(ds.Tables[2]).ToList();

            foreach (DataRow drPU in ds.Tables[0].Rows)
            {
                var newPU = new PersonUpdate()
                {
                    PersonUpdateID = drPU.Field<int>("PersonUpdateID"),
                    CreationDate = drPU.Field<DateTime>("CreationDate")
                };
                var pudChildRows = drPU.GetChildRows(ds.Relations[0]);
                //var dv = new DataView(ds.Tables[1], "PersonUpdateID=" + newPU.PersonUpdateID, "", DataViewRowState.OriginalRows);
                foreach (DataRow drDetail in pudChildRows)
                {
                    var newPerson = new PersonUpdateDetail()
                    {
                        PersonUpdateID = (int)drDetail["PersonUpdateID"],
                        PersonID = (int)drDetail["PersonID"],
                        FirstName = (string)drDetail["FirstName"],
                        LastName = (string)drDetail["LastName"],
                        EmployeeID = (int)drDetail["EmployeeID"],
                        //DivisionTypeID = (short)drv["DivisionTypeID"],
                        //TypeCode = (string)drv["TypeCode"],
                        SocialSecurityNumber = (string)drDetail["SocialSecurityNumber"],
                        PersonGUID = new System.Guid(drDetail["PersonGUID"] == DBNull.Value ? "" : drDetail["PersonGUID"].ToString()),
                        OrderBy = (int)drDetail["OrderBy"],
                        IsPending = (bool)drDetail["IsPending"],
                        IsSuccessful = (bool)drDetail["IsSuccessful"],
                        TransmitStart = (DateTime?) (drDetail["TransmitStart"] == DBNull.Value ? null : drDetail["TransmitStart"]),
                        TransmitEnd = (DateTime?) (drDetail["TransmitEnd"] == DBNull.Value ? null : drDetail["TransmitEnd"])
                    };
                    //var dvPerson = new DataView(ds.Tables[2], "PersonUpdateID = " + newPU.PersonUpdateID + " AND PersonID=" + newPerson.PersonID, "", DataViewRowState.OriginalRows);
                    var badgeChildRows = drDetail.GetChildRows(ds.Relations[1]);
                    foreach (DataRow drBadge in badgeChildRows)
                    {
                        var newBadge = new BadgeRecord()
                        {
                            PersonGUID = new System.Guid(drBadge["PersonGUID"] == DBNull.Value ? "" : drBadge["PersonGUID"].ToString()),
                            PersonID = drBadge.Field<int>("PersonID"),
                            BadgeID = drBadge.Field<int>("BadgeID"),
                            DivisionTypeID = drBadge.Field<short>("DivisionTypeID"),
                            TypeCode = drBadge.Field<string>("TypeCode"),
                            BadgeNumber = drBadge.Field<string>("BadgeNumber"),
                            CorporationName = drBadge.Field<string>("CorporationName"),
                            BadgeStatusCode = drBadge.Field<string>("BadgeStatusCode"),
                            BadgeID_IWS = drBadge["BadgeID_IWS"] == DBNull.Value ? 0 : drBadge.Field<long>("BadgeID_IWS"),
                            ReasonForDeactivation = drBadge.Field<string>("ReasonForDeactivation")
                        };
                        newPerson.Badges.Add(newBadge);
                    }
                    newPU.Persons.Add(newPerson);
                }
                returnValue.Add(newPU);
            }

            return returnValue;
            //foreach (var update in PersonUpdates)
            //{
            //    update.Persons = PersonUpdateDetails.Where(x => x.PersonUpdateID == update.PersonUpdateID).ToList();
            //    foreach (var person in update.Persons)
            //    {
            //        person.Badges = PersonUpdateBadges.Where(y => y.PersonID == person.PersonID).ToList();
            //    }
            //}
            //return PersonUpdates;
        }

        #endregion

    }
}
