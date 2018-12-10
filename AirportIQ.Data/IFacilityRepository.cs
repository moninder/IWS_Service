using System;
using System.Data;

namespace AirportIQ.Data
{
    public interface IFacilityRepository
    {
        DataTable getWorkLocations(string facilityCode);
        DataTable getStaff(string facilityCode); //JBienvenu 2013-01-10 new
    }
}
