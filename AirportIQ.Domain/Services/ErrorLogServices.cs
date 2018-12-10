using System;
using System.Diagnostics;

using AirportIQ.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;

namespace AirportIQ.Domain.Services
{
    public class ErrorLogServices : IErrorLog
    {

        #region "Private Variables"

            private readonly IErrorRepository errorLogRepository;

        #endregion

        #region "Constructors"
   
            public ErrorLogServices() : this(new ErrorLogRepository()) { }

            public ErrorLogServices(IErrorRepository errorLogRepository)
            {
                if (errorLogRepository == null) throw new ArgumentNullException("errorLogRepository");
                this.errorLogRepository = errorLogRepository;
            }

        #endregion

        #region "Public Methods"

            public string logError(Exception Ex, string ErrorMessage)
            {
                this.errorLogRepository.logError(Ex, ErrorMessage);
				string s = GetFriendlyMessageForException(Ex);
				Debug.WriteLine(s);
				return s; 
            }
        #endregion

            #region "Private Methods"
            private static string GetFriendlyMessageForException(Exception ex)
            {

                string message = "Error: There was a problem while processing your request: " + ex.Message;

                if (ex.InnerException != null)
                {
                    Exception inner = ex.InnerException;
                    if (inner is System.Data.Common.DbException)
                        message = "Our database is currently experiencing problems. " + inner.Message;
                    else if (inner is NullReferenceException)
                        message = "There are one or more required fields that are missing.";
                    else if (inner is ArgumentException)
                    {
                        string paramName = ((ArgumentException)inner).ParamName;
                        message = string.Concat("The ", paramName, " value is illegal.");
                    }
                    else if (inner is ApplicationException)
                        message = "Exception in application" + inner.Message;
                    else
                        message = inner.Message;

                }

                return GetFormattedErrorMessage(message);
            }




            private enum MessageType { Success, Error, Notice }


            private static string GetFormattedSuccessMessage(string message)
                {
                    return GetFormattedMessage(message, MessageType.Success);
                }

            private static string GetFormattedErrorMessage(string message)
                {
                    return GetFormattedMessage(message, MessageType.Error);
                }

            private static string GetFormattedNoticeMessage(string message)
                {
                    return GetFormattedMessage(message, MessageType.Notice);
                }

            private static string GetFormattedMessage(string message, MessageType messageType = MessageType.Notice)
                {
                    // These divs mess up the gcr dialog boxes.
                    //switch (messageType)
                    //{
                    //    case MessageType.Success: return "<div style='text-transform:capitalize;font-weight:bold;font-size:large' class='success'>" + message + "</div>";
                    //    case MessageType.Error: return "<div style='text-transform:capitalize;font-weight:bold;font-size:large' class='error'>" + message + "</div>";
                    //    default: return "<div style='text-transform:capitalize;font-weight:bold;font-size:large' class='notice'>" + message + "</div>";
                    //}
                    return message;
                }

            public string GetFormattedMessage(string ErrorMessage)
            {
                //return "<div style='text-transform:capitalize;font-weight:bold;font-size:large' class='error'>" + ErrorMessage + "</div>";
                return ErrorMessage;
            }


        #endregion



            
    }
}
