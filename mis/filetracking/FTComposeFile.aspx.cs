using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class FTComposeFile : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Department_ID"] = Session["Department_ID"].ToString();
                    hyprDoc.Visible = false;
                    if (Request.QueryString["File_ID"] != null)
                    {
                        ViewState["File_ID"] = objdb.Decrypt(Request.QueryString["File_ID"].ToString());
                        FillEditDetail();
                    }
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEditDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFTComposeFile",
                    new string[] { "flag", "File_ID" },
                    new string[] { "10", ViewState["File_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFile_Type.ClearSelection();
                ddlFile_Type.Items.FindByValue(ds.Tables[0].Rows[0]["File_Type"].ToString()).Selected = true;
                ddlFile_Priority.ClearSelection();
                ddlFile_Priority.Items.FindByText(ds.Tables[0].Rows[0]["File_Priority"].ToString()).Selected = true;
                txtFile_No.Text = ds.Tables[0].Rows[0]["File_No"].ToString();
                txtFile_Title.Text = ds.Tables[0].Rows[0]["File_Title"].ToString();
                ViewState["Document_Upload"] = ds.Tables[0].Rows[0]["Document_Upload"].ToString();
                if (ViewState["Document_Upload"] != "")
                {
                    hyprDoc.NavigateUrl = "../Uploads/" + ViewState["Document_Upload"].ToString();
                    hyprDoc.Visible = true;
                }
                txtQRCode.Text = ds.Tables[0].Rows[0]["QRCode"].ToString();
                txtQRCode.Attributes.Add("readonly", "readonly");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ChangeColor()", true);
                txtFile_Description.Text = ds.Tables[0].Rows[0]["File_Description"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        ddlFile_Type.ClearSelection();
        ddlFile_Priority.ClearSelection();
        txtFile_No.Text = "";
        txtFile_Title.Text = "";
        txtFile_Description.Text = "";
        txtQRCode.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string type = "";
            string typeMsg = "";
            string File_IsActive = "1";
            string Forwarded_IsActive = "1";
            string ProfileImagePath = "";
            if (ddlFile_Type.SelectedIndex < 0)
            {
                msg += "Select File Type \n";
            }
            if (ddlFile_Priority.SelectedIndex < 0)
            {
                msg += "Select File Priority \n";
            }
            if (txtFile_No.Text.Trim() == "")
            {
                msg += "Enter File No. \n";
            }
            if (txtFile_Title.Text.Trim() == "")
            {
                msg += "Enter File Title \n";
            }
            if (txtFile_Description.Text.Trim() == "")
            {
                msg += "Enter File Description \n";
            }
            if (Document_Upload.HasFile)
            {
                ProfileImagePath = "../filetracking/Uploads/" + Guid.NewGuid() + "-" + Document_Upload.FileName;
                Document_Upload.PostedFile.SaveAs(Server.MapPath(ProfileImagePath));
            }
            if (msg.Trim() == "")
            {
                if (btnSave.Text == "Create")
                {
                    ds = objdb.ByProcedure("SpFTComposeFile",
                    new string[] { "flag", "File_No", "QRCode" },
                    new string[] { "3", txtFile_No.Text, txtQRCode.Text }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        msg = "File No. is Already Exist";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                    }
                    if (ds.Tables[1].Rows.Count != 0)
                    {
                        msg = "BAR Code. is Already Exist";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                    }
                    else
                    {
                        if (msg == "")
                        {
                            ds = objdb.ByProcedure("SpFTComposeFile",
                        new string[] { "flag", "File_IsActive", "Office_ID", "File_Type", "File_Priority", "File_No", "File_Title", "Document_Upload", "File_Description", "Department_ID", "QRCode", "File_UpdatedBy", "FileCreatedbyOpt" },
                        new string[] { "0", File_IsActive, ViewState["Office_ID"].ToString(), ddlFile_Type.SelectedItem.Text, ddlFile_Priority.SelectedItem.Text, txtFile_No.Text, txtFile_Title.Text, ProfileImagePath, txtFile_Description.Text, ViewState["Department_ID"].ToString(), txtQRCode.Text, ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                            string fileID = ds.Tables[0].Rows[0]["File_ID"].ToString();


                            objdb.ByProcedure("SpFTComposeFile",
                            new string[] { "flag", "Forwarded_IsActive", "File_ID", "Emp_ID", "File_Status" },
                            new string[] { "4", Forwarded_IsActive, fileID, ViewState["Emp_ID"].ToString(), "Available" }, "dataset");

                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                            ClearText();
                        }
                    }
                }
                else if (btnSave.Text == "Edit")
                {
                    ds = objdb.ByProcedure("SpFTComposeFile",
                    new string[] { "flag", "File_No", "File_ID" },
                    new string[] { "19", txtFile_No.Text, ViewState["File_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows[0]["Status"].ToString() != "0")
                    {
                        msg = "File No. is Already Exist";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                    }
                    else if (ds.Tables[0].Rows[0]["Status"].ToString() == "0")
                    {
                        if (ProfileImagePath == "")
                        {
                            ProfileImagePath = ViewState["Document_Upload"].ToString();
                        }
                        ds = objdb.ByProcedure("SpFTComposeFile",
                        new string[] { "flag", "File_ID", "File_Type", "File_Priority", "File_No", "File_Title", "Document_Upload", "File_Description", "File_UpdatedBy" },
                        new string[] { "13", ViewState["File_ID"].ToString(), ddlFile_Type.SelectedItem.Text, ddlFile_Priority.SelectedItem.Text, txtFile_No.Text, txtFile_Title.Text, ProfileImagePath, txtFile_Description.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        btnSave.Text = "Create";
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ClearText();
    }
}