using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_filetracking_EditOutwardDetail : System.Web.UI.Page
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
                    InsertDiv.Visible = false;
                    UpdateGrid.Visible = false;
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
    protected void AddcopyTo()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("CopyTo", typeof(string)));
        ViewState["dt"] = dt;
        btnSaveData.Visible = false;
    }
    protected void FillGrid()
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dt"];
            dt.Rows.Add(txtCopyTo.Text);
            GridView2.DataSource = dt;
            GridView2.DataBind();
            txtCopyTo.Text = "";
            btnSaveData.Visible = true;
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
            lblMsg.Text = "";
            lblGridMsg.Text = "";
            GridView2.DataSource = new string[] { };
            GridView2.DataBind();
            ds = objdb.ByProcedure("SpFTOutwardFiles",
                     new string[] { "flag", "LetterNo" },
                     new string[] { "8", txtLetterNo.Text }, "dataset");
            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                {
                    if (ds.Tables[1].Rows.Count != 0)
                    {
                        GridView1.DataSource = ds.Tables[1];
                        GridView1.DataBind();
                        InsertDiv.Visible = false;
                        UpdateGrid.Visible = true;
                    }
                    else
                    {
                        ViewState["Outward_ID"] = ds.Tables[2].Rows[0]["Outward_ID"].ToString();
                        InsertDiv.Visible = true;
                        UpdateGrid.Visible = false;
                        AddcopyTo();
                    }
                }
                else
                {
                    lblGridMsg.Text = "There is no Letter No. Exists...";
                }
            }
            else
            {

            }
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
        GridView2.DataSource = ViewState["dt"];
        GridView2.DataBind();
        if (dt.Rows.Count != 0)
        {
            btnSaveData.Visible = true;
        }
        else
        {
            btnSaveData.Visible = false;
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
    protected void btnSaveData_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                Label lblCopyTo = (Label)row.FindControl("lblCopyTo");
                objdb.ByProcedure("SpFTOutwardFiles",
                new string[] { "flag", "Outward_ID", "CopyTo", "Outward_Updatedby" },
                new string[] { "3", ViewState["Outward_ID"].ToString(), lblCopyTo.Text, ViewState["Emp_ID"].ToString() }, "dataset");
            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            InsertDiv.Visible = false;
            ViewState["dt"] = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //Update Detail
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillCopyToDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            LinkButton btn = (LinkButton)sender;
            GridViewRow gv = (GridViewRow)btn.NamingContainer;
            LinkButton lnkUpdate = (LinkButton)gv.FindControl("lnkUpdate");
            LinkButton lnkCancel = (LinkButton)gv.FindControl("lnkCancel");

            Label lblCopyTo = (Label)gv.FindControl("lblCopyTo");
            TextBox txtCopyTo = (TextBox)gv.FindControl("txtCopyTo");
            RequiredFieldValidator gvtxtCustName = (RequiredFieldValidator)gv.FindControl("gvtxtCopyTo");

            lblCopyTo.Visible = false;
            txtCopyTo.Visible = true;
            gvtxtCustName.Visible = true;

            lnkCancel.Visible = true;
            lnkUpdate.Visible = true;
            btn.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gv = (GridViewRow)btn.NamingContainer;
            Label lblCopyTo_ID = (Label)gv.FindControl("lblCopyTo_ID");

            TextBox txtCopyTo = (TextBox)gv.FindControl("txtCopyTo");

            objdb.ByProcedure("SpFTOutwardFiles", new string[] { "flag", "CopyTo_ID", "CopyTo", "Outward_Updatedby" },
                new string[] { "9", lblCopyTo_ID.Text, txtCopyTo.Text, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Updated Successfully");
            FillCopyToDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            LinkButton btn = (LinkButton)sender;
            GridViewRow gv = (GridViewRow)btn.NamingContainer;

            Label lblCopyTo = (Label)gv.FindControl("lblCopyTo");
            TextBox txtCopyTo = (TextBox)gv.FindControl("txtCopyTo");
            RequiredFieldValidator gvtxtCopyTo = (RequiredFieldValidator)gv.FindControl("gvtxtCopyTo");

            LinkButton lnkUpdate = (LinkButton)gv.FindControl("lnkUpdate");
            LinkButton lnkCancel = (LinkButton)gv.FindControl("lnkCancel");
            LinkButton lnkEdit = (LinkButton)gv.FindControl("lnkEdit");

            lblCopyTo.Visible = true;
            txtCopyTo.Visible = false;
            gvtxtCopyTo.Visible = false;

            lnkCancel.Visible = false;
            lnkUpdate.Visible = false;
            btn.Visible = false;
            lnkEdit.Visible = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}