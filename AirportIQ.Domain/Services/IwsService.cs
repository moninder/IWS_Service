using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;

namespace AirportIQ.Domain.Services
{
    public class IwsService : IIws
    {
        #region members
        
        private readonly IIwsRepository _iwsRepository;
        string facilityCode = Convert.ToString(ConfigurationManager.AppSettings["Facility"]);
        
        #endregion

        #region Constructors
            public IwsService() : this(new IwsRepository()) { }

            public IwsService(IIwsRepository iwsRepository)
            {
                if (iwsRepository == null) throw new ArgumentNullException("iwsRepository");
                this._iwsRepository = iwsRepository;                
            }
      #endregion

      #region FiscServices
      public DataTable GetBadges(Guid personID)
      {
         return _iwsRepository.GetBadges(personID);
      }
      public DataTable GetPerson(Guid personID)
      {
         return _iwsRepository.GetPerson(personID);
      }
      public DataTable GetFingerprints(DateTime start, DateTime end)
      {
         return _iwsRepository.GetFingerprints(start, end);
      }
      public DataTable GetDojStatus(Guid personID)
      {
         return _iwsRepository.GetDojStatus(personID);
      }

      public DataTable GetDocuments(Guid personID)
      {
         return _iwsRepository.GetDocuments(personID);
      }
      public Byte[] GetEBTS(Guid personID)
      {
         return _iwsRepository.GetEBTS(personID);
      }

      public bool AddNote(Guid personID, string note)
      {
         return _iwsRepository.AddNote(personID, note);
      }
      #endregion

      #region IIWs interface implementation

      public bool BiometricUpdate(Guid personID, byte[] image)
        {
            return _iwsRepository.BiometricUpdate(personID, image);
        }

        public bool UpdateBadgeID(int cardID, int badgeID)
        {
            return _iwsRepository.UpdateBadgeID(cardID, badgeID);
        }

		public bool InitiateBackgroundCheck(Guid personID, int TSCTransactionTypeID, string TransactionControlNumber, DateTime TransactionDate, string ProgramIdentification,
											string ResponseIdentification, string Status, string StatusText, string XMLdata, string Direction)
        {
            return _iwsRepository.InitiateBackgroundCheck(personID, TSCTransactionTypeID, TransactionControlNumber, TransactionDate, ProgramIdentification, 
											ResponseIdentification, Status, StatusText, XMLdata, Direction);
        }

		public bool UpdateBackgroundCheck(Guid personID, string AgencyCode, string CheckTypeCode, string TransactionTypeCode, string TransactionControlNumber,
											DateTime TransactionDate, string Result, DateTime ResultDate, string ResultDetails, DateTime ResultDetailDate)
        {
            return _iwsRepository.UpdateBackgroundCheck(personID, AgencyCode, CheckTypeCode, TransactionTypeCode, TransactionControlNumber,
											TransactionDate, Result, ResultDate, ResultDetails, ResultDetailDate);
        }

		public bool UpdateBackgroundCheckStatus(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, DateTime TransactionDate,
										 string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, string XMLdata)
				{
						return _iwsRepository.UpdateBackgroundCheckStatus(personID, TransactionName, AgencyCode, TransactionControlNumber, TransactionDate,
												 ProgramIdentification, ResponseIdentification, TransmissionStatus, TransmissionStatusText, XMLdata);
				}

    public string RetreiveFbiHistoryUrl(Guid personID)
        {
            return _iwsRepository.RetreiveFbiHistoryUrl(personID);
        }

		public bool ExpireBadge(Guid personID, int cardID)
		{
			return _iwsRepository.ExpireBadge(personID, cardID);
		}

		public DataTable ProvisionedByCard(int cardID)
		{
			return _iwsRepository.ProvisionedByCard(cardID);
		}

		public bool ProvisioningComplete(int cardID)
		{
			return _iwsRepository.ProvisioningComplete(cardID);
		}


    public bool IsDbAlive()
        {
            return _iwsRepository.IsDbAlive();
        }

        #endregion
    }
}
