using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FAQ_QuestionAnswerDetails : System.Web.UI.Page
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
                //BIndSection();
                txtAnswer.Enabled = false;
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    //protected void BIndSection()
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        ddlSection.Items.Clear();

    //        ds = obj.ByProcedure("SP_QuestionAndAnswer", new string[] { "flag" }, new string[] { "2" }, "dataset");
    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            ddlSection.DataTextField = "Section_Name";
    //            ddlSection.DataValueField = "Qna_ID";
    //            ddlSection.DataSource = ds.Tables[0];
    //            ddlSection.DataBind();
    //        }
    //        ddlSection.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
    //    }
    //}
    //protected void BindQuestion()
    //{
    //    try
    //    {
    //        ddlQuestion.Items.Clear();
    //        ds = obj.ByProcedure("SP_QuestionAndAnswer", new string[] { "flag" }, new string[] { "6" }, "dataset");

    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlQuestion.DataTextField = "Question";
    //            ddlQuestion.DataValueField = "Qna_ID";
    //            ddlQuestion.DataSource = ds.Tables[0];
    //            ddlQuestion.DataBind();

    //        }
    //        ddlQuestion.Items.Insert(0, new ListItem("Select", "0"));

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
    //    }
    //}
    protected void ddlQuestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtAnswer.Text = "";
            if (ddlQuestion.SelectedIndex > 0)
            {
                ds = obj.ByProcedure("SP_QuestionAndAnswer",
                   new string[] { "flag", "Qna_ID" },
                   new string[] { "7", ddlQuestion.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtAnswer.Text = ds.Tables[0].Rows[0]["Answer"].ToString();
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlQuestion.Items.Clear();

            ds = obj.ByProcedure("SP_QuestionAndAnswer",
                   new string[] { "flag", "SectionID" },
                   new string[] { "9", ddlSection.SelectedValue }, "dataset");

            if (ds != null && ds.Tables.Count > 0)
            {
                ddlQuestion.DataTextField = "Question";
                ddlQuestion.DataValueField = "Qna_ID";
                ddlQuestion.DataSource = ds;
                ddlQuestion.DataBind();
                ddlQuestion.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}