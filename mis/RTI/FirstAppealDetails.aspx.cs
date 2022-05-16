using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTI_FirstAppealDetails : System.Web.UI.Page
{
    //common obj = new common();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    ViewState["MobileNo"] = Session["MobileNo"].ToString();
        //    ViewState["AID"] = Session["AID"].ToString();
        //    Fillgrid();
        //    DetailDiv.Visible = false;
        //}
    }
    private void Fillgrid()
    {
        //ds = obj.ByProcedure("SpRTIApplicantDetail", new string[] { "flag", "AID" }, new string[] { "8", ViewState["AID"].ToString() }, "dataset");

        //GridView1.DataSource = ds;
        //GridView1.DataBind();
    }
    protected void lnkbtnDetail_Command(object sender, CommandEventArgs e)
    {
        //DetailDiv.Visible = true;
        //int iStID = Int32.Parse(e.CommandArgument.ToString());
        //DataSet dsRTI = new DataSet();
        //dsRTI = obj.ByProcedure("SpRTIApplicantDetail", new string[] { "flag", "ApplicationID" }, new string[] { "9", iStID.ToString() }, "dataset");
        //RTIDetails.InnerHtml = dsRTI.Tables[0].Rows[0]["ApplicationDetail"].ToString();
        //hyprReqDoc.NavigateUrl = dsRTI.Tables[0].Rows[0]["SupportingDoc"].ToString();

        //if (dsRTI.Tables[1].Rows.Count > 0)
        //{
        //    string POStatus = dsRTI.Tables[1].Rows[0]["ReplyStatus"].ToString();
        //    if (POStatus == "Pending")
        //    {
        //        divPORply.InnerHtml = "";
        //    }
        //    if (POStatus == "Replied")
        //    {
        //        divPORply.InnerHtml = dsRTI.Tables[1].Rows[0]["ReplyDescription"].ToString();
        //        lnkPODocument.NavigateUrl = dsRTI.Tables[1].Rows[0]["ReplyDocument"].ToString();
        //    }
        //}
        //else
        //{


        //}


        // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
    }
    protected void btnSecondAppeal_Click(object sender, EventArgs e)
    {
        //Response.Redirect("../RTI/SecondAppeal.aspx");
    }
    
}