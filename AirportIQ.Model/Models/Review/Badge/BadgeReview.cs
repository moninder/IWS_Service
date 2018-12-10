using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AirportIQ.Model.Models.Review.Badge
{
	public class BadgeReview
	{
		#region Private Members

		private Location _SelectedLocation;
		private List<Location> _Locations = new List<Location>();
		private List<BadgeStatusPeriod> _StatusHistory = new List<BadgeStatusPeriod>();
		private List<BadgeIcon> _CurrentIcons = new List<BadgeIcon>();
		private List<BadgeFile> _CurrentFiles = new List<BadgeFile>();
		private List<BadgeAccessCategory> _CurrentAccess = new List<BadgeAccessCategory>();
		private Miscellaneous _Miscellaneous = new Miscellaneous();
		private Person _Person = new Person();
		private Division _Division = new Division();
		private Badge _Badge = new Badge();
		private Company _Company = new Company();

		#endregion Private Members

		#region Constructors and Destructors

		private BadgeReview()
		{
		}

		public BadgeReview(DataSet ds)
		{
			LoadData(ds);
		}

		#endregion Constructors and Destructors

		#region Properties

		public Badge Badge
		{
			get { return _Badge; }
			set { _Badge = value; }
		}

		public Person Person
		{
			get { return _Person; }
			set { _Person = value; }
		}

		public Company Company
		{
			get { return _Company; }
			set { _Company = value; }
		}

		public Division Division
		{
			get { return _Division; }
			set { _Division = value; }
		}


		public Miscellaneous Miscellaneous
		{
			get { return _Miscellaneous; }
			set { _Miscellaneous = value; }
		}

		public List<Location> Locations
		{
			get { return _Locations; }
			set { _Locations = value; }
		}

		public Location SelectedLocation
		{
			get { return _SelectedLocation; }
			set { _SelectedLocation = value; }
		}

		public List<BadgeStatusPeriod> StatusHistory
		{
			get { return _StatusHistory; }
			set { _StatusHistory = value; }
		}

		public List<BadgeIcon> CurrentIcons
		{
			get { return _CurrentIcons; }
			set { _CurrentIcons = value; }
		}
		public List<BadgeFile> CurrentFiles
		{
			get { return _CurrentFiles; }
			set { _CurrentFiles = value; }
		}

		public List<BadgeAccessCategory> CurrentAccess
		{
			get { return _CurrentAccess; }
			set { _CurrentAccess = value; }
		}



		#endregion Properties

		#region Public Methods

		public string ToJson()
		{
			IsoDateTimeConverter idtc = new IsoDateTimeConverter();
			idtc.DateTimeFormat = "MM/dd/yyyy";
			return JsonConvert.SerializeObject(this, idtc);
		}

		public void Save()
		{
			var tmp = this;
			BadgeReview.SaveObject(tmp);
		}

		#endregion Public Methods

		#region Public Static Methods

		public static void Save(string jsonData)
		{
			var tmp = (BadgeReview)JsonConvert.DeserializeObject(jsonData, typeof(BadgeReview));
			BadgeReview.SaveObject(tmp);
		}

		#endregion Public Static Methods

		#region Private Methods

		private static void SaveObject(BadgeReview br)
		{
			//TODO: Write code to save here
		}

		private void LoadData(DataSet ds)
		{
			const int TBL_GENERAL = 0;
			const int TBL_LOCATIONS = 1;
			const int TBL_STATUS_HISTORY = 2;
			const int TBL_CURRENT_ICONS = 3;
			const int TBL_CURRENT_FILES = 4;
			const int TBL_CURRENT_ACCESS = 5;

			const int ROW_FIRST = 0;

			DataTable dtGeneral = ds.Tables[TBL_GENERAL];
			if (dtGeneral.Rows.Count > 0)
			{
				DataRow dr = dtGeneral.Rows[ROW_FIRST];

				// badge info
				Badge.BadgeID = (int) dr["BadgeID"];
				Badge.BadgeNumber = (string) dr["BadgeNumber"];
				Badge.BadgeColor = (string) dr["BadgeColor"];
				Badge.WhenBecomesActive = (DateTime) dr["WhenBadgeBecomesActive"];
				Badge.WhenExpires = (DateTime) dr["WhenBadgeExpires"];
				Badge.BadgeStatus = (string) dr["BadgeStatus"];

				// person info
				Person.NamePrefix = (string) dr["NamePrefixDescription"];
				Person.FirstName = (string) dr["FirstName"];
				Person.MiddleName = (string) dr["MiddleName"];
				Person.LastName = (string) dr["LastName"];
				Person.NameSuffix = (string) dr["NameSuffixDescription"];

				// Unknown info
				Miscellaneous.FingerprintDate = Badge.WhenBecomesActive;
				Miscellaneous.PrintDate = Badge.WhenBecomesActive;
				Miscellaneous.PrintUser = "John Dough";

				// Company info
				Company.CompanyCode = (string) dr["CompanyCode"];
				Company.CompanyID = (int) dr["CompanyID"];
				Company.CorporationName = (string) dr["CorporationName"];

				// Division info
				Division.DivisionID = (int) dr["DivisionID"];
				Division.DivisionCode = (string) dr["DivisionCode"];
				Division.DivisionName = (string) dr["DivisionName"];

                if (dr.Table.Columns.Contains("DivisionTypeName"))
                    Division.DivisionTypeName = (string)dr["DivisionTypeName"];

				Division.JobRole = (string) dr["JobRoleDescription"];
			}


			DataTable dtLocations = ds.Tables[TBL_LOCATIONS];
			if (dtLocations.Rows.Count > 0)
			{
				foreach (DataRow row in dtLocations.Rows)
				{
					Locations.Add(new Location(row));
				}

				SelectedLocation = Locations[0];
			}

			DataTable dtStatusHistory = ds.Tables[TBL_STATUS_HISTORY];
			StatusHistory.Clear();
			if (dtStatusHistory.Rows.Count > 0)
			{
				foreach (DataRow row in dtStatusHistory.Rows)
				{
					StatusHistory.Add(new BadgeStatusPeriod(row));
				}
			}

			DataTable dtCurrentIcons = ds.Tables[TBL_CURRENT_ICONS];
			CurrentIcons.Clear();
			if (dtCurrentIcons.Rows.Count > 0)
			{
				foreach (DataRow row in dtCurrentIcons.Rows)
				{
					CurrentIcons.Add(new BadgeIcon(row));
				}
			}

			DataTable dtCurrentFiles = ds.Tables[TBL_CURRENT_FILES];
			CurrentFiles.Clear();
			if (dtCurrentFiles.Rows.Count > 0)
			{
				foreach (DataRow row in dtCurrentFiles.Rows)
				{
					CurrentFiles.Add(new BadgeFile(row));
				}
			}
			/****
						else
						{
							CurrentFiles.Add(new BadgeFile("text file (54) (spoofed data)", "54", "text/plain"));
							CurrentFiles.Add(new BadgeFile("pdf file (5) (spoofed data)", "5", "application/pdf"));
						}
			*/

			DataTable dt = ds.Tables[TBL_CURRENT_ACCESS];
			CurrentAccess.Clear();
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow row in dt.Rows)
				{
					CurrentAccess.Add(new BadgeAccessCategory(row));
				}
			}
			/***
			else
			{
				CurrentAccess.Add(new BadgeAccessCategory());
				CurrentAccess.Add(new BadgeAccessCategory());
				CurrentAccess.Add(new BadgeAccessCategory());
				CurrentAccess.Add(new BadgeAccessCategory());
			}
			*/
		}

		#endregion Private Methods
	}
}