using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AirportIQ.Model.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AirportIQ.Model.Models.Review.Badge
{
	public class Safe
	{
		#region Constructors and Destructors

		public Safe() { }

		public Safe(DataSet ds)
		{
			const int TBL_DETAILS = 2;
			const int TBL_HEARING = 3;
			
			DataTable dtCitationDetail = null;
			DataTable dtCitationHearing = null;

			try
			{
				dtCitationDetail = ds.Tables[TBL_DETAILS];
				dtCitationHearing = ds.Tables[TBL_HEARING];
				
				if (dtCitationDetail != null)
				{
					foreach (DataRow dr in dtCitationDetail.Rows)
					{
						this.CitationNumber = dr["CitationNumber"].ToString();
						this.BadgeNumberCurrent = dr["BadgeNumber"].ToString();
						this.CitationDate = Convert.ToDateTime(dr["WhenCreated"].ToString());
						this.Violation = DictionaryHelper.FromTable(ds.Tables["Violation"]);
						this.ViolationCurrent = Convert.ToInt32(dr["ViolationID"].ToString());
						this.LocationCurrent = dr["ViolationLocation"].ToString();
						this.ConditionsCurrent = dr["Conditions"].ToString();
						this.CompanyCurrent = dr["CorporationName"].ToString();
						this.DivisionCurrent = dr["DivisionName"].ToString();
						this.VehicleManufacturer = dr["VehicleMakeAbbreviation"].ToString();
						this.VehicleModel = dr["VehicleBodyAbbreviation"].ToString();
						this.LicenseNumber = dr["LicensePlate"].ToString();
						this.PointValue = Convert.ToInt32(dr["ViolationPoints"].ToString());
						this.CitingOfficerName = dr["CitingOfficerName"].ToString();
						this.CitingOfficerBadgeNumber = dr["CitingOfficerBadgeNumber"].ToString();

						this.LicenseCountry = DictionaryHelper.FromTable(ds.Tables["Countries"]);
						this.LicenseCountryCurrent = dr["CountryCode_LicensePlate"].ToString();

						// states-------------
						String stateCode;
						if (!String.IsNullOrEmpty(this.LicenseCountryCurrent))
						{
							stateCode = "ALLSTATES" + this.LicenseCountryCurrent;
						}
						else
						{
							stateCode = "ALLSTATESUSACAN";
						}
						//this.LicenseState = DictionaryHelper.FromTable(ds.Tables[stateCode]);
						this.LicenseState = DictionaryHelper.FromTable(ds.Tables["ALLSTATESUSACAN"]);
						this.LicenseStateCurrent = dr["CountrySubdivisionCode_LicensePlate"].ToString();

						//this.Group = o.FillLookup("Description", "Code", "SafeViolationTypes");
						Group = DictionaryHelper.FromTable(ds.Tables["SafeViolationTypes"]);

						this.GroupCurrent = dr["ViolationTypeID"].ToString();

						//this.BadgeDisplayed = o.YesNoLookup();
						BadgeDisplayed = new Dictionary<string, object> {{"Yes", "Yes"}, {"No", "No"}};

						// =======
						Boolean lc = false;
						String ls = "";

						ls = dr["BadgeDisplayed"].ToString();
						if (!String.IsNullOrEmpty(ls)) { lc = Convert.ToBoolean(ls); }
						this.BadgeDisplayedCurrent = lc ? "Yes" : "No";

						lc = false;
						ls = dr["Cancelled"].ToString();
						if (!String.IsNullOrEmpty(ls)) { lc = Convert.ToBoolean(ls); }
						this.IsCancelled = lc;

						lc = false;
						ls = dr["IsDeleted"].ToString();
						if (!String.IsNullOrEmpty(ls)) { lc = Convert.ToBoolean(ls); }
						this.IsDeleted = lc;

						lc = false;
						ls = dr["LicenseValid"].ToString();
						if (!String.IsNullOrEmpty(ls)) { lc = Convert.ToBoolean(ls); }
						this.LicenseValid = lc;

						lc = false;
						ls = dr["DriverIcon"].ToString();
						if (!String.IsNullOrEmpty(ls)) { lc = Convert.ToBoolean(ls); }
						this.DriverIcon = lc;
						// =======

						// administration
						this.DateCreated = Convert.ToDateTime(dr["WhenCreated"].ToString());
						this.ReturnToSAFEBy = Convert.ToDateTime(dr["WhenDue"].ToString());
						this.DateResolved = Convert.ToDateTime(dr["WhenResolved"].ToString());

						// hearing
						List<HearingSummary> dtCitationHearingList = new List<HearingSummary>();
						if (dtCitationHearingList != null)
						{
							foreach (DataRow drr in dtCitationHearing.Rows)
							{
								dtCitationHearingList.Add(
									new HearingSummary
									(
											Convert.ToInt32(drr["SafeHearingID"]),
											Convert.ToInt32(drr["SafeCitationID"]),
											Convert.ToDateTime(drr["SafeHearingDate"].ToString()),
											drr["SafeHearingNotes"].ToString()
									)
								);
							}
							this.HearingSummaries = dtCitationHearingList;
						}

						break;
					}
				}
			}
			finally
			{
				if (dtCitationDetail != null)
					dtCitationDetail.Dispose();
				if (dtCitationHearing != null)
					dtCitationHearing.Dispose();
			}
		}

		#endregion Constructors and Destructors

		#region Properties for TAB 3:::SAFE

		// Safe Point Count
		public String Driving { get; set; }

		public String Operations { get; set; }

		public String Safety { get; set; }

		public String TotalPoints { get; set; }

		// Citation Summary (table) grids
		public List<CitationSummary> CitationSummaries { get; set; }

		public List<HearingSummary> HearingSummaries { get; set; }

		// Citation Code
		public String CitationNumber { get; set; }

		public DateTime? CitationDate { get; set; }

		public Int32? PointValue { get; set; }

		public Dictionary<string, object> BadgeDisplayed { get; set; }

		public String BadgeDisplayedCurrent { get; set; }

		public Boolean LicenseValid { get; set; }   // checkbox

		public Boolean DriverIcon { get; set; }     // checkbox

		public Dictionary<string, object> Group { get; set; }           // ddl

		public String GroupCurrent { get; set; }              // ddl current value

		public Dictionary<string, object> Violation { get; set; }       // ddl

		public Int32 ViolationCurrent { get; set; }          // ddl current value

		//public Dictionary<string, object> Location { get; set; }        // ddl
		public String LocationCurrent { get; set; }
		public String ConditionsCurrent { get; set; }

		public Dictionary<string, object> BadgeNumber { get; set; }     // ddl

		public String BadgeNumberCurrent { get; set; }        // ddl current value

		// public Dictionary<string, object> Company { get; set; }         // ddl   per Michael, make a text box instead because of slow load times
		public String CompanyCurrent { get; set; }

		public Dictionary<string, object> Division { get; set; }        // ddl

		public String DivisionCurrent { get; set; }           // ddl current value

		public String CitingOfficerName { get; set; }

		public String CitingOfficerBadgeNumber { get; set; }

		// Vehicle Manufactured
		public String VehicleManufacturer { get; set; }

		public String LicenseNumber { get; set; }

		public String VehicleModel { get; set; }        // ddl current value

		public Dictionary<string, object> LicenseCountry { get; set; }   // ddl

		public String LicenseCountryCurrent { get; set; }      // ddl current value

		public Dictionary<string, object> LicenseState { get; set; }     // ddl

		public String LicenseStateCurrent { get; set; }        // ddl current value

		// administration
		public DateTime? DateReceived { get; set; }

		public DateTime? DateCreated { get; set; }

		public DateTime? ReturnToSAFEBy { get; set; }

		public DateTime? DateResolved { get; set; }

		public Boolean IsCancelled { get; set; }        // checkbox

		public Boolean IsDeleted { get; set; }          // checkbox

		public Boolean ScheduleHearing { get; set; }    // checkbox

		public Dictionary<string, object> Watch { get; set; }            // ddl

		public String WatchCurrent { get; set; }               // ddl current value

		public Dictionary<string, object> CitingOfficer { get; set; }    // ddl

		public String CitingOfficerCurrent { get; set; }       // ddl current value

		#endregion Properties for TAB 3:::SAFE

		public string ToJson()
		{
			var idtc = new IsoDateTimeConverter { DateTimeFormat = "MM/dd/yyyy" };
			return JsonConvert.SerializeObject(this, idtc);
		}
	}
}