using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Masters_OfficeItemBillingHeadMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    APIProcedure objdb1 = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["ItemBillingHead_ID"] = "0";
                    FillGrid();
                    lblMsg.Text = "";
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
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
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
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
            string IsActive = "1";
            if (txtItemBillingHead_Name.Text.Trim() == "")
            {
                msg += "Enter ItemBillingHead Name";
            }
            //if (txtItemBillingHead_Code.Text.Trim() == "")
            //{
            //    msg += "Enter ItemBillingHead Code";
            //}
            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
                       new string[] { "flag", "ItemBillingHead_Name", "ItemBillingHead_Type", "ItemBillingHead_ID" },
                       new string[] { "4", txtItemBillingHead_Name.Text.Trim(), ddlItemBillingHead_Type.SelectedValue, ViewState["ItemBillingHead_ID"].ToString() }, "dataset");


                if (btnSave.Text == "Save" && ViewState["ItemBillingHead_ID"].ToString() == "0" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
                    new string[] { "flag", "IsActive", "ItemBillingHead_Name", "ItemBillingHead_Type","ItemBillingHead_Code", "CreatedBy", "ItemBillingHead_Remark", "CreatedByIP" },
                    new string[] { "0", IsActive, txtItemBillingHead_Name.Text.Trim(), ddlItemBillingHead_Type.SelectedValue,"0", ViewState["Emp_ID"].ToString(), txtItemBillingHead_Remark.Text, objdb1.GetLocalIPAddress() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }


                else if (btnSave.Text == "Edit" && ViewState["ItemBillingHead_ID"].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
                    new string[] { "flag", "ItemBillingHead_ID", "ItemBillingHead_Name", "ItemBillingHead_Type", "CreatedBy", "ItemBillingHead_Remark", "CreatedByIP" },
                    new string[] { "5", ViewState["ItemBillingHead_ID"].ToString(), txtItemBillingHead_Name.Text.Trim(), ddlItemBillingHead_Type.SelectedValue, ViewState["Emp_ID"].ToString(), txtItemBillingHead_Remark.Text, objdb1.GetLocalIPAddress() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This ItemBillingHead Is Already Exist.");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This ItemBillingHead  Is Already Exist');", true);
                }

                ddlItemBillingHead_Type.ClearSelection();
                txtItemBillingHead_Name.Text = "";
                txtItemBillingHead_Remark.Text = "";
               // txtItemBillingHead_Code.Text = "";
                btnSave.Text = "Save";
                ViewState["ItemBillingHead_ID"] = "0";
                FillGrid();
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

    //protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
    //        CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
    //        string ItemBillingHead_ID = chk.ToolTip.ToString();
    //        string IsActive = "0";
    //        if (chk != null & chk.Checked)
    //        {
    //            IsActive = "1";
    //        }
    //        objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
    //                   new string[] { "flag", "IsActive", "ItemBillingHead_ID", "CreatedBy" },
    //                   new string[] { "6", IsActive, ItemBillingHead_ID, ViewState["Emp_ID"].ToString() }, "dataset");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ViewState["ItemBillingHead_ID"] = GridView1.SelectedValue.ToString();
    //        lblMsg.Text = "";
    //        ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
    //                   new string[] { "flag", "ItemBillingHead_ID" },
    //                   new string[] { "3", ViewState["ItemBillingHead_ID"].ToString() }, "dataset");

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtItemBillingHead_Name.Text = ds.Tables[0].Rows[0]["ItemBillingHead_Name"].ToString();
    //            txtItemBillingHead_Remark.Text = ds.Tables[0].Rows[0]["ItemBillingHead_Remark"].ToString();
    //           // txtItemBillingHead_Code.Text = ds.Tables[0].Rows[0]["ItemBillingHead_Code"].ToString();
    //            ddlItemBillingHead_Type.SelectedValue = ds.Tables[0].Rows[0]["ItemBillingHead_Type"].ToString();
    //            btnSave.Text = "Edit";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
}