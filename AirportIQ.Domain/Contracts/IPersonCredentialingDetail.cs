using System;
using System.Data;
namespace AirportIQ.Domain.Contracts
{
    public interface IPersonCredentialingDetail
    {
        DataSet PerCredPersonLoad(Int32 userID, Int32 personID, Int32? divisionID);
        DataSet PerCredSAFELoad(Int32 userID, Int32 personID, Int32? citationID);
		DataSet PerCredBadgeLoad(Int32 userID, Int32 personID, Int32 badgeID);
		DataSet PerCredFileLoad(Int32 docID);
	}

}
