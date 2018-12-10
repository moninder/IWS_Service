using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AirportIQ.Domain.Contracts;
using AirportIQ.Domain.Services;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Trirand.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;


namespace AirportIQ.Domain.Helpers
{
    public class SecurityPermissionHelper
    {
        #region Private constants.....
            private const int _RESOURCE_TOKEN_TABLE = 0;
        #endregion
        #region Private variables.....
                private static SecurityPermissionHelper c_instance = null;
                Dictionary<string,SecurityResourceModel> _resources;
        #endregion
        #region Private methods.....
            /// <summary>
            /// constructor
            /// </summary>
            private SecurityPermissionHelper()
            {
            }
            /// <summary>
            /// Load any required data
            /// </summary>
            private void initialize()
            {
                LoadResources();
            }
            /// <summary>
            /// Load the resource tablevalues and populate the Resources Dictionary
            /// </summary>
            private void LoadResources()
            {

                IAdmin ds = new AirportIQ.Domain.Services.AdminServices();
                DataSet ret = ds.LoadResourceTokens();
                DataTable dt = new DataTable();
                dt = ret.Tables[_RESOURCE_TOKEN_TABLE];

                //-----Create Dictionary-------------------------
                _resources =
                     (from model in dt.AsEnumerable()
                      select new
                      {
                          ResourceName = model.Field<string>("ResourceName"),
                          AccessCode = model.Field<long>("AccessCode"),
                          AccessTokenID = model.Field<Int32>("AccessTokenID")
                      }
                      ).Select(x => new SecurityResourceModel() { ResourceName = x.ResourceName, AccessCode = x.AccessCode, AccessTokenID = x.AccessTokenID }).ToDictionary(model => model.ResourceName);

            }
            private void makeReadOnly(Control ctrl)
            {
                if (ctrl.Controls.Count <= 0)
                {
                    // a switch cannot be used here as we are dealing with a control and not a switch compatible type
                    // -----------------------------------------------------------------------------------------------
                    if (ctrl is TextBox)
                    {
                        ((TextBox)ctrl).ReadOnly = true;    // disabling makes text hard to read...readonly though allows click inside
                    }
                    else if (ctrl is CheckBox)
                    {
                        ((CheckBox)ctrl).Enabled = false;
                    }
                    else if (ctrl is DropDownList)
                    {
                        ((DropDownList)ctrl).Enabled = false;
                    }
                    else if (ctrl is Button)
                    {
                        ((Button)ctrl).Enabled = false;     // use visible property instead if more secure method is needed. 
                    }
                    else if (ctrl is ListBox)
                    {
                        ((ListBox)ctrl).Enabled = false;
                    }
                    else if (ctrl is CheckBoxList)
                    {
                        ((CheckBoxList)ctrl).Enabled = false;
                    }
                    else if (ctrl is HyperLink)
                    {
                        ((HyperLink)ctrl).Enabled = false;    // ?? should hyperlinks be left alone?
                    }
                    else if (ctrl is LinkButton)
                    {
                        ((LinkButton)ctrl).Enabled = false;
                        ((LinkButton)ctrl).OnClientClick = "";
                    }
                    else if (ctrl is ImageButton)
                    {
                        ((ImageButton)ctrl).Visible = false;     // more secure this way
                    }
                    else if (ctrl is FileUpload)
                    {
                        ((FileUpload)ctrl).Visible = false;     // more secure this way  
                    }
                    else if (ctrl is GridView)
                    {
                        ((GridView)ctrl).Enabled = false;
                    }
                    else if (ctrl is RadioButtonList)
                    {
                        ((RadioButtonList)ctrl).Enabled = false;
                    }
                    else if (ctrl is RadioButton)
                    {
                        ((RadioButton)ctrl).Enabled = false;
                    }
                    else if (ctrl is Accordion)
                    {
                        ((Accordion)ctrl).Enabled = false;
                    }
                    else if (ctrl is Trirand.Web.UI.WebControls.JQGrid)
                    {
                        ((Trirand.Web.UI.WebControls.JQGrid)ctrl).ToolBarSettings.ShowAddButton = false;
                        ((Trirand.Web.UI.WebControls.JQGrid)ctrl).ToolBarSettings.ShowEditButton = false;
                        ((Trirand.Web.UI.WebControls.JQGrid)ctrl).ToolBarSettings.ShowDeleteButton = false;
                        ((Trirand.Web.UI.WebControls.JQGrid)ctrl).ToolBarSettings.ShowInlineAddButton = false;
                        ((Trirand.Web.UI.WebControls.JQGrid)ctrl).ToolBarSettings.ShowInlineDeleteButton = false;
                        ((Trirand.Web.UI.WebControls.JQGrid)ctrl).ToolBarSettings.ShowInlineEditButton = false;
                        int cc = (int)((Trirand.Web.UI.WebControls.JQGrid)ctrl).ToolBarSettings.CustomButtons.Count;
                        if (cc > 0)
                        {
                            ((Trirand.Web.UI.WebControls.JQGrid)ctrl).ToolBarSettings.CustomButtons.Clear();
                        }
                    }
                    else if (ctrl is HtmlButton)
                    {
                        ((HtmlButton)ctrl).Disabled= false;
                    }
                    else if (ctrl is HtmlGenericControl)
                    {
                        HtmlGenericControl dv = (HtmlGenericControl)ctrl;
                        if (dv.TagName.ToLower() == "div")
                        {
                            ((HtmlGenericControl)ctrl).Disabled = true;
                        }
                    }
                }
                else
                {
                    foreach (Control childControl in ctrl.Controls)
                    {
                        // a switch cannot be used here as we are dealing with a control and not a switch compatible type
                        // -----------------------------------------------------------------------------------------------
                        if (childControl is TextBox)
                        {
                            ((TextBox)childControl).ReadOnly = true;    // disabling makes text hard to read...readonly though allows click inside
                        }
                        else if (childControl is CheckBox)
                        {
                            ((CheckBox)childControl).Enabled = false;
                        }
                        else if (childControl is DropDownList)
                        {
                            ((DropDownList)childControl).Enabled = false;
                        }
                        else if (childControl is Button)
                        {
                            ((Button)childControl).Enabled = false;     // use visible property instead if more secure method is needed. 
                        }
                        else if (childControl is ListBox)
                        {
                            ((ListBox)childControl).Enabled = false;
                        }
                        else if (childControl is CheckBoxList)
                        {
                            ((CheckBoxList)childControl).Enabled = false;
                        }
                        else if (childControl is HyperLink)
                        {
                            ((HyperLink)childControl).Enabled = false;    // ?? should hyperlinks be left alone?
                        }
                        else if (childControl is LinkButton)
                        {
                            ((LinkButton)childControl).Enabled = false;
                            ((LinkButton)childControl).OnClientClick = "";
                        }
                        else if (childControl is ImageButton)
                        {
                            ((ImageButton)childControl).Visible = false;     // more secure this way
                        }
                        else if (childControl is FileUpload)
                        {
                            ((FileUpload)childControl).Visible = false;     // more secure this way  
                        }
                        else if (childControl is GridView)
                        {
                            ((GridView)childControl).Enabled = false;
                        }
                        else if (childControl is RadioButtonList)
                        {
                            ((RadioButtonList)childControl).Enabled = false;
                        }
                        else if (childControl is RadioButton)
                        {
                            ((RadioButton)childControl).Enabled = false;
                        }
                        else if (childControl is Accordion)
                        {
                            ((Accordion)childControl).Enabled = false;
                        }
                        else if (childControl  is Trirand.Web.UI.WebControls.JQGrid)
                        {
                            ((Trirand.Web.UI.WebControls.JQGrid)childControl).ToolBarSettings.ShowAddButton = false;
                            ((Trirand.Web.UI.WebControls.JQGrid)childControl).ToolBarSettings.ShowEditButton = false;
                            ((Trirand.Web.UI.WebControls.JQGrid)childControl).ToolBarSettings.ShowDeleteButton = false;
                            ((Trirand.Web.UI.WebControls.JQGrid)childControl).ToolBarSettings.ShowInlineAddButton = false;
                            ((Trirand.Web.UI.WebControls.JQGrid)childControl).ToolBarSettings.ShowInlineDeleteButton = false;
                            ((Trirand.Web.UI.WebControls.JQGrid)childControl).ToolBarSettings.ShowInlineEditButton = false;
                            int cc = (int)((Trirand.Web.UI.WebControls.JQGrid)childControl).ToolBarSettings.CustomButtons.Count;
                            if (cc > 0)
                            {
                                ((Trirand.Web.UI.WebControls.JQGrid)childControl ).ToolBarSettings.CustomButtons.Clear();
                            }
                            // TODO: investigate to see if there is a way to remove icon of custom button off of screen. The commented code below removes the javascript but
                            // does not remove the icon itself.
                            //if (cc > 0)
                            //{
                            //    foreach ( Trirand.Web.UI.WebControls.JQGridToolBarButton c in ((Trirand.Web.UI.WebControls.JQGrid)childControl).ToolBarSettings.CustomButtons)
                            //    {
                            //        if ( c.ButtonIcon != "ui-icon-document")
                            //        {
                            //            c.OnClick="";
                            //        }
                            //    }
                            //}
                        }
                        else if (childControl is HtmlButton)
                        {
                            ((HtmlButton)childControl).Disabled  = true;   
                        }
                        else if (childControl is HtmlGenericControl)
                        {
                            HtmlGenericControl dv = (HtmlGenericControl)childControl;
                            if (dv.TagName.ToLower() == "div")   // use div like panel to disable html controls
                            {
                                ((HtmlGenericControl)childControl).Disabled = true;
                            }
                        }

                        if (childControl.Controls.Count > 0)
                        {
                            makeReadOnly(childControl);
                        }
                    }
                }
            }

         #endregion
        #region Public methods.....
        /// <summary>
            /// instance accessor
            /// </summary>
            public static SecurityPermissionHelper Instance
            {
                get
                {
                    if (c_instance == null)
                    {
                        c_instance = new SecurityPermissionHelper();
                        c_instance.initialize();
                    }

                    return c_instance;
                }
            }
            /// <summary>
            /// Check to see if a named application resource is/should be read accessible by the current user's permission token
            /// If an invalid resource name is sent in, method will return false.
            /// </summary>
            /// <param name="resource"></param>
            /// <param name="securityClass"></param>
            /// <returns>true if the user has read access to the resource</returns>
            public bool hasReadAccess(string resource, SecurityTokenHelper securityClass)
            {
                bool retValue = false;
                long resourceAccessCode=0;
                Int32 resourceIndex=0;
                long userToken;
                long groupReadToken;

                resource = resource.ToUpper();

                // get resource token for which access is being checked. This is a dictionary lookup.
                // List<long> userReadToken = securityClass.ReadTokens; 
                if (_resources.ContainsKey(resource))
                {
                    resourceAccessCode  = _resources[resource].AccessCode;
                    resourceIndex = (Int32)_resources[resource].AccessTokenID;
                }

                // compare the resource token with the user's read token (if any). 
                if ((resourceAccessCode > 0) && (securityClass.ReadTokens.Count > 0))
                {
                    userToken = securityClass.ReadTokens[resourceIndex-1];
                    retValue = ((userToken & resourceAccessCode) >= 1);      // binary compare
                }

                // get resource token for which access is being checked. This is a dictionary lookup.
                // List<long> groupReadToken = securityClass.GroupTokens;
                if (_resources.ContainsKey(resource))
                {
                    resourceAccessCode = _resources[resource].AccessCode;
                    resourceIndex = (Int32)_resources[resource].AccessTokenID;
                }

                // compare the resource token with the group token (if any). 
                if ((resourceAccessCode > 0) && (securityClass.GroupReadTokens.Count > 0))
                {
                    groupReadToken = securityClass.GroupReadTokens[resourceIndex - 1];
                    retValue = (retValue || ((groupReadToken & resourceAccessCode) >= 1));      // binary compare
                }
                //--------------------------------------------------------------------------------------

                return (retValue);
            }
            /// <summary>
            /// Check to see if a named application resource is/should be write accessible by the current user's permission token.
            /// If an invalid resource name is sent in, method will return false.
            /// </summary>
            /// <param name="resource"></param>
            /// <param name="securityClass"></param>
            /// <returns>true if user has write access to the resource</returns>
            public bool hasWriteAccess(string resource, SecurityTokenHelper securityClass)
            {
                bool retValue = false;
                long resourceAccessCode = 0;
                Int32 resourceIndex = 0;
                long userToken;
                long groupWriteToken;

                resource = resource.ToUpper();

                // get resource token for which access is being checked. This is a dictionary lookup.
                // List<long> userReadToken = securityClass.WriteTokens;
                if (_resources.ContainsKey(resource))
                {
                    resourceAccessCode = _resources[resource].AccessCode;
                    resourceIndex = (Int32)_resources[resource].AccessTokenID;
                }

                // compare the resource token with the user's write token (if any)
                if ((resourceAccessCode>0) && (securityClass.WriteTokens.Count > 0))
                {
                    userToken = securityClass.WriteTokens[resourceIndex-1];
                    retValue = ((userToken & resourceAccessCode) >= 1);           // binary compare
                }

                // get resource token for which access is being checked. This is a dictionary lookup.
                // List<long> groupReadToken = securityClass.GroupTokens;
                if (_resources.ContainsKey(resource))
                {
                    resourceAccessCode = _resources[resource].AccessCode;
                    resourceIndex = (Int32)_resources[resource].AccessTokenID;
                }

                // compare the resource token with the group token (if any). 
                if ((resourceAccessCode > 0) && (securityClass.GroupWriteTokens.Count > 0))
                {
                    groupWriteToken = securityClass.GroupWriteTokens[resourceIndex - 1];
                    retValue = (retValue || ((groupWriteToken & resourceAccessCode) >= 1));      // binary compare
                }

                return (retValue);
            }
            
            /// <summary>
            /// Checks to see if form can be accessed. If it can, checks to see if access is read or write
            /// </summary>
            /// <param name="resource"></param>
            /// <param name="securityClass"></param>
            /// <param name="p"></param>
            /// <param name="r"></param>
            /// <returns></returns>
            public void LockResource(string resource, SecurityTokenHelper securityClass, Control ctrl, HttpResponse resp)
            {
                bool isRead;
                bool isWrite;
               
                isRead = hasReadAccess(resource, securityClass);
                isWrite = hasWriteAccess(resource, securityClass);

                if (!(isRead || isWrite))
                {
                    // if neither isread or iswrite, transfer to no access page.
                    // NOTE!!! this code will generate a frame error if caller is in a try catch.
                    resp.Redirect("~/InvalidAccess.aspx");
                }
                else if (isRead && !isWrite)
                {
                    // if is read, change all ctrls to disabled
                    makeReadOnly(ctrl);
                    // TODO: see if we can walk the Request.Form html objects and disable those here as well ...Request.Form["xxxxx"];
                }
            }

            /// <summary>
            /// Used when a page has no meaning when it is read only.  If the user has write access they should have normal access
            /// if the user has read access only they will be redirected to an access denied page. Because we do not allow just write
            /// access by itself, the requirement is that the user must have both read & write access.
            /// </summary>
            /// <param name="resource"></param>
            /// <param name="securityClass"></param>
            /// <param name="ctrl"></param>
            /// <param name="resp"></param>
            public void LockMustWriteResource(string resource, SecurityTokenHelper securityClass, HttpResponse resp)
            {
                bool isRead;
                bool isWrite;

                isRead = hasReadAccess(resource, securityClass);
                isWrite = hasWriteAccess(resource, securityClass);

                if (!(isRead && isWrite))
                {
                    // NOTE!!! this code will generate a frame error if caller is in a try catch.
                    resp.Redirect("~/InvalidAccess.aspx");
                }
            }
            /// <summary>
            /// This checks to see if the user has any access. This is for pages which are already 100% readonly
            /// </summary>
            /// <param name="resource"></param>
            /// <param name="securityClass"></param>
            /// <param name="resp"></param>
            public void LockAnyAccessResource(string resource, SecurityTokenHelper securityClass, HttpResponse resp)
            {
                bool isRead;
                bool isWrite;

                isRead = hasReadAccess(resource, securityClass);
                isWrite = hasWriteAccess(resource, securityClass);

                if (!(isRead || isWrite))
                {
                    // if neither isread or iswrite, transfer to no access page.
                    // NOTE!!! this code will generate a frame error if caller is in a try catch.
                    resp.Redirect("~/InvalidAccess.aspx");
                }               
            }
            /// <summary>
            /// This method is gridview specific. It makes all data rows of the gridview readonly without impacting the sort and/or pager templates.
            /// </summary>
            /// <param name="resource"></param>
            /// <param name="securityClass"></param>
            /// <param name="gridview"></param>
            /// <param name="resp"></param>
            public void LockGridViewDataRowsResource(string resource, SecurityTokenHelper securityClass, GridView gridview, HttpResponse resp)
            {
                bool isRead;
                bool isWrite;

                isRead = hasReadAccess(resource, securityClass);
                isWrite = hasWriteAccess(resource, securityClass);

                if (!(isRead || isWrite))
                {
                    // if neither isread or iswrite, transfer to no access page.
                    // NOTE!!! this code will generate a frame error if caller is in a try catch.
                    resp.Redirect("~/InvalidAccess.aspx");
                }
                else if (isRead && !isWrite)
                {
                    // if is read, change all ctrls to disabled
                    foreach (GridViewRow r in gridview.Rows)
                    {
                        if (r.RowType == DataControlRowType.DataRow)
                        {
                            r.Enabled = false;
                        }
                    }
                }
            }
            /// <summary>
            /// This method allows registration of javascript to fire during onload event. 
            /// </summary>
            /// <param name="resource"></param>
            /// <param name="securityClass"></param>
            /// <param name="pg" contains page></param>
            /// <param name="jscript" Javascript function to be called></param>
            /// <param name="resp"></param>
            public void LockJSOnLoadResource(string resource, SecurityTokenHelper securityClass, Page pg, String jscript, HttpResponse resp)
            {
                bool isRead;
                bool isWrite;

                isRead = hasReadAccess(resource, securityClass);
                isWrite = hasWriteAccess(resource, securityClass);

                if (!(isRead || isWrite))
                {
                    // if neither isread or iswrite, transfer to no access page.
                    // NOTE!!! this code will generate a frame error if caller is in a try catch.
                    resp.Redirect("~/InvalidAccess.aspx");
                }
                else if (isRead && !isWrite)
                {
                    pg.ClientScript.RegisterStartupScript(typeof(Page), "OnLoad", jscript, true);
                }
            }                  
        #endregion
    }
}
