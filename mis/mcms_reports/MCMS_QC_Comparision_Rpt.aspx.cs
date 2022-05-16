using ClosedXML.Excel;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_mcms_reports_MCMS_QC_Comparision_Rpt : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.SessionID != null)
        {
            if (!IsPostBack)
            {

                txtFdt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTdt.Text = DateTime.Now.ToString("dd/MM/yyyy");

                GetDS(sender, e);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    private void ExportGridToExcel()
    {
        try
        { 

            ds = null;
            ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                  new string[] { "flag", "Office_Parant_ID ", "OfficeID", "fromDate", "Todate" },
                                  new string[] { "10", ddlDSName3.SelectedValue, ddlCCName3.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt);

                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;filename=" + "MCMS_QC_Comparision_Rpt" + DateTime.Now + ".xlsx");
                            using (MemoryStream MyMemoryStream = new MemoryStream())
                            {
                                wb.SaveAs(MyMemoryStream);
                                MyMemoryStream.WriteTo(Response.OutputStream);
                                Response.Flush();
                                Response.End();
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Data Not Fount");
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Data Not Fount");
                }
            }
            else
            {
                lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Data Not Fount");
            }


        }

        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }


    protected void GetDS(object sender, EventArgs e)
    {
        try
        {
            //DataSet ds1 = apiprocedure.ByProcedure("SpAdminOffice",
                                  //new string[] { "flag", "OfficeType_ID" },
                                  //new string[] { "5", "2" }, "dataset");
			 DataSet ds1 = apiprocedure.ByProcedure("SpAdminOffice",
                                  new string[] { "flag", "OfficeType_ID", "Office_ID" },
                                  new string[] { "56",apiprocedure.OfficeType_ID(),apiprocedure.Office_ID() }, "dataset");


            ddlDSName3.DataSource = ds1;
            ddlDSName3.DataTextField = "Office_Name";
            ddlDSName3.DataValueField = "Office_ID";
            ddlDSName3.DataBind();
            //ddlDSName3.Items.Insert(0, new ListItem("Select", "0"));

            //ddlDSName3.SelectedValue = apiprocedure.Office_ID();
            ddlDSName3.Enabled = false;
            ddlDSName3_SelectedIndexChanged(sender, e);


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ex.Message.ToString());
        }
    }

    protected void ddlDSName3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // if (ddlDSName3.SelectedIndex != 0)
            //{
                ddlCCName3.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                              new string[] { "flag", "Office_Parant_ID" },
                              new string[] { "22", ddlDSName3.SelectedValue }, "dataset");
                ddlCCName3.DataTextField = "Office_Name";
                ddlCCName3.DataValueField = "Office_ID";
                ddlCCName3.DataBind();
                ddlCCName3.Items.Insert(0, new ListItem("All", "0"));
				if(apiprocedure.OfficeType_ID() == "4")
                {
                    ddlCCName3.SelectedValue = apiprocedure.Office_ID();
                    ddlCCName3.Enabled = false;
                }

           // }
           // else
            //{

                //ddlCCName3.DataSource = string.Empty;
               // ddlCCName3.DataBind();
               // ddlCCName3.Items.Insert(0, new ListItem("All", "0"));
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7:" + ex.Message.ToString());
        }
    }


    protected void btnCCWiseTankerQCReport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            ds = null;
            ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                  new string[] { "flag", "Office_Parant_ID ", "OfficeID", "fromDate", "Todate" },
                                  new string[] { "10", ddlDSName3.SelectedValue, ddlCCName3.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_CCWiseTankerSealReport.DataSource = ds;
                        gv_CCWiseTankerSealReport.DataBind();
                    }
                    else
                    {
                        gv_CCWiseTankerSealReport.DataSource = string.Empty;
                        gv_CCWiseTankerSealReport.DataBind();
                    }
                }
                else
                {
                    gv_CCWiseTankerSealReport.DataSource = string.Empty;
                    gv_CCWiseTankerSealReport.DataBind();
                }
            }
            else
            {
                gv_CCWiseTankerSealReport.DataSource = string.Empty;
                gv_CCWiseTankerSealReport.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 6:" + ex.Message.ToString());
        }
    }



}