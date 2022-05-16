using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;
using System.Web;

public partial class MainMaster : System.Web.UI.MasterPage
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] == null)
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //UserAccess();
            if (Request.QueryString["IsMainPage"] != null)
            {
                Session["Module_Id"] = null;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
            }
			spnUsername.InnerHtml = objdb.Emp_Name() + " - " + objdb.Office_Name();
            //spnUsername.InnerHtml = objdb.Emp_Name();
            Navigation.InnerHtml = "<ul class='sidebar-menu' data-widget='tree'>";
            Navigation.InnerHtml += "<li class='header' style='text-align: center;'>" + @DateTime.Now.ToString("D") + "</li>";
            Navigation.InnerHtml += "<li><hr style='margin: 0' /></li>";
            if (Convert.ToString(Session["UserTypeId"]) == "11")
            {
                Navigation.InnerHtml += "<li><a href='../../mis/Dashboard/producerDashboard.aspx?IsMainPage=1'><i class='fa fa-home'></i><span>&nbsp;Main Page</span></a></li>";
            }
            else
            {
                Navigation.InnerHtml += "<li><a href='../../mis/Dashboard/Home.aspx?IsMainPage=1'><i class='fa fa-home'></i><span>&nbsp;Main Page</span></a></li>";
            }

            if (Request.QueryString["Module_ID"] != null)
            {
                Session["Module_Id"] = Request.QueryString["Module_ID"].ToString();
            }

            if (Session["Module_Id"] == null)
            {
                ds = objdb.ByProcedure("SpUMHome",
                         new string[] { "flag", "Emp_ID", "UserTypeId" },
                         new string[] { "1", Session["Emp_ID"].ToString(), objdb.UserTypeID() }, "dataset");
				int Count = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
					
                    // Navigation.InnerHtml += "<li><a href='../../mis/Dashboard/Home.aspx?Module_Id=" + ds.Tables[0].Rows[i]["Module_ID"].ToString() + "'><i class='fa fa fa-hand-o-right'></i>&nbsp;" + "<span>" + ds.Tables[0].Rows[i]["Module_Name"].ToString() + "</span></a></li>";
					if (ds.Tables[0].Rows[i]["Module_ID"].ToString() == "37")
                    {
                       
                        DataSet dsNotifications = objdb.ByProcedure("Usp_Notifications", new string[] { "flag", "Office_Id", "V_EntryType" }, new string[] { "1", objdb.Office_ID(), "Out" }, "dataset");
                         if (dsNotifications != null && dsNotifications.Tables.Count > 0)
                         {
                         if(dsNotifications.Tables[0].Rows.Count > 0)
                         {
                             //Count += dsNotifications.Tables[0].Rows.Count;
                             Session["Count"] = "1";
                         }
                         if (dsNotifications.Tables[1].Rows.Count > 0)
                         {
                            //Count += dsNotifications.Tables[1].Rows.Count;
                         }
                         }
                        Navigation.InnerHtml += "<li><a href='../../mis/MilkCollection/Notifications.aspx?Module_Id=" + ds.Tables[0].Rows[i]["Module_ID"].ToString() + "' class='notification'><i class='fa fa-bell' aria-hidden='true'></i>&nbsp;" + "<span>" + ds.Tables[0].Rows[i]["Module_Name"].ToString() + "</span><span class='badge'>" + Count.ToString() + "</span></a></li>";
                    }
                    else
                    {
                        Navigation.InnerHtml += "<li><a href='../../mis/Dashboard/Home.aspx?Module_Id=" + ds.Tables[0].Rows[i]["Module_ID"].ToString() + "'><i class='fa fa fa-hand-o-right'></i>&nbsp;" + "<span>" + ds.Tables[0].Rows[i]["Module_Name"].ToString() + "</span></a></li>";
                    }
                }
            }
            else
            {
				
                ds = objdb.ByProcedure("SpUMHome",
                         new string[] { "flag", "Emp_ID", "Module_ID", "UserTypeId" },
                         new string[] { "2", Session["Emp_ID"].ToString(), Session["Module_Id"].ToString(), objdb.UserTypeID() }, "dataset");

                string Menu_Name = "";
                string NavigationLi = "";
                int IsMainPage = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    NavigationLi = "";
                    IsMainPage = 0;
                    Menu_Name = ds.Tables[0].Rows[i]["Menu_Name"].ToString();

                    while (ds.Tables[0].Rows[i]["Menu_Name"].ToString() == Menu_Name)
                    {
                        IsMainPage++;
                        NavigationLi += "<li><a href='" + ds.Tables[0].Rows[i]["Form_Path"].ToString() + "'><i class='fa fa-hand-o-right'></i>" + "<span>" + ds.Tables[0].Rows[i]["Form_Name"].ToString() + "</span>" + "</a></li>";
                        i++;
                        if (ds.Tables[0].Rows.Count == i)
                        {
                            break;
                        }
                    }
                    i--;
                    if (IsMainPage == 1)
                    {
                        Navigation.InnerHtml += NavigationLi;
                    }
                    else
                    {
                        Navigation.InnerHtml += "<li class='treeview'>";
                        Navigation.InnerHtml += "<a href='#'>" + ds.Tables[0].Rows[i]["Menu_Icon"].ToString() + "<span>" + ds.Tables[0].Rows[i]["Menu_Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";
                        Navigation.InnerHtml += "<ul class='treeview-menu' style='display: none;'>";
                        Navigation.InnerHtml += NavigationLi;
                        Navigation.InnerHtml += "</ul>";
                        Navigation.InnerHtml += "</li>";
                    }
                }
            }
            Navigation.InnerHtml += "<li><a href='../admin/ChangePassword.aspx'><i class='fa fa-key'></i><span>Change Password</span></a></li>";
            Navigation.InnerHtml += "<li><a href='../Login.aspx'><i class='fa fa-power-off'></i><span>Logout</span></a></li>";
            Navigation.InnerHtml += "</ul>";
        }
    }
    public void UserAccess()
    {
        if (Session["FormPath"] != null)
        {
            int k = 0;
            string path = ".." + HttpContext.Current.Request.Url.AbsolutePath;

            string HomePage = "../mis/Dashboard/Home.aspx";
            string ChangePass = "../mis/admin/ChangePassword.aspx";

            DataSet FormPath = (DataSet)Session["FormPath"];

            if (path == HomePage || path == ChangePass)
            {
                k = 1;
            }
            else if (FormPath != null && FormPath.Tables.Count > 0 && FormPath.Tables[0].Rows.Count > 0)
            {
                int Pathcount = FormPath.Tables[0].Rows.Count;
                for (int i = 0; i < Pathcount; i++)
                {
                    string FormAdd = FormPath.Tables[0].Rows[i]["Form_Path"].ToString();
                    FormAdd = "../mis" + FormAdd.Remove(0, 2); ;
                    if (path == FormAdd)
                    {
                        k = 1;
                        break;
                    }
                }
            }
            if (k == 0)
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
}
