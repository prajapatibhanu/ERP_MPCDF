using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_filetracking_FTOutward : System.Web.UI.Page
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
                    txtDispatchDate.Attributes.Add("readonly", "readonly");
                    hyprDoc.Visible = false;
                    DivCopyTo.Visible = true;
                    AddcopyTo();
                    if (Request.QueryString["Outward_ID"] != null)
                    {
                        ViewState["Outward_ID"] = objdb.Decrypt(Request.QueryString["Outward_ID"].ToString());
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
            ds = objdb.ByProcedure("SpFTOutwardFiles",
                     new string[] { "flag", "Outward_ID" },
                     new string[] { "6", ViewState["Outward_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtDispatchDate.Text = ds.Tables[0].Rows[0]["DispatchDate"].ToString();
                txtLetterNo.Text = ds.Tables[0].Rows[0]["LetterNo"].ToString();
                txtLetterSubject.Text = ds.Tables[0].Rows[0]["LetterSubject"].ToString();
                txtEndorsementNumber.Text = ds.Tables[0].Rows[0]["EndorsementNo"].ToString();
                txtLetterReceiveFrom.Text = ds.Tables[0].Rows[0]["LetterReceiveFrom"].ToString();
                txtAddressTo.Text = ds.Tables[0].Rows[0]["AddressTo"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                txtForwardDepartment.Text = ds.Tables[0].Rows[0]["ForwardToDepartment"].ToString();
                txtForwardOfficer.Text = ds.Tables[0].Rows[0]["ForwardToOfficer"].ToString();
                ViewState["Doc1"] = ds.Tables[0].Rows[0]["Doc1"].ToString();
                if (ViewState["Doc1"] != "")
                {
                    hyprDoc.NavigateUrl = "../Uploads/" + ViewState["Doc1"].ToString();
                    hyprDoc.Visible = true;
                }
                DataTable dt = ds.Tables[1];
                ViewState["dt"] = dt;
                GridView1.DataSource = ds.Tables[1];
                GridView1.DataBind();
                DivCopyTo.Visible = true;
                //FillCopyToDetail();
                btnForward.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCopyToDetail()
    {
        try
        {
            ds = objdb.ByProcedure("SpFTOutwardFiles",
                     new string[] { "flag", "Outward_ID" },
                     new string[] { "6", ViewState["Outward_ID"].ToString() }, "dataset");
            if (ds.Tables[1].Rows.Count != 0)
            {
                GridView1.DataSource = ds.Tables[1];
                GridView1.DataBind();
                GridView1.Columns[2].Visible = false;
                //foreach(GridViewRow row in GridView1.Rows)
                //{
                //    LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
                //    lnkDelete.Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void AddcopyTo()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("CopyTo", typeof(string)));
        ViewState["dt"] = dt;

    }
    protected void ClearText()
    {
        txtLetterNo.Text = "";
        txtLetterSubject.Text = "";
        txtLetterReceiveFrom.Text = "";
        txtAddressTo.Text = "";
        txtRemark.Text = "";
        txtEndorsementNumber.Text = "";
        txtDispatchDate.Text = "";
        txtForwardDepartment.Text = "";
        txtForwardOfficer.Text = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
        AddcopyTo();
    }
    protected void btnForward_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string IsActive = "1";
            string ProfileImagePath = "";
            if (txtDispatchDate.Text == "")
            {
                msg += "Select Dispatch Date. \n";
            }
            if (txtLetterNo.Text.Trim() == "")
            {
                msg += "Enter Letter No. \n";
            }
            if (txtLetterSubject.Text.Trim() == "")
            {
                msg += "Enter letter Subject \n";
            }
            if (txtLetterReceiveFrom.Text.Trim() == "")
            {
                msg += "Enter Letter Receive From. \n";
            }
            if (txtAddressTo.Text.Trim() == "")
            {
                msg += "Enter Address To. \n";
            }
            if (txtRemark.Text.Trim() == "")
            {
                msg += "Enter Remark. \n";
            }
            if (txtForwardDepartment.Text.Trim() == "")
            {
                msg += "Enter Forward To Department. \n";
            }
            if (txtForwardOfficer.Text.Trim() == "")
            {
                msg += "Enter Forward To Officer. \n";
            }
            if (FileUpload1.HasFile)
            {
                ProfileImagePath = "../filetracking/Uploads/" + Guid.NewGuid() + "-" + FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath(ProfileImagePath));
            }
            if (msg.Trim() == "")
            {
                if (btnForward.Text == "Forward")
                {
                    ds = objdb.ByProcedure("SpFTOutwardFiles",
                     new string[] { "flag", "LetterNo" },
                     new string[] { "5", txtLetterNo.Text }, "dataset");
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        msg += "Letter No is Already Exists";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                    }
                    else
                    {
                        DataSet ds1 = objdb.ByProcedure("SpFTOutwardFiles",
               new string[] { "flag", "LetterNo", "LetterSubject", "DispatchDate", "EndorsementNo", "LetterReceiveFrom", "AddressTo", "Remark", "Doc1", "ForwardToDepartment", "ForwardToOfficer", "IsActive", "Office_ID", "Outward_Updatedby" },
               new string[] { "0", txtLetterNo.Text, txtLetterSubject.Text, Convert.ToDateTime(txtDispatchDate.Text, cult).ToString("yyyy/MM/dd"), txtEndorsementNumber.Text, txtLetterReceiveFrom.Text, txtAddressTo.Text, txtRemark.Text, ProfileImagePath, txtForwardDepartment.Text, txtForwardOfficer.Text, IsActive, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                        if (ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                        {
                            string Outward_ID = ds1.Tables[0].Rows[0]["Outward_ID"].ToString();
                            foreach (GridViewRow row in GridView1.Rows)
                            {
                                Label lblCopyTo = (Label)row.FindControl("lblCopyTo");
                                objdb.ByProcedure("SpFTOutwardFiles",
                                new string[] { "flag", "Outward_ID", "CopyTo", "Outward_Updatedby" },
                                new string[] { "3", Outward_ID, lblCopyTo.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                            }
                        }
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        ClearText();
                    }
                }
                else if (btnForward.Text == "Edit")
                {
                    try
                    {
                        if (ProfileImagePath == "")
                        {
                            ProfileImagePath = ViewState["Doc1"].ToString();
                        }
                        objdb.ByProcedure("SpFTOutwardFiles",
                        new string[] { "flag", "Outward_ID", "LetterNo", "LetterSubject", "DispatchDate", "EndorsementNo", "LetterReceiveFrom", "AddressTo", "Remark", "Doc1", "ForwardToDepartment", "ForwardToOfficer", "IsActive", "Outward_Updatedby" },
                        new string[] { "7", ViewState["Outward_ID"].ToString(), txtLetterNo.Text, txtLetterSubject.Text, Convert.ToDateTime(txtDispatchDate.Text, cult).ToString("yyyy/MM/dd"), txtEndorsementNumber.Text, txtLetterReceiveFrom.Text, txtAddressTo.Text, txtRemark.Text, ProfileImagePath, txtForwardDepartment.Text, txtForwardOfficer.Text, "0", ViewState["Emp_ID"].ToString() }, "dataset");
                        if(GridView1.Rows.Count > 0)
                        {
                            objdb.ByProcedure("SpFTOutwardFiles",
                                new string[] { "flag", "Outward_ID"},
                                new string[] { "10", ViewState["Outward_ID"].ToString() }, "dataset");
                            foreach (GridViewRow row in GridView1.Rows)
                            {
                                Label lblCopyTo = (Label)row.FindControl("lblCopyTo");
                                objdb.ByProcedure("SpFTOutwardFiles",
                                new string[] { "flag", "Outward_ID", "CopyTo", "Outward_Updatedby" },
                                new string[] { "3", ViewState["Outward_ID"].ToString(), lblCopyTo.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                            }
                        }
                        
                        btnForward.Text = "Forward";
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        
                        DivCopyTo.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
    protected void FillGrid()
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dt"];
            dt.Rows.Add(txtCopyTo.Text);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            txtCopyTo.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void OnDelete(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        DataTable dt = ViewState["dt"] as DataTable;
        dt.Rows.RemoveAt(row.RowIndex);
        ViewState["dt"] = dt;
        GridView1.DataSource = ViewState["dt"];
        GridView1.DataBind();
    }
}