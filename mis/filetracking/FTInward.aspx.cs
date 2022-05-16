using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_filetracking_FTInvert : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
                    txtCreateDate.Attributes.Add("readonly", "readonly");
                    txtCreateDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtInwardDate.Attributes.Add("readonly", "readonly");
                    //txtCurrentDate.Attributes.Add("readonly", "readonly");
                    //txtCurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                txtCreateDate.Text = ds.Tables[0].Rows[0]["LetterCreateDate"].ToString();
                txtInwardDate.Text = ds.Tables[0].Rows[0]["ReceivingDate"].ToString();
                txtFile_No.Text = ds.Tables[0].Rows[0]["File_No"].ToString();
                ddlFile_Priority.ClearSelection();
                ddlFile_Priority.Items.FindByText(ds.Tables[0].Rows[0]["File_Priority"].ToString()).Selected = true;
                txtSubject.Text = ds.Tables[0].Rows[0]["File_Title"].ToString();
                txtReceivingFrom.Text = ds.Tables[0].Rows[0]["LetterFrom"].ToString();
                txtAddressTo.Text = ds.Tables[0].Rows[0]["AddressTo"].ToString();
                ViewState["Document_Upload"] = ds.Tables[0].Rows[0]["Document_Upload"].ToString();
                if (ViewState["Document_Upload"] != "")
                {
                    hyprDoc.NavigateUrl = "../Uploads/" + ViewState["Document_Upload"].ToString();
                    hyprDoc.Visible = true;
                }
                txtQRCode.Text = ds.Tables[0].Rows[0]["QRCode"].ToString();
                txtQRCode.Attributes.Add("readonly", "readonly");
                txtRemark.Text = ds.Tables[0].Rows[0]["File_Description"].ToString();
                BtnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string ReceivingDate = "";
            string ProfileImagePath = "";

            if (txtCreateDate.Text == "")
            {
                msg += "select Create Date. \n";
            }
            if (txtInwardDate.Text == "")
            {
                msg += "select Receving Date. \n";
            }
            if (txtFile_No.Text.Trim() == "")
            {
                msg += "Enter File No. \n";
            }
            if (ddlFile_Priority.SelectedIndex < 0)
            {
                msg += "Select File Priority \n";
            }
            if (txtReceivingFrom.Text.Trim() == "")
            {
                msg += "Enter Receving From \n";
            }
            if (txtAddressTo.Text.Trim() == "")
            {
                msg += "Enter Address To \n";
            }
            if (txtInwardDate.Text != "")
            {
                ReceivingDate = Convert.ToDateTime(txtInwardDate.Text, cult).ToString("yyyy/MM/dd");
            }
            else
            {
                ReceivingDate = "";
            }
            if (UploadFile.HasFile)
            {
                ProfileImagePath = "../filetracking/Uploads/" + Guid.NewGuid() + "-" + UploadFile.FileName;
                UploadFile.PostedFile.SaveAs(Server.MapPath(ProfileImagePath));
            }
            if (msg.Trim() == "")
            {
                if (BtnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("SpFTComposeFile",
                new string[] { "flag", "File_No", "QRCode", "Office_ID" },
                new string[] { "3", txtFile_No.Text, txtQRCode.Text, ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0 || ds.Tables[1].Rows.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            msg += "Letter No is Already Exist .\\n";
                            txtFile_No.Text = "";
                        }
                        if (ds.Tables[1].Rows.Count != 0)
                        {
                            msg += "Bar Code is Already Exist .\\n";
                            txtQRCode.Text = "";
                        }
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                        //ClearText();
                    }
                    else
                    {
                        ds = objdb.ByProcedure("SpFTComposeFile",
               new string[] { "flag", "File_IsActive", "Office_ID", "File_Type", "File_Priority", "File_No", "File_Title", "Document_Upload", "File_Description", "Department_ID", "LetterCreateDate", "ReceivingDate", "LetterFrom", "AddressTo", "QRCode", "File_UpdatedBy", "FileCreatedbyOpt" },
               new string[] { "1", "1", ViewState["Office_ID"].ToString(), "Letter", ddlFile_Priority.SelectedItem.Text, txtFile_No.Text, txtSubject.Text, ProfileImagePath, txtRemark.Text, ViewState["Department_ID"].ToString(), Convert.ToDateTime(txtCreateDate.Text, cult).ToString("yyyy/MM/dd"), ReceivingDate, txtReceivingFrom.Text, txtAddressTo.Text, txtQRCode.Text, ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                        string fileID = ds.Tables[0].Rows[0]["File_ID"].ToString();


                        objdb.ByProcedure("SpFTComposeFile",
                        new string[] { "flag", "Forwarded_IsActive", "File_ID", "Emp_ID", "File_Status" },
                        new string[] { "4", "1", fileID, ViewState["Emp_ID"].ToString(), "Available" }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        ClearText();
                    }
                }
                else if (BtnSave.Text == "Edit")
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
                        new string[] { "flag", "File_ID", "File_Type", "File_Priority", "File_No", "File_Title", "Document_Upload", "File_Description", "LetterCreateDate", "ReceivingDate", "LetterFrom", "AddressTo", "File_UpdatedBy" },
                        new string[] { "14", ViewState["File_ID"].ToString(), "Letter", ddlFile_Priority.SelectedItem.Text, txtFile_No.Text, txtSubject.Text, ProfileImagePath, txtRemark.Text, Convert.ToDateTime(txtCreateDate.Text, cult).ToString("yyyy/MM/dd"), ReceivingDate, txtReceivingFrom.Text, txtAddressTo.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        BtnSave.Text = "Save";
                        ClearText();
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
    protected void ClearText()
    {
        ddlFile_Priority.ClearSelection();
        txtFile_No.Text = "";
        ddlFile_Priority.ClearSelection();
        txtRemark.Text = "";
        txtReceivingFrom.Text = "";
        txtAddressTo.Text = "";
        txtSubject.Text = "";
        txtInwardDate.Text = "";
        txtQRCode.Text = "";
    }
}