using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AirportIQ.Model.EF.SAFE
{
    public partial class Safe_CitationPoliceOfficers
    {
        public static bool Save(ICollection<Safe_CitationPoliceOfficers> citationPoliceOfficers, int safeCitationID)
        {
            bool result = false;

            try
            {
                using (var context = new SafeEntities())
                {

                    #region Get SafeCitationID

                    var tmp = from c in citationPoliceOfficers
                              select c;

                    var citationId = safeCitationID;

                    #endregion

                    #region Get List of items to delete

                    var ids = from id in citationPoliceOfficers
                              select new { id.OfficerBadgeID };

                    var idList = ids.Select(item => item.OfficerBadgeID).ToList();

                    var deleteList = from d in context.Safe_CitationPoliceOfficers
                                     where d.SafeCitationID == citationId
                                                                 && !idList.Contains(d.OfficerBadgeID)
                                     select d;
                    #endregion

                    #region Remove deleted items

                    foreach (var item in deleteList)
                        context.Safe_CitationPoliceOfficers.Remove(item);

                    #endregion

                    #region Update changes

                    var updateList = from d in context.Safe_CitationPoliceOfficers
                                     where d.SafeCitationID == citationId
                                     && idList.Contains(d.OfficerBadgeID)
                                     select d;

                    foreach (var item in updateList)
                    {
                        var theItem = item;

                        // find matching record from param
                        var records = from record in citationPoliceOfficers
                                      where record.OfficerBadgeID == theItem.OfficerBadgeID
                                      select record;

                        Safe_CitationPoliceOfficers sourceRecord = records.FirstOrDefault();

                        PropertyInfo[] props = theItem.GetType().GetProperties();
                        Array.ForEach(props, prop =>
                        {
                            if (!prop.GetGetMethod().IsVirtual && prop.Name != "C_DataChanges_RowID")
                            {
                                var value = prop.GetValue(sourceRecord, null);
                                prop.SetValue(theItem, value, null);
                            }
                        });

                        theItem.SafeCitationID = citationId;
                    }

                    #endregion

                    #region Add new items

                    var adds = from d in context.Safe_CitationPoliceOfficers
                               where d.SafeCitationID == citationId
                               select d;

                    var addIds = adds.Select(item => item.OfficerBadgeID).ToList();

                    var addRecords = from addRecord in citationPoliceOfficers
                                     where !addIds.Contains(addRecord.OfficerBadgeID)
                                     select addRecord;

                    foreach (var item in addRecords)
                    {
                        var theItem = item;
                        var newRecord = new Safe_CitationPoliceOfficers();

                        PropertyInfo[] props = theItem.GetType().GetProperties();
                        Array.ForEach(props, prop =>
                        {
                            if (!prop.GetGetMethod().IsVirtual)
                            {
                                var value = prop.GetValue(theItem, null);
                                prop.SetValue(newRecord, value, null);
                            }
                        });
                        newRecord.SafeCitationID = citationId;
                        context.Safe_CitationPoliceOfficers.Add(newRecord);
                    }

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