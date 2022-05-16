using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;

public partial class mis_ResearchandDev_RD_Plan_Intimate_Implementation_To_DS : System.Web.UI.Page
{
    APIProcedure objapi = new APIProcedure();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        FillGRD();
    }
    private void FillGRD() {
        ds = new DataSet();
        ds = objapi.ByProcedure("SP_RD_Plan_For_Request_Implementation_by_DS", new string[] { "flag", "DSID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
        grdlist.DataSource = ds;
        grdlist.DataBind();
        GC.SuppressFinalize(objapi);
        GC.SuppressFinalize(ds);
    }
    
    protected void btnSurvey_Click(object sender, EventArgs e) {
        ds = new DataSet();
        //DataSet ds1 = new DataSet();
        ds = objapi.ByProcedure("SP_RD_Plan_ImplementationDate_DS_Update", new string[] { "flag", "ImplentationDate", "ImplementationRemark", "RDPlanID", "RDImplementedID" }, new string[] { "0", Convert.ToString(txtimplementationDate.Text), Convert.ToString(txtImplementationRemark.Text), Convert.ToString(hdnRDPlanID.Value), Convert.ToString(hdnDSID.Value) }, "dataset");
        if (ds.Tables[0].Rows[0]["ERRORMsg"].ToString() == "OK")
        {
            lblmsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");

            FillGRD();
        }
        else
        {
            lblmsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('');", true);
        }
    }
    private string GetXML()
    {
        string result = string.Empty;
        if (GridView1.Rows.Count > 0)
        {
            XmlWriterSettings xws = new XmlWriterSettings
            {
                Indent = true,
                NewLineOnAttributes = true,
                OmitXmlDeclaration = true,
                CheckCharacters = true,
                CloseOutput = false,
                Encoding = Encoding.UTF8
            };
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
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        string Attachment = string.Empty;
        //if (fileupload.HasFile)
        //{
        //    Attachment = Guid.NewGuid() + "-" + fileupload.FileName;
        //    fileupload.PostedFile.SaveAs(Server.MapPath("~/mis/ResearchandDev/Upload_Doc/" + Attachment));
        //}
        //else
        //{
            Attachment = string.Empty;
       // }
        ds = new DataSet();
        DataSet ds1 = new DataSet();
        ds = objapi.ByProcedure("SP_RD_Plan_FinalProgress_DS_Update", new string[] { "flag", "ImplentationEndDate", "ActualStatus", "RDPlanID", "RDImplementedID", "ImplementedDOC" }, new string[] { "0", Convert.ToString(txtEnddate.Text), Convert.ToString(txtProcess.Text), Convert.ToString(hdnRDPlanID.Value), Convert.ToString(hdnDSID.Value), Attachment }, "dataset");
        if (ds.Tables[0].Rows[0]["ERRORMsg"].ToString() == "OK")
        {
            string xml = GetXML();
            if (xml != string.Empty)
            {
                ds1 = objapi.ByProcedure("sp_RD_Plan_Document_Uploaded_Insert",
                                       new string[] { "flag", "DocmentTypeID", "RDPlanID", "str" },
                                       new string[] { "0", "3", Convert.ToString(hdnRDPlanID.Value), GetXML() }, "Dataset");

            }
            lblmsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");

            FillGRD();
        }
        else
        {
            lblmsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('');", true);
        }
        GC.SuppressFinalize(ds);
        GC.SuppressFinalize(ds1);
        GC.SuppressFinalize(objapi);
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        try
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            lblmsg.Text = objapi.Alert("fa-error", "alert-error", "Error!", "File is removed from folder.");
        }
        
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
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupProcess();", true);
        // Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void UploadFile(object sender, EventArgs e)
    {
        string Attachment1 = string.Empty;
        if (fileupload.HasFile)
        {
            Attachment1 = Guid.NewGuid() + "_" + hdnRDPlanID.Value + "-" + fileupload.FileName;
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
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupProcess();", true);

    }
    protected void grdlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandArg = Convert.ToString(e.CommandArgument);
        string[] command = commandArg.Split('|');
        hdnRDPlanID.Value = command[0];
        hdnDSID.Value = command[1];
        ViewState["InsertRecord"] = null; 
        txtimplementationDate.Text = string.Empty;
        txtImplementationRemark.Text = string.Empty;
        switch (e.CommandName)
        {
            case "Detail":
                 ds = new DataSet();
                //DataSet ds1 = new DataSet();
                 ds = objapi.ByProcedure("SP_RD_Plan_Documents_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "1", Convert.ToString(command[0]) }, "dataset");
                 grdDocument.DataSource = ds;
                 grdDocument.DataBind();
                 GC.SuppressFinalize(ds);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
                break;
            case "implement":
                ds = new DataSet();
                //DataSet ds1 = new DataSet();
                ds = objapi.ByProcedure("SP_RD_Plan_Implementation_By_ID_List", new string[] { "flag", "RDPlanID", "RDimplementationID" }, new string[] { "0", Convert.ToString(command[0]), Convert.ToString(command[1]) }, "dataset");
                SurveyEntry.Visible = true;
               
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRDType1.Text = Convert.ToString(ds.Tables[0].Rows[0]["RDType"]);
                    //lblPlanType1.Text = Convert.ToString(ds.Tables[0].Rows[0]["PlanType"]);
                    lblTitle1.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchTitle"]);
                   // lblStartDate1.Text = Convert.ToString(ds.Tables[0].Rows[0]["StartDate"]);
                    lbldetails1.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchDetails"]);
                   // lblOutcomes1.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExpectedOutComes"]);
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["ImplementatonStarted"]) != 0)
                    {
                        btnSurvey.Visible = false;
                        SurveyEntry.Visible = false;
                     
                    
                    }
                    else
                    {
                        btnSurvey.Visible = true;
                        SurveyEntry.Visible = true;
                       
                    }
                }

                txtimplementationDate.Text = string.Empty;
                txtImplementationRemark.Text = string.Empty;
                GC.SuppressFinalize(objapi);
                GC.SuppressFinalize(ds);

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupSurvery();", true);
                break;
            case "Process":
                ds = new DataSet();
                //DataSet ds1 = new DataSet();
                ds = objapi.ByProcedure("SP_RD_Plan_Implementation_By_ID_List", new string[] { "flag", "RDPlanID", "RDimplementationID" }, new string[] { "0", Convert.ToString(command[0]), Convert.ToString(command[1]) }, "dataset");
                ImplementationEntry.Visible = true;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRDType2.Text = Convert.ToString(ds.Tables[0].Rows[0]["RDType"]);
                    //lblPlanType1.Text = Convert.ToString(ds.Tables[0].Rows[0]["PlanType"]);
                    lblTitle2.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchTitle"]);
                    // lblStartDate1.Text = Convert.ToString(ds.Tables[0].Rows[0]["StartDate"]);
                    lbldetails2.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchDetails"]);
                    // lblOutcomes1.Text = Convert.ToString(ds.Tables[0].Rows[0]["ExpectedOutComes"]);
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["ImplementatonEnded"]) != 0)
                    {
                        btnProcess.Visible = false;
                        ImplementationEntry.Visible = false;
                        ImplementationDetail.Visible = true;
                        DataSet ds1 = new DataSet();
                        //DataSet ds1 = new DataSet();
                        ds1 = objapi.ByProcedure("SP_RD_Plan_ImplementationFinalStage_DS_By_ID", new string[] { "flag", "PlanImplementationID", "RDPlanID" }, new string[] { "0", Convert.ToString(command[1]), Convert.ToString(command[0]) }, "dataset");
                        grdimplementation.DataSource = ds1;
                        grdimplementation.DataBind();
                    }
                    else
                    {
                        btnProcess.Visible = true;
                        ImplementationEntry.Visible = true;
                        ImplementationDetail.Visible = false;

                    }
                }

                txtEnddate.Text = string.Empty;
                txtProcess.Text = string.Empty;

                GC.SuppressFinalize(objapi);
                GC.SuppressFinalize(ds);

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupProcess();", true);
                break;
            default:
                break;
        }
    }
}