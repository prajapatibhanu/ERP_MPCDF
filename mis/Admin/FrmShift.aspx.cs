using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Admin_FrmShift : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Shift_ID"] = "0";
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("Sp_Shift",
                new string[] { "flag" },
                new string[] { "2" }, "dataset");
            GridView1.DataSource = ds;
            GridView1.DataBind();

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
            string State_IsActive = "1";
            if (txtShift_Name.Text.Trim() == "")
            {
                msg += "Enter Shift Name";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Sp_Shift", new string[] { "flag", "ShiftName" }, new string[] { "1", txtShift_Name.Text }, "dataset");

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Shift Name Already Exist.');", true);
                            }
                        }
                        FillGrid();
                        txtShift_Name.Text = "";
                    }
                    else
                    {

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Shift Created Successfully");
                        txtShift_Name.Text = "";
                        FillGrid();
                    }
                }
                else
                {
                    ds = objdb.ByProcedure("Sp_Shift", new string[] { "flag", "ShiftName", "ShiftId" }, new string[] { "4", txtShift_Name.Text, ViewState["ShiftId"].ToString() }, "dataset");

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Shift Name Already Exist.');", true);
                            }
                        }
                        FillGrid();
                        txtShift_Name.Text = "";
                    }
                    else
                    {

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Shift Update Successfully");
                        txtShift_Name.Text = "";
                        FillGrid();
                    }
                
                }



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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ShiftId"] = GridView1.SelectedValue.ToString();
        lblMsg.Text = "";
        ds = objdb.ByProcedure("Sp_Shift",
                   new string[] { "flag", "ShiftId" },
                   new string[] { "3", ViewState["ShiftId"].ToString() }, "dataset");

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtShift_Name.Text = ds.Tables[0].Rows[0]["ShiftName"].ToString();
            btnSave.Text = "MODIFY";
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}