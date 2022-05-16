using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpServiceBook : System.Web.UI.Page
{
    DataSet ds;
    DataSet ds1;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Attachment;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();                   
                    FillDropdown();
                    btnPrint.Visible = false;
                    //FillGrid();
                    //string[] ImagePaths = Directory.GetFiles(Server.MapPath("~/mis/HR/abc/"));
                    //List<ListItem> Imgs = new List<ListItem>();
                    //foreach (string imgPath in ImagePaths)
                    //{
                    //    string Attachment = Path.GetFileName(imgPath);
                    //    Imgs.Add(new ListItem(Attachment, "~/mis/HR/abc/" + Attachment));
                    //}
                    //GridView1.DataSource = Imgs;
                    //GridView1.DataBind();


                    //Attachment = Guid.NewGuid() + "-" + FU_ServiceBook.FileName;
                    //FU_ServiceBook.PostedFile.SaveAs(Server.MapPath("~/mis/HR/abc/" + Attachment));
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            }            
            ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            ds = null;
            FillEmployee();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlEmployee.Items.Clear();
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            if(ddlOfficeName.SelectedIndex >0)
            {
                FillEmployee();
            }          

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEmployee()
    {
        try
        {
            ddlEmployee.Items.Clear();
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            DataSet ds2 = objdb.ByProcedure("SpHRTransfer", new string[] { "flag", "OldOffice" }, new string[] { "6", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds2.Tables.Count > 0)
            {
                ddlEmployee.DataSource = ds2;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            }
          
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            lblMsg.Text = "";
            string ServiceBook_IsActive = "1";
            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Current Office. \\n";
            }
            if (ddlEmployee.SelectedIndex == 0)
            {
                msg += "Select Employee Name. \\n";
            }
            if (txtSNo.Text.Trim() == "")
            {
                msg += "Enter S No";
            }
            if (FU_ServiceBook.HasFile)
            {
                Attachment = Guid.NewGuid() + "-" + FU_ServiceBook.FileName;
                FU_ServiceBook.PostedFile.SaveAs(Server.MapPath("~/mis/HR/ServiceBook/" + Attachment));
            }
            else
            {
                Attachment = "";
            }

            if (msg.Trim() == "")
            {
                objdb.ByProcedure("SpHREmpServiceBook", new string[] { "flag", "ServiceBook_IsActive", "OfficeID", "Emp_ID", "PDFSNO", "UploadServiceBook", "ServiceBook_Updated_By" },
                  new string[] { "0",ServiceBook_IsActive, ddlOfficeName.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString(),  txtSNo.Text,Attachment, ViewState["Emp_ID"].ToString() }, "dataset");
                txtSNo.Text = "";
                FillGrid();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            //GridView1.DataSource = null;
            //GridView1.DataBind();
            ds = objdb.ByProcedure("SpHREmpServiceBook", new string[] { "flag", "Emp_ID" }, new string[] { "1",ddlEmployee.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = new string[]{};
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmployee.SelectedIndex > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}