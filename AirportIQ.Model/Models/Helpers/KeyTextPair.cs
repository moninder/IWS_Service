using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace AirportIQ.Model.Models.Helpers
{
	/// <summary>
	/// Helper class for storing key/value pairs for creating JSON code for DDLs.
	/// </summary>
	[Obsolete("Please use System.Collections.Generic.KeyValuePair<string, object> instead.")]
	public class KeyTextPair : IJsonDatasource
	{
		/// <summary>
		/// Constructor off of values.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="text"></param>
		public KeyTextPair(string key, string text)
		{
			Key = key;
			Text = text;
		}

		public string Key { get; set; }

		public string Text { get; set; }

		/// <summary>
		/// Implementation of the IJsonDatasource interface
		/// </summary>
		/// <returns></returns>
		public string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}