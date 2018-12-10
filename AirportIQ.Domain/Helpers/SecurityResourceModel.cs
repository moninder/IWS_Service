using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SecurityAccessModel
/// </summary>
public class SecurityResourceModel
{
	public SecurityResourceModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ResourceName { get; set; }
    public long AccessCode { get; set; }
    public long AccessTokenID { get; set; }
}