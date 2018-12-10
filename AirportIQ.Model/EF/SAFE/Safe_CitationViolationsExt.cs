using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace AirportIQ.Model.EF.SAFE
{
	public partial class Safe_CitationViolations
	{
		public static bool Save(ICollection<Safe_CitationViolations> citationViolations, int safeCitationID)
		{
			bool result = false;
			try
			{
				using (var context = new SafeEntities())
				{

					#region Get SafeCitationID

					var tmp = from c in citationViolations
										select c;
					var citationId = safeCitationID;

					#endregion
					
					#region Get List of items to delete
					
					var ids = from id in citationViolations
										select new {id.ViolationID};
					var idList = ids.Select(item => item.ViolationID).ToList();


					var deleteList = from d in context.Safe_CitationViolations
					                 where d.SafeCitationID == citationId
																 && !idList.Contains(d.ViolationID)
					                 select d;
					#endregion


					#region Remove deleted items

					foreach (var item in deleteList)
					{
						context.Safe_CitationViolations.Remove(item);
					}

					#endregion

					#region Update changes

					var updateList = from d in context.Safe_CitationViolations
													 where d.SafeCitationID == citationId
                                                     && idList.Contains(d.ViolationID)
													 select d;


					foreach (var item in updateList)
					{
						var theItem = item;
						// find matching record from param

						var records = from record in citationViolations
													where record.ViolationID == theItem.ViolationID
						              select record;

						Safe_CitationViolations sourceRecord = records.FirstOrDefault();
						
						PropertyInfo[] props = theItem.GetType().GetProperties();
						Array.ForEach(props, prop =>
						{
							if (!prop.GetGetMethod().IsVirtual)
							{
								var value = prop.GetValue(sourceRecord, null);
								prop.SetValue(theItem, value, null);
							}
						});

						theItem.SafeCitationID = citationId;
						

					}

					#endregion

					#region Add new items

					var adds = from d in context.Safe_CitationViolations
													 where d.SafeCitationID == citationId
													 select d;

					var addIds = adds.Select(item => item.ViolationID).ToList();

					var addRecords = from addRecord in citationViolations
					                 where !addIds.Contains(addRecord.ViolationID)
					                 select addRecord;

					foreach (var item in addRecords)
					{
						var theItem = item;
						var newRecord = new Safe_CitationViolations();

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
                        newRecord.Conditions = string.Empty;
						context.Safe_CitationViolations.Add(newRecord);
					}


					#endregion

					#region Save changes
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
					

					#endregion
					

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

		public static bool UpdateOrInsert(int safeCitationID, int violationID, Safe_CitationViolations data)
		{
			bool result;
			try
			{
				using (var context = new SafeEntities())
				{
					var dbRecord = (from r in context.Safe_CitationViolations
					                where r.SafeCitationID == safeCitationID
					                      && r.ViolationID == violationID
					                select r).Single() ?? new Safe_CitationViolations();

					PropertyInfo[] props = data.GetType().GetProperties();

					Array.ForEach(props, prop =>
					{
						var val = prop.GetType().GetProperty(prop.Name).GetValue(data, null);
						prop.SetValue(dbRecord, val, null);
					});

					context.SaveChanges();

				}
				result = true;
			}
			catch (Exception )
			{
				result = false;
			}

			return result;
		}


	}
}

