using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTI_Guideline : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                HEnglish.Visible = false;
                HHindi.Visible = true;
                EnglishContent.Visible = false;
                HindiContent.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void rbtnLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
         try
        {
            if (rbtnLanguage.Items.FindByText("English").Selected == true)
            {
                HEnglish.Visible = true;
                HHindi.Visible = false;
                EnglishContent.Visible = true;
                HindiContent.Visible = false;
            }
            if (rbtnLanguage.Items.FindByText("Hindi").Selected == true)
            {
                HEnglish.Visible = false;
                HHindi.Visible = true;
                EnglishContent.Visible = false;
                HindiContent.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("RegistrationForm.aspx");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-bell", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}