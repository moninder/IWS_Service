using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AirportIQ.Model.Models.Review.Badge
{
	/// <summary>
	/// BadgeNotes
	/// </summary>
	/// <remarks></remarks>
	public class BadgeNotes
	{
		// Fields...
		#region Private Members
		private List<BadgeNote> _Notes = new List<BadgeNote>();
		#endregion

		#region Constructors and Destructors
		/// <summary>
		/// Initializes a new instance of the <see cref="BadgeNotes"/> class.
		/// </summary>
		/// <param name="dt">DataTable <see cref="System.Data.DataTable"/></param>
		/// <remarks></remarks>
		public BadgeNotes(DataTable dt)
		{
			foreach (DataRow row in dt.Rows)
			{
				Notes.Add(new BadgeNote(row));
			}
		}
		#endregion


		#region Properties
		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>The notes.</value>
		/// <remarks></remarks>
		public List<BadgeNote> Notes
		{
			get { return _Notes; }
			set { _Notes = value; }
		}
		#endregion

		/// <summary>
		/// Serializes this object to a JSON string.
		/// </summary>
		/// <returns>string</returns>
		/// <remarks></remarks>
		public string ToJson()
		{
			IsoDateTimeConverter idtc = new IsoDateTimeConverter();
			idtc.DateTimeFormat = "MM/dd/yyyy";

			return JsonConvert.SerializeObject(this, idtc);
		}
	}
}
