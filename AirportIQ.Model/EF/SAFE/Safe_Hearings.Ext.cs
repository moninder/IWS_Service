using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AirportIQ.Model.EF.SAFE
{
	public partial class Safe_Hearings
	{
        public string OfficerSerialNumber { get; set; }

		public static bool Save(ICollection<Safe_Hearings> safeHearings, int safeCitationID)
		{
			bool result;
			try
			{
				using (var context = new SafeEntities())
				{

					#region Deletes
					// get citationID
					int theCitationID = safeCitationID;

					// get list of ids from param: safeHearings
					IEnumerable<int> ids = safeHearings.Select(x => x.SafeHearingID).Distinct();

					// get list of hearings to delete
					var toDeletes = from toDelete in context.Safe_Hearings
													where ids.Contains(toDelete.SafeHearingID) == false
													&& toDelete.SafeCitationID == theCitationID
													select toDelete;

					// delete hearings
					foreach (var item in toDeletes)
					{
						context.Safe_Hearings.Remove(item);
					}

                    context.SaveChanges();

					#endregion
										
					Safe_Hearings theSafeHearing;
					foreach (var hearing in safeHearings)
					{
						if (hearing.SafeHearingID < 1)
						{ // new record
                            hearing.SafeCitationID = theCitationID;
							theSafeHearing = new Safe_Hearings();							
						}
						else
						{ // get record
							var theSafeHearings = from safeHearing in context.Safe_Hearings
																where safeHearing.SafeHearingID == hearing.SafeHearingID
																select safeHearing;

							if (!theSafeHearings.Any())
							{
								continue;
							}
							theSafeHearing = theSafeHearings.FirstOrDefault();
						}

						PropertyInfo[] props = theSafeHearing.GetType().GetProperties();

						Array.ForEach(props, prop =>
						{
							if (!prop.GetGetMethod().IsVirtual)
							{
								var source = prop.GetValue(hearing, null);
								prop.SetValue(theSafeHearing, source, null);
							}
						});

                        var leoOfficer = (from officer in context.LEO_OfficerBadges.Include("Person_FullName")
                                          where officer.SerialNumber == hearing.OfficerSerialNumber
                                            select officer).FirstOrDefault();

                        if (leoOfficer != null)
                        {
                            var safeOfficer = (from officer in context.Safe_Officers
                                               where officer.OfficerBadgeNumber == leoOfficer.SerialNumber
                                               select officer).FirstOrDefault();

                            if (safeOfficer == null)
                            {
                                var newSafeOfficer = new Safe_Officers
                                {
                                    SafeDivisionID = -1,
                                    OfficerName = leoOfficer.Person_FullName.FullName,
                                    OfficerBadgeNumber = leoOfficer.SerialNumber,
                                    IsActive = true
                                };

                                context.Safe_Officers.Add(newSafeOfficer);
                                context.SaveChanges();

                                theSafeHearing.SafeOfficerID_Hearing = newSafeOfficer.SafeOfficerID;
                            }
                            else
                                theSafeHearing.SafeOfficerID_Hearing = safeOfficer.SafeOfficerID;
                        }

                        if (theSafeHearing.SafeHearingID == 0)
                            context.Safe_Hearings.Add(theSafeHearing);	

						context.SaveChanges();
						//SAFE.Safe_HearingPoliceOfficers.Save(hearing.Safe_HearingPoliceOfficers, theSafeHearing.SafeHearingID);

						theCitationID = hearing.SafeCitationID;
					}

					result = true;
				}
			}
			catch (Exception ex)
			{
				result = false;
			}

			return result;
		}
	}
}

