using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.Collections.ObjectModel;
using System.Web;

using log4net;
using log4net.Config;
using log4net.Util;


namespace AirportIQ.IWS.Service.Web
{
    public class ErrorHandler : IErrorHandler
    {
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault) 
        {
        } 

        public bool HandleError(Exception error) 
        {
            LogManager.GetLogger("AirportIQ.IWS.Service.Web").Debug(error.ToString());
            return false;
        } 

    }

    public class ErrorServiceBehavior : IServiceBehavior
    {
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            ErrorHandler handler = new ErrorHandler();
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(handler);
            }
        }
    }

    public class ErrorLoggingAttribute : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            ErrorHandler handler = new ErrorHandler();
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(handler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }


    public class ErrorHandlerBehavior : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new ErrorServiceBehavior();
        }

        public override Type BehaviorType
        {
            get { return typeof(ErrorServiceBehavior); }
        }
    }
}