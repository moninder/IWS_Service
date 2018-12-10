using System;
using System.Collections.Generic;


namespace AirportIQ.Model.Models.Review.Badge
{
	public class Badge
	{
		#region Private Members
		private List<BadgeStatusPeriod> _BadgeStatusHistory = new List<BadgeStatusPeriod>();
		private List<BadgeStatusPeriod> _BadgeStatusPeriods = new List<BadgeStatusPeriod>();
		private List<BadgeIcon> _BadgeIcons = new List<BadgeIcon>();
		private List<BadgeFile> _BadgeFiles = new List<BadgeFile>();
		private List<BadgeAccessCategory> _BadgeAccess = new List<BadgeAccessCategory>();
		#endregion
		

		#region Properties
		public int BadgeID { get; set; }
		public string BadgeNumber { get; set; }
		public DateTime WhenBecomesActive { get; set; }
		public DateTime WhenExpires { get; set; }
		public string BadgeStatus { get; set; }
		public string BadgeColor { get; set; }
		
		public List<BadgeStatusPeriod> BadgeStatusPeriods
		{
			get { return _BadgeStatusPeriods; }
			set { _BadgeStatusPeriods = value; }
		}

		public List<BadgeStatusPeriod> BadgeStatusHistory
		{
			get { return _BadgeStatusHistory; }
			set { _BadgeStatusHistory = value; }
		}

		public List<BadgeIcon> BadgeIcons
		{
			get { return _BadgeIcons; }
			set { _BadgeIcons = value; }
		}

		public List<BadgeFile> BadgeFiles
		{
			get { return _BadgeFiles; }
			set { _BadgeFiles = value; }
		}

		public List<BadgeAccessCategory> BadgeAccess
		{
			get { return _BadgeAccess; }
			set { _BadgeAccess = value; }
		}

		#endregion
	}
}