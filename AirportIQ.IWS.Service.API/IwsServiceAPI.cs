using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using AirportIQ.IWS.Service; //real service - need to point this locally?

using AirportIQ.IWS.Service.API.IwsService;    //real service
using System.Data;

namespace AirportIQ.IWS.Service.API
{
    public class IwsServiceAPI
    {

	 #region "Members"

        protected IwsServiceClient _service = null;


      /// <summary>
      /// Defines the return value flags for the one of the function calls
      /// </summary>
      //public enum StatusUpdate
      //{
      //  NewEmployee = 0x0001,
      //UpdateEmployee = 0x0002,
      //  Cop = 0x0004,
      //Airport = 0x0008
      //}

      #endregion


      #region "Service Methods

      public Documents GetDocuments(Guid personID)
      {
         return Service.GetDocuments(personID);
      }
      
      public Fingerprinted HasFingerprints(DateTime start, DateTime end)
      {

         return Service.GetFingerprints(start, end);
      }
      /// <summary>
      /// Get the Doj status for the persons latest fingerprint
      /// </summary>
      /// <param name="personID"></param>
      /// <returns></returns>
      public Doj GetDojStatus(Guid personID)
      {
         return Service.GetDojStatus(personID);
      }

      public FingerprintImages GetFingerprintImages(Guid person)
      {
         return Service.GetFingerprintImages(person);
      }

      public Person GetPerson(Guid personID)
      {
         if (null == personID )
            throw new Exception("Invalid parameter");

         return Service.GetPerson(personID);
      }

      public Badges GetBadges(Guid personID)
      {
         return Service.GetBadges(personID);
      }
      public byte [] GetEBTS(Guid personID)
      {
         return Service.GetEBTS(personID);
      }

      public bool AddNote(Guid personID, string note)
      {
         return Service.AddNote(personID, note);
      }

      /// <summary>
      /// Updates a person with a the passed image.
      /// Corresponds to Interface Requirements Definition item 2
      /// </summary>
      /// <param name="personID">Guid for the person the image is to be associated with></param>
      /// <param name="image">byte array of Image data.</param>
      /// <returns>True if person successfully updated with the image.  False if there was an issue</returns>
      public bool BiometricUpdate(Guid personID, byte [] image)
		{
            if (null == personID || null == image)
                throw new Exception("Invalid parameter");

			return Service.BiometricUpdate(personID, image);
		}


        /// <summary>
        /// On Activate, tell BOAA who for and ID(s)
        /// </summary>
        /// <param name="personID">Guid of the person being updated</param>
        /// <param name="cardID">ID of the card being updated</param>
        /// <param name="badgeID">ID of the badge being updated</param>
        /// <returns>True if successful, false if not</returns>
        public bool UpdateBadgeID(int cardID, int badgeID)
        {
            return Service.UpdateBadgeID(cardID, badgeID);
        }

        /// <summary>
        /// 
        /// </summary>
		/// <param name="personID">Guid of the person being updated</param>
        /// <returns>True if update successful</returns>
		public bool InitiateBackgroundCheck(Guid personID, int TSCTransactionTypeID, string TransactionControlNumber, DateTime TransactionDate, string ProgramIdentification,
											string ResponseIdentification, string Status, string StatusText, string XMLdata, string Direction, bool fromFisc)
        {
            if (null == personID)
                throw new Exception("Invalid person ID.  Value is null");

            return Service.InitiateBackgroundCheck(personID, TSCTransactionTypeID, TransactionControlNumber, TransactionDate, ProgramIdentification, 
											ResponseIdentification, Status, StatusText, XMLdata, Direction, fromFisc);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="personID">Guid of the person being updated</param>
		/// <returns>True if update successful</returns>
		public bool UpdateBackgroundCheck(Guid personID, string AgencyCode, string CheckTypeCode, string TransactionTypeCode, string TransactionControlNumber,
											DateTime TransactionDate, string Result, DateTime ResultDate, string ResultDetails, DateTime ResultDetailDate, bool fromFisc)
        {
            if (null == personID)
                throw new Exception("Invalid person ID.  Value is null");

            return Service.UpdateBackgroundCheck(personID, AgencyCode, CheckTypeCode, TransactionTypeCode, TransactionControlNumber,
											TransactionDate, Result, ResultDate, ResultDetails, ResultDetailDate, fromFisc);
        }

		/// <summary>
		/// New IWS process 6/2013
		/// </summary>
		public bool UpdateBackgroundCheckStatus(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, DateTime TransactionDate,
					 string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, string XMLdata, bool fromFisc)
		{
			if (null == personID)
				throw new Exception("Invalid person ID.  Value is null");

			return Service.UpdateBackgroundCheckStatus(personID, TransactionName, AgencyCode, TransactionControlNumber, TransactionDate,
					 ProgramIdentification, ResponseIdentification, TransmissionStatus, TransmissionStatusText, XMLdata, fromFisc);
		}

		/// <summary>
		/// New IWS process 6/2013
		/// </summary>
		public bool UpdateBackgroundCheckResult(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, DateTime TransactionDate,
					 string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, string XMLdata,
					 string AgencyResult, DateTime AgencyResultDate, string AgencyResultDetails, DateTime AgencyResultDetailDate,
					 string BackgroundCheckID, string BackgroundCheckType, bool fromFisc, string company)
		{
			if (null == personID)
				throw new Exception("Invalid person ID.  Value is null");

			return Service.UpdateBackgroundCheckResult(personID, TransactionName, AgencyCode, TransactionControlNumber, TransactionDate,
					 ProgramIdentification, ResponseIdentification, TransmissionStatus, TransmissionStatusText, XMLdata,
					 AgencyResult, AgencyResultDate, AgencyResultDetails, AgencyResultDetailDate,
					 BackgroundCheckID, BackgroundCheckType, fromFisc, company);
		}


        /// <summary>
        /// 
        /// </summary>
		/// <param name="personID">Guid of the person being updated</param>
        /// <returns>URL for FBI history</returns>
		public string RetreiveFbiHistoryUrl(Guid personID)
        {
            if (null == personID)
                throw new Exception("Invalid person ID.  Value is null");

            return Service.RetreiveFbiHistoryUrl(personID);
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="personID">Guid of the person being updated</param>
      /// <param name="caseNumber">FBI case number for the person</param>
      /// /// <param name="result">FBI result</param>
      /// <returns>true on success, false otherwise</returns>
      public bool SetFBICaseNumber(Guid iwsPersonID, string caseNumber, string result = "PASS", DateTime resultDate = new DateTime())
		{
         if (resultDate == new DateTime())
            resultDate = DateTime.Now;
         return Service.SetFBICaseNumber(iwsPersonID, caseNumber, result, resultDate);
		}

      /// <summary>
      /// 
      /// </summary>
      /// <param name="personID">Guid of the person being updated</param>
      /// <param name="caseNumber">STA case number for the person</param>
      /// /// <param name="result">STA result</param>
      /// <returns>true on success, false otherwise</returns>
      public bool SetTSACaseNumber(Guid iwsPersonID, string caseNumber, string result , DateTime resultDate)
      {
         return Service.SetTSACaseNumber(iwsPersonID, caseNumber, result, resultDate);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="personID">Guid of the person being updated</param>
      /// <returns>True if badge deactivated successfully</returns>
      public bool ExpireBadge(Guid personID, int cardID)
		{
			if (null == personID)
				throw new Exception("Invalid person ID.  Value is null");

			return Service.ExpireBadge(personID, cardID);
		}



		public ProvisionData ProvisionedByCard(int cardID)
		{
			return Service.ProvisionedByCard(cardID);
		}


		public ProvisionData ProvisionedByBOAABadgeID(int BOAABadgeID)
		{
			return Service.ProvisionedByBOAABadgeID(BOAABadgeID);
		}

		public bool ProvisioningComplete(int cardID)
		{
			return Service.ProvisioningComplete(cardID);
		}





        /// <summary>
        /// This is a test method.  It will determine if the client/API is setup proper to talk with the 
        /// Service.  The service will return a simple true on the call if the API is connected correctly.  
        /// </summary>
        /// <returns>true if the connection to the service is proper.  Will fail otherwise.</returns>
        public bool IsAlive()
        {
            return Service.IsAlive();
        }

        /// <summary>
        /// Test function to insure that that WebService is properly connected to the database.
        /// </summary>
        /// <returns>Returns true if web service is properly connected to the DB.</returns>
        public bool IsDbConnected()
        {
            return Service.IsDbConnected();
        }

        #endregion 

        #region "Properties"

        /// <summary>
        /// property to encapsulate the class' access to the service so it can be properly instantiated
        /// </summary>
        protected IwsServiceClient Service
        {
            get
            {
                if (null == _service)
                    _service = new IwsServiceClient();

                return _service;
            }
        }

        #endregion

	}

}
