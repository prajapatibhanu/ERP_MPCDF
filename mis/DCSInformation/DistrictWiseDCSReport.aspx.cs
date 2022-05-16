using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class mis_DCSInformation_DistrictWiseDCSReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (objdb.createdBy() != null && objdb.Office_ID() != null)
            {
                if (!Page.IsPostBack)
                {                    
                    FillDistrict();                  
                }
            }
            else
            {
                objdb.redirectToHome();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDistrict()
    {
        try
        {
            ds = objdb.ByProcedure("SpKCCUnionDistrictMapping", new string[] { "flag" }, new string[] { "7" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}