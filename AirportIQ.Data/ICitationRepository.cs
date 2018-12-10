using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AirportIQ.Data
{
    public interface ICitationRepository
    {
        DataSet LoadCitationEntry(int citaitonID);
    }
}
