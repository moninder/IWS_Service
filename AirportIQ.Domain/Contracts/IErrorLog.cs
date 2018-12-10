using System;

namespace AirportIQ.Domain.Contracts
{
    public interface IErrorLog
    {
        string logError(Exception Ex, string ErrorMessage);
        string GetFormattedMessage(string ErrorMessage);
    }
}
