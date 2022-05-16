using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_dailyplan_RptSheetGhee : System.Web.UI.Page
{
    DataSet ds;
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
                FillOffice();
                FillProductSection();
                FillDropdown();
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
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
            //ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            //ds = objdb.ByProcedure("USP_Mst_ShiftMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlShift.DataSource = ds;
            //    ddlShift.DataTextField = "ShiftName";
            //    ddlShift.DataValueField = "Shift_id";
            //    ddlShift.DataBind();
            //    ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
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
    protected void FillProductSection()
    {
        try
        {

            //lblMsg.Text = "";
            //ddlProductSection.Items.Clear();
            //ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
            //     new string[] { "flag", "Office_ID" },
            //     new string[] { "0", ddlDS.SelectedValue.ToString() }, "dataset");

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlProductSection.DataSource = ds.Tables[0];
            //    ddlProductSection.DataTextField = "ProductSection_Name";
            //    ddlProductSection.DataValueField = "ProductSection_ID";
            //    ddlProductSection.DataBind();

            //}
            //ddlProductSection.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //FillGrid();
        FillReport();
    }

    protected void ddlDS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //lblSeletedInfo.Text = "";
            //FillProductSection();
            //GridView1.DataSource = new string[] { };
            //GridView1.DataBind();
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
            //lblSeletedInfo.Text = "";
            //GridView1.DataSource = new string[] { };
            //GridView1.DataBind();
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
            //GridView1.DataSource = new string[] { };
            //GridView1.DataBind();
            //lblSeletedInfo.Text = "";
            //ViewState["SelectedOffice"] = ddlDS.SelectedValue.ToString();
            //ViewState["SelectedSection"] = ddlProductSection.SelectedValue.ToString();
            //ViewState["TranDt"] = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
            //ViewState["Shift_ID"] = ddlShift.SelectedValue.ToString();

            //ds = objdb.ByProcedure("spProductionItemStock", new string[] { "flag", "ReceiverOffice_ID", "ReceiverSection_ID", "TranDt", "Shift_ID" }, new string[] { "10", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ViewState["TranDt"].ToString(), ViewState["Shift_ID"].ToString() }, "dataset");
            //if (ds != null && ds.Tables[0].Rows.Count != 0)
            //{
            //    GridView1.DataSource = ds.Tables[0];
            //    GridView1.DataBind();
            //    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    GridView1.UseAccessibleHeader = true;

            //}
            //if (ds != null)
            //{
            //    //btnSave.Visible = true;
            //}
            ///********************************/

            //lblSeletedInfo.Text = "<b>Dugdh Sangh :</b> " + ddlDS.SelectedItem.ToString() + " </p> <p><b>Product Section :</b> " + ddlProductSection.SelectedItem.ToString() + " </p> <p><b>Date :</b> " + txtOrderDate.Text.ToString() + " </p><p> <b>Shift :</b>   " + ddlShift.SelectedItem.ToString() + "</p> ";

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
            //lblMsg.Text = "";
            ////ViewState["SelectedOffice"] = ddlProductSection.SelectedValue.ToString();
            ////ViewState["SelectedSection"] = ddlProductSection.SelectedValue.ToString();
            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            //    Label ItemName = (Label)row.FindControl("ItemName");
            //    string Item_id = ItemName.ToolTip.ToString();
            //    TextBox LastTarget = (TextBox)row.FindControl("txtTarget");
            //    Label lblGenStatus = (Label)row.FindControl("lblGenStatus");

            //    if (chkSelect.Checked == true && lblGenStatus.Text != "Received")
            //    {
            //        //ViewState["TranDt"] = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
            //        ds = objdb.ByProcedure("spProductionItemStock", new string[] { "flag", "SenderOffice_ID", "ReceiverSection_ID", "TranDt", "Shift_ID", "ReceiverID", "ReceiverOffice_ID", "Item_id" }, new string[] { "4", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ViewState["TranDt"].ToString(), ViewState["Shift_ID"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), Item_id }, "dataset");
            //    }

            //}


            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            //FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void FillReport()
    {

        StringBuilder htmlStr = new StringBuilder();
        /****************/
        htmlStr.Append("<div class='row'>");
        htmlStr.Append("<div class='col-md-12'><div class='col-md-2 col-sm-2 col-xs-2'><h5>Book No.</h5><h5>Page No</h5></div><div class='col-md-8  col-sm-8 col-xs-8 text-center'><h3>BHOPAL SAHAKARI DUGDH SANGH MARYADIT</h3><h5>BHOPAL DAIRY PLANT HABIBGANJ - BHOPAL</h5><h5>DAILY GHEE ACCOUNT SHEET</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>PROD F-03</h5></div></div>");
        htmlStr.Append("<div class='col-md-12'><div>");

        /********** Table Start******/
        htmlStr.Append("<table class='table'>");
        htmlStr.Append("<tr class='text-center'>");
        htmlStr.Append("<th rowspan='2'>PARTICULAR</th>");
        htmlStr.Append("<th rowspan='2'>Loose</th>");
        htmlStr.Append("<th colspan='2'>1 Liter</th>");
        htmlStr.Append("<th colspan='2'>500 ml</th>");
        htmlStr.Append("<th colspan='2'>200 ml</th>");
        htmlStr.Append("<th colspan='2'>5 Liter</th>");
        htmlStr.Append("<th colspan='2'>15 Kg. Tin</th>");
        htmlStr.Append("<th rowspan='2'>Total(Liter)</th>");
        htmlStr.Append("</tr>");
        /***********************/
        htmlStr.Append("<tr><th>No.</th><th>Qty.</th><th>No.</th><th>Qty.</th><th>No.</th><th>Qty.</th><th>No.</th><th>Qty.</th><th>No.</th><th>Qty.</th></tr>");

        /***********************/

        ds = objdb.ByProcedure("spProductionSheetRpt",
                                               new string[] { "flag", "Office_ID", "Production_Date" },
                                               new string[] { "4", ddlDS.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), }, "dataset");

        if (ds.Tables.Count > 0)
        {


            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    htmlStr.Append("<tr>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["FromTo"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["Loose"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["1 Liter"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["1 Liter Qty"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["500 ml No"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["500 ml Qty"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["200 ml"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["200 ml Qty"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["5 Liter"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["5 Liter Qty"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["15 Kg Tin"].ToString() + "</td>");
            //    htmlStr.Append("<td>" + ds.Tables[1].Rows[0]["15 Kg Tin Qty"].ToString() + "</td>");
            //    float totalQty = float.Parse(ds.Tables[1].Rows[0]["Loose"] == DBNull.Value ? "0" : ds.Tables[1].Rows[0]["Loose"].ToString()) + float.Parse(ds.Tables[1].Rows[0]["1 Liter Qty"] == DBNull.Value ? "0" : ds.Tables[1].Rows[0]["1 Liter Qty"].ToString()) + float.Parse(ds.Tables[1].Rows[0]["500 ml Qty"] == DBNull.Value ? "0" : ds.Tables[1].Rows[0]["500 ml Qty"].ToString()) + float.Parse(ds.Tables[1].Rows[0]["200 ml Qty"] == DBNull.Value ? "0" : ds.Tables[1].Rows[0]["200 ml Qty"].ToString()) + float.Parse(ds.Tables[1].Rows[0]["5 Liter Qty"] == DBNull.Value ? "0" : ds.Tables[1].Rows[0]["5 Liter Qty"].ToString()) + float.Parse(ds.Tables[1].Rows[0]["15 Kg Tin Qty"] == DBNull.Value ? "0" : ds.Tables[1].Rows[0]["15 Kg Tin Qty"].ToString());
            //    htmlStr.Append("<td>" + totalQty.ToString() + "</td>");
            //    htmlStr.Append("</tr>");


            //}

            if (ds.Tables[0].Rows.Count > 0)
            {
                int Count2 = ds.Tables[0].Rows.Count;
                for (int p = 0; p < Count2; p++)
                {

                    htmlStr.Append("<tr>");

                    if (ds.Tables[0].Rows[p]["Particular"].ToString() == "TOTAL")
                    {
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["Particular"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["Loose"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["1 Liter"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["1 Liter Qty"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["500 ml No"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["500 ml Qty"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["200 ml"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["200 ml Qty"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["5 Liter"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["5 Liter Qty"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["15 Kg Tin"].ToString() + "</th>");
                        htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["15 Kg Tin Qty"].ToString() + "</th>");
                        if (ds.Tables[0].Rows[p]["Total(Liter)"].ToString()=="0")
                        {
                            htmlStr.Append("<th> </th>");
                        }else{
                            htmlStr.Append("<th>" + ds.Tables[0].Rows[p]["Total(Liter)"].ToString() + "</th>");
                        }
                        

                    }
                    else
                    {

                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["Particular"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["Loose"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["1 Liter"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["1 Liter Qty"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["500 ml No"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["500 ml Qty"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["200 ml"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["200 ml Qty"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["5 Liter"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["5 Liter Qty"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["15 Kg Tin"].ToString() + "</td>");
                        htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["15 Kg Tin Qty"].ToString() + "</td>");
                        if (ds.Tables[0].Rows[p]["Total(Liter)"].ToString() == "0")
                        {
                            htmlStr.Append("<td> </td>");
                        }
                        else
                        {

                            htmlStr.Append("<td>" + ds.Tables[0].Rows[p]["Total(Liter)"].ToString() + "</td>");
                        }

                    }
                    htmlStr.Append("</tr>");

                }
            }

        }
        /***********************/
        htmlStr.Append("</table>");
        /************** Table End *********/

        htmlStr.Append("</div></div>");
        htmlStr.Append("<div class='col-md-12 footerprint'><div class='col-md-4 col-sm-4 col-xs-4'><h5>D.G.M. Production</h5></div><div class='col-md-6  col-sm-6 col-xs-6 text-center'><h5>Manager(Product)</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>A.G.M. Production</h5></div></div>");
        htmlStr.Append("</div>");

        /****************/
        mainsheet.InnerHtml = htmlStr.ToString();


    }


}