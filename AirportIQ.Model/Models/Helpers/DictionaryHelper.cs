using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace AirportIQ.Model.Models.Helpers
{
	public static class DictionaryHelper
	{
		/// <summary>
		/// Froms the table.
		/// </summary>
		/// <param name="keyCol">int key col.</param>
		/// <param name="valueCol">int value col.</param>
		/// <param name="dt">DataTable  dt.</param>
		/// <returns>Dictionary<string, object></returns>
		/// <remarks></remarks>
		public static Dictionary<string, object> FromTable(int keyCol, int valueCol, DataTable dt)
		{
			var result = new Dictionary<string, object>();

			foreach (DataRow row in dt.Rows)
			{
				result.Add(row[keyCol].ToString(), row[valueCol] );
			}

			return result;
		}

		/// <summary>
		/// Froms the table.
		/// </summary>
		/// <param name="keyCol">string key col.</param>
		/// <param name="valueCol">string value col.</param>
		/// <param name="dt">DataTable dt.</param>
		/// <returns>Dictionary<string, object></returns>
		/// <remarks></remarks>
		public static Dictionary<string, object> FromTable(string keyCol, string valueCol, DataTable dt)
		{
			var result = new Dictionary<string, object>();

			foreach (DataRow row in dt.Rows)
			{
				result.Add(row[keyCol].ToString(), row[valueCol]);
			}

			return result;
		}

		/// <summary>
		/// Froms the table.
		/// </summary>
		/// <param name="dt">The dt.</param>
		/// <returns>Dictionary<string, object></returns>
		/// <remarks>Assumes that the first col is the key and the second col is the value</remarks>
		public static Dictionary<string, object> FromTable(DataTable dt)
		{
			var result = new Dictionary<string, object>();

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow row in dt.Rows)
				{
					result.Add(row[0].ToString(), row[1]);
				}
			}
			return result;
		}
	}
}
