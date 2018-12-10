using System;

namespace AirportIQ.Data
{
    public interface IErrorRepository
    {
        void logError(Exception Ex, string ErrorMessage);   
    }
}

       
   