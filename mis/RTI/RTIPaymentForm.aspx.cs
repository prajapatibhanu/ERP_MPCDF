using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTI_RTIApplicantsForms_RTIPaymentForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (rbtnIB.Checked || rbtnATM.Checked || rbtnCDC.Checked )
            {
                Response.Redirect("RegistrationNo.aspx");
            }
            else
            {
                lblMsg.Text = "Select Payment Mode.";
            }
        }
        catch
        {

        }

    }
}