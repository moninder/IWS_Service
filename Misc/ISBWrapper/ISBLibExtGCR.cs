using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

using log4net;
using log4net.Config;

using isblib = ImageWare.ISBLibrary;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using ISBWrapper;


// Change History
//
// 10.24.2012 - KAS
// General mods throughout CMS calls. BOAABadgeID stored as external system ID in CMS
// instead of the BOAABadgeID card text data field
//
// The following functions support passing either a BOAA Badge ID or IWS Card ID.
//
// CMSGetProvisioningDataByBOAABadgeID(BOAABadgeID) and CMSGetProvisioningData(iwsCardID)
// CMSSyncProvisioningDataByBOAABadgeID(BOAABadgeID) and CMSSyncProvisioningData(iwsCardID)
// CMSActivateCardExBOAABadgeID(BOAABadgeID) and CMSActivateCardEx(iwsCardID)
// CMSRevokeCardByBOAABadgeID(BOAABadgeID) and CMSRevokeCard(iwsCardID)
//
// Note that the xxxByBOAABadgeID() calls are making an extra call to CMS
// to retrieve the iws card id and will take longer.

// CMSGetPersonByBOAABadgeID() Removed SystemID param, BOAA system id is always 4, Search by external system id
// New functionality, CMSActivateCardExBOAABadgeID and CMSActivateCardEx

// Added two new method calls to the list on 5/2/2013 - New methods are UpdatePersonStatus(string personGUID, int newStatus) and _
// UpdatePersonAppearance(string personGUID, int newHeight, int newWeight)

// rguidi 5/9/2013 new Bulk Provisioning call

// rguidi 6/5/2013 new ReTransmit call 

namespace ISBLibTest
{
    public class CardProvisionData
    {
        public int Position { get; set; }

        public int AccessType { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public DateTime ActiveDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public CardProvisionData()
        {
            Position = AccessType = CategoryID = -1;
            CategoryName = "";
        }
    }

    // class to store person info we're
    // returning to the caller
    public class IBMAPerson
    {
        public isblib.IDMSPerson Person { get; set; }
        public List<int> IBMACardID { get; set; }
        public List<int> BOAACardID { get; set; }
        public IBMAPerson()
        {
            Person = null; IBMACardID = new List<int>(); BOAACardID = new List<int>();
        }
    }

    public class ISBLibExtGCR
    {
        private isblib.DocumentManager _docMgr = new isblib.DocumentManager();
        private isblib.IDMS _IDMS = new isblib.IDMS();
        private isblib.CMS _CMS = new isblib.CMS();
        private string _CMS_baseURL;
        private string _EBTS_baseURL;
        private int BOAA_SYSTEM_ID = 4;

        private static ILog ILog;// = LogManager.GetLogger(typeof(ISBLibExtGCR));

        public ISBLibExtGCR(string DOCM_IP, string IDMS_IP, string CMS_IP, string EBTS_IP)
        {
            _docMgr.URL = @"http://" + DOCM_IP + "/DocumentManagerEJB/DocumentManagerBeanService/DocumentManagerBean";
            _IDMS.URL = @"http://" + IDMS_IP + "/IdentityManagerEJB/IdentityManagerBeanService/IdentityManagerBean";
            _CMS_baseURL = @"http://" + CMS_IP + "/";
            _EBTS_baseURL = @"http://" + EBTS_IP + "/EBTSServerEJB/EBTSServerBeanService/EBTSServerBean";

            ConfigureIWSLog();
            ConfigureLog4NetLog2();
        }

        private void ConfigureIWSLog()
        {
            isblib.ISBLibrary i = new isblib.ISBLibrary();

            if (ConfigurationManager.AppSettings["EnableLogging"] != null)
            {
                bool enableLogging = bool.Parse(ConfigurationManager.AppSettings["EnableLogging"]);

                if (enableLogging)
                {
                    string loggingPath = @"c:\temp\iws.txt";
                    bool enableTracing = false;

                    if (ConfigurationManager.AppSettings["IWSLoggingPath"] != null)
                        loggingPath = ConfigurationManager.AppSettings["IWSLoggingPath"];

                    if (ConfigurationManager.AppSettings["EnableTracing"] != null)
                        enableTracing = bool.Parse(ConfigurationManager.AppSettings["EnableTracing"]);

                    i.InitializeLogging(
                        loggingPath,
                        enableTracing == true ? isblib.enLogMode.enLogTrace : isblib.enLogMode.enLogErrors);
                }
            }
        }

        private void ConfigureLog4NetLog()
        {
            if (ConfigurationManager.AppSettings["EnableLogging"] != null)
            {
                bool enableLogging = bool.Parse(ConfigurationManager.AppSettings["EnableLogging"]);

                if (enableLogging)
                {
                    try
                    {
                        // get the interface
                        var logger = (log4net.Repository.Hierarchy.Logger)ILog.Logger;
                        //var root = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root;

                        logger.RemoveAllAppenders();

                        // get the layout string
                        //string log4netLayoutString =  "%d [%x]%n   %m%n  %P MessageData}%n%n";
                        string log4netLayoutString = "%d%n   %m%n%n";

                        // get logging path
                        string log4netPath = @"c:\temp\iws_wrapper.txt";

                        if (ConfigurationManager.AppSettings["IWSWrapperLoggingPath"] != null)
                            log4netPath = ConfigurationManager.AppSettings["IWSWrapperLoggingPath"];

                        // create the appender
                        var rollingFileAppender = new RollingFileAppender();

                        // setup the appender
                        rollingFileAppender.MaxFileSize = 1000000;
                        rollingFileAppender.MaxSizeRollBackups = 5;
                        rollingFileAppender.RollingStyle = RollingFileAppender.RollingMode.Size;
                        rollingFileAppender.StaticLogFileName = true;

                        // update file property of appender
                        rollingFileAppender.File = log4netPath;

                        // add the layout
                        PatternLayout patternLayout = new PatternLayout(log4netLayoutString);
                        rollingFileAppender.Layout = patternLayout;

                        //// add the filter for the log source
                        //NdcFilter sourceFilter = new NdcFilter();
                        //sourceFilter.StringToMatch = "iws";
                        //rollingFileAppender.AddFilter(sourceFilter);

                        //// now add the deny all filter to end of the chain
                        //DenyAllFilter denyAllFilter = new DenyAllFilter();
                        //rollingFileAppender.AddFilter(denyAllFilter);

                        // activate the options
                        rollingFileAppender.ActivateOptions();

                        // add the appender
                        logger.AddAppender(rollingFileAppender);

                        BasicConfigurator.Configure();
                    }
                    catch (Exception ex)
                    {
                        //this.ErrorLog.Error("Error creating LIS3 data log appender for " + LogSourceName, x);
                        Debug.WriteLine(ex.ToString());
                        Trace.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private void ConfigureLog4NetLog2()
        {
            if (ConfigurationManager.AppSettings["EnableLogging"] != null)
            {
                bool enableLogging = bool.Parse(ConfigurationManager.AppSettings["EnableLogging"]);

                if (enableLogging)
                {
                    try
                    {
                        Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
                        RollingFileAppender roller = new RollingFileAppender();
                        roller.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
                        roller.AppendToFile = true;
                        roller.RollingStyle = RollingFileAppender.RollingMode.Composite;
                        roller.MaxSizeRollBackups = 5;
                        roller.MaximumFileSize = "1000KB";
                        roller.DatePattern = "yyyyMMdd";
                        roller.Layout = new log4net.Layout.PatternLayout();

                        // get logging path
                        string log4netPath = @"c:\temp\iws_wrapper.txt";

                        if (ConfigurationManager.AppSettings["IWSWrapperLoggingPath"] != null)
                            log4netPath = ConfigurationManager.AppSettings["IWSWrapperLoggingPath"];

                        roller.File = log4netPath;
                        roller.StaticLogFileName = true;

                        PatternLayout patternLayout = new PatternLayout();

                        string log4netLayoutString = "%d%n   %m%n%n";

                        patternLayout.ConversionPattern = log4netLayoutString;
                        patternLayout.ActivateOptions();

                        roller.Layout = patternLayout;
                        roller.ActivateOptions();
                        hierarchy.Root.AddAppender(roller);

                        hierarchy.Root.Level = Level.All;
                        hierarchy.Configured = true;

                        DummyLogger dummyILogger = new DummyLogger("ISBLibExtGCR");
                        dummyILogger.Hierarchy = hierarchy;
                        dummyILogger.Level = log4net.Core.Level.All;
                        dummyILogger.AddAppender(roller);

                        ILog = new LogImpl(dummyILogger);

                        log("*********** Logging Started ***********");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                        Trace.WriteLine(ex.ToString());
                    }
                }
            }
        }

        public void log(string ss)
        {
            ILog.Warn(ss);
            Debug.WriteLine(ss);
        }

        private string MakeCMSURL(string urlBase, string service)
        {
            return (urlBase + "CMS/CMS" + service + "EJBService/CMS" + service + "EJB");
        }

        internal int MakeInt(string val, int vdef)
        {
            int retval = vdef;
            int t;

            if (int.TryParse(val, out t))
            {
                retval = t;
            }
            return retval;
        }

        internal DateTime MakeDate(string val)
        {
            DateTime retval = new DateTime();
            DateTime t;

            // default to a bad date
            retval = DateTime.MinValue;

            if (DateTime.TryParse(val, out t))
            {
                retval = t;
            }
            return retval;
        }

        /// <summary>
        /// IWS utility function to get their cardID from our badgeID.
        /// </summary>
        public int CMSGetIWSCardIDfromBOAABadgeID(int boaaBadgeID)
        {
            int retval = -1;
            string name = "[CMSGetIWSCardIDfromBOAABadgeID]";

            try
            {
                isblib.CMSSearchRequest searchreq = new isblib.CMSSearchRequest();

                isblib.CMSSearchResponse searchrsp = null;

                // 10.24.2012 - boaa badge id is now stored as a external system ID in CMS
                searchreq.ExternalCardID = new isblib.CMSExternalSystemID(BOAA_SYSTEM_ID, boaaBadgeID.ToString());

                string s = MakeCMSURL(_CMS_baseURL, "Query");

                searchrsp = _CMS.Search(s, searchreq);

                if (searchrsp != null && searchrsp.CardData.Count > 0)
                {
                    retval = ((isblib.CMSCardData)searchrsp.CardData[0]).CardID;
                }
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }

            return retval;
        }

        /// <summary>
        /// CMSUpdateBadgePIN - updates the PIN value for 1 or more cards,
        // if updateAllCards == true the PIN's of all cards associated to the card holder will
        // be updated
        /// </summary>
        public bool CMSUpdateBadgePIN(int iwsCardID, string newPIN, bool updateAllCards)
        {
            string name = "CMSUpdateBadgePIN";
            bool retval = false;

            try
            {
                isblib.CMSCardUpdateRequest updatereq = new isblib.CMSCardUpdateRequest();

                updatereq.CardID = iwsCardID;
                updatereq.CardDataField.Add(new isblib.CMSField("PIN", isblib.enCMSDataType.cmsString, newPIN));

                // add the action to update all cards
                if (updateAllCards)
                {
                    // begin action
                    // add the action that will return additional data about the card holder
                    isblib.RequestActions reqaction = new isblib.RequestActions();
                    isblib.RequestAction action = new isblib.RequestAction();

                    reqaction.Name = "TestAction";
                    reqaction.Type = "Sample";

                    action.Action = "com.iwsinc.lawa.cms.PINUpdateAction";
                    action.Event = isblib.RequestAction.enEventType.enActionSuccess;

                    reqaction.Action.Add(action);

                    // must set IsValid to true or action will NOT be passed to underlying web service
                    reqaction.IsValid = true;

                    updatereq.RequestActions = reqaction;

                    // end action
                }

                int resp = _CMS.UpdateCard(MakeCMSURL(_CMS_baseURL, "UpdateCard"), updatereq);

                retval = resp == 1;
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Activate the card and update the card provisioning data in CMS
        /// </summary>
        /// <param name="boaaBadgeID"></param>
        /// <returns>Returns the card status code</returns>
        public int CMSActivateCardExBOAABadgeID(int boaaBadgeID)
        {
            string name = "[CMSActivateCardExBOAABadgeID]";
            int retval = -1;

            try
            {
                // CMSActivateCardExBOAABadgeID makes an additional call to CMS to
                // retrieve the iws card id. if you have the iws card id call
                // CMSActivateCardExBOAABadgeID

                int iwsCardID = CMSGetIWSCardIDfromBOAABadgeID(boaaBadgeID);

                // uh-oh
                if (iwsCardID == -1)
                {
                    log(name + ": Failed to retrieve card id for BOAA badge id: " + boaaBadgeID.ToString());
                    return retval;
                }

                retval = CMSActivateCardEx(iwsCardID);
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Activate the card and update the card provisioning data in CMS
        /// </summary>
        /// <returns>Returns the card status code</returns>
        public int CMSActivateCardEx(int iwsCardID)
        {
            string name = "[CMSActivateCardEx]";
            int retval = -1;

            try
            {
                isblib.CMSActivateCardRequest request = new isblib.CMSActivateCardRequest();
                isblib.CMSActivateCardResponse response = new isblib.CMSActivateCardResponse();

                // specify the cusom action
                isblib.RequestActions reqaction = new isblib.RequestActions();
                isblib.RequestAction action = new isblib.RequestAction();

                reqaction.Name = "TestAction";
                reqaction.Type = "Sample";

                //action.Action = "com.iwsinc.lawa.cms.ActivateProvisionedByCardAction";
                action.Action = "com.iwsinc.lawa.cms.ActivateInPPAction";
                action.Event = isblib.RequestAction.enEventType.enActionSuccess;

                reqaction.Action.Add(action);

                // must set IsValid to true or action will NOT be passed to underlying web service
                reqaction.IsValid = true;

                request.RequestActions = reqaction;
                // end action

                request.CardID = iwsCardID;

                response = _CMS.ActivateCard(MakeCMSURL(_CMS_baseURL, "Activate"), request);

                // return the current card status
                if (response != null)
                    retval = response.CardStatus;
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Return picture documents for this person
        /// Item 6 in New IDSB Interface Requirement Definition v0.85.xlsx
        /// </summary>
        public List<isblib.DMDocument> GetPicture(string personGUID)
        {
            //  IWS' document types:
            //
            // 1 - DGN
            // 2 - QuickBadge Portrait
            // 3 - Migrated EFT
            // 4 - Migrated Picture
            // 5 - EBTS File
            // 10 - Face
            // 11 - Fingerprint
            // 12 - Document (EBTS related)
            // 13 - Iris

            List<isblib.DMDocument> ret = new List<isblib.DMDocument>();
            List<isblib.DMDocument> lis = new List<isblib.DMDocument>();

            int[] pictureTypes = new int[] { 10 };

            foreach (int pt in pictureTypes)
            {
                lis = GetDocument(personGUID, pt);
                foreach (isblib.DMDocument d in lis)
                {
                    ret.Add(d);
                }
            }
            return ret;
        }

        public int GetIdentityDocumentVersion(string IDMSGUID)
        {
            string name = "[GetIdentityDocumentVersion]";
            short type = 10;

            isblib.ISBSearch search = new isblib.ISBSearch();
            isblib.SearchConjunction searchConjunction = new isblib.SearchConjunction();
            searchConjunction.LogicalOp = isblib.SearchLogicalOp.AND;
            searchConjunction.AddCriteria(new isblib.SearchCriteria("DOCUMENT_FILTER!TYPE", type.ToString(), isblib.enValueType.vtInt, isblib.SearchCompareOp.EQ));
            searchConjunction.AddCriteria(new isblib.SearchCriteria("DOCUMENT_FILTER!PERSON_GUID", IDMSGUID, isblib.enValueType.vtString, isblib.SearchCompareOp.EQ));
            search.SearchType = isblib.SearchType.enSTUndefined;
            search.FilterSearchType = isblib.SearchType.enSTConjunction;
            search.FilterAddConjunction(searchConjunction);
            List<isblib.DMDocumentInfo> listDMInfo = _docMgr.SearchDocuments(search);

            if (listDMInfo.Count == 0)
            {
                log(name + "The DocumentManager did not return a portrait for this person");
                return -1;
            }
            return listDMInfo[listDMInfo.Count - 1].Version;
        }

        public isblib.DMDocument GetIdentityDocument(string IDMSGUID)
        {
            string name = "[GetIdentityDocument]";
            short type = 10;

            isblib.ISBSearch search = new isblib.ISBSearch();
            isblib.SearchConjunction searchConjunction = new isblib.SearchConjunction();
            searchConjunction.LogicalOp = isblib.SearchLogicalOp.AND;
            searchConjunction.AddCriteria(new isblib.SearchCriteria("DOCUMENT_FILTER!TYPE", type.ToString(), isblib.enValueType.vtInt, isblib.SearchCompareOp.EQ));
            searchConjunction.AddCriteria(new isblib.SearchCriteria("DOCUMENT_FILTER!PERSON_GUID", IDMSGUID, isblib.enValueType.vtString, isblib.SearchCompareOp.EQ));
            search.SearchType = isblib.SearchType.enSTUndefined;
            search.FilterSearchType = isblib.SearchType.enSTConjunction;
            search.FilterAddConjunction(searchConjunction);
            List<isblib.DMDocumentInfo> listDMInfo = _docMgr.SearchDocuments(search);

            if (listDMInfo.Count == 0)
            {
                log(name + " The DocumentManager did not return a portrait for this person");
                return null;
            }
            isblib.DMDocument docToLoad = new isblib.DMDocument();
            docToLoad.DocumentInfo = listDMInfo[listDMInfo.Count - 1];

            return _docMgr.GetDocument(docToLoad);
        }

        /// <summary>
        /// Return all documents for this person and document type
        /// Used by item 6 and 20 in New IDSB Interface Requirement Definition v0.85.xlsx
        /// </summary>
        public List<isblib.DMDocument> GetDocument(string personGUID, int docType)
        {
            string name = "GetDocument";
            List<isblib.DMDocument> retval = new List<isblib.DMDocument>();
            isblib.ISBSearch query = new isblib.ISBSearch();

            // there are 2 types of searches, Filter type search which targets core table fields and
            // normal searches which search meta data fields.
            // Document Filter search fields are:
            // DOCUMENT_FILTER!TYPE, DOCUMENT_FILTER!PERSON_GUID, DOCUMENT_FILTER!CREATED, DOCUMENT_FILTER!CREATED_BY,
            // DOCUMENT_FILTER!LAST_UPDATED, DOCUMENT_FILTER!LAST_UPDATED_BY
            // All other fields are considered meta data searches. You can search on both Filter and MetaData
            // fields in 1 query.

            isblib.SearchConjunction sc = new isblib.SearchConjunction();

         // create the criteria
         isblib.SearchCriteria c1 = new isblib.SearchCriteria("DOCUMENT_FILTER!PERSON_GUID", personGUID, isblib.enValueType.vtString, isblib.SearchCompareOp.EQ);
         isblib.SearchCriteria c2 = new isblib.SearchCriteria("DOCUMENT_FILTER!TYPE", docType.ToString(), isblib.enValueType.vtInt, isblib.SearchCompareOp.EQ);

         // add criteria to search
         sc.AddCriteria(c1);
            sc.AddCriteria(c2);

            // set the search conjunction logical operator
            sc.LogicalOp = isblib.SearchLogicalOp.AND;

             query.FilterAddConjunction(sc);

        // isblib.SearchConjunction sc2 = new isblib.SearchConjunction();
        // isblib.SearchCriteria c3 = new isblib.SearchCriteria("DOCUMENT_FILTER!TYPE", 5.ToString(), isblib.enValueType.vtInt, isblib.SearchCompareOp.EQ);
        // isblib.SearchCriteria c4 = new isblib.SearchCriteria("DOCUMENT_FILTER!TYPE", 10.ToString(), isblib.enValueType.vtInt, isblib.SearchCompareOp.EQ);
        //// sc2.LogicalOp = isblib.SearchLogicalOp.OR;
        // query.FilterAddConjunction(sc2);

         // set the search type
         query.FilterSearchType = isblib.SearchType.enSTConjunction;

            List<isblib.DMDocumentInfo> results = null; //List of documents that match the query
            try
            {
                // execute the search, returns doc info but no document data
                results = _docMgr.SearchDocuments(query);

                foreach (isblib.DMDocumentInfo dmi in results)
                {
                    isblib.DMDocument doc = new isblib.DMDocument();

                    // GetDocument can retrieve by GUID or LegacyID
                    doc.DocumentInfo.GUID = dmi.GUID;

                    // get the document
                    doc = _docMgr.GetDocument(doc);

                    // add to list of documents,
                    // the document bytes are in doc.Document
                    if (doc != null && doc.Document != null)
                        retval.Add(doc);
                }
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);

                //throw ex;
            }

            return retval;
        }

        public void RemoveDocument(string DocumentGUID)
        {
            log("[RemoveDocument]" + Environment.NewLine + "DocumentGUID: " + DocumentGUID);

            isblib.DMDocument doc = new isblib.DMDocument();
            doc.DocumentInfo.GUID = DocumentGUID;
            doc = _docMgr.GetDocument(doc);

            _docMgr.DeleteDocument(doc, false);            
        }

        /// <summary>
        /// Deactivate 'BOAA_BadgeID' for 'reason'
        /// Item 10 in New IDSB Interface Requirement Definition v0.85.xlsx
        /// </summary>
        /// <param name="boaaBadgeID"></param>
        public isblib.CMSRevokeCardResponse CMSRevokeCardByBOAABadgeID(int boaaBadgeID, string reason)
        {
            string name = "[CMSRevokeCardByBOAABadgeID]";
            log(name + "(BOAA_BadgeID:" + boaaBadgeID + ", reason:'" + reason + "')");

            isblib.CMSRevokeCardResponse retval = null;
            isblib.CMSRevokeCardRequest rvkreq = new isblib.CMSRevokeCardRequest();

            try
            {
                // CMSRevokeCardByBOAABadgeID makes an additional call to CMS to
                // retrieve the iws card id. if you have the iws card id call
                // CMSRevokeCard

                int iwsCardID = CMSGetIWSCardIDfromBOAABadgeID(boaaBadgeID);

                // uh-oh
                if (iwsCardID == -1)
                {
                    log(name + ": Failed to retrieve card id for BOAA badge id: " + boaaBadgeID.ToString());
                    return retval;
                }

                retval = CMSRevokeCard(iwsCardID, reason);
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Item 10, Deactivate this badge.
        /// </summary>
        public isblib.CMSRevokeCardResponse CMSRevokeCard(int iwsCardID, string reason)
        {
            string name = "[CMSRevokeCard]";
            isblib.CMSRevokeCardResponse retval = null;
            isblib.CMSRevokeCardRequest rvkreq = new isblib.CMSRevokeCardRequest();


            try
            {
                //map BOAA Revoke reason to IWS ReasonForDeactivation
                //string IWSReasonForDeactivation = MapIWSReasonForDeactivation(reason);
                string IWSReasonForDeactivation = reason.ToUpper();

                rvkreq.CardID = iwsCardID;
                rvkreq.Reason = IWSReasonForDeactivation;

                // specify the cusom action
                isblib.RequestActions reqaction = new isblib.RequestActions();
                isblib.RequestAction action = new isblib.RequestAction();

                reqaction.Name = "TestAction";
                reqaction.Type = "Sample";

                action.Action = "com.iwsinc.lawa.cms.RevokeBadgeAction";
                action.Event = isblib.RequestAction.enEventType.enActionSuccess;

                reqaction.Action.Add(action);

                // must set IsValid to true or action will NOT be passed to underlying web service
                reqaction.IsValid = true;

                rvkreq.RequestActions = reqaction;
                // end action

                string s = MakeCMSURL(_CMS_baseURL, "Revoke");

                retval = _CMS.RevokeCard(MakeCMSURL(_CMS_baseURL, "Revoke"), rvkreq);
                if (retval != null)
                {
                    log(">>> RevokeCard success");
                    log("RevokeCard Response: " + Environment.NewLine + retval.ToString());
                }
                else
                {
                    log(">>> RevokeCard failed!");
                }
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Sync's CMS badge provisioning data by...
        /// a) Requesting badge provisioning data from GCR
        /// b) Updates CMS card provisioning data with data from (a)
        /// c) Posts card provisioning data to Picture Perfect database
        /// Item 12
        /// </summary>
        public bool CMSSyncProvisioningDataByBOAABadgeID(int boaaBadgeID)
        {
            string name = "[CMSSyncProvisioningDataByBOAABadgeID]";
            log(name + " (BOAA badge id: " + boaaBadgeID + ")");

            bool retval = false;

            try
            {
                // CMSSyncProvisioningDataByBOAABadgeID makes an additional call to CMS to
                // retrieve the iws card id. if you have the iws card id call
                // CMSSyncProvisioningData

                int iwsCardID = CMSGetIWSCardIDfromBOAABadgeID(boaaBadgeID);

                // uh-oh
                if (iwsCardID == -1)
                {
                    log(name + ": Failed to retrieve card id for BOAA badge id: " + boaaBadgeID.ToString());
                    return retval;
                }

                retval = CMSSyncProvisioningData(iwsCardID);
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Sync's CMS badge provisioning data by...
        /// a) Requesting badge provisioning data from GCR
        /// b) Updates CMS card provisioning data with data from (a)
        /// c) Posts card provisioning data to Picture Perfect database
        /// Item 12 - use this method if you have the iws card id
        /// </summary>
        public bool CMSSyncProvisioningData(int iwsCardID)
        {
            string name = "[CMSSyncProvisioningData]";
            log(name + " (IWS card id: " + iwsCardID + ")");

            bool retval = false;

            try
            {
                isblib.CMSCardUpdateRequest updatereq = new isblib.CMSCardUpdateRequest();

                // update by CMS card ID
                updatereq.CardID = iwsCardID;

                // add the action to retrieve door info, etc
                // begin action
                // add the action that will return additional data about the card holder
                isblib.RequestActions reqaction = new isblib.RequestActions();
                isblib.RequestAction action = new isblib.RequestAction();

                reqaction.Name = "TestAction";
                reqaction.Type = "Sample";

                action.Action = "com.iwsinc.lawa.cms.ProvisionedByCardAction";
                action.Event = isblib.RequestAction.enEventType.enActionSuccess;

                reqaction.Action.Add(action);

                // must set IsValid to true or action will NOT be passed to underlying web service
                reqaction.IsValid = true;

                updatereq.RequestActions = reqaction;

                // end action

                int resp = _CMS.UpdateCard(MakeCMSURL(_CMS_baseURL, "UpdateCard"), updatereq);

                log(name + " response: " + resp);

                retval = resp == 1;
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Return all doors provisioned for this badgeID
        /// Item 14 in New IDSB Interface Requirement Definition v0.85.xlsx
        /// </summary>
        public List<CardProvisionData> CMSGetProvisioningDataByBOAABadgeID(int boaaBadgeID)
        {
            string name = "CMSGetProvisioningDataByBOAABadgeID";
            List<CardProvisionData> retval = new List<CardProvisionData>();

            try
            {
                // CMSGetProvisioningDataByBOAABadgeID makes an additional call to CMS to
                // retrieve the iws card id. if you have the iws card id call
                // CMSGetProvisioningDataByIWSCardID

                int iwsCardID = CMSGetIWSCardIDfromBOAABadgeID(boaaBadgeID);

                // uh-oh
                if (iwsCardID == -1)
                {
                    log(name + ": Failed to retrieve card id for BOAA badge id: " + boaaBadgeID.ToString());
                    return retval;
                }

                log(name + ": BOAA badge id: " + boaaBadgeID.ToString() + " is mapped to IWS Card ID: " + iwsCardID);

                retval = CMSGetProvisioningData(iwsCardID);
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Return a list of CardProvisionData objects
        /// Item 14. Call if you have the iws card id
        /// </summary>
        public List<CardProvisionData> CMSGetProvisioningData(int iwsCardID)
        {
            string name = "[CMSGetProvisioningData]";
            List<CardProvisionData> retval = new List<CardProvisionData>();

            try
            {
                isblib.CMSSearchRequest searchreq = new isblib.CMSSearchRequest();
                isblib.CMSSearchResponse searchrsp = null;

                searchreq.CardID = iwsCardID;
                searchrsp = _CMS.Search(MakeCMSURL(_CMS_baseURL, "Query"), searchreq);

                if (searchrsp != null)
                {
                    // the search returns card meta data
                    log("[Search Response]:\r\n" + searchrsp.ToString());

                    isblib.CMSCardData cardData = (isblib.CMSCardData)searchrsp.CardData[0];

                    if (cardData != null)
                    {
                        retval = GetCardProvisionData(cardData.CardDataField);
                    }
                }
                else
                {
                    log("SearchCards returned no matches");
                }
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Original entry point for item 14- matches spec
        /// </summary>
        public List<CardProvisionData> GetProvisioningAuditData(int CMS_CardID, int BOAA_BadgeID)
        {
            return CMSGetProvisioningDataByBOAABadgeID(BOAA_BadgeID);
        }

        /// <summary>
        /// Item 14 utility function
        /// </summary>
        internal List<CardProvisionData> GetCardProvisionData(ArrayList cardData)
        {
            List<CardProvisionData> retval = new List<CardProvisionData>();

            // provision field names and values in cms
            // P13_AccessType13, 68
            // P13_CategoryID, 60
            // P13_CategoryName13, TBIT-GENERAL AOA
            // P13_WhenBecomesActive13, 2001-04-26-07:00
            // P13_WhenExpires13, 2003-04-26-07:00

            // provision fields always have 'PPxx_' prefix
            Regex rgx = new Regex(@"^[pP]{2}\d{2}_");			// modified to support new PP prefixes.
            // crop out the field name
            Regex rgx1 = new Regex(@"^[a-zA-Z]*");
            bool bAddToList = false;

            List<CardProvisionData> tempList = new List<CardProvisionData>();

            // add the card ids
            foreach (isblib.CMSField cf in cardData)
            {
                if (rgx.IsMatch(cf.Name))
                {
                    int pos = Convert.ToInt32(cf.Name.Substring(2, 2));
                    CardProvisionData cpdTemp = null;

                    foreach (CardProvisionData cp in tempList)
                    {
                        if (cp.Position == pos)
                        {
                            cpdTemp = cp;
                            break;
                        }
                    }

                    bAddToList = false;

                    if (cpdTemp == null)
                    {
                        cpdTemp = new CardProvisionData();
                        cpdTemp.Position = pos;
                        bAddToList = true;
                    }

                    // move past the prefix
                    string fieldName = cf.Name.Substring(5);

                    // the fieldname could have some digits at the end, chop them off
                    fieldName = rgx1.Match(fieldName).Value;

                    switch (fieldName.ToLower())
                    {
                        case "accesstype":
                            cpdTemp.AccessType = MakeInt(cf.Value, -1);
                            break;

                        case "categoryid":
                            cpdTemp.CategoryID = MakeInt(cf.Value, -1);
                            break;

                        case "categoryname":
                            cpdTemp.CategoryName = cf.Value;
                            break;

                        case "whenbecomesactive":
                            cpdTemp.ActiveDate = MakeDate(cf.Value);
                            break;

                        case "whenexpires":
                            cpdTemp.ExpireDate = MakeDate(cf.Value);
                            break;
                    }

                    if (bAddToList)
                    {
                        tempList.Add(cpdTemp);
                    }
                }

                retval = tempList;
            }

            return retval;
        }

        /// <summary>
        /// Returns person info based on CMS_CardID
        /// Item 16 in New IDSB Interface Requirement Definition v0.85.xlsx
        /// </summary>
        public IBMAPerson GetPersonInfoForBadge(int BOAA_BadgeID)
        {
            string name = "[GetPersonInfoForBadge]";
            IBMAPerson retval = null;

            log(name + "(" + BOAA_BadgeID + ")");

            try
            {
                isblib.CMSSearchRequest searchreq = new isblib.CMSSearchRequest();
                isblib.CMSSearchResponse searchrsp = null;

                // 10.24.2012 - boaa badge id no longer stored as "BOAABadgeID" card text data field,
                // now stored as a external system ID
                searchreq.ExternalCardID = new isblib.CMSExternalSystemID(BOAA_SYSTEM_ID, BOAA_BadgeID.ToString());

                // begin action
                // add the action that will return additional data about the card holder
                isblib.RequestActions reqaction = new isblib.RequestActions();
                isblib.RequestAction action = new isblib.RequestAction();

                reqaction.Name = "TestAction";
                reqaction.Type = "Sample";

                action.Action = "com.iwsinc.lawa.cms.QueryByBOAABadgeIDAction";
                action.Event = isblib.RequestAction.enEventType.enActionSuccess;

                reqaction.Action.Add(action);

                // must set IsValid to true or action will NOT be passed to underlying web service
                reqaction.IsValid = true;

                searchreq.RequestActions = reqaction;

                // end action

                searchrsp = _CMS.Search(MakeCMSURL(_CMS_baseURL, "Query"), searchreq);

                if (searchrsp != null)
                {
                    // the search returns card meta data
                    log("[Search Response]:\r\n" + searchrsp.ToString());

                    retval = new IBMAPerson();

                    // add the card ids, should only be 1 though
                    foreach (isblib.CMSCardData cd in searchrsp.CardData)
                    {
                        if (cd.CardID > 0)
                            retval.IBMACardID.Add(cd.CardID);
                    }

                    // see if the search returned info about the card holder
                    if (searchrsp.IsCardHolderDataValid)
                    {
                        isblib.CMSCardHolder ch = (isblib.CMSCardHolder)searchrsp.CardHolderData[0];

                        // we can retrieve the person data from IDMS using the
                        // card holder record id
                        if (ch.ExternalCardHolderID.Count > 0)
                        {
                            isblib.CMSExternalSystemID sysid = (isblib.CMSExternalSystemID)ch.ExternalCardHolderID[0];

                            log("External system id: " + sysid.ToString() + "\r\n");

                            // should be our person guid in idms
                            if (sysid.RecordID != "")
                            {
                                isblib.IDMSPerson pers = new isblib.IDMSPerson();

                                // set the guid we will search on
                                pers.GUID = sysid.RecordID;

                                // do the search in IDMS
                                pers = _IDMS.GetPerson(pers);

                                if (pers != null)
                                {
                                    // got the person data
                                    log("Person found:\r\n" + pers.ToString());

                                    // return the info
                                    retval.Person = pers;
                                }
                                else
                                {
                                    log(string.Format("Person {0} not found", sysid.RecordID));
                                }
                            }
                            else
                            {
                                log("Card Holder missing Record ID");
                            }
                        }
                        else
                        {
                            log("No CardHolder data returned from search");
                        }
                    }
                }
                else
                {
                    log("SearchCards returned no matches");
                }
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);

                //throw ex;
            }

            return retval;
        }

        /// <summary>
        /// Return a list of CMS_CardIDs that represent the set of
        /// all cards associated with this person in CMS
        /// Item 17 in New IDSB Interface Requirement Definition v0.85.xlsx
        /// </summary>
        public IBMAPerson GetCardIDsForPerson(string personGUID)
        {
            string name = "[GetCardIDsForPerson]";
            IBMAPerson retval = null;

            log(name + "(" + personGUID + ")");

            try
            {
                isblib.CMSSearchRequest searchreq = new isblib.CMSSearchRequest();
                isblib.CMSSearchResponse searchrsp = null;

                // search by card holder Person GUID
                searchreq.ExternalCardHolderID = new isblib.CMSExternalSystemID(BOAA_SYSTEM_ID, personGUID);

                // begin action
                // add the action that will return additional data about the card holder
                isblib.RequestActions reqaction = new isblib.RequestActions();
                isblib.RequestAction action = new isblib.RequestAction();

                reqaction.Name = "TestAction";
                reqaction.Type = "Sample";

                action.Action = "com.iwsinc.lawa.cms.QueryByPersonGUIDAction";
                action.Event = isblib.RequestAction.enEventType.enActionSuccess;

                reqaction.Action.Add(action);

                // must set IsValid to true or action will NOT be passed to underlying web service
                reqaction.IsValid = true;

                searchreq.RequestActions = reqaction;

                // end action

                searchrsp = _CMS.Search(MakeCMSURL(_CMS_baseURL, "Query"), searchreq);

                if (searchrsp != null)
                {
                    // the search returns card meta data
                    log("[Search Response]:\r\n" + searchrsp.ToString());

                    retval = new IBMAPerson();

                    // add the card ids
                    foreach (isblib.CMSCardData cd in searchrsp.CardData)
                    {
                        if (cd.CardID > 0)
                        {
                            retval.BOAACardID.Add(cd.CardID);
                        }
                    }
                }
                else
                {
                    log("SearchCards returned no matches");
                }
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);

                //throw ex;
            }
            return retval;
        }

        /// <summary>
        /// Return a list of all documents associated with this person in Document Manager
        /// Item 18 in New IDSB Interface Requirement Definition v0.85.xlsx
        /// </summary>
        public List<isblib.DMDocumentInfo> GetPersonDocuments(string personGUID)
        {
            string name = "[GetPersonDocuments]";

            List<isblib.DMDocumentInfo> retval = new List<isblib.DMDocumentInfo>();
            isblib.ISBSearch query = new isblib.ISBSearch();

            // create the criteria
            query.FilterCriteriaSearch = new isblib.SearchCriteria("DOCUMENT_FILTER!PERSON_GUID", personGUID, isblib.enValueType.vtString, isblib.SearchCompareOp.EQ);

            // set the search type, this search contains a single criteria which allows us
            // to use a simple criteria search type
            query.FilterSearchType = isblib.SearchType.enSTCriteria;

            List<isblib.DMDocumentInfo> results = null;
            try
            {
                // execute the search, returns doc info but no document data
                results = _docMgr.SearchDocuments(query);

                if (results != null)
                    retval = results;
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);

                //throw ex;
            }

            return retval;
        }

        /// <summary>
        /// Return all Person related information for this person GUID
        /// Item 19 in New IDSB Interface Requirement Definition v0.85.xlsx
        /// </summary>
        public isblib.IDMSPerson GetPerson(string personGUID)
        {
            string name = "[GetPerson]";
            isblib.IDMSPerson retval = null;

            // ISBLibrary error conditions are thrown as exceptions
            try
            {
                log(string.Format("Getting person: {0}", personGUID));

                isblib.IDMSPerson pers = new isblib.IDMSPerson();

                // set the GUID field
                pers.GUID = personGUID;

                // returns all person data and associated meta data
                pers = _IDMS.GetPerson(pers);
                if (pers != null)
                {
                    log("Person retrieved");
                    retval = pers; // successfully retrieved
                }
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }
            return retval;
        }

        /// <summary>
        /// Return all non-biometric documents for this person
        /// Item 20 in New IDSB Interface Requirement Definition v0.85.xlsx
        /// </summary>
        public List<isblib.DMDocument> GetNonBiometricDocuments(string personGUID)
        {
            List<isblib.DMDocument> ret = new List<isblib.DMDocument>();
            List<isblib.DMDocument> l = new List<isblib.DMDocument>();

            int[] nonBiometricTypes = new int[] { 1, 3, 5, 12 };

            foreach (int nbt in nonBiometricTypes)
            {
                l = GetDocument(personGUID, nbt);
                foreach (isblib.DMDocument d in l)
                {
                    ret.Add(d);
                }
            }
            return ret;
        }

        public bool UpdatePersonAppearance(string personGUID, int newHeight, int newWeight)
        {
            bool retVal = false;

            // ISBLibrary error conditions are thrown as exceptions
            try
            {
                isblib.IDMSPerson pers = new isblib.IDMSPerson();

                // set the GUID field
                pers.GUID = personGUID;

                // retrieve record to get the appearance ID
                pers = _IDMS.GetPerson(pers);
                if (pers != null)
                {
                    Debug.WriteLine("GetPerson: \r\n" + pers.ToString());

                    if (pers.Appearance.Count == 0)
                    {
                        isblib.IDMSAppearance appr = new isblib.IDMSAppearance();
                        appr.Height = newHeight;
                        appr.Weight = newWeight;

                        // doesn't exist, add it
                        pers.Appearance.Add(appr);
                        pers = _IDMS.AddPersonData(pers, isblib.PersonDataFlag.enAppearance);
                    }
                    else
                    {
                        isblib.IDMSAppearance appr = (isblib.IDMSAppearance)pers.Appearance[0];
                        appr.Weight = newWeight;
                        appr.Height = newHeight;

                        // edit
                        pers = _IDMS.UpdatePersonData(pers, isblib.PersonDataFlag.enAppearance);
                    }
                    retVal = true; // successfully updated
                }
            }
            catch (isblib.ISBException ex)
            {
                Debug.WriteLine("[UpdatePersonAppearance] Exception: " + ex.Message);
            }
            return retVal;
        }

        public bool UpdatePerson(string personGUID, List<string> PersonData, List<string> BadgeData, string accessLevel, bool sendUpdateToTSC = true)
        {
            bool retVal = false;
            bool proceed = false;

            //PersonData list
            //0 LastName
            //1 FirstName
            //2 MiddleName
            //3 SexCode
            //4 DateOfBirth
            //5 Address
            //6 ApartmentNumber
            //7 City
            //8 PostalCode
            //9 CountryCode
            //10 CountrySubdivisionCode
            //11 HomePhoneNumber
            //12 Aliases[0].FirstName
            //13 Aliases[0].LastName
            //14 Aliases[0].MiddleName
            //15 SocialSecurityNumber
            //16 CountryCode_Birth
            //17 CountryCode_Citizenship

            //DMB 3-17-2014 TFS:24310 Added ARN HERE
            //18 staDocs.Alien.AlienRegistrationNumber
            // 18 - 14 are now one number higher each.  
            //18 StaDocs.Non_Immigrant.I94Number
            //19 StaDocs.Non_Immigrant.VisaControlNumber
            //20 StaDocs.Non_Immigrant.PassportNumber
            //21 StaDocs.Non_Immigrant.CountryIssued
            //22 Certificate of Birth
            //23 Employer //Company name
            //24 Suffix //apparently they just made this mandatory -as METADATA!. Why?

            //BadgeData
            //0 badgeNumber (NOT THE IWS Badge ID, at least as of 2014-07-06!)
            //1 (Active if via Maintenance,  Revoked if via Invalidate)
            //2 ReasonForDeactivation 
            //3 AccessLevel (SIDA, Sterile)  this can not be changed, so pass back their existing value if exists
            //4 LocalBadgeType color //use AccessLevel for now


            //NOTE: ONLY TRANSMIT TO IDMS IF AT LEAST ONE OF THESE FIELDS OF INTEREST HAS CHANGED!
            //proceed = true;
            //TODO: test this Logical and add to BadgeData section also!

			string message = "[UpdatePerson] for PersonGUID: " + personGUID + " starting.";

			if (PersonData.Count > 0)
				message += " Name: " + PersonData[1].ToString() + " " + PersonData[0].ToString() + ".";

			log(message);

            // ISBLibrary error conditions are thrown as exceptions
            try
            {
                isblib.IDMSPerson pers = new isblib.IDMSPerson();

                // set the GUID field
                pers.GUID = personGUID;

                // retrieve record to get the appearance ID
                pers = _IDMS.GetPerson(pers);
                if (pers != null)
                {
					if (pers.LastUpdatedBy == null)
						pers.LastUpdatedBy = "otis"; // required

                    //Debug.WriteLine("GetPerson: \r\n" + pers.ToString());

                    //PERSON DATA
                    if (PersonData.Count > 0)
                    {

                        //Update Person details
                        if (pers.LastName != PersonData[0].ToString()) { proceed = true; };
                        if (pers.FirstName != PersonData[1].ToString()) { proceed = true; };
                        if (nullToEmptyString(pers.MiddleName) != PersonData[2].ToString()) { proceed = true; };
                        if (pers.Sex != PersonData[3].ToString()) { proceed = true; };
                        if (pers.DOB.Substring(10, 0) != PersonData[4].ToString().Substring(10, 0)) { proceed = true; };

                        pers.LastName = PersonData[0].ToString();
                        pers.FirstName = PersonData[1].ToString();
                        pers.MiddleName = PersonData[2].ToString();                        
                        
                        // workaround for issue with IWS middle name field, which is required and does not accept empty strings.                     
                        string DojPersonMiddleName = (pers.MiddleName.Length == 0 ? " " : pers.MiddleName);
                        pers.MetaData.SetValue("DojPersonMiddleName", DojPersonMiddleName, isblib.enValueType.vtString);

                        pers.Sex = PersonData[3].ToString();
                        pers.DOB = PersonData[4].ToString();  // format 1972-03-29-08:00


                        //update MetaData

                        // GetMetaField returns a reference to the actual metafield from the list so updates to the object are reflected in the list

                        //SSN
                        isblib.NameValueSequence ssn = GetMetaField(pers.MetaData, "ssn");

						var social = PersonData[15].ToString().Replace("-", "");

						if (social.Substring(0, 4) != "9999")	// ignore any socials coming in that start with 9999
						{
							if (ssn != null)
							{
								if (ssn.Value != social) { proceed = true; };
								ssn.Value = social;
							}
							else
							{
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "SSN", social, isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
								proceed = true;
							}
						}
						else
						{
							// Remove previous social since we have a junky one now.
							if (ssn != null)
								pers.MetaData.Remove(ssn);
						}

						//BirthPlaceCode
						isblib.NameValueSequence BPC = GetMetaField(pers.MetaData, "BirthPlaceCode");
						if (BPC != null)
						{
							if (BPC.Value != PersonData[16].ToString()) { proceed = true; }
							BPC.Value = PersonData[16].ToString();
						}
						else
						{
							isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "BirthPlaceCode", PersonData[16].ToString(), isblib.enValueType.vtString);
							pers.MetaData.Add(nvs);
							proceed = true;
						}

						// CountryCode_Birth
						isblib.NameValueSequence CCB = GetMetaField(pers.MetaData, "CountryCode_Birth");
						if (CCB != null)
						{
							if (CCB.Value != PersonData[16].ToString()) { proceed = true; }
							CCB.Value = PersonData[16].ToString();
						}
						else
						{
							isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "CountryCode_Birth", PersonData[16].ToString(), isblib.enValueType.vtString);
							pers.MetaData.Add(nvs);
							proceed = true;
						}

                        // CountryCode_Citizenship
                        isblib.NameValueSequence CCC = GetMetaField(pers.MetaData, "CountryCode_Citizenship");
                        if (CCC != null)
                        {
                            if (CCC.Value != PersonData[17].ToString()) { proceed = true; };
                            CCC.Value = PersonData[17].ToString();
                        }
                        else
                        {
                            isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "CountryCode_Citizenship", PersonData[17].ToString(), isblib.enValueType.vtString);
                            pers.MetaData.Add(nvs);
                            proceed = true;
                        }

						// AlienRegistrationNumber
						isblib.NameValueSequence ARN = GetMetaField(pers.MetaData, "AlienRegistrationNumber");

						if (PersonData[18] != null && PersonData[18].ToString() != "")
						{
							if (ARN != null)
							{
								//ARN.Value = PersonData[18].ToString();
								pers.MetaData.Remove(ARN);
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "AlienRegistrationNumber", PersonData[18].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}
							else
							{
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "AlienRegistrationNumber", PersonData[18].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}

							proceed = true;
						}
						else
						{
							if (ARN != null)
								pers.MetaData.Remove(ARN);
						}

						// I94ArrivalDepartureFormNumber
						isblib.NameValueSequence ADFN = GetMetaField(pers.MetaData, "I94ArrivalDepartureFormNumber");

                        if (PersonData[19] != null && PersonData[19].ToString() != "")
                        {
							if (ADFN != null)
							{
								//ADFN.Value = PersonData[19].ToString();
								pers.MetaData.Remove(ADFN);
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "I94ArrivalDepartureFormNumber", PersonData[19].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}
							else
							{
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "I94ArrivalDepartureFormNumber", PersonData[19].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}

							proceed = true;
                        }
						else
						{
							if (ADFN != null)
								pers.MetaData.Remove(ADFN);
						}

						// NonImmigrantVISANumber
						isblib.NameValueSequence NIVN = GetMetaField(pers.MetaData, "NonImmigrantVISANumber");

                        if (PersonData[20] != null && PersonData[20].ToString() != "")
                        {
							if (NIVN != null)
							{
								//NIVN.Value = PersonData[20].ToString();
								pers.MetaData.Remove(NIVN);
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "NonImmigrantVISANumber", PersonData[20].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}
							else
							{
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "NonImmigrantVISANumber", PersonData[20].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}

							proceed = true;
                        }
						else
						{
							if (NIVN != null)
								pers.MetaData.Remove(NIVN);
						}
						// PassportNumber and PassportIssuingCountry
						isblib.NameValueSequence PN = GetMetaField(pers.MetaData, "PassportNumber");
						isblib.NameValueSequence PIC = GetMetaField(pers.MetaData, "PassportIssuingCountry");

                        if (PersonData[21] != null && PersonData[22] != null && PersonData[21].ToString() != "" && PersonData[22].ToString() != "")
                        {
                            //PassportNumber                            
							if (PN != null)
							{
								//PN.Value = PersonData[21].ToString();
								pers.MetaData.Remove(PN);
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "PassportNumber", PersonData[21].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}
							else
							{
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "PassportNumber", PersonData[21].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}

                            //PassportIssuingCountry                            
							if (PIC != null)
							{
								//PIC.Value = PersonData[22].ToString();
								pers.MetaData.Remove(PIC);
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "PassportIssuingCountry", PersonData[22].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}
							else
							{
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "PassportIssuingCountry", PersonData[22].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}

							proceed = true;
                        }
						else
						{
							if (PN != null)
								pers.MetaData.Remove(PN);

							if (PIC != null)
								pers.MetaData.Remove(PIC);
						}

						isblib.NameValueSequence COB = GetMetaField(pers.MetaData, "DS1350CertificationOfBirthAbroadNumber");

                        if (PersonData[23] != null && PersonData[23].ToString() != "")
                        {
							if (COB != null)
							{
								//COB.Value = PersonData[23].ToString();
								pers.MetaData.Remove(COB);
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "DS1350CertificationOfBirthAbroadNumber", PersonData[23].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}
							else
							{
								isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "DS1350CertificationOfBirthAbroadNumber", PersonData[23].ToString(), isblib.enValueType.vtString);
								pers.MetaData.Add(nvs);
							}

							proceed = true;
                        }
						else
						{
							if (COB != null)
								pers.MetaData.Remove(COB);
						}

                        //Employer
                        if (PersonData[24] != null && PersonData[24].ToString() != "")
                        {
                            isblib.NameValueSequence EMP = GetMetaField(pers.MetaData, "Employer");
                            if (EMP != null)
                            {
                                if (EMP.Value != PersonData[24].ToString()) { proceed = true; };
                                EMP.Value = PersonData[24].ToString();
                            }
                            else
                            {
                                isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "Employer", PersonData[24].ToString(), isblib.enValueType.vtString);
                                pers.MetaData.Add(nvs);
                                proceed = true;
                            }
                        }

                        string suffix = PersonData[25].ToString(); //Suffix Mandatory??
                        if (PersonData[25].ToString() == "") { suffix = " "; }

                        pers.MetaData.SetValue("DojPersonSuffixName", suffix, isblib.enValueType.vtString);
                        
                        isblib.NameValueSequence SFX = GetMetaField(pers.MetaData, "Suffix");
                        if (SFX != null)
                        {
                            if (SFX.Value != suffix) { proceed = true; };
                            SFX.Value = suffix;
                        }
                        else
                        {
                            isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "Suffix", suffix, isblib.enValueType.vtString);
                            pers.MetaData.Add(nvs);
                            proceed = true;
                        }
                        //}

                        //Update Person Address
                        if (pers.Address.Count == 0)
                        {
                            isblib.IDMSAddress Adds = new isblib.IDMSAddress();
                            Adds.Street1 = PersonData[5].ToString();
                            Adds.Street2 = PersonData[6].ToString();
                            Adds.City = PersonData[7].ToString();
                            Adds.ZipCode = PersonData[8].ToString();
                            Adds.Country = PersonData[9].ToString();
                            Adds.State = PersonData[10].ToString();
                            isblib.NameValueSequence HP = new isblib.NameValueSequence(0, "HomePhone", PersonData[11].ToString(), isblib.enValueType.vtString);
                            if (!Adds.MetaData.Contains(HP))
                                Adds.MetaData.Add(HP);

                            // doesn't exist, add it
                            pers.Address.Add(Adds);
                            proceed = true;

                        }
                        else
                        {
                            isblib.IDMSAddress Adds = (isblib.IDMSAddress)pers.Address[0];
                            if (Adds.Street2 != PersonData[5].ToString() || Adds.Street1 != PersonData[6].ToString() || Adds.City != PersonData[7].ToString() || Adds.ZipCode != PersonData[8].ToString() || Adds.Country != PersonData[9].ToString() || Adds.State != PersonData[10].ToString())
                            {
                                proceed = true;
                            };

                            Adds.TypeCode = "Home";
                            Adds.Street2 = PersonData[5].ToString();
                            Adds.Street1 = PersonData[6].ToString();
                            Adds.City = PersonData[7].ToString();
                            Adds.ZipCode = PersonData[8].ToString();
                            Adds.Country = PersonData[9].ToString();
                            Adds.State = PersonData[10].ToString();
                            isblib.NameValueSequence HP = new isblib.NameValueSequence(0, "HomePhone", PersonData[11].ToString(), isblib.enValueType.vtString);
                            Adds.MetaData.Clear();
                            Adds.MetaData.Add(HP);
                        }

                        pers.Aliases.Clear();
                        //Update Person Alias name
                        if (PersonData[12].ToString() != "" && PersonData[13].ToString() != "")
                        {
                            proceed = true; // TODO: Check if aliases actually changed.
                            //parallel arrays for each name portion
                            string[] FirstNames = PersonData[12].Split('|');
                            string[] LastNames = PersonData[13].Split('|');
                            string[] MiddleNames = PersonData[14].Split('|');

                            if ((FirstNames.Length != MiddleNames.Length) || (MiddleNames.Length != LastNames.Length))
                            {
                                throw new Exception("Aliases are in an incorrect format - they must all be pipe-delimited to the same amount of elements.");
                            }
                            
                            for(int i = 0; i < FirstNames.Length; i++)
                            {
                                var alias = new isblib.IDMSAlias();                                
                                alias.FirstName = FirstNames[i];
                                alias.LastName = LastNames[i];
                                alias.MiddleName = MiddleNames[i];
                                pers.Aliases.Add(alias);
                            }
                        }

                    } //PersonData.Count > 0 


                    if (BadgeData.Count > 0)
                    {
                        //map BOAA Revoke reason to IWS ReasonForDeactivation
                        string IWSReasonForDeactivation = MapIWSReasonForDeactivation(BadgeData[2].ToString());

                        //Update Badge Document
                        int i = -1;
                        if (pers.Document.Count > 0)
                        {
                            i = FindIDMSpersonDocumentIndex(pers, BadgeData[0].ToString()); //examine all Documents to see if this Badge exists:
                        }

                        if (i == -1)
                        { // Add it : even if not Active so IDMS can send to TSC: 

                            isblib.IDMSDocument Badge = new isblib.IDMSDocument();
                            Badge.Number = BadgeData[0].ToString();
                            Badge.TypeCode = accessLevel; //or use PersonData[25].ToString(); //AccessLevel

                            //Badge.MetaData.Clear();
                            if (BadgeData[3].ToString() != "")
                            {
                                isblib.NameValueSequence AL = new isblib.NameValueSequence(0, "AccessLevel", BadgeData[3].ToString(), isblib.enValueType.vtString);
                                Badge.MetaData.Add(AL);
                            }

                            if (BadgeData[1].ToString() != "")
                            {
                                isblib.NameValueSequence STAT = new isblib.NameValueSequence(0, "BadgeStatus", BadgeData[1].ToString(), isblib.enValueType.vtString);
                                Badge.MetaData.Add(STAT);
                            }

                            if (BadgeData[2].ToString() != "")
                            {
                                isblib.NameValueSequence DEAC = new isblib.NameValueSequence(0, "ReasonForDeactivation", IWSReasonForDeactivation, isblib.enValueType.vtString);
                                Badge.MetaData.Add(DEAC);
                            }

                            if (BadgeData.Count > 0)
                            Badge.MetaData.SetValue("employerName", BadgeData[5].ToString(), isblib.enValueType.vtString);
                            else
                                Badge.MetaData.SetValue("employerName", "NA", isblib.enValueType.vtString);
                            
                            pers.Document.Add(Badge);
                            proceed = true;
                        }
                        else
                        { //instantiate existing BadgeDoc
                            isblib.IDMSDocument Badge = (isblib.IDMSDocument)pers.Document[i];


                            // Update status
                            if (BadgeData[1].ToString() != "")
                            {
                                isblib.NameValueSequence STAT = GetMetaField(Badge.MetaData, "BadgeStatus");
                                if (STAT != null)
                                {
                                    if (STAT.Value != BadgeData[1].ToString())
                                    {
                                        STAT.Value = BadgeData[1].ToString();
                                        proceed = true;
                                    }
                                }
                                else
                                {
                                    isblib.NameValueSequence newSTAT = new isblib.NameValueSequence(-1, "BadgeStatus", BadgeData[1].ToString(), isblib.enValueType.vtString);
                                    Badge.MetaData.Add(newSTAT);
                                    proceed = true;
                                }
                            }

                            //Access Level:  this can not be changed as it is an attribute of the Division, so add if missing but do not update: set it to its current value (is mandatory)
                            if (BadgeData[3].ToString() != "")
                            {
                                isblib.NameValueSequence ALVL = GetMetaField(Badge.MetaData, "AccessLevel");
                                if (ALVL != null)
                                { ALVL.Value = ALVL.Value; }
                                else
                                {
                                    isblib.NameValueSequence newALVL = new isblib.NameValueSequence(-1, "AccessLevel", BadgeData[3].ToString(), isblib.enValueType.vtString);
                                    Badge.MetaData.Add(newALVL);
                                    proceed = true;
                                }
                            }

							isblib.NameValueSequence DEAC = GetMetaField(Badge.MetaData, "ReasonForDeactivation");

                            //Reason for Deactivation: only use for Invalidate
							if (IWSReasonForDeactivation != "")
							{
								if (DEAC != null)
								{
									if (DEAC.Value != IWSReasonForDeactivation)
									{
										DEAC.Value = IWSReasonForDeactivation;
										proceed = true;
									}
								}
								else
								{
									isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "ReasonForDeactivation", IWSReasonForDeactivation, isblib.enValueType.vtString);
									Badge.MetaData.Add(nvs);
									proceed = true;
								}
							}
							else
							{
								// We're not revoking, so clear out any old revokations on this badge.
								if (DEAC != null)
									Badge.MetaData.Remove(DEAC);
							}

                            if (BadgeData.Count > 0)
                            Badge.MetaData.SetValue("employerName", BadgeData[5].ToString(), isblib.enValueType.vtString);                            
                            else
                                Badge.MetaData.SetValue("employerName", "NA", isblib.enValueType.vtString);

                        } // new/existing

                        //IWS BUG: SUffix is mandatory, even if only updating the Badge. If null, pass in a 1 char string.
                        isblib.NameValueSequence SFX = GetMetaField(pers.MetaData, "Suffix");
                        if (SFX != null)
                        { SFX.Value = " "; }
                        else
                        {
                            isblib.NameValueSequence nvs = new isblib.NameValueSequence(-1, "Suffix", " ", isblib.enValueType.vtString);
                            pers.MetaData.Add(nvs);
                        }

                    } // BadgeData.Count > 0

					// Make sure there's always a pending badge out there.
                    //int pendingBadgeIndex = -1;
                    //if (pers.Document.Count > 0)
                    //{
                    //    pendingBadgeIndex = FindIDMSpersonDocumentNumberIndex(pers, "0"); //examine all Documents to see if this Badge exists:
                    //}

                    //if (pendingBadgeIndex == -1)	// not found
                    //{
                    //    isblib.IDMSDocument Badge = new isblib.IDMSDocument();
                    //    Badge.Number = "0";
                    //    Badge.TypeCode = accessLevel;

                    //    isblib.NameValueSequence AL = new isblib.NameValueSequence(0, "AccessLevel", accessLevel, isblib.enValueType.vtString);
                    //    Badge.MetaData.Add(AL);

                    //    isblib.NameValueSequence STAT = new isblib.NameValueSequence(0, "BadgeStatus", "Pending", isblib.enValueType.vtString);
                    //    Badge.MetaData.Add(STAT);

                    //    if (BadgeData.Count > 0)
                    //    {
                    //    Badge.MetaData.SetValue("employerName", BadgeData[5].ToString(), isblib.enValueType.vtString);
                    //    }
                    //    else
                    //    {
                    //        Badge.MetaData.SetValue("employerName", "NA", isblib.enValueType.vtString);
                    //    }

                    //    pers.Document.Add(Badge);
                    //    proceed = true;
                    //}
                    //else
                    //{
                    //    isblib.IDMSDocument Badge = (isblib.IDMSDocument)pers.Document[pendingBadgeIndex];

                    //    isblib.NameValueSequence ALVL = GetMetaField(Badge.MetaData, "AccessLevel");
                    //    if (ALVL != null)
                    //    {
                    //        if (ALVL.Value != accessLevel)
                    //        {
                    //            ALVL.Value = ALVL.Value;
                    //            proceed = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        isblib.NameValueSequence newALVL = new isblib.NameValueSequence(-1, "AccessLevel",accessLevel, isblib.enValueType.vtString);
                    //        Badge.MetaData.Add(newALVL);
                    //        proceed = true;
                    //    }

                    //    if (BadgeData.Count > 0) {
                    //        Badge.MetaData.SetValue("employerName", BadgeData[5].ToString(), isblib.enValueType.vtString);
                    //    else
                    //        Badge.MetaData.SetValue("employerName", "NA", isblib.enValueType.vtString);
                    //}

                    if (proceed)
                    {
                        // begin action
                        isblib.RequestActions reqaction = new isblib.RequestActions();
                        isblib.RequestAction action = new isblib.RequestAction();

                        reqaction.Name = "TestAction";
                        reqaction.Type = "Sample";

                        if (sendUpdateToTSC)
                            action.Action = "com.iwsinc.lawa.idms.PerpetualBackgroundCheckByUpdateAction";

                        action.Event = isblib.RequestAction.enEventType.enActionSuccess;

                        reqaction.Action.Add(action);

                        // must set IsValid to true or action will NOT be passed to underlying web service
                        reqaction.IsValid = true;

                        pers.RequestActions = reqaction;
                        // end action
                       
                        //call
                        pers = _IDMS.UpdatePersonData(pers, isblib.PersonDataFlag.enPerson);
                    }

                    retVal = true; // successfully updated
                } //if (pers != null)
            }
            catch (isblib.ISBException ex)
            {
                string name = "[UpdatePerson]";

                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);
            }

            return retVal;
        }

        private string nullToEmptyString(string s)
        {
            if (String.IsNullOrEmpty(s))
                return "";
            else
                return s;

        }

        private isblib.NameValueSequence GetMetaField(System.Collections.ArrayList metaList, string fieldName)
        {
            isblib.NameValueSequence retval = null;

            // if field present in list return it
            foreach (isblib.NameValueSequence nvp in metaList)
            {
                if (string.Compare(nvp.Name, fieldName, true) == 0)
                {
                    retval = nvp;
                    break;
                }
            }

            return retval;
        }

        private int FindIDMSpersonDocumentIndex(isblib.IDMSPerson IDMSperson, string Name)
        {
            int retval = -1;
            for (int i = 0; i <= IDMSperson.Document.Count - 1; i++)
            {
                if (IDMSperson.Document[i].ToString().Contains(Name))
                {
                    retval = i;
                    break;
                }
            }
            return retval;
        }

		private int FindIDMSpersonDocumentNumberIndex(isblib.IDMSPerson IDMSperson, string Name)
		{
			int retval = -1;
			for (int i = 0; i <= IDMSperson.Document.Count - 1; i++)
			{
				if (((isblib.IDMSDocument)IDMSperson.Document[i]).Number == Name)
				{
					retval = i;
					break;
				}
			}
			return retval;
		}

        public bool UpdatePersonStatus(string personGUID, int newStatus)
        {
            bool retVal = false;

            // ISBLibrary error conditions are thrown as exceptions
            try
            {
                isblib.IDMSPerson pers = new isblib.IDMSPerson();

                // it would probably be better to search for the person record
                // to be sure it exists before you try an update, but if you are certain
                // it does exist you can skip the search

                // set the GUID field
                pers.GUID = personGUID;
                pers.LastUpdatedBy = "otis"; // required
                pers.Status = newStatus;
                pers = _IDMS.UpdatePersonData(pers, isblib.PersonDataFlag.enPersonStatus);
                retVal = true; // successfully retrieved
            }
            catch (isblib.ISBException ex)
            {
                log("[UpdatePersonStatus] Exception: " + ex.Message);
            }
            return retVal;
        }

        /// <summary>
        /// Call triggers IWS provisioning updates for all Cards which meet the criteria:
        /// IWS places a CMSSyncProvisioningDataByBOAABadgeID call for every matching Card
        /// </summary>
        public bool CMSBulkProvision(string CompanyDivision, int RoleId)
        {
            string name = "[CMSBulkProvision]";
            bool retval = true;

            log(string.Format("{0} start for CompanyDivision: {1}, JobRoleId: {2}", name, CompanyDivision, RoleId));

            try
            {
                isblib.CMSSearchRequest searchreq = new isblib.CMSSearchRequest();
                isblib.CMSSearchResponse searchrsp = null;

                searchreq.CardFieldValue.Add(new isblib.CMSSearchableField("CompanyDivision", isblib.enCMSDataType.cmsString, CompanyDivision));
                searchreq.CardFieldValue.Add(new isblib.CMSSearchableField("JobRoleID", isblib.enCMSDataType.cmsString, RoleId.ToString()));

                // begin action
                isblib.RequestActions reqaction = new isblib.RequestActions();
                isblib.RequestAction action = new isblib.RequestAction();

                reqaction.Name = "Bulk Reprovisioning";
                reqaction.Type = "LAWA";

                action.Action = "com.iwsinc.lawa.cms.QueryWithBulkUpdate";
                action.Event = isblib.RequestAction.enEventType.enActionSuccess;

                reqaction.Action.Add(action);

                // must set IsValid to true or action will NOT be passed to underlying web service
                reqaction.IsValid = true;

                searchreq.RequestActions = reqaction;
                // end action

                searchrsp = _CMS.Search(MakeCMSURL(_CMS_baseURL, "Query"), searchreq);

                if (searchrsp != null)
                {
                    // the search return
                    log("[CMSBulkProvision Response]:\r\n" + searchrsp.ToString());

                    //?retval = new ??
                    // Do we need to interrogate anything and act on it?
                }
                else
                    log("Bulk Reprovisioning returned no response");
            }
            catch (isblib.ISBException ex)
            {
                log(name + " Exception.Message: " + ex.Message);
                log(name + " Exception.InnerException: " + ex.InnerException);
                log(name + " Exception.Source: " + ex.Source);
                log(name + " Exception.StackTrace: " + ex.StackTrace);

                retval = false;
            }

            log(name + " end");

            return retval;
        }

        /// <summary>
        /// Map BOAA Revoke reason to IWS ReasonForDeactivation
        /// </summary>
        private string MapIWSReasonForDeactivation(string BOAAReasonForDeactivation)
        {
            string IWSReasonForDeactivation = "";
            switch (BOAAReasonForDeactivation)
            {
                case "LOST":
                    IWSReasonForDeactivation = "Lost";
                    break;
                case "LSTLN":
                    IWSReasonForDeactivation = "Lost";
                    break;
                case "STLN":
                    IWSReasonForDeactivation = "Stolen";
                    break;
                case "RTRN":
                    IWSReasonForDeactivation = "Returned";
                    break;
                case "EXPR":
                    IWSReasonForDeactivation = "Expired";
                    break;
                case "":
                    IWSReasonForDeactivation = "";
                    break;
                default:
                    IWSReasonForDeactivation = "Otherwise unaccounted for";
                    break;
            }
            return IWSReasonForDeactivation;
        }

        public string RetransmitTSCTransaction(string TransactionControlNumber)
        {
            string retval = "";

            try
            {
                isblib.RequestActions reqactions = new isblib.RequestActions();
                isblib.RequestAction action = new isblib.RequestAction();
                isblib.EBTSRequest req = new isblib.EBTSRequest();
                string ebtsAction = "ReRun";

                // initialize params
                reqactions.Name = ebtsAction;

                // required params
                reqactions.ActionParameter.Add(new isblib.ParameterType("USER_NAME", Environment.MachineName, isblib.enValueType.vtString));
                reqactions.ActionParameter.Add(new isblib.ParameterType("ORIG_TRANS_GUID", TransactionControlNumber, isblib.enValueType.vtString));

                // flag requestaction data as valid, otherwise it is ignored by isblibrary
                reqactions.IsValid = true;

                req.RequestActions = reqactions;
                // ISBLibrary error conditions are thrown as exceptions

                isblib.EBTSServer _ebtsServer = new isblib.EBTSServer();

                _ebtsServer.URL = _EBTS_baseURL;

                isblib.EBTSResponse rsp = null;
                rsp = _ebtsServer.ReRunAction(req);
                retval = rsp.ToString();


                log("[RetransmitTSCTransaction Response]:\r\n" + rsp.ToString());
            }
            catch (isblib.ISBException ex)
            {
                log("[RetransmitTSCTransaction] Exception: " + ex.Message);
                retval = ex.ToString();

                if (retval.Contains("ISBEBTSServerException"))
                {
                    int i = retval.IndexOf("ErrorMessage:");
                    i = i + 14;
                    int i2 = retval.IndexOf("\r\n ");
                    retval = "Error: " + retval.Substring(i, i2 - i);
                }
                else
                {
                    retval = "Transaction is resubmitted.";
                }
            }

            log("[RetransmitTSCTransaction] Exit");

            return retval;
        }

        public void DeletePersonFromIDMS(string PersonGUID)
        {
            log("[DeletePersonFromIDMS] start");
            log("PersonGUID: " + PersonGUID);

            isblib.IDMSPerson pers = new isblib.IDMSPerson();
            pers.GUID = PersonGUID;
            pers = _IDMS.GetPerson(pers);

            if (pers != null)
            {                
                _IDMS.DeletePerson(pers);
            }
            else
            {
                log(string.Format("Person {0} not found", PersonGUID));
            }
            log("[DeletePersonFromIDMS] Exit");
        }

        public string DeletePersonFromTSC(string PersonGUID, string EmployeeID)
        {
            log("[DeletePersonFromTSC] start");
            log("PersonGUID: " + PersonGUID + "\t" + "EmployeeID: " + EmployeeID);

            var ReqActions = new isblib.RequestActions();
            var ReqAction = new isblib.RequestAction();
            var EBTSRequest = new isblib.EBTSRequest();
            var EBTSServer = new isblib.EBTSServer();
            EBTSServer.URL = this._EBTS_baseURL;

            ReqActions.Name = "Delete";
            ReqActions.Type = "Sample";

            ReqAction.Action = "TscChrcSubmitDeletePerson";
            ReqAction.Parameter.Add(new isblib.ParameterType("USER_NAME", "sa", isblib.enValueType.vtString));
            ReqAction.Parameter.Add(new isblib.ParameterType("TSC_PERSON_ID", EmployeeID, isblib.enValueType.vtInt));
            ReqAction.Parameter.Add(new isblib.ParameterType("PERSON_GUID", PersonGUID, isblib.enValueType.vtString));
            ReqAction.Parameter.Add(new isblib.ParameterType("USER_NAME", "GCR", isblib.enValueType.vtString));
            ReqActions.Action.Add(ReqAction);

            ReqActions.IsValid = true;

            EBTSRequest.RequestActions = ReqActions;
            isblib.EBTSResponse response = EBTSServer.ProcessActions(EBTSRequest);
            log("[DeletePerson] response: " + response == null ? string.Empty : response.ToString());
            if (response == null || response.ResultParameters.Count == 0)
                return string.Empty;
            else
            {
                return (response.ResultParameters[0] as ImageWare.ISBLibrary.ParameterType).Value;
            }
        }

        public bool RevokePendingBadge(string PersonGUID, bool sendUpdateToTSC)
        {
            log("[RevokePendingBadge] Start");
            log("PersonGUID: " + PersonGUID  + ", SendUpdateToTSC:" + sendUpdateToTSC);      
            try
            {
                isblib.IDMSPerson person = this.GetPerson(PersonGUID);
                var Documents = (isblib.IDMSDocument[]) person.Document.ToArray(typeof(isblib.IDMSDocument));
                isblib.IDMSDocument idmsDocument = Documents.FirstOrDefault(x => x.Number == "0");

                if (idmsDocument == null)
                {
                    log("No pending badge found.");                        
                }
                else
                {
                    idmsDocument.MetaData.SetValue("BadgeStatus", "Revoked", isblib.enValueType.vtString);
                    idmsDocument.MetaData.SetValue("ReasonForDeactivation", "Delete Person", isblib.enValueType.vtString);
                    
                    var requestActions = new isblib.RequestActions();
                    var requestAction = new isblib.RequestAction();
                    
                    if (sendUpdateToTSC)
                        requestAction.Action = "com.iwsinc.lawa.idms.PerpetualBackgroundCheckByUpdateAction";
                    
                    requestActions.Name = "TestAction";
                    requestActions.Type = "Sample";
                    
                    requestAction.Event = isblib.RequestAction.enEventType.enActionSuccess;
                    
                    requestActions.Action.Add(requestAction);                    
                    requestActions.IsValid = true;
                    person.RequestActions = requestActions;
                    
                    _IDMS.UpdatePersonData(person, isblib.PersonDataFlag.enPerson);
                }
            }
            catch (Exception ex)
            {
                log("Exception: " + ex.ToString());
                return false;
            }

            log("[RevokePendingBadge] End");
            return true;
        }        
    }
}