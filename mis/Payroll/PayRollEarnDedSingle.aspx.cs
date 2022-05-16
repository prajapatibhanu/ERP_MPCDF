using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


public partial class mis_Payroll_PayRollEarnDedSingle : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                fillAllHeads();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    public void fillAllHeads()
    {
        ds = objdb.ByProcedure("SpPayrollEarnDedOfficeWiseMaster",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ViewState["Office_ID"].ToString() }, "dataset");

        string First = "";
        string Second = "";
        string Third = "";
        int Count = 0;

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                First += (i + 1).ToString() + " ) " + ds.Tables[0].Rows[i]["EarnDeduction_Name"].ToString() + " <br/>";
            }
        }
        

        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            Count = ds.Tables[1].Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                Second += (i + 1).ToString() + " ) " + ds.Tables[1].Rows[i]["EarnDeduction_Name"].ToString() + " <br/>";
            }


        }
        

        if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
        {
            Count = ds.Tables[2].Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                Third += (i + 1).ToString() + " ) " + ds.Tables[2].Rows[i]["EarnDeduction_Name"].ToString() + " <br/>";
            }
        }

        lblLoanContribution.Text = Second;
        lblAutoCal.Text = Third;
        lblSingleHead.Text = First;

    }


}