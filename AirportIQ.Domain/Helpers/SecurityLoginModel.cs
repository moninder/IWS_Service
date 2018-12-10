using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AirportIQ.Domain.Helpers;

/// <summary>
/// Summary description for SecurityAccessModel
/// </summary>
public class SecurityLoginModel
{

	public SecurityLoginModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    /// <summary>
    /// Domain & User Name combined into a single string (staff only)
    /// </summary>
    public string userLoginDomainAndName { get; set; }
    /// <summary>
    /// The windows domain of a user (staff only)
    /// </summary>
    public string userLoginDomain { get; set; }
    /// <summary>
    /// The AirportIQ userName (staff only). This must be the same as the windows ID.
    /// </summary>
    public string userLoginName { get; set; }
    /// <summary>
    /// The AirportIQ userID from Security.Users
    /// </summary>
    public int userId { get; set; }            //required
    /// <summary>
    /// The id of the application under which the user is registered
    /// </summary>
    public int applicationId { get; set; }    //required
    /// <summary>
    /// The full name of the logged in user
    /// </summary>
    public string userFullName { get; set; }
    /// <summary>
    /// Airport IQ app specific roles (all users)
    /// </summary>
    public Int64[] userRoles { get; set; }
    /// <summary>
    /// Airport IQ app security Permission token for forms (all users)
    /// </summary>
    public Int64 userFormPermission { get; set; }
    /// <summary>
    /// Airport IQ app security Permission token for controls &/or options
    /// </summary>
    public Int64 userControlPermission { get; set; }
    /// <summary>
    /// obsolete--do not use
    /// </summary>
    public Int64 userOptionPermission { get; set; }
    /// <summary>
    /// obsolete--do not use
    /// </summary>
    public Int64 userDataPermission { get; set; }
    /// <summary>
    /// Collection of assigned security groups
    /// </summary>
    public string[] groupsArray { get; set; }
    /// <summary>
    /// Collection of all windows roles (staff only)
    /// </summary>
    public string[] rolesArray { get; set; }
    /// <summary>
    /// The length of time (drawn from Facilities database per user) before a postback is required or else will time out
    /// </summary>
    public int masterTimeoutValueSeconds { get; set; }
    /// <summary>
    /// The primary (usually most important or group with the higher # of permissions
    /// </summary>
    public string primaryLoginGroup { get; set; }
    /// <summary>
    /// (Staff Only)
    /// </summary>
    public string userDepartmentName { get; set; }
    /// <summary>
    /// Facility Code
    /// </summary>
    public string userLoginFacilityCode { get; set; }
    /// <summary>
    /// (Staff Only) ID if staff member
    /// </summary>
    public Int32 staffID { get; set; }
    /// <summary>
    /// The person.PersonBiographics ID of the OAS or Staff user
    /// </summary>
    public Int32 personID { get; set; }
    /// <summary>
    /// Expiration date of login
    /// </summary>
    public DateTime loginExpirationDate { get; set; }
    /// <summary>
    /// if false, the login is inactive
    /// </summary>
    public Boolean isActive { get; set; }
    /// <summary>
    /// if windows, the SID of the windows login
    /// </summary>
    public System.Byte[] activeDirectoryUserSID { get; set; }
    /// <summary>
    /// provides access to user permissions
    /// </summary>
    public SecurityPermissionHelper securityClass { get; set; }
    /// <summary>
    /// provides access to user tokens
    /// </summary>
    public SecurityTokenHelper securityUserTokenObject { get; set; }
       
}
