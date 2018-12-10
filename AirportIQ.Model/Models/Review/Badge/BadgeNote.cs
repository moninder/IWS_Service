using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AirportIQ.Model.Models.Review.Badge
{
	/// <summary>
	/// BadgeNote
	/// </summary>
	/// <remarks></remarks>
	public class BadgeNote
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="BadgeNote"/> class.
		/// </summary>
		/// <param name="row">The row of note. <see cref="System.Data.DataRow"/> class</param>
		/// <remarks></remarks>
		public BadgeNote(DataRow row)
		{
            UniqueKey = row["UniqueKey"].ToString();
            NoteID = int.Parse(row["NoteID"].ToString());
            StaffFullName = row["FullName"].ToString();
            WhenBegins = Convert.ToDateTime(row["WhenBegins"].ToString());
            WhenEnds = Convert.ToDateTime(row["WhenEnds"].ToString());
            //IsHotNote = MapBool(Convert.ToBoolean(row["IsHotNote"].ToString()));
            IsHotNote = row["IsHotNote"].ToString() == "1" ? "Yes" : "No";
            Note = row["Note"].ToString();
            NoteType = row["NoteType"].ToString();
		}

		#region Properties

        public string UniqueKey { get; set; }
        public Int32 NoteID { get; set; }
        public string NoteType { get; set; }
        public Int32 PersonID { get; set; }
        public string CompanyCode { get; set; }
        public string CorporationName { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string Note { get; set; }
        public DateTime WhenBegins { get; set; }
        public DateTime WhenEnds { get; set; }
        public string IsHotNote { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? NoteDate { get; set; }
        public string StaffFullName { get; set; }

		#endregion

        public string MapBool(Boolean b)
        {
            if (b) { return "Yes"; } else { return "No"; }
        }
	}
}
