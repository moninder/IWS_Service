using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AirportIQ.Model.EF.SAFE
{
	public partial class Safe_HearingPoliceOfficers
	{
		public static bool Save(ICollection<Safe_HearingPoliceOfficers> safeHearingPoliceOfficers, int safeHearingId)
		{
			bool result;
			try
			{
				using (var context = new SafeEntities())
				{
					#region Remove
					
					if (safeHearingPoliceOfficers.Count() < 1) // no change or all deleted
					{
						var delOfficers = from delOfficer in context.Safe_HearingPoliceOfficers
															where delOfficer.SafeHearingID == safeHearingId
															select delOfficer;

						foreach (var item in delOfficers)
						{
							context.Safe_HearingPoliceOfficers.Remove(item);
						}

					}
					else // posable delete
					{
						var officerBadgeIds = (from officerBadgeId in safeHearingPoliceOfficers
																	 where officerBadgeId.SafeHearingID == safeHearingId
																	 select officerBadgeId.OfficerBadgeID).ToList();

						var delOfficers = from delOfficer in context.Safe_HearingPoliceOfficers
															where officerBadgeIds.Contains(delOfficer.OfficerBadgeID) == false
															&& delOfficer.SafeHearingID == safeHearingId
															select delOfficer;

						foreach (var item in delOfficers)
						{
							context.Safe_HearingPoliceOfficers.Remove(item);
						}

					}
					#endregion Remove

					#region Add

					var newOfficerBadgeIds = (from newOfficerBadgeId in context.Safe_HearingPoliceOfficers
																	 where newOfficerBadgeId.SafeHearingID == safeHearingId
																	 select newOfficerBadgeId.OfficerBadgeID).ToList();																	 
																		 

					var addOfficers = from addOfficer in safeHearingPoliceOfficers
														where newOfficerBadgeIds.Contains(addOfficer.OfficerBadgeID) == false
														select addOfficer;

					foreach (var item in addOfficers)
					{
						Safe_HearingPoliceOfficers newOfficer = new Safe_HearingPoliceOfficers();

						PropertyInfo[] props = item.GetType().GetProperties();

						Array.ForEach(props, prop =>
						{
							if (!prop.GetGetMethod().IsVirtual)
							{
								var source = prop.GetValue(item, null);
								prop.SetValue(newOfficer, source, null);
							}
						});
						context.Safe_HearingPoliceOfficers.Add(newOfficer);
					}

					#endregion Add

					#region Save

					context.SaveChanges();

					#endregion Save
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

