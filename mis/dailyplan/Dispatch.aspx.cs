using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_dailyplan_Dispatch : System.Web.UI.Page
{
    DataSet ds, ds2;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillDropdown();
                txtDispatchDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDispatchDate.Attributes.Add("ReadOnly", "ReadOnly");
                divbtn.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void FillDropdown()
    {
        try
        {
            ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds;
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));

             //   ddlShift.Items.FindByValue("1").Selected = true;
            }
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                new string[] { "flag", "Office_ID" },
                new string[] { "0", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProductSection.DataSource = ds.Tables[0];
                ddlProductSection.DataTextField = "ProductSection_Name";
                ddlProductSection.DataValueField = "ProductSection_ID";
                ddlProductSection.DataBind();

            }
            ddlProductSection.Items.Insert(0, new ListItem("Select", "0"));
            // ddlProductSectionTo.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        lblMsg.Text = "";
        string msg = "";
        if (txtDispatchDate.Text == "")
        {
            msg += "Select Dispatch Date. \\n";
        }
        if (ddlShift.SelectedIndex <= 0)
        {
            msg += "Select Shift. \\n";
        }
        if (ddlProductSection.SelectedIndex <= 0)
        {
            msg += "Select Product Section. \\n";
        }
        string Status = "";
        foreach (GridViewRow grv in GridView1.Rows)
        {
            CheckBox chkSelect = (CheckBox)grv.FindControl("chkSelect");
            if (chkSelect.Checked == true)
                Status = "1";
        }
        if (Status == "")
        {
            msg += "Select at least one item. \\n";
        }
        if (msg == "")
        {
            string Office_ID = ViewState["Office_ID"].ToString();
            string Emp_ID = ViewState["Emp_ID"].ToString();

            string TranDt = Convert.ToDateTime(txtDispatchDate.Text, cult).ToString("yyyy-MM-dd").ToString();
            string Shift_id = ddlShift.SelectedValue.ToString();
            string SenderSection_ID = ddlProductSection.SelectedValue.ToString();
            string SaveStatus = "";
            int rw = 1;
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                DropDownList ddlProductSectionTo = (DropDownList)gr.FindControl("ddlProductSectionTo");
                TextBox txtOutward = (TextBox)gr.FindControl("txtOutwardQuantity");
                if (chkSelect.Checked == true && ddlProductSectionTo.SelectedIndex > 0 && txtOutward.Text != "")
                {
                    Label lblItemid = (Label)gr.FindControl("lblItemid");
                    Label lblParticular = (Label)gr.FindControl("lblParticular");
                    Label lblBatchNo = (Label)gr.FindControl("lblBatchNo");
                    Label lblLotNo = (Label)gr.FindControl("lblLotNo");
                    TextBox txtRemark = (TextBox)gr.FindControl("txtRemark");

                    string BatchNo = lblBatchNo.Text;
                    string LotNo = lblLotNo.Text;
                    string TransactionType = "Dispatch";
                    string ReceiverSection_ID = "";

                    //if (ddlBatchNo.SelectedIndex > 0)
                    //    BatchNo = ddlBatchNo.SelectedValue.ToString();

                    //if (ddlSerialLotNo.SelectedIndex > 0)
                    //    LotNo = ddlSerialLotNo.SelectedValue.ToString();


                    ReceiverSection_ID = ddlProductSectionTo.SelectedValue.ToString();
                    ds = null;
                    ds = objdb.ByProcedure("spProductionItemStock",
                        new string[] { "flag", "Shift_id", "TranDt","Item_id","Outward", "BatchNo", "LotNo", "TransactionType", "SenderSection_ID"
                                     , "SenderOffice_ID", "SenderID", "SenderType", "Item_IsActive","ReceiverType",  "ReceiverOffice_ID", "ReceiverSection_ID", "ReceivingRemark" },
                        new string[] { "9", Shift_id, TranDt, lblItemid.Text, txtOutward.Text, BatchNo, LotNo, TransactionType, SenderSection_ID
                                     ,Office_ID,Emp_ID,"Section","1","Section",Office_ID,ReceiverSection_ID,txtRemark.Text}, "dataset");

                    if (ds != null)
                    {
                        string status = ds.Tables[0].Rows[0]["Status"].ToString();
                        if (status == "True")
                        {
                            SaveStatus = "1";
                        }                          
                        else
                        {
                            if(rw == 1)
                            {
                                msg += "Item not submitted because available quantity is less than outward quantity. \\n Item List :-  \\n";
                                msg +=(rw).ToString() +". "+ lblParticular.Text + " - Available Stock : " + ds.Tables[0].Rows[0]["AvalableItemStock"].ToString() + "\\n";
                                rw = rw + 1;
                            }
                            else
                            {
                                msg += (rw).ToString() + ". " + lblParticular.Text + " - Available Stock : " + ds.Tables[0].Rows[0]["AvalableItemStock"].ToString() + "\\n";
                                rw = rw + 1;
                            }

                        }
                    }
                  
                }



                
            }
            if (SaveStatus == "1")
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Submitted.");

            if (msg != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
            FillGrid();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        lblMsg.Text = "";
        string msg = "";
        string Shiftid = "0";
        if (txtDispatchDate.Text == "")
        {
            msg += "Select Dispatch Date. \\n";
        }
        if (ddlShift.SelectedIndex <= 0)
        {
            msg += "Select Shift. \\n";
        }
        if (ddlProductSection.SelectedIndex <= 0)
        {
            msg += "Select Product Section. \\n";
        }
        if (msg == "")
        {

            FillGrid();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }
    }

    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            DataSet ds1, ds2;
            string SenderOffice_ID = ViewState["Office_ID"].ToString();
            string SenderSection_ID = ddlProductSection.SelectedValue.ToString();


            ds2 = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                     new string[] { "flag", "Office_ID", "ProductSection_ID" },
                     new string[] { "5", SenderOffice_ID, SenderSection_ID }, "dataset");




            ds = objdb.ByProcedure("spProductionItemStock",
                new string[] { "flag", "SenderOffice_ID ", "SenderSection_ID", "TranDt" },
                new string[] { "7", SenderOffice_ID, SenderSection_ID, Convert.ToDateTime(txtDispatchDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    divbtn.Visible = true;
                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        DropDownList ddlProductSectionTo = (DropDownList)gr.FindControl("ddlProductSectionTo");
                        if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                        {
                            ddlProductSectionTo.DataSource = ds2.Tables[0];
                            ddlProductSectionTo.DataTextField = "ProductSection_Name";
                            ddlProductSectionTo.DataValueField = "ProductSection_ID";
                            ddlProductSectionTo.DataBind();

                        }
                        ddlProductSectionTo.Items.Insert(0, new ListItem("Select", "0"));
                        //Label lblItemid = (Label)gr.FindControl("lblItemid");
                        //Label lblOpeningDate = (Label)gr.FindControl("lblOpeningDate");
                        //DropDownList ddlBatchNo = (DropDownList)gr.FindControl("ddlBatchNo");
                        //DropDownList ddlSerialLotNo = (DropDownList)gr.FindControl("ddlSerialLotNo");

                        //Label lblQualityTest = (Label)gr.FindControl("lblQualityTest");

                        //string OpeningDate = "";
                        //if (lblOpeningDate.Text != "")
                        //{
                        //    OpeningDate = Convert.ToDateTime(lblOpeningDate.Text, cult).ToString("yyyy-MM-dd");
                        //}
                        //ds1 = objdb.ByProcedure("spProductionItemStock",
                        //    new string[] { "flag", "SenderOffice_ID ", "SenderSection_ID", "TranDt", "OpeningTranDt", "Item_id", "Test_Result" },
                        //    new string[] { "8", SenderOffice_ID, SenderSection_ID, Convert.ToDateTime(txtDispatchDate.Text, cult).ToString("yyyy-MM-dd"), OpeningDate, lblItemid.Text, lblQualityTest.Text }, "dataset");
                        //if (ds1 != null)
                        //{
                        //    if (ds1.Tables[0].Rows.Count > 0)
                        //    {
                        //        ddlBatchNo.DataSource = ds1.Tables[0];
                        //        ddlBatchNo.DataTextField = "BatchNo";
                        //        ddlBatchNo.DataValueField = "BatchNo";
                        //        ddlBatchNo.DataBind();

                        //        ddlSerialLotNo.DataSource = ds1.Tables[0];
                        //        ddlSerialLotNo.DataTextField = "LotNo";
                        //        ddlSerialLotNo.DataValueField = "LotNo";
                        //        ddlSerialLotNo.DataBind();

                        //    }
                        //    //if (ds1.Tables[1].Rows.Count > 0)
                        //    //{
                        //    //    ddlSerialLotNo.DataSource = ds1.Tables[1];
                        //    //    ddlSerialLotNo.DataTextField = "LotNo";
                        //    //    ddlSerialLotNo.DataValueField = "LotNo";
                        //    //    ddlSerialLotNo.DataBind();

                        //    //}
                        //}

                        //ddlBatchNo.Items.Insert(0, new ListItem("Select", "0"));
                        //ddlSerialLotNo.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
    protected void ddlProductSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            divbtn.Visible = false; 
            // ddlProductSectionTo.Items.Clear();
            if (ddlProductSection.SelectedIndex > 0)
            {
                //ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                //     new string[] { "flag", "Office_ID", "ProductSection_ID" },
                //     new string[] { "5", ViewState["Office_ID"].ToString(), ddlProductSection.SelectedValue.ToString() }, "dataset");

                //if (ds != null && ds.Tables[0].Rows.Count > 0)
                //{
                //    ddlProductSectionTo.DataSource = ds.Tables[0];
                //    ddlProductSectionTo.DataTextField = "ProductSection_Name";
                //    ddlProductSectionTo.DataValueField = "ProductSection_ID";
                //    ddlProductSectionTo.DataBind();

                //}
                FillGrid();
            }
            // ddlProductSectionTo.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    //protected void ddlBatchNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList ddlLabTest = (DropDownList)sender;
    //    DataGridItem row = (DataGridItem)ddlLabTest.NamingContainer;
    //    DropDownList ddlAddLabTestShortName = (DropDownList)row.FindControl("ddlBatchNo");

    //    ddlAddLabTestShortName.SelectedIndex = intSelectedIndex;
    //}
}