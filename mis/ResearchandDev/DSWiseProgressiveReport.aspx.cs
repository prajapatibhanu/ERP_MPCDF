using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_ResearchandDev_DSWiseProgressiveReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            divtable.Visible = false;
            div1.Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        divtable.Visible = false;
        div1.Visible = false;
        int Count = 0;
        foreach (ListItem item in ddlOffice.Items)
        {
            if (item.Selected)
            {

                Count += Count + 1;
                    
            }
        }
        if (Count == 1)
        {
            div1.Visible = true;
        }
        //else if (ddlOffice.SelectedIndex == )
        //{
        //    divtable.Visible = true;
        //}
        else
        {
            divtable.Visible = true;
            //div1.Visible = false;
        }
        
        
    }
}