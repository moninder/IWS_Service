using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportIQ.Model.Models.Common
{
	public class AjaxResult
	{

		public AjaxResult(bool success, object result)
		{
			Success = success;
			Result = result;
		}
		public bool Success { get; set; }
		public object Result { get; set; }
	}
}
