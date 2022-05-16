using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

public partial class mis_ResearchandDev_UpdateRandDStatus : System.Web.UI.Page
{
    APIProcedure objapi = new APIProcedure();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillGrd();
    }
    private void fillGrd()
    {
        ds = new DataSet();
        ds = objapi.ByProcedure("SP_RD_Plan_List", new string[] { "flag", "officeID" }, new string[] { "1", objapi.Office_ID() }, "dataset");
        grdlist.DataSource = ds;
        grdlist.DataBind();
        GC.SuppressFinalize(objapi);
        GC.SuppressFinalize(ds);
    }
    private string GetXML()
    {
        string result = string.Empty;
        if (GridView1.Rows.Count > 0)
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

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                xw.WriteStartElement("ROW");
                xw.WriteAttributeString("Attachment", gvr.Cells[0].Text);
                xw.WriteAttributeString("Office_ID", objapi.Office_ID());
                xw.WriteEndElement();
            }
            xw.WriteEndDocument();
            xw.Flush();
            xw.Close();
            result = sb.ToString();
        }
        return result;

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string Attachment = string.Empty;
        bool valid;
        string msg = validate(out valid);
        DataSet ds1 = new DataSet();
        if (valid)
        {
            //if (fileupload.HasFile)
            //{
            //    Attachment = Guid.NewGuid() + "-" + fileupload.FileName;
            //    fileupload.PostedFile.SaveAs(Server.MapPath("~/mis/ResearchandDev/Upload_Doc/" + Attachment));
            //}
            //else { Attachment = string.Empty; }
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_RD_Plan_Update_Status", new string[] { "flag", "RDPlanID", "Remark", "ActualOutComes", "RDStatus", "ActualDocument", "ProgressDate", "OfficeID" }, new string[] { "0", Convert.ToString(hdnvalue.Value), txtRemark.Text, txtActualoutcome.Text, hdnstatus.Value, Attachment, txtProgressDate.Text, objapi.Office_ID() }, "dataset");
            if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
            {
                string xml = string.Empty;
                switch (hdnstatus.Value)
                {
                    case "1":
                        xml = GetXML();
                        if (xml != string.Empty)
                        {

                            ds1 = objapi.ByProcedure("sp_RD_Plan_Document_Uploaded_Insert",
                                                   new string[] { "flag", "DocmentTypeID", "RDPlanID", "str" },
                                                   new string[] { "0", "1", Convert.ToString(hdnvalue.Value), GetXML() }, "Dataset");

                        }
                        break;
                    case "2":
                        xml = GetXML();
                        if (xml != string.Empty)
                        {

                            ds1 = objapi.ByProcedure("sp_RD_Plan_Document_Uploaded_Insert",
                                                   new string[] { "flag", "DocmentTypeID", "RDPlanID", "str" },
                                                   new string[] { "0", "2", Convert.ToString(hdnvalue.Value), GetXML() }, "Dataset");

                        }
                        break;
                    case "3":
                        xml = GetXML();
                        if (xml != string.Empty)
                        {

                            ds1 = objapi.ByProcedure("sp_RD_Plan_Document_Uploaded_Insert",
                                                   new string[] { "flag", "DocmentTypeID", "RDPlanID", "str" },
                                                   new string[] { "0", "5", Convert.ToString(hdnvalue.Value), GetXML() }, "Dataset");

                        }
                        break;
                    default:
                        break;
                }


                lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Details Status Successfully Completed ");
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Thank You! Operation Successfully Completed');", true);

                btnupdate.Visible = false;
                fillGrd();
            }
            GC.SuppressFinalize(ds);
            GC.SuppressFinalize(ds1);
            GC.SuppressFinalize(objapi);
        }
        else
        {
            lblMsg.Text = objapi.Alert("fa-check", "alert-error", "Fail !", msg);
        }

    }

    private string validate(out bool valid)
    {
        StringBuilder msg = new StringBuilder();
        valid = true;
        int statusID = Convert.ToInt32(hdnstatus.Value);
        if (statusID > 0)
        {
            if (txtRemark.Text == string.Empty)
            {
                msg.Append("Please Enter Remark.\\n");
                valid = false;
            }
            if (statusID == 1)
            {
                if (txtProgressDate.Text == string.Empty)
                {
                    msg.Append("Please Enter Progess Date.\\n");
                    valid = false;
                }
            }
            else if (statusID == 3 || statusID == 4)
            {
                if (txtRemark.Text == string.Empty)
                {
                    msg.Append("Please Enter Remark.\\n");
                    valid = false;
                }
            }

            else
            {
                if (txtActualoutcome.Text == string.Empty)
                {
                    msg.Append("Please Enter Actual Outcome.\\n");
                    valid = false;
                }
            }
        }
        else
        {
            msg.Append("Please Select Type of Research.\\n");
            valid = false;
        }

        return msg.ToString();
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
    protected void DeleteFile(object sender, EventArgs e)
    {

        GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
        DataTable dt3 = ViewState["InsertRecord"] as DataTable;
        dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
        ViewState["InsertRecord"] = dt3;
        string filePath = (sender as LinkButton).CommandArgument;
        File.Delete(filePath);
        GridView1.DataSource = dt3;
        GridView1.DataBind();
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
        // Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void UploadFile(object sender, EventArgs e)
    {
        string Attachment1 = string.Empty;
        if (fileupload.HasFile)
        {
            Attachment1 = Guid.NewGuid() + "_" + hdnvalue.Value + "-" + fileupload.FileName;
            fileupload.PostedFile.SaveAs(Server.MapPath("~/mis/ResearchandDev/Upload_Doc/" + Attachment1));
        }
        else
        {
            Attachment1 = string.Empty;

        }
        //string fileName = Path.GetFileName(FileUpload3.PostedFile.FileName);
        //FileUpload3.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
        if (Attachment1 != string.Empty)
        {
            int CompartmentType = 0;
            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("TypeID", typeof(int)));
                dt.Columns.Add(new DataColumn("ImageType", typeof(string)));
                dt.Columns.Add(new DataColumn("ImagePath", typeof(string)));
                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = Attachment1;
                dr[2] = Server.MapPath("~/mis/ResearchandDev/Upload_Doc/") + Attachment1;
                dt.Rows.Add(dr);
                ViewState["InsertRecord"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("TypeID", typeof(int)));
                dt.Columns.Add(new DataColumn("ImageType", typeof(string)));
                dt.Columns.Add(new DataColumn("ImagePath", typeof(string)));
                DT = (DataTable)ViewState["InsertRecord"];

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (Attachment1 == DT.Rows[i]["ImageType"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }

                if (CompartmentType == 1)
                {
                    //  lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Head \"" + ddlHeaddetails.SelectedItem.Text + "\" already exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = Attachment1;
                    dr[2] = Server.MapPath("~/mis/ResearchandDev/Upload_Doc/") + Attachment1;
                    dt.Rows.Add(dr);

                    // dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);

    }
    protected void grdlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        txtRemark.Text = string.Empty;
        ViewState["InsertRecord"] = null;
        txtActualoutcome.Text = string.Empty;
        btnupdate.Visible = true;
        lblMsg.Text = string.Empty;
        switch (e.CommandName)
        {
            case "Detail":
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_RD_Plan_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "1", Convert.ToString(hdnvalue.Value) }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRDType.Text = Convert.ToString(ds.Tables[0].Rows[0]["RDType"]);
                   // lblPlanType.Text = Convert.ToString(ds.Tables[0].Rows[0]["PlanType"]);
                    lblTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchTitle"]);
                    lblStartDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["StartDate"]);
                    lbldetails.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchDetails"]);
                    lblOutcomes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExpectedOutComes"]);
                }
                ddlStatus.SelectedValue = "0";
                hdnstatus.Value = "0";
                GC.SuppressFinalize(objapi);
                GC.SuppressFinalize(ds);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
                break;
            case "Progress":
                ds = new DataSet();
                DataSet ds1 = new DataSet();
                ds = objapi.ByProcedure("SP_RD_Plan_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "1", Convert.ToString(hdnvalue.Value) }, "dataset");
                ds1 = objapi.ByProcedure("SP_RD_Plan_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "2", Convert.ToString(hdnvalue.Value) }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbltype1.Text = Convert.ToString(ds.Tables[0].Rows[0]["RDType"]);
                    lbltitle1.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchTitle"]);
                    grdProgress.DataSource = ds1;
                    grdProgress.DataBind();
                }
                
                GC.SuppressFinalize(objapi);
                GC.SuppressFinalize(ds);
                GC.SuppressFinalize(ds1);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupProgress();", true);
                break;
            default:
                break;
        }
    }
    protected void grdlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdlist.PageIndex = e.NewPageIndex;
        fillGrd();
    }
}