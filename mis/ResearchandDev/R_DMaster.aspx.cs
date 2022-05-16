using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

public partial class mis_ResearchandDev_R_DMaster : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillRbtn();
        fillGrd();
        lblPopupMsg.Text = string.Empty;
        lblMsg.Text = string.Empty;
    }
    private void fillRbtn()
    {
        ds = null;
        ds = objdb.ByProcedure("SP_RD_Type_List", new string[] { "flag" }, new string[] { "1" }, "dataset");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            rbtnRDType.DataSource = ds;
            rbtnRDType.DataTextField = "VRDType";
            rbtnRDType.DataValueField = "TIRDTypeID";
            rbtnRDType.DataBind();

        }
        GC.SuppressFinalize(objdb);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = string.Empty;
            string Attachment1 = string.Empty;
            string Attachment2 = string.Empty;
            string Attachment3 = string.Empty;
            string rr = rbtnRDType.SelectedValue;
            if (rbtnRDType.SelectedValue == "")
            {
                msg += "Please Select Type of Research.\\n";
            }
            if (ddlType.SelectedValue == "0")
            {
                msg += "Please Select Type.\\n";
            }
            if (txtTilte.Text == string.Empty)
            {
                msg += "Please Enter Title.\\n";
            }
            if (txtResearchDetails.Text == string.Empty)
            {
                msg += "Please Enter Research detail.\\n";
            }
            if (txtStartDate.Text == string.Empty)
            {
                msg += "Please Enter Start Date.\\n";
            }
            if (txtEndDate.Text == string.Empty)
            {
                msg += "Please Enter End Date.\\n";
            }
            //if (fileupload.HasFile)
            //{
            //    Attachment1 = Guid.NewGuid() + "-" + fileupload.FileName;
            //    fileupload.PostedFile.SaveAs(Server.MapPath("~/mis/ResearchandDev/Upload_Doc/" + Attachment1));
            //}
            //else
            //{
                Attachment1 = string.Empty;
            //}
            //if (fileupload1.HasFile)
            //{
            //    Attachment2 = Guid.NewGuid() + "-" + fileupload1.FileName;
            //    fileupload.PostedFile.SaveAs(Server.MapPath("~/mis/ResearchandDev/Upload_Doc/" + Attachment2));
            //}
            //else
            //{
                Attachment2 = string.Empty;
            //}
            //if (fileupload2.HasFile)
            //{
            //    Attachment3 = Guid.NewGuid() + "-" + fileupload2.FileName;
            //    fileupload.PostedFile.SaveAs(Server.MapPath("~/mis/ResearchandDev/Upload_Doc/" + Attachment3));
            //}
            //else
            //{
                Attachment3 = string.Empty;
            //}
            if (msg == "")
            {

                DataSet ds = new DataSet();
                if (btnSave.Text.Contains("Submit"))
                {
                    ds = objdb.ByProcedure("SP_RD_Plan_Insert",
                                new string[] { "flag", "RDTypeID", "PlanTypeID", "ResearchTitle", "ResearchDetails", "Start_Date", "ENDDate", "RDAttachment", "RDAttachment1", "RDAttachment2", "ExpectedOutComes", "RDStatus", "Inserted_By", "Office_ID", "SpecialNote", "SpecialInstruction" },
                                new string[] { "0", rbtnRDType.SelectedValue, ddlType.SelectedValue, txtTilte.Text, txtResearchDetails.Text, txtStartDate.Text, txtEndDate.Text, Attachment1, Attachment2, Attachment3, txtoutcome.Text, "1", objdb.Office_ID(), objdb.Office_ID(), txtnote.Text, txtinstruction.Text }, "Dataset");
                }
                else
                {
                    ds = objdb.ByProcedure("SP_RD_Plan_Update",
                            new string[] { "flag", "RDPlanID", "RDTypeID", "PlanTypeID", "ResearchTitle", "ResearchDetails", "Start_Date", "ENDDate", "RDAttachment", "RDAttachment1", "RDAttachment2", "ExpectedOutComes", "RDStatus", "Inserted_By", "Office_ID", "SpecialNote", "SpecialInstruction" },
                            new string[] { "0", hdnvalue.Value, rbtnRDType.SelectedValue, ddlType.SelectedValue, txtTilte.Text, txtResearchDetails.Text, txtStartDate.Text, txtEndDate.Text, Attachment1, Attachment2, Attachment3, txtoutcome.Text, "1", objdb.Office_ID(), objdb.Office_ID(), txtnote.Text, txtinstruction.Text }, "Dataset");

                }
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Thank You! Operation Successfully Completed');", true);
                    ClearText();
                    fillGrd();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('');", true);
                }


            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed ! " + msg, " ");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed ! " + ex.ToString(), " ");
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + ex.ToString() + "');", true);
        }
    }

    private void ClearText()
    {
        ddlType.SelectedValue = "0";
        txtTilte.Text = string.Empty;
        txtResearchDetails.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtoutcome.Text = string.Empty;
        rbtnRDType.ClearSelection();
        hdnvalue.Value = "0";
        btnSave.Text = "Submit";
        txtinstruction.Text = string.Empty;
        txtnote.Text = string.Empty;
        // lblMsg.Text = string.Empty;
    }
    private void fillGrd()
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_RD_Plan_List", new string[] { "flag", "officeID" }, new string[] { "1", objdb.Office_ID() }, "dataset");
        grdlist.DataSource = ds;
        grdlist.DataBind();
        GC.SuppressFinalize(objdb);
        GC.SuppressFinalize(ds);

    }
    protected void grdlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        ViewState["InsertRecord"] = null;
        lblPopupMsg.Text = string.Empty;
        GridView1.DataSource = null;
        GridView1.DataBind();
        switch (e.CommandName)
        {
            case "Change":
                ds = new DataSet();
                ds = objdb.ByProcedure("SP_RD_Plan_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "0", Convert.ToString(e.CommandArgument) }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        rbtnRDType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][1]);
                        ddlType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0][2]);
                        txtTilte.Text = Convert.ToString(ds.Tables[0].Rows[0][3]);
                        txtStartDate.Text = Convert.ToString(ds.Tables[0].Rows[0][4]);
                        txtEndDate.Text = Convert.ToString(ds.Tables[0].Rows[0][5]);
                        txtoutcome.Text = Convert.ToString(ds.Tables[0].Rows[0][6]);
                        txtResearchDetails.Text = Convert.ToString(ds.Tables[0].Rows[0][7]);
                        txtinstruction.Text = Convert.ToString(ds.Tables[0].Rows[0][8]);
                        txtnote.Text = Convert.ToString(ds.Tables[0].Rows[0][9]);
                        btnSave.Text = "Update Changes";
                        lblMsg.Text = string.Empty;
                    }
                }
                GC.SuppressFinalize(objdb);
                GC.SuppressFinalize(ds);

                break;
            case "UploadDoc":
                ds = new DataSet();
                DataSet ds1 = new DataSet();
                ds = objdb.ByProcedure("RD_Plan_Uploaded_Document_By_PlanID_DocID", new string[] { "flag", "RDPlanID", "DocID" }, new string[] { "0", Convert.ToString(e.CommandArgument), "4" }, "dataset");
                ds1 = objdb.ByProcedure("RD_Plan_Uploaded_Document_By_PlanID_DocID", new string[] { "flag", "RDPlanID", "DocID" }, new string[] { "1", Convert.ToString(e.CommandArgument),"4" }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["NoofDocument"]) > 0)
                        {
                           
                            btnupdate.Visible = false;
                            uploader.Visible = false;
                            uploadedgrd.Visible = false;
                            uploadedDoc.Visible = true;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            GridView1.Visible = false;
                            GridView2.DataSource = ds1;
                            GridView2.DataBind();
                            GridView2.Visible = true;
                        }
                        else
                        {
                            
                            btnupdate.Visible = true;
                            uploader.Visible = true;
                            uploadedgrd.Visible = true;
                            uploadedDoc.Visible = false;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            GridView1.Visible = true;
                            GridView2.DataSource = null;
                            GridView2.DataBind();
                            GridView2.Visible = false;
                        }
                    }
                }
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
                break;
            default:
                break;
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
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
        // Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
    protected void UploadFile(object sender, EventArgs e)
    {
       
        string Attachment1 = string.Empty;
        if (FileUpload3.HasFile)
        {
            Attachment1 = Guid.NewGuid()+"_"+hdnvalue.Value + "-" + FileUpload3.FileName;
            FileUpload3.PostedFile.SaveAs(Server.MapPath("~/mis/ResearchandDev/Upload_Doc/" + Attachment1));
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();
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
                xw.WriteAttributeString("Office_ID", objdb.Office_ID());
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
        string xml = GetXML();
        if (xml != string.Empty)
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("sp_RD_Plan_Document_Uploaded_Insert",
                                   new string[] { "flag", "DocmentTypeID", "RDPlanID", "str" },
                                   new string[] { "0","4", hdnvalue.Value, GetXML() }, "Dataset");
            if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
            {
                lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
            }
            else
            {
                lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
            }
        }
    }
    protected void grdlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdlist.PageIndex = e.NewPageIndex;
        fillGrd();
    }
}
