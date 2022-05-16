using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Masters_RateChart_AMCU : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    APIProcedure objdb1 = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    GetData();
                    GetOfficeWiseRateDetail();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }


        }
    }

    private void GetOfficeWiseRateDetail()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("MilkCalculation",
                         new string[] { "flag", "OfficeId", "Item_id", "OfficeType_ID" },
                         new string[] { "9", objdb1.Office_ID(), objdb1.DcsRawMilkItemId_ID(),objdb.OfficeType_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {

                lblpurchaserate.Text = ds.Tables[0].Rows[0]["PurchaseRate"].ToString();
                lblDeductionRate1.Text = ds.Tables[0].Rows[0]["DeductionRate1"].ToString();
                lbllblDeductionRate2.Text = ds.Tables[0].Rows[0]["DeductionRate2"].ToString();
                lblEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();

                lblpurchaserate_SNF.Text = ds.Tables[0].Rows[0]["PurchaseRate"].ToString();
                lblDeductionRate1_SNF.Text = ds.Tables[0].Rows[0]["DeductionRate1"].ToString();
                lbllblDeductionRate2_SNF.Text = ds.Tables[0].Rows[0]["DeductionRate2"].ToString();
                lblEffectiveDate_SNF.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


    private void GetData()
    {

        try
        {
            if (objdb1.OfficeType_ID() == "2")
            {
                SAMCU.Visible = true;
                SNONAMCU.Visible = true;
                GetMilkQualityDetails();
                GetMilkQualityDetails_NonAMCU();
            }
            else
            {
               ds = objdb.ByProcedure("MilkCalculation",
                           new string[] { "flag", "OfficeId" },
                           new string[] { "8", objdb1.Office_ID() }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {

                    int AMCUTYPE = Convert.ToInt32(ds.Tables[0].Rows[0]["AMCU"].ToString());

                    if (AMCUTYPE == 1)
                    {
                        SAMCU.Visible = true;
                        SNONAMCU.Visible = false;
                        GetMilkQualityDetails();
                    }
                    else
                    {
                        SAMCU.Visible = false;
                        SNONAMCU.Visible = true;
                        GetMilkQualityDetails_NonAMCU();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    // AMCU

    private void GetMilkQualityDetails()
    {
        try
        {
			 string flag;
            if (objdb.Office_ID() == "7")
            {
                //flag = "12";
				flag = "6";

            }
            else
            {
                flag = "6";
            }
            ds = null;

            DataTable dt = new DataTable();
            DataRow dr;

            decimal Column = 0;
            decimal ColumnNamedt = 0;
            decimal Row = 0;

            Row = Convert.ToDecimal(6.9);

            Column = Convert.ToDecimal(3.1);
            ColumnNamedt = Convert.ToDecimal(3.1);

            dt.Columns.Add(new DataColumn("FAT-", typeof(string)));

            for (int i = 1; i <= 79; i++)
            {
                Column = Column + Convert.ToDecimal(0.1);

                dt.Columns.Add(new DataColumn(Column.ToString(), typeof(decimal)));
            }

            dr = dt.NewRow();

            for (int j = 0; j < 31; j++)
            {
                Row = Row + Convert.ToDecimal(0.1);

                dr = dt.NewRow();

                dr[0] = Row;

                for (int k = 0; k < dt.Columns.Count; k++)
                {

                    if (k == 0)
                    {
                        dr[k] = Row;
                    }
                    else
                    {
                        ColumnNamedt = ColumnNamedt + Convert.ToDecimal(0.1);

                        ds = objdb.ByProcedure("MilkCalculation",
                            new string[] { "flag", "OfficeId", "SNFPer", "FatInPer", "OfficeType_ID" },
                            new string[] { flag, objdb1.Office_ID(), Row.ToString(), ColumnNamedt.ToString(), objdb1.OfficeType_ID() }, "dataset");

                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            dr[k] = Convert.ToDecimal(ds.Tables[0].Rows[0]["Rate_Per_Ltr"].ToString());

                        }
                    }


                }

                ColumnNamedt = Convert.ToDecimal(3.1);
                dt.Rows.Add(dr);
            }

            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }



    }

    private void ExportGridToExcel()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "RateChart_NONAMCU" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView2.GridLines = GridLines.Both;
            GridView2.HeaderStyle.Font.Bold = true;
            GridView2.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }


    // Non AMCU

    private void GetMilkQualityDetails_NonAMCU()
    {
        try
        {
			string flag = "";
             if(objdb.OfficeType_ID() == "2")
            {
                if (objdb.Office_ID() == "7")
                {
                    //flag = "11";
					flag = "7";

                }

                else
                {
                    flag = "7";
                }
            }
            else
            {
                ds = objdb.ByProcedure("MilkCalculation", new string[] {"flag","OfficeId" }, new string[] {"14",objdb.Office_ID() }, "dataset");
                if(ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    string Office_Parant_ID = ds.Tables[0].Rows[0]["Office_Parant_ID"].ToString();
                    if (Office_Parant_ID == "7")
                    {
                        //flag = "11";
						flag = "7";

                    }

                    else
                    {
                        flag = "7";
                    }
                }
            }
            ds = null;

            DataTable dt = new DataTable();
            DataRow dr;

            decimal Column = 0;
            decimal ColumnNamedt = 0;
            decimal Row = 0;

            Row = Convert.ToDecimal(31);

            Column = Convert.ToDecimal(3.1);
            ColumnNamedt = Convert.ToDecimal(3.1);

            dt.Columns.Add(new DataColumn("FAT-", typeof(string)));

            for (int i = 1; i <= 69; i++)
            {
                Column = Column + Convert.ToDecimal(0.1);

                dt.Columns.Add(new DataColumn(Column.ToString(), typeof(decimal)));
            }


            dr = dt.NewRow();

            for (int j = 30; j > 19; j--)
            {
                Row = Row - Convert.ToDecimal(1);

                dr = dt.NewRow();

                dr[0] = Row;

                for (int k = 0; k < dt.Columns.Count; k++)
                {

                    if (k == 0)
                    {
                        dr[k] = Row;
                    }
                    else
                    {
                        ColumnNamedt = ColumnNamedt + Convert.ToDecimal(0.1);

                        ds = objdb.ByProcedure("MilkCalculation",
                            new string[] { "flag", "OfficeId", "CLR", "FatInPer", "OfficeType_ID" },
                            new string[] { flag, objdb1.Office_ID(), Row.ToString(), ColumnNamedt.ToString(), objdb1.OfficeType_ID() }, "dataset");

                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            dr[k] = Convert.ToDecimal(ds.Tables[0].Rows[0]["Rate_Per_Ltr"].ToString());

                        }
                    }


                }

                ColumnNamedt = Convert.ToDecimal(3.1);
                dt.Rows.Add(dr);
            }

            GridView3.DataSource = dt;
            GridView3.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }



    }

    private void ExportGridToExcel_NonAMCU()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "RateChart_AMCU" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView3.GridLines = GridLines.Both;
            GridView3.HeaderStyle.Font.Bold = true;
            GridView3.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void btnNonAmcu_Click(object sender, EventArgs e)
    {
        ExportGridToExcel_NonAMCU();
    }


}