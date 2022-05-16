using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FAQ_QuestionAnswerMaster : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                FillGrid();
            }
        }

        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }


    protected void FillGrid()
    {
        try
        {
            grdQustionAnswer.DataSource = null;
            grdQustionAnswer.DataBind();

            ds = obj.ByProcedure("SP_QuestionAndAnswer", new string[] { "flag" }, new string[] { "2" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdQustionAnswer.DataSource = ds;
                grdQustionAnswer.DataBind();
                grdQustionAnswer.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdQustionAnswer.UseAccessibleHeader = true;
                foreach (GridViewRow dd in grdQustionAnswer.Rows)
                {
                    CheckBox check = (CheckBox)dd.FindControl("chActive");
                    LinkButton btSelect = (LinkButton)dd.FindControl("btnSelect");

                    if (check != null && check.Checked)
                    {
                        btSelect.Enabled = true;
                    }
                    else
                    {
                        btSelect.Enabled = false;
                    }
                }
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnSave.Text.Trim() == "Save")
            {
                ds = obj.ByProcedure("SP_QuestionAndAnswer", new string[] { "flag", "Question", "Answer", "Isactive", "SectionID", "Section_Name", "CreatedBy" }
                , new string[] { "1", txtQuestion.Text.Trim(), txtAnswer.Text.Trim(), "1", ddlSection.SelectedValue, ddlSection.SelectedItem.Text, ViewState["Emp_ID"].ToString() }, "dataset");
            }

            else if (btnSave.Text.Trim() == "Edit" && ViewState["Qna_ID"].ToString() != "0" && ViewState["Qna_ID"].ToString() != null)
            {

                ds = obj.ByProcedure("SP_QuestionAndAnswer", new string[] { "flag", "Qna_ID", "Question", "Answer", "SectionID", "Section_Name", "UpdatedBy" }
               , new string[] { "4", ViewState["Qna_ID"].ToString(), txtQuestion.Text.Trim(), txtAnswer.Text.Trim(), ddlSection.SelectedValue, ddlSection.SelectedItem.Text, ViewState["Emp_ID"].ToString() }, "dataset");
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string Msg = ds.Tables[0].Rows[0]["MSG"].ToString();
                string ErrorMsg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                if (Msg == "OK")
                {
                    lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thank You! ", ErrorMsg);
                    txtQuestion.Text = "";
                    txtAnswer.Text = "";
                    ViewState["Emp_ID"] = "";
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Sorry! : Error", ErrorMsg);
                }

                btnSave.Text = "Save";
                FillGrid();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! : Error 10", ex.Message.ToString());
        }
    }
    protected void chActive_CheckedChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        int gridrowindex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox check = (CheckBox)grdQustionAnswer.Rows[gridrowindex].FindControl("chActive");
        LinkButton btSelect = (LinkButton)grdQustionAnswer.Rows[gridrowindex].FindControl("btnSelect");
        string ID = check.ToolTip.ToString();
        string ISactive = "0";

        if (check != null && check.Checked)
        {
            ISactive = "1";
            btSelect.Enabled = true;
        }
        else
        {
            btSelect.Enabled = false;
        }


        ds = obj.ByProcedure("SP_QuestionAndAnswer",
                   new string[] { "flag", "Isactive", "Qna_ID", "CreatedBy" },
                   new string[] { "3", ISactive, ID, ViewState["Emp_ID"].ToString() }, "dataset");

        txtQuestion.Text = "";
        txtAnswer.Text = "";
    }
    protected void grdQustionAnswer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["Qna_ID"] = grdQustionAnswer.SelectedDataKey.Value.ToString();

            ds = obj.ByProcedure("SP_QuestionAndAnswer",
                   new string[] { "flag", "Qna_ID" },
                   new string[] { "5", ViewState["Qna_ID"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtQuestion.Text = ds.Tables[0].Rows[0]["Question"].ToString();
                txtAnswer.Text = ds.Tables[0].Rows[0]["Answer"].ToString();
                ddlSection.ClearSelection();
                ddlSection.Items.FindByValue(ds.Tables[0].Rows[0]["SectionID"].ToString()).Selected = true;
                btnSave.Text = "Edit";
            }
            grdQustionAnswer.HeaderRow.TableSection = TableRowSection.TableHeader;
            grdQustionAnswer.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! : Error 10", ex.Message.ToString());
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdQustionAnswer.DataSource = null;
            grdQustionAnswer.DataBind();
            if (ddlSection.SelectedIndex > 0)
            {
                ds = obj.ByProcedure("SP_QuestionAndAnswer",
                                 new string[] { "flag", "SectionID" },
                                 new string[] { "9", ddlSection.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    grdQustionAnswer.DataSource = ds;
                    grdQustionAnswer.DataBind();
                    grdQustionAnswer.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdQustionAnswer.UseAccessibleHeader = true;
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! : Error 10", ex.Message.ToString());
        }
    }
}