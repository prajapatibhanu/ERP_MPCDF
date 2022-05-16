using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_MilkCollection_View_Invoice_DcsLocalSale : System.Web.UI.Page
{ 
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    IFormatProvider cult = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDCSItemDetail();
        }
    }

    private void GetDCSItemDetail()
    {
        try
        {
            if (objdb.Decrypt(Request.QueryString["Invid"]) != null)
            {
                string DcsLocalSale_Id = objdb.Decrypt(Request.QueryString["Invid"].ToString());

                DataSet dsfordcs = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                      new string[] { "flag", "DcsLocalSale_Id" },
                      new string[] { "2", DcsLocalSale_Id }, "dataset");

                if (dsfordcs.Tables[0].Rows.Count != 0)
                {

                    lblcofficename.Text = "दुग्ध उत्पादक सरकारी संस्था मर्यादित - " + dsfordcs.Tables[0].Rows[0]["Office_Name"].ToString();

                    DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(dsfordcs.Tables[0].Rows[0]["InvoiceDt"].ToString(), cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(dsfordcs.Tables[0].Rows[0]["InvoiceDt"].ToString(), cult).ToString("hh:mm:ss tt")), cult);
                    lblInvDT.Text = ADate.ToString();
                    lblInvoiceno.Text = dsfordcs.Tables[0].Rows[0]["InvoiceNo"].ToString();
                    lblofficeaddress.Text = dsfordcs.Tables[0].Rows[0]["Office_Address"].ToString();
                    lblpincode.Text = dsfordcs.Tables[0].Rows[0]["Office_Pincode"].ToString();
                    lblProducerName.Text = dsfordcs.Tables[0].Rows[0]["ProducerName"].ToString();
                }


                ds = null;
                ds = objdb.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                    new string[] { "flag", "DcsLocalSale_Id" },
                    new string[] { "3", DcsLocalSale_Id }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        gv_localsaleINV.DataSource = ds.Tables[0];
                        gv_localsaleINV.DataBind();

                        Label lbltotalamount = (gv_localsaleINV.FooterRow.FindControl("lbltotalamount") as Label);

                        lbltotalamount.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                        lblInword.Text = "In Words :  " + objdb.changeToWords(lbltotalamount.Text);

                        lblInword.Text = lblInword.Text.Replace("Only", "Rupees Only");
                    }

                }



            }

            else
            {
                objdb.redirectToHome();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MilkCollection/DcsLocalSale.aspx");
    }
}