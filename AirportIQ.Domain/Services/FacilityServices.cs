using System;
using System.Data;
using System.Configuration;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;

namespace AirportIQ.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// History:
    /// JBienvenu 2013-01-10 19303 Added property 'Code' to IFacility. Removed private member 'facilityCode'. Added method 'staff'.
    /// </remarks>
    public class FacilityServices : IFacility
    {
         
        #region "Private Variables"

            private readonly IFacilityRepository facilityRepository;
            //string facilityCode = ConfigurationManager.AppSettings["Facility"]); changed because returned null.

        #endregion

        #region "Public Properties"

            public Func<string> Code { get; set; }

        #endregion

            #region "Constructors"

            public FacilityServices() : this(new FacilityRepository()) { }

            public FacilityServices(IFacilityRepository facilityRepository)
            {
                if (facilityRepository == null) throw new ArgumentNullException("facilityRepository");
                this.facilityRepository = facilityRepository;
            }
            
        #endregion

        public DataTable workLocationsFormLoad()
        {
            return this.facilityRepository.getWorkLocations(this.Code()); 
        }

        public DataTable staffListLoad()
        {
            return this.facilityRepository.getStaff(this.Code());
        }
    }
}
