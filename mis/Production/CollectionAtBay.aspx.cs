using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Production_CollectionAtBay : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            //GetShift();
            //GetCenters(); 
        }
        catch (Exception ex)
        {
            
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 01:" + ex.Message.ToString());
        }
    }
    private void GetShift()
    {
        try
        {
            ddlshift.DataSource = objddl.ShiftFill().Tables[0];
            ddlshift.DataTextField = "ShiftName";
            ddlshift.DataValueField = "PUShift_id";
            ddlshift.DataBind();
            ddlshift.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 01:" + ex.Message.ToString());
        }
    }
    //private void GetCenters()
    //{
    //    try
    //    {
    //        ddlCentreName.DataSource = objddl.GetCentres().Tables[0];
    //        ddlCentreName.DataTextField = "OfficeTypeCode";
    //        ddlCentreName.DataValueField = "OfficeType_ID";
    //        ddlCentreName.DataBind();
    //        ddlCentreName.Items.Insert(0, new ListItem("Select", "0"));
            
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 01:" + ex.Message.ToString());
    //    }
    //}
   
}