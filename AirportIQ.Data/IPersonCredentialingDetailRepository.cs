using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace AirportIQ.Data
{
    public interface IPersonCredentialingDetailRepository
    {
        DataSet PerCredPersonLoad(Int32 UserId, Int32 PersonID, Int32? DivisionID);
        DataSet PerCredSAFELoad(Int32 UserId, Int32 PersonID, Int32? CitationID);
		DataSet PerCredBadgeLoad(Int32 UserId, Int32 PersonID, Int32 BadgeID);
		DataSet PerCredFileLoad(Int32 docID);
	}
}
