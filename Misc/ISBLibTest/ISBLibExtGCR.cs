using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;

using isblib = ImageWare.ISBLibrary;

namespace ISBLibTest
{
  class ISBLibExtGCR
  {
    private isblib.DocumentManager _docMgr = new isblib.DocumentManager();
    private isblib.IDMS _IDMS = new isblib.IDMS();
    private isblib.CMS _CMS = new isblib.CMS();

    public ISBLibExtGCR() 
    {
      // set the urls
      _docMgr.URL = @"http://192.168.15.78:8080/DocumentManagerEJB/DocumentManagerBeanService/DocumentManagerBean";
      _IDMS.URL = @"http://192.168.15.78:8080/IdentityManagerEJB/IdentityManagerBeanService/IdentityManagerBean";
    }

    // GetDocument - Return all documents for this person and document type
    // Item 6 in New IDSB Interface Requirement Definition v0.85.xlsx

    public List<isblib.DMDocument> GetDocument(string personGUID, int docType)
    {
      List<isblib.DMDocument> retval = new List<isblib.DMDocument>();
      isblib.ISBSearch query = new isblib.ISBSearch();

      // there are 2 types of searches, Filter type search which targets core table fields and
      // normal searches which search meta data fields.
      // Document Filter search fields are:
      // DOCUMENT_FILTER!TYPE, DOCUMENT_FILTER!PERSON_GUID, DOCUMENT_FILTER!CREATED, DOCUMENT_FILTER!CREATED_BY, 
      // DOCUMENT_FILTER!LAST_UPDATED, DOCUMENT_FILTER!LAST_UPDATED_BY  
      // All other fields are considered meta data searches. You can search on both Filter and MetaData
      // fields in 1 query.

      // create the criteria
      isblib.SearchCriteria c1 = new isblib.SearchCriteria("DOCUMENT_FILTER!PERSON_GUID", personGUID, isblib.enValueType.vtString, isblib.SearchCompareOp.EQ);
      isblib.SearchCriteria c2 = new isblib.SearchCriteria("DOCUMENT_FILTER!TYPE", docType.ToString(), isblib.enValueType.vtInt, isblib.SearchCompareOp.EQ);

      isblib.SearchConjunction sc = new isblib.SearchConjunction();

      // add criteria to search
      sc.AddCriteria(c1);
      sc.AddCriteria(c2);

      // set the search conjunction logical operator
      sc.LogicalOp = isblib.SearchLogicalOp.AND;

      query.FilterAddConjunction(sc);

      // set the search type
      query.FilterSearchType = isblib.SearchType.enSTConjunction;

      List<isblib.DMDocumentInfo> results = null;
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
        Debug.WriteLine("[GetDocument] Exception: "+ex.InnerException);
      }

      return retval;
    }

    // GetPersonDocuments - Return a list of all documents associated with this person in Document Manager
    // Item 18 in New IDSB Interface Requirement Definition v0.85.xlsx

    public List<isblib.DMDocumentInfo> GetPersonDocuments(string personGUID)
    {
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
        Debug.WriteLine("[GetPersonDocuments] Exception: " + ex.InnerException);
      }

      return retval;
    }

    // GetPerson - Return all Person related information for this person GUID
    // Item 19 in New IDSB Interface Requirement Definition v0.85.xlsx

    public isblib.IDMSPerson GetPerson(string personGUID)
    {
      isblib.IDMSPerson retval = null;

      // ISBLibrary error conditions are thrown as exceptions
      try
      {
        isblib.IDMSPerson pers = new isblib.IDMSPerson();

        // set the GUID field
        pers.GUID = personGUID;

        // returns all person data and associated meta data
        pers = _IDMS.GetPerson(pers);
        if (pers != null)
        {
          Debug.WriteLine("GetPerson: \r\n" + pers.ToString());

          retval = pers; // successfully retrieved
        }
      }
      catch (isblib.ISBException ex)
      {
        Debug.WriteLine("[GetPerson] Exception: " + ex.InnerException);
      }
      return retval;
    }

  }
}
