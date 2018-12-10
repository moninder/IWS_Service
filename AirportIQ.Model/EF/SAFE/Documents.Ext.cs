using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AirportIQ.Model.EF.SAFE
{
    public partial class Documents
    {
        public static bool Save(ICollection<Documents> documents, int citationId)
        {
            bool result = false;

            try
            {
                using (var context = new SafeEntities())
                {
                    //var tmp = from c in citationPoliceOfficers
                    //          select c;

                    #region Deletes

                    var ids = from id in documents
                              select new { id.DocumentID };

                    var idList = ids.Select(item => item.DocumentID).ToList();

                    var identificationNumber = "CITATION:" + citationId;

                    var deleteList = from d in context.Documents
                                     where d.DocumentTypeNumber == (short)133 
                                        && d.IdentificationNumber == identificationNumber
                                        && !idList.Contains(d.DocumentID)
                                     select d;

                    foreach (var item in deleteList)
                        context.Documents.Remove(item);

                    #endregion

                    #region Updates

                    var updateList = from d in context.Documents
                                     where d.DocumentTypeNumber == (short)133
                                        && d.IdentificationNumber == "CITATION:0"
                                        && idList.Contains(d.DocumentID)
                                     select d;

                    foreach (var item in updateList)
                    {
                        var theItem = item;

                        theItem.IdentificationNumber = "CITATION:" + citationId;
                    }

                    #endregion

                    #region Adds

                    //var adds = from d in context.Documents
                    //           where d.DocumentTypeNumber == (short)133
                    //                && d.IdentificationNumber == "CITATION:0"
                    //           select d;

                    //var addIds = adds.Select(item => item.DocumentID).ToList();

                    //var addRecords = from addRecord in documents
                    //                 where !addIds.Contains(addRecord.DocumentID)
                    //                 select addRecord;

                    //foreach (var item in addRecords)
                    //{
                    //    var theItem = item;
                    //    var newRecord = new Documents();

                    //    PropertyInfo[] props = theItem.GetType().GetProperties();
                    //    Array.ForEach(props, prop =>
                    //    {
                    //        if (!prop.GetGetMethod().IsVirtual)
                    //        {
                    //            var value = prop.GetValue(theItem, null);
                    //            prop.SetValue(newRecord, value, null);
                    //        }
                    //    });

                    //    context.Documents.Add(newRecord);
                    //}

                    #endregion

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                                Trace.TraceInformation(message);
                            }
                        }
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                Exception theException = ex;
                while (theException.InnerException != null)
                {
                    theException = theException.InnerException;
                }
                throw theException;
            }

            return result;
        }
    }
}