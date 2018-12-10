using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.EF.LookUps
{
  public partial class Lookups_Company_Companies
  {
    public static Dictionary<int, string> GetCompaniesListByFacilityCode(string facilityCode)
    {

      Dictionary<int, string> result;

      using (LookupEntities context = new LookupEntities())
		  {


      var divisions = (from d in context.Lookups_Division_Divisions
                      where d.FacilityCode == facilityCode
                      select d.CompanyID).Distinct();

      if (divisions.Count() < 1)
      {
        return null;
      }      


      var companies = from c in context.Lookups_Company_Companies
               where divisions.Contains(c.CompanyID)
							 orderby c.CorporationName
               select new
               {
                 c.CompanyID, 
                 c.CorporationName
               };

      result = companies.ToDictionary(r => r.CompanyID, r => r.CorporationName);

     }

      return result;
      
    }

    public static List<Lookups_Company_Companies> CompaniesWithActiveDivisions(string facilityCode)
    {
        List<Lookups_Company_Companies> companies = null;

        using (LookupEntities context = new LookupEntities())
        {
            var divisions = (from d in context.Lookups_Division_Divisions
                             where d.FacilityCode == facilityCode
                             && d.DivisionStatusCode == "A"
                             select d.CompanyID).Distinct();

            companies = (from c in context.Lookups_Company_Companies
                         where divisions.Contains(c.CompanyID)
                         orderby c.CorporationName
                         select c).ToList<Lookups_Company_Companies>();
        }

        return companies;
    }
  }
}
