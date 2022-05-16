using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTI_FirstAppeal : System.Web.UI.Page
{
   // common obj = new common();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        
       
    }
        
    protected void btn1_Click(object sender, EventArgs e)
    {
        Response.Redirect("FirstAppealDetails.aspx");
    }
}