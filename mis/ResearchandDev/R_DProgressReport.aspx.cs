using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

public partial class mis_ResearchandDev_R_DProgressReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillGrd();
    }
    private void fillGrd()
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_RD_Plan_List", new string[] { "flag", "officeID" }, new string[] { "2", objdb.Office_ID() }, "dataset");
        grdlist.DataSource = ds;
        grdlist.DataBind();
        GC.SuppressFinalize(objdb);
        GC.SuppressFinalize(ds);

    }
    private void fillDSoffice()
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_RD_DS_Office_List", new string[] { "flag" }, new string[] { "0" }, "dataset");
        chkoffice.DataSource = ds;
        //chkoffice.DataTextField = "OfficeName";
        //chkoffice.DataValueField = "OfficeID";
        chkoffice.DataBind();
        GC.SuppressFinalize(objdb);
        GC.SuppressFinalize(ds);

    }
    private void fillDSofficeforimplmentation()
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_RD_DS_Office_List", new string[] { "flag" }, new string[] { "1" }, "dataset");
        chkDS.DataSource = ds;
        chkDS.DataTextField = "OfficeName";
        chkDS.DataValueField = "OfficeID";
        chkDS.DataBind();
        GC.SuppressFinalize(objdb);
        GC.SuppressFinalize(ds);

    }
    private void CHECKimplementationdone()
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_RD_Plan_Implementation_DS_Count", new string[] { "flag", "RDPlanID" }, new string[] { "0", hdnValue.Value }, "dataset");
        if (ds.Tables[0].Rows.Count > 0)
        {
            hdncount.Value = Convert.ToString(ds.Tables[0].Rows[0]["NoofCount"]);
        }
        GC.SuppressFinalize(objdb);
        GC.SuppressFinalize(ds);

    }
    private void Implementationdetail()
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_RD_Plan_Implementation_DS_Count", new string[] { "flag", "RDPlanID" }, new string[] { "1", hdnValue.Value }, "dataset");
        grdimplementation.DataSource = ds;
        grdimplementation.DataBind();
        GC.SuppressFinalize(objdb);
        GC.SuppressFinalize(ds);

    }
    private void ImplementatedDSdetail()
    {
        chkDS.ClearSelection();
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_RD_Plan_Implementation_DS_Count", new string[] { "flag", "RDPlanID" }, new string[] { "1", hdnValue.Value }, "dataset");
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            for (int i = 0; i < chkDS.Items.Count; i++)
            {
                if (Convert.ToInt16(chkDS.Items[i].Value) == Convert.ToInt16(item["DSID"])) { chkDS.Items[i].Selected = true; chkDS.Items[i].Enabled = false; break; }

            }
        }
        GC.SuppressFinalize(objdb);
        GC.SuppressFinalize(ds);

    }
    protected void btnSurvey_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = objdb.ByProcedure("SP_RD_Survey_Details_Insert", new string[] { "flag", "RDPlanID", "SurveyDate", "SurveyRemark", "InsertedBy" }, new string[] { "0", Convert.ToString(hdnValue.Value), Convert.ToString(txtSurveyDate.Text), Convert.ToString(txtSurveyDetails.Text), objdb.Office_ID() }, "dataset");
        if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
        {
            lblmsg.Text = objdb.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Thank You! Operation Successfully Completed');", true);
            txtSurveyDate.Text = string.Empty;
            txtSurveyDetails.Text = string.Empty;
            btnSurvey.Visible = false;
            fillGrd();
        }
    }
    protected void grdlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        hdnValue.Value = Convert.ToString(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Detail":
                ds = new DataSet();
                DataSet ds1 = new DataSet();
                ds = objdb.ByProcedure("SP_RD_Plan_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "1", Convert.ToString(id) }, "dataset");
                ds1 = objdb.ByProcedure("SP_RD_Plan_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "2", Convert.ToString(id) }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRDType.Text = Convert.ToString(ds.Tables[0].Rows[0]["RDType"]);
                    lblPlanType.Text = Convert.ToString(ds.Tables[0].Rows[0]["PlanType"]);
                    lblTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchTitle"]);
                    lblStartDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["StartDate"]);
                    lbldetails.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchDetails"]);
                    lblOutcomes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExpectedOutComes"]);
                }
                grd.DataSource = ds1;
                grd.DataBind();
                GC.SuppressFinalize(objdb);
                GC.SuppressFinalize(ds);
                GC.SuppressFinalize(ds1);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
                break;
            case "Survey":
                ds = new DataSet();
                //DataSet ds1 = new DataSet();
                ds = objdb.ByProcedure("SP_RD_Plan_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "1", Convert.ToString(id) }, "dataset");
                SurveyEntry.Visible = true;
                SurveyDetail.Visible = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRDType1.Text = Convert.ToString(ds.Tables[0].Rows[0]["RDType"]);
                    //lblPlanType1.Text = Convert.ToString(ds.Tables[0].Rows[0]["PlanType"]);
                    lblTitle1.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchTitle"]);
                    // lblStartDate1.Text = Convert.ToString(ds.Tables[0].Rows[0]["StartDate"]);
                    lbldetails1.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchDetails"]);
                    // lblOutcomes1.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExpectedOutComes"]);
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["RDSurveyID"]) > 0)
                    {
                        btnSurvey.Visible = false;
                        SurveyEntry.Visible = false;
                        SurveyDetail.Visible = true;
                        DataSet ds2 = new DataSet();
                        ds2 = objdb.ByProcedure("SP_RD_Survey_Details_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "0", Convert.ToString(id) }, "dataset");
                        grdsurvey.DataSource = ds2;
                        grdsurvey.DataBind();
                        GC.SuppressFinalize(ds2);
                    }
                    else
                    {
                        btnSurvey.Visible = true;
                        SurveyEntry.Visible = true;
                        SurveyDetail.Visible = false;
                    }
                }

                txtSurveyDate.Text = string.Empty;
                txtSurveyDetails.Text = string.Empty;
                GC.SuppressFinalize(objdb);
                GC.SuppressFinalize(ds);

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupSurvery();", true);
                break;
            case "implement":
                fillDSoffice();
                CHECKimplementationdone();
                if (Convert.ToInt32(hdncount.Value) > 0)
                {
                    btnimplement.Visible = false;
                    ImplementationEntry.Visible = false;
                    ImplementationDetail.Visible = true;
                    Implementationdetail();
                }
                else
                {
                    btnimplement.Visible = true;
                    ImplementationEntry.Visible = true;
                    ImplementationDetail.Visible = false;
                }


                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupImplement();", true);
                break;
            case "Editimplement":
                fillDSofficeforimplmentation();
                ImplementatedDSdetail();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupEditImplement();", true);
                break;
            default:
                break;
        }
    }
    protected void btnimplement_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("sp_RD_Plan_Implementation_DS_Insert", new string[] { "flag", "str" }, new string[] { "0", Convert.ToString(GetXML()) }, "dataset");
        if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
        {
            lblmsg.Text = objdb.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Details implemented Successfully Completed ");
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Thank You! Operation Successfully Completed');", true);

            btnimplement.Visible = false;
            fillGrd();
        }

    }
    protected void btneditimplementation_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("sp_RD_Plan_Implementation_DS_Update", new string[] { "flag", "str" }, new string[] { "0", Convert.ToString(GetEditXML()) }, "dataset");
        if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
        {
            lblmsg.Text = objdb.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Details implemented Successfully Completed ");
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Thank You! Operation Successfully Completed');", true);

            btnimplement.Visible = false;
            fillGrd();
        }

    }

    private string GetXML()
    {
        XmlWriterSettings xws = new XmlWriterSettings();
        xws.Indent = true;
        xws.NewLineOnAttributes = true;
        xws.OmitXmlDeclaration = true;
        xws.CheckCharacters = true;
        xws.CloseOutput = false;
        xws.Encoding = Encoding.UTF8;
        StringBuilder sb = new StringBuilder();
        XmlWriter xw = XmlWriter.Create(sb, xws);
        xw.WriteStartDocument();
        xw.WriteStartElement("ROOT");

        foreach (GridViewRow li in chkoffice.Rows)
        {
            CheckBox chk = (CheckBox)li.FindControl("CheckBox1");
            HiddenField hdnoffice = (HiddenField)li.FindControl("hdnDS");
            if (chk.Checked)
            {
                xw.WriteStartElement("ROW");
                xw.WriteAttributeString("Plan_ID", hdnValue.Value);
                xw.WriteAttributeString("DS_ID", hdnoffice.Value);
                xw.WriteAttributeString("Office_ID", objdb.Office_ID());
                //selectedValues.Add(li.Value);
                xw.WriteEndElement();
            }
        }
        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
        return sb.ToString();
    }
    private string GetEditXML()
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_RD_Plan_Implementation_DS_Count", new string[] { "flag", "RDPlanID" }, new string[] { "1", hdnValue.Value }, "dataset");

        XmlWriterSettings xws = new XmlWriterSettings();
        xws.Indent = true;
        xws.NewLineOnAttributes = true;
        xws.OmitXmlDeclaration = true;
        xws.CheckCharacters = true;
        xws.CloseOutput = false;
        xws.Encoding = Encoding.UTF8;
        StringBuilder sb = new StringBuilder();
        XmlWriter xw = XmlWriter.Create(sb, xws);
        xw.WriteStartDocument();
        xw.WriteStartElement("ROOT");

        bool foud = false;
        foreach (ListItem li in chkDS.Items)
        {
            if (li.Selected)
            {

                xw.WriteStartElement("ROW");
                xw.WriteAttributeString("Plan_ID", hdnValue.Value);
                xw.WriteAttributeString("DS_ID", li.Value);
                xw.WriteAttributeString("Office_ID", objdb.Office_ID());
                //selectedValues.Add(li.Value);
                xw.WriteEndElement();

            }

        }


        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
        return sb.ToString();
    }
}