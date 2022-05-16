using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
public partial class mis_filetracking_FTComposeFileByOperator : System.Web.UI.Page
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
                    FillDropdown();
                    FillGrid();
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
    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployeeList.DataSource = ds;
                ddlEmployeeList.DataTextField = "Emp_Name";
                ddlEmployeeList.DataValueField = "Emp_ID";
                ddlEmployeeList.DataBind();
                ddlEmployeeList.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = objdb.ByProcedure("SpFTComposeFile",
                        new string[] { "flag", "FileCreatedbyOpt" },
                        new string[] { "20", ViewState["Emp_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label FilePriority = (Label)row.FindControl("FilePriority");
                    LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
                    LinkButton lnkInfo = (LinkButton)row.FindControl("lnkInfo");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    string File_ID = FilePriority.ToolTip.ToString();
                    ds = objdb.ByProcedure("SpFTComposeFile",
                        new string[] { "flag", "File_ID" },
                        new string[] { "11", File_ID }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        if (ds.Tables[0].Rows[0]["ForwardCount"].ToString() == "0")
                        {
                            //row.Visible = true;
                            lnkDelete.Visible = true;
                            lnkInfo.Visible = false;
                            lnkEdit.Visible = true;
                        }
                        else
                        {
                            //row.Visible = false;
                            lnkDelete.Visible = false;
                            lnkInfo.Visible = true;
                            lnkInfo.Enabled = false;
                            lnkEdit.Visible = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        try
        {
            ddlFile_Type.ClearSelection();
            ddlFile_Priority.ClearSelection();
            ddlEmployeeList.ClearSelection();
            txtFile_No.Text = "";
            txtFile_Title.Text = "";
            txtQRCode.Text = "";
            txtCreateDate.Text = "";
            txtFile_Description.Text = "";
            hyprDoc.Visible = false;
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
            if (ddlEmployeeList.SelectedIndex < 0)
            {
                msg += "Select Employee Name \n";
            }
            if (txtCreateDate.Text.Trim() == "")
            {
                msg += "Select Create Date \n";
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
                    new string[] { "flag", "File_No" },
                    new string[] { "3", txtFile_No.Text }, "dataset");
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //msg = "File No. is Already Exist";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + txtFile_No.Text + " File No. is Already Exist.');", true);
                    }
                    else
                    {
                        ds = objdb.ByProcedure("SpFTComposeFile",
                    new string[] { "flag", "File_IsActive", "Office_ID", "File_Type", "File_Priority", "File_No", "File_Title", "Document_Upload", "File_Description", "Department_ID", "QRCode", "File_UpdatedBy", "File_UpdatedOn", "FileCreatedbyOpt" },
                    new string[] { "22", File_IsActive, ViewState["Office_ID"].ToString(), ddlFile_Type.SelectedItem.Text, ddlFile_Priority.SelectedItem.Text, txtFile_No.Text, txtFile_Title.Text, ProfileImagePath, txtFile_Description.Text, ViewState["Department_ID"].ToString(), txtQRCode.Text, ddlEmployeeList.SelectedValue.ToString(), Convert.ToDateTime(txtCreateDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");

                        string fileID = ds.Tables[0].Rows[0]["File_ID"].ToString();

                        objdb.ByProcedure("SpFTComposeFile",
                        new string[] { "flag", "Forwarded_IsActive", "File_ID", "Emp_ID", "File_Status" },
                        new string[] { "4", Forwarded_IsActive, fileID, ddlEmployeeList.SelectedValue.ToString(), "Available" }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        FillGrid();
                        ClearText();
                    }
                }
                if (btnSave.Text == "Update")
                {
                    if (ProfileImagePath == "")
                    {
                        ProfileImagePath = ViewState["Document_Upload"].ToString();
                    }
                    ds = objdb.ByProcedure("SpFTComposeFile",
                        new string[] { "flag", "File_ID", "File_IsActive", "Office_ID", "File_Type", "File_Priority", "File_No", "File_Title", "Document_Upload", "File_Description", "Department_ID", "QRCode", "File_UpdatedBy", "File_UpdatedOn", "FileCreatedbyOpt" },
                        new string[] { "24", ViewState["EditFileID"].ToString(), File_IsActive, ViewState["Office_ID"].ToString(), ddlFile_Type.SelectedItem.Text, ddlFile_Priority.SelectedItem.Text, txtFile_No.Text, txtFile_Title.Text, ProfileImagePath, txtFile_Description.Text, ViewState["Department_ID"].ToString(), txtQRCode.Text, ddlEmployeeList.SelectedValue.ToString(), Convert.ToDateTime(txtCreateDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    FillGrid();
                    ClearText();
                    btnSave.Text = "Create";
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string FileID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            ds = objdb.ByProcedure("SpFTComposeFile",
                            new string[] { "flag", "File_ID" },
                            new string[] { "21", FileID }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ViewState["EditFileID"] = e.CommandArgument.ToString();
        if (e.CommandName == "RecordEdit")
        {
            DataSet dsEdit = objdb.ByProcedure("SpFTComposeFile",
                    new string[] { "flag", "File_ID" },
                    new string[] { "10", ViewState["EditFileID"].ToString() }, "dataset");
            if (dsEdit != null && dsEdit.Tables[0].Rows.Count > 0)
            {
                ddlFile_Type.ClearSelection();
                ddlFile_Type.Items.FindByValue(dsEdit.Tables[0].Rows[0]["File_Type"].ToString()).Selected = true;
                ddlFile_Priority.ClearSelection();
                ddlFile_Priority.Items.FindByText(dsEdit.Tables[0].Rows[0]["File_Priority"].ToString()).Selected = true;
                txtFile_No.Text = dsEdit.Tables[0].Rows[0]["File_No"].ToString();
                txtFile_Title.Text = dsEdit.Tables[0].Rows[0]["File_Title"].ToString();
                txtCreateDate.Text = dsEdit.Tables[0].Rows[0]["File_UpdatedOn"].ToString();
                ddlEmployeeList.ClearSelection();
                ddlEmployeeList.Items.FindByValue(dsEdit.Tables[0].Rows[0]["File_UpdatedBy"].ToString()).Selected = true;
                ViewState["Document_Upload"] = dsEdit.Tables[0].Rows[0]["Document_Upload"].ToString();
                if (ViewState["Document_Upload"] != "")
                {
                    hyprDoc.NavigateUrl = "../UploadsEdit/" + ViewState["Document_Upload"].ToString();
                    hyprDoc.Visible = true;
                }
                if (dsEdit.Tables[0].Rows[0]["QRCode"].ToString() != "" && dsEdit.Tables[0].Rows[0]["QRCode"].ToString() != null)
                {
                    txtQRCode.Text = dsEdit.Tables[0].Rows[0]["QRCode"].ToString();
                    txtQRCode.Attributes.Add("readonly", "readonly");
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ChangeColor()", true);
                txtFile_Description.Text = dsEdit.Tables[0].Rows[0]["File_Description"].ToString();
                btnSave.Text = "Update";
            }
        }
    }
}