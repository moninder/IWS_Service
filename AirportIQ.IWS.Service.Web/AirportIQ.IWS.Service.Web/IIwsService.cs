using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Data;

namespace AirportIQ.IWS.Service.Web
{

   /// <summary>
   /// This WCF behavior changes metadata generation for service contracts, so that operation
   /// parameters are required by default (XML schema minOccurs="1").
   /// See http://thorarin.net/blog/post.aspx?id=5fe3b4b6-0e3e-463e-ac42-10c1c4808853 for
   /// a more thorough explanation.
   /// </summary>
   /// <remarks>
   /// The OptionalAttribute can be used to mark individual parameters as optional.
   /// </remarks>
   /// <example>
   /// <code>
   /// [ServiceContract]
   /// [RequiredParametersBehavior]
   /// public interface IGreetingService
   /// {
   ///     [OperationContract]
   ///     string Greet(string name, [Optional] string language);
   /// }
   /// </code>
   /// </example>
   [AttributeUsage(AttributeTargets.Interface)]
   public class RequiredParametersBehaviorAttribute : Attribute, IContractBehavior, IWsdlExportExtension
   {
      private List<RequiredMessagePart> _requiredParameter;

      #region IContractBehavior Members (nothing to be done)

      void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
      {
      }

      void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
      {
      }

      void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
      {
      }

      void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
      {
      }

      #endregion

      #region IWsdlExportExtension Members

      /// <summary>
      /// When ExportContract is called to generate the necessary metadata, we inspect the service
      /// contract and build a list of parameters that we'll need to adjust the XSD for later.
      /// </summary>
      void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
      {
         _requiredParameter = new List<RequiredMessagePart>();

         foreach (var operation in context.Contract.Operations)
         {
            var inputMessage = operation.Messages.Where(m => m.Direction == MessageDirection.Input).First();
            var parameters = operation.SyncMethod.GetParameters();
            Debug.Assert(parameters.Length == inputMessage.Body.Parts.Count);

            for (int i = 0; i < parameters.Length; i++)
            {
               object[] attributes = parameters[i].GetCustomAttributes(typeof(OptionalAttribute), false);
               if (attributes.Length == 0)
               {
                  // The parameter has no [Optional] attribute, add it to the list of parameters
                  // that we need to adjust the XML schema for later on.
                  _requiredParameter.Add(new RequiredMessagePart()
                  {
                     Namespace = inputMessage.Body.Parts[i].Namespace,
                     Message = operation.Name,
                     Name = inputMessage.Body.Parts[i].Name
                  });
               }
            }
         }
      }

      /// <summary>
      /// When ExportEndpoint is called, the XML schemas have been generated. Now we can manipulate to
      /// our heart's content.
      /// </summary>
      void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
      {
         foreach (var p in _requiredParameter)
         {
            var schemas = exporter.GeneratedXmlSchemas.Schemas(p.Namespace);

            foreach (XmlSchema schema in schemas)
            {
               var message = (XmlSchemaElement)schema.Elements[p.XmlQualifiedName];
               var complexType = message.ElementSchemaType as XmlSchemaComplexType;
               Debug.Assert(complexType != null, "Expected input message to be complex type.");
               var sequence = complexType.Particle as XmlSchemaSequence;
               Debug.Assert(sequence != null, "Expected a sequence.");

               foreach (XmlSchemaElement item in sequence.Items)
               {
                  if (item.Name == p.Name)
                  {
                     item.MinOccurs = 1;
                     item.MinOccursString = "1";
                     break;
                  }
               }
            }
         }

         // Throw away the temporary list we generated
         _requiredParameter = null;
      }

      #endregion

      #region Nested types

      private class RequiredMessagePart
      {
         public string Namespace { get; set; }
         public string Message { get; set; }
         public string Name { get; set; }

         public XmlQualifiedName XmlQualifiedName
         {
            get
            {
               return new XmlQualifiedName(Message, Namespace);
            }
         }
      }

      #endregion
   }

   [AttributeUsage(AttributeTargets.Parameter)]
   public class OptionalAttribute : Attribute
   {
   }


   //



   // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
   [ServiceContract]
   [RequiredParametersBehavior]
   interface IIwsService
   {

      [OperationContract]
      FingerprintImages GetFingerprintImages(Guid personID);

      [OperationContract]
      Documents GetDocuments(Guid personID);

      [OperationContract]
      Doj GetDojStatus(Guid personID);

      [OperationContract]
      Badges GetBadges(Guid personID);

      [OperationContract]
      Person GetPerson(Guid personID);

      [OperationContract]
      Fingerprinted GetFingerprints(DateTime start, DateTime end);

      [OperationContract]
      Byte[] GetEBTS(Guid personID);

      [OperationContract]
       bool AddNote(Guid personID, string note);
      
      [OperationContract]
      bool BiometricUpdate(Guid personID, byte[] image);

      [OperationContract]
      bool UpdateBadgeID(int cardID, int badgeID);

      [OperationContract]
      bool InitiateBackgroundCheck(Guid personID, int TSCTransactionTypeID, string TransactionControlNumber, DateTime TransactionDate, string ProgramIdentification,
                               string ResponseIdentification, string Status, string StatusText, string XMLdata, string Direction, bool fromFisc);

      [OperationContract]
      bool UpdateBackgroundCheck(Guid personID, string AgencyCode, string CheckTypeCode, string TransactionTypeCode, string TransactionControlNumber,
                         DateTime TransactionDate, string Result, DateTime ResultDate, string ResultDetails, DateTime ResultDetailDate, bool fromFisc);

      //Rguidi 6/7/2013 : NEW process redesign: replaces InitiateBackgroundCheck
      [OperationContract]
      bool UpdateBackgroundCheckStatus(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, DateTime TransactionDate,
                           string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, string XMLdata, bool fromFisc);

      //Rguidi 6/7/2013 : NEW process redesign: replaces UpdateBackgroundCheck
      [OperationContract]
      bool UpdateBackgroundCheckResult(Guid personID, string TransactionName, string AgencyCode, string TransactionControlNumber, DateTime TransactionDate,
                           string ProgramIdentification, string ResponseIdentification, string TransmissionStatus, string TransmissionStatusText, string XMLdata,
                           string AgencyResult, DateTime AgencyResultDate, string AgencyResultDetails, DateTime AgencyResultDetailDate,
                           string BackgroundCheckID, string BackgroundCheckType, bool fromFisc, string company);

      [OperationContract]
      string RetreiveFbiHistoryUrl(Guid personID);

      [OperationContract]
      bool SetFBICaseNumber(Guid iwsPersonID, string caseNumber, string result, DateTime resultDate);
      [OperationContract]
      bool SetTSACaseNumber(Guid iwsPersonID, string caseNumber, string result, DateTime resultDate);

      [OperationContract]
      bool ExpireBadge(Guid personID, int cardID);

      [OperationContract]
      ProvisionData ProvisionedByCard(int cardID);

      [OperationContract]
      ProvisionData ProvisionedByBOAABadgeID(int BOAABadgeID);

      [OperationContract]
      bool ProvisioningComplete(int cardID);

      [OperationContract]
      bool IsAlive();

      [OperationContract]
      bool IsDbConnected();

      [OperationContract]
      bool DoDeletePersonTasks(DeletePersonInfo info);

      [OperationContract]
      DataTable GetPersonsForBatchUpdate();

      [OperationContract]
      PersonUpdate BeginPersonUpdate(int PersonUpdateID);

      [OperationContract]
      void CancelPersonUpdate();

      [OperationContract]
      PersonUpdate GetCurrentPersonUpdate();

      [OperationContract]
      List<PersonUpdate> GetAllPersonUpdates();

      [OperationContract]
      PersonUpdate PopulatePersonUpdate(List<int> BadgeIDs);
   }


   public class DeletePersonInfo
   {
      public string PersonGUID { get; set; }
      public int EmployeeID { get; set; }
      public int PersonDivisionCheckID { get; set; }
      public int UserID { get; set; }
      public int LastTransactionID { get; set; }
      public string SSN { get; set; }

      private List<DeletePersonBadgeInfo> _BadgeInfo = new List<DeletePersonBadgeInfo>();
      public List<DeletePersonBadgeInfo> BadgeInfo
      {
         get { return _BadgeInfo; }
         set { _BadgeInfo = value; }
      }

      private List<DeletePersonCompanyInfo> _CompanyInfo = new List<DeletePersonCompanyInfo>();
      public List<DeletePersonCompanyInfo> CompanyInfo
      {
         get { return _CompanyInfo; }
         set { _CompanyInfo = value; }
      }
   }

   public class Doj
   {
      /// <summary>
      /// The status of DOJ for the persons latest fingerprint
      /// </summary>
      public string DojStatus { get; set; }
   }

   public class DeletePersonBadgeInfo
   {
      public int BadgeID { get; set; }
      public int BadgeID_IWS { get; set; }
      public string BadgeNumber { get; set; }
      public string AccessLevel { get; set; }
      public string CorporationName { get; set; }
   }

   public class DeletePersonCompanyInfo
   {
      public string CompanyCode { get; set; }
      public string DivisionCode { get; set; }
   }

   public class PersonUpdate
   {
      public int PersonUpdateID { get; set; }
      public DateTime CreationDate { get; set; }

      private List<PersonUpdateDetail> _persons = new List<PersonUpdateDetail>();
      public List<PersonUpdateDetail> Persons
      {
         get { return _persons; }
         set { _persons = value; }
      }
   }

   public class PersonUpdateDetail
   {
      public int PersonUpdateID { get; set; }
      public int PersonID { get; set; }
      public string BadgeNumber { get; set; }
      public string BadgeStatusCode { get; set; }
      public string ReasonForDeactivation { get; set; }
      public string CorporationName { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public int EmployeeID { get; set; }
      public int DivisionTypeID { get; set; }
      public string TypeCode { get; set; }
      public string SocialSecurityNumber { get; set; }
      public Guid PersonGUID { get; set; }
      public int OrderBy { get; set; }
      public string DataSubmitted { get; set; }
      public bool IsPending { get; set; }
      public bool? IsSuccessful { get; set; }
      public DateTime? TransmitStart { get; set; }
      public DateTime? TransmitEnd { get; set; }
      public string ExceptionText { get; set; }

      private List<BadgeRecord> _badges = new List<BadgeRecord>();
      public List<BadgeRecord> Badges
      {
         get { return _badges; }
         set { _badges = value; }
      }
   }

   [DataContract]
   public class Person
   {
      [DataMember]
      public string LastName { get; set; }
      [DataMember]
      public string MiddleName { get; set; }
      [DataMember]
      public string FirstName { get; set; }
      [DataMember]
      public int EmployeeID { get; set; }

      [DataMember]
      public string citizenship { get; set; }

      [DataMember]
      public DateTime DateOfBirth { get; set; }

      [DataMember]
      public string SexCode { get; set; }

      [DataMember]
      public int HeightInInches { get; set; }

      [DataMember]
      public int WeightInPounds { get; set; }

      [DataMember]
      public string addressCity { get; set; }

      [DataMember]
      public int addressZip { get; set; }
      [DataMember]
      public string cardPayloadStatus { get; set; }
      [DataMember]
      public string cardPayloadType { get; set; }
      [DataMember]
      public string cardPayloadLpublic { get; set; }
      [DataMember]
      public string addressState { get; set; }
      [DataMember]
      public string CountrySubdivisionCode { get; set; }
      [DataMember]
      public string countryOfBirth { get; set; }
      [DataMember]
      public string addressCountry { get; set; }
      [DataMember]
      public string airportCode { get; set; }
      [DataMember]
      public string contactPhone { get; set; }
      [DataMember]
      public string contactPhone2 { get; set; }
      [DataMember]
      public string email { get; set; }
      [DataMember]
      public string EmailAddress_Alternate { get; set; }
      [DataMember]
      public int addressAptNo { get; set; }
      [DataMember]
      public string eyeColor { get; set; }
      [DataMember]
      public string fax { get; set; }
      [DataMember]
      public string hairColor { get; set; }
      [DataMember]
      public string race { get; set; }
      [DataMember]
      public string prefix { get; set; }
      [DataMember]
      public string residenceAddress { get; set; }
      [DataMember]
      public string SocialSecurityNumber { get; set; }
      [DataMember]
      public string suffix { get; set; }
      [DataMember]
      public string employer { get; set; }
      [DataMember]
      public string occupation { get; set; }
      [DataMember]
      public string division { get; set; }
      [DataMember]
      public string orgCode { get; set; }

      //-----------DOCUMENTS------------------------
      [DataMember]
      public string certNumberDs1350 { get; set; }
      [DataMember]
      public string formNumberI94 { get; set; }
      [DataMember]
      public string nonImmigrantVisaNumber { get; set; }
      [DataMember]
      public string passportIssuingCountry { get; set; }
      [DataMember]
      public string pasportNumber { get; set; }
      [DataMember]
      public string alienRegistrationNumber { get; set; }


      [DataMember]
      public string Alias1FirstName { get; set; }
      [DataMember]
      public string Alias1MiddleName { get; set; }
      [DataMember]
      public string Alias1LastName { get; set; }

      [DataMember]
      public string Alias2FirstName { get; set; }
      [DataMember]
      public string Alias2MiddleName { get; set; }
      [DataMember]
      public string Alias2LastName { get; set; }


      [DataMember]
      public string Alias3FirstName { get; set; }
      [DataMember]
      public string Alias3MiddleName { get; set; }
      [DataMember]
      public string Alias3LastName { get; set; }
      [DataMember]
      public string DojStatus { get; set; }

   }

   [DataContract]
   public class BadgeRecord
   {
      [DataMember]
      public System.Guid PersonGUID { get; set; }
      [DataMember]
      public int PersonID { get; set; }
      [DataMember]
      public int BadgeID { get; set; }
      [DataMember]
      public short DivisionTypeID { get; set; }
      [DataMember]
      public string TypeCode { get; set; }
      [DataMember]
      public string BadgeNumber { get; set; }
      [DataMember]
      public string CorporationName { get; set; }
      [DataMember]
      public string BadgeStatusCode { get; set; }
      [DataMember]
      public long BadgeID_IWS { get; set; }
      [DataMember]
      public string ReasonForDeactivation { get; set; }
   }

   [DataContract]
   public class Badges
   {
      [DataMember]
      public List<BadgeDetails> badges = new List<BadgeDetails>();
   }

      [DataContract]
   public class BadgeDetails
   {
      [DataMember]
      public System.Guid PersonGUID { get; set; }
      [DataMember]
      public string BadgeColor { get; set; }
      [DataMember]

      public int BadgeNumber { get; set; }
      [DataMember]
      public int DivisionID { get; set; }
      [DataMember]
      public int CompanyID { get; set; }
      [DataMember]
      public string Employer { get; set; }
      [DataMember]
      public string DvisionName { get; set; }

      [DataMember]
      public string jobTitle { get; set; }
      [DataMember]
      public string JobRoleDescription { get; set; }
      [DataMember]
      public string BadgeStatusCode { get; set; }
      [DataMember]
      public string customsType { get; set; }
      [DataMember]
      public string BadgeIssuanceReasonCode { get; set; }

      [DataMember]
      public DateTime originalBadgeDate { get; set; }

      [DataMember]
      public DateTime issueDate { get; set; }

      [DataMember]
      public DateTime expiryDate { get; set; }

      [DataMember]
      public DateTime lastUpdateDateTime { get; set; }
   }

   /// <summary>
   /// Defines the return values for FIX THIS
   /// </summary>
   [DataContract]
   public enum StatusUpdate
   {
      NewEmployee = 0x0001,
      UpdateEmployee = 0x0002,
      Cop = 0x0004,
      Airport = 0x0008
   }

   [DataContract]
   public class Documents
   {
      [DataMember]
      public List<Document> documents = new List<Document>();
   }



   [DataContract]
   public class Document
   {
      [DataMember]
      public string Description { get; set; }
      [DataMember]
      public DateTime WhenCreated { get; set; }
      [DataMember]
      public string CountryCode { get; set; }
      [DataMember]
      public string StateCode { get; set; }
      [DataMember]
      public string School { get; set; }
      [DataMember]
      public DateTime ExpirationDate { get; set; }
   }

   [DataContract]
   public class FingerprintImage
   {
      [DataMember]
      public string sequence { get; set; }
      [DataMember]
      public Byte [] image { get; set; }
   }

   [DataContract]
   public class FingerprintImages
   {
      [DataMember]
      public List<FingerprintImage> fingerprintImages { get; set; }
      [DataMember]
      public int version { get; set; }
   }



   [DataContract]
   public class Fingerprint
   {
      [DataMember]
      public System.Guid PersonGUID { get; set; }
      [DataMember]
      public DateTime Created { get; set; }
      [DataMember]
      public string CreatedBy { get; set; }
      [DataMember]
      public string FPType { get; set; }
   }

   [DataContract]
   public class Fingerprinted
   {
      [DataMember]
      public List<Fingerprint> fingerprinted = new List<Fingerprint>() { };
   }


   [DataContract]
   public class CategoryData
   {
      [DataMember]
      public char AccessType; // D for Default or S for Special

      [DataMember]
      public int CategoryID;

      [DataMember]
      public string CategoryName;

      [DataMember]
      public DateTime WhenBecomesActive;

      [DataMember]
      public DateTime WhenExpires;

      public CategoryData(char accessType, int categoryID, string categoryName, DateTime whenBecomesActive, DateTime whenExpires)
      {
         AccessType = accessType;
         CategoryID = categoryID;
         CategoryName = categoryName;
         WhenBecomesActive = whenBecomesActive;
         WhenExpires = whenExpires;
      }
   }



   /// <summary>
   /// Wrapper class for the provisioned access data for Cards.
   /// </summary>
   [DataContract]
   public class ProvisionData
   {
      [DataMember]
      public long IWS_CardID;

      [DataMember]
      public string PIN;

      [DataMember]
      public string EmployeeID;

      [DataMember]
      public string CompanyDivision;

      [DataMember]
      public int JobRoleID;

      [DataMember]
      public List<CategoryData> ProvisionedCategories;

      public ProvisionData(int cardID, string pin, string employeeid, string companydivision, int jobroleid)
      {
         IWS_CardID = cardID;
         PIN = pin;
         EmployeeID = employeeid;
         CompanyDivision = companydivision;
         JobRoleID = jobroleid;
         ProvisionedCategories = new List<CategoryData>();
      }
      public ProvisionData(ProvisionData p)
      {
         IWS_CardID = p.IWS_CardID;
         PIN = p.PIN;
         EmployeeID = p.EmployeeID;
         CompanyDivision = p.CompanyDivision;
         JobRoleID = p.JobRoleID;
         ProvisionedCategories = p.ProvisionedCategories;
      }
   }

}
