using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.EF.LookUps
{
  public partial class Person_PersonDivisionXref
  {
    public static Person_PersonDivisionXref GetPersonDivisionXref(int personID, int divisionID)
    {
      Person_PersonDivisionXref result = null;
    try
    {
      using (LookupEntities context = new LookupEntities())
      {
        result = (from p in context.Person_PersonDivisionXref
                  where p.PersonID == personID
                  && p.DivisionID == divisionID
                  select p).Single();
        ;
      }
    }
    catch (Exception)
    {
      result = null;
    }
    return result;
    }
  }
}
