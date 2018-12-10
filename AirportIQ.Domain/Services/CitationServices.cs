using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using AirportIQ.Data;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Domain.Contracts;

namespace AirportIQ.Domain.Services
{
    public class CitationServices : ICitation
    {
        #region "Private Variables"

        private readonly ICitationRepository _citationRepository;

        #endregion

        
		#region "Constructors"

		public CitationServices() : this(new CitationRepository()) { }

        public CitationServices(ICitationRepository citationRepository)
		{
            if (citationRepository == null) throw new ArgumentNullException("citationRepository");
            this._citationRepository = citationRepository;
		}

		#endregion "Constructors"

        #region "Methods"

        public DataSet LoadCitationEntry(int citaitonID)
        {
            return this._citationRepository.LoadCitationEntry(citaitonID);
        }

        #endregion
    }
}
