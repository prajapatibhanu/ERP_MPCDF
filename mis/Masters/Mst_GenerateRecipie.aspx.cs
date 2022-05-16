using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;

public partial class mis_Masters_Mst_GenerateRecipie : System.Web.UI.Page
{


    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1 = new DataSet();
    string orderdate = "", demanddate = "", currentdate = "", currrentime = "", deliverydat = ""; Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void GetItem()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_AdvanceCard",
                       new string[] { "flag", "ItemCat_id", "Office_ID" },
                       new string[] { "12", "2", objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlProductName.DataTextField = "Abbreviation";
                ddlProductName.DataValueField = "ItemType_id";
                ddlProductName.DataSource = ds1.Tables[0];
                ddlProductName.DataBind();
                ddlProductName.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    protected void ddlProductName_Init(object sender, EventArgs e)
    {
        GetItem();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        pnlProduct.Visible = true;

    }
}
