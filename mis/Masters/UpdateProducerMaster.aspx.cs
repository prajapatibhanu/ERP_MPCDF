using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Drawing;
using QRCoder;
using System.Globalization;


public partial class mis_Masters_UpdateProducerMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    IFormatProvider cult = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
            {
                Fillds();
                FillGrid();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void FillGrid()
    {
        ds = null;
        ds = objdb.ByProcedure("SpProducerMaster",
                        new string[] { "flag", "DCSId" },
                        new string[] { "14", DdlDCS.SelectedValue }, "dataset");
        GridDetails.DataSource = ds;
        GridDetails.DataBind();
        GridDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        GridDetails.UseAccessibleHeader = true;
    }

    protected void Fillds()
    {
        ds = null;
        ds = objdb.ByProcedure("SpProducerMaster",
                        new string[] { "flag" },
                        new string[] { "8" }, "dataset");
        //Fill DS
        if (ds.Tables.Count > 0)
        {
            ddlDS.DataSource = ds.Tables[0];
            ddlDS.DataTextField = "Office_Name";
            ddlDS.DataValueField = "Office_ID";
            ddlDS.DataBind();
            ddlDS.Items.Insert(0, new ListItem("Select", "0"));
            FillDCS();
        }
    }
    protected void FillDCS()
    {
        ds = null;
        ds = objdb.ByProcedure("SpProducerMaster",
                        new string[] { "flag", "DCSId" },
                        new string[] { "12", objdb.Office_ID() }, "dataset");
        if (ds.Tables.Count > 0)
        {
            
            DdlDCS.DataSource = ds.Tables[1];
            DdlDCS.DataTextField = "DCS";
            DdlDCS.DataValueField = "Office_ID";
            DdlDCS.DataBind();

            ddlDS.Enabled = false;
            DdlDCS.Enabled = false;

        }
    }


    protected void txtProducerCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                int selRowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
                Label lblRowNumber = (Label)GridDetails.Rows[selRowIndex].FindControl("lblRowNumber");
                TextBox txtProducerCode = (TextBox)GridDetails.Rows[selRowIndex].FindControl("txtProducerCode");

                if (txtProducerCode.Text != "")
                {
                    // objdb.ByProcedure("SpProducerMaster",
                            // new string[] { "flag", "ProducerCardNo", "ProducerId" },
                            // new string[] { "15", txtProducerCode.Text, lblRowNumber.ToolTip.ToString() }, "dataset");

                    // lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Updated Successfully");
					ds = objdb.ByProcedure("SpProducerMaster",
                            new string[] { "flag", "ProducerCardNo", "ProducerId", "DCSId" },
                            new string[] { "15", txtProducerCode.Text, lblRowNumber.ToolTip.ToString(),objdb.Office_ID() }, "dataset");
                    if(ds != null && ds.Tables.Count > 0)
                    {
                        if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", Success);
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", warning);
                        }
                        else
                        {
                            string danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", danger);
                        }
                    }

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Opps!", "Enter Valid Milk Quty");

                }

            }
             
            FillGrid();
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}