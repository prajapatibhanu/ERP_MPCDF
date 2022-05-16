using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;


public partial class mis_MilkCollection_DCSMilkReceive : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass CM_Obj = new MilkCalculationClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
            {

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Attributes.Add("readonly", "readonly");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFilterDate.Attributes.Add("readonly", "readonly");
                txtFilterDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                FillOffice(sender, e);
                FillDCS();
                ViewState["InsertRecord"] = "";
                //GetChallan();
                txtFilterDate_TextChanged(sender, e);
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void FillOffice(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtsocietyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    txtBlock.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                    txtBlock.Enabled = false;
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDCS()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "22", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDCS.DataTextField = "Office_Name";
                    ddlDCS.DataValueField = "Office_ID";
                    ddlDCS.DataSource = ds;
                    ddlDCS.DataBind();
                    //ddlBMCTankerRootName.Items.Insert(0, "Select");
                   
                }
                else
                {

                }
            }
            ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlShift_Init(object sender, EventArgs e)
    {
        try
        {
            ddlShift.SelectedValue = "Morning";
            //DataSet dsct = objdb.ByProcedure("USP_GetServerDatetime",
            //                 new string[] { "flag" },
            //                 new string[] { "1" }, "dataset");

            //string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

            //string[] s = currrentime.Split(':');

            //if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
            //{
            //    ddlShift.SelectedValue = "Morning";
            //}
            //else
            //{
            //    ddlShift.SelectedValue = "Evening";
            //}

            //ddlShift.Enabled = false;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

   private decimal GetSNF(string CLR, string FAT)
   {
       decimal snf = 0, clr = 0, fat = 1;
       try
       {
           //Formula for Get SNF as given below:
           if (CLR != "")
           { clr = Convert.ToDecimal(CLR); }
           if (FAT != "")
           { fat = Convert.ToDecimal(FAT); }
   
           //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
           //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
           // snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44)); 
           snf = CM_Obj.GetSNFPer_DCS(fat, clr);
       }
       catch (Exception ex)
       {
           lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
       }
       return Math.Round(snf, 2);
   }

   
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("DCSMilkReceive.aspx", false);
    }

    private DataTable GetMilkCollectionData()
    {
        
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Office_ID", typeof(int));
        dt.Columns.Add("MilkType", typeof(string));
        dt.Columns.Add("Temp", typeof(string));
        dt.Columns.Add("MilkQuantity", typeof(decimal));
        dt.Columns.Add("MilkQuality", typeof(string));
        dt.Columns.Add("Fat", typeof(decimal));
        dt.Columns.Add("Snf", typeof(decimal));
        dt.Columns.Add("Clr", typeof(decimal));
        dt.Columns.Add("FatInKg", typeof(decimal));
        dt.Columns.Add("SnfInKg", typeof(decimal));

        foreach(GridViewRow rows in GridView1.Rows)
        {
            Label lblOffice_ID = (Label)rows.FindControl("lblOffice_ID");
            Label lblMilkType = (Label)rows.FindControl("lblMilkType");
            Label lblMilkQuality = (Label)rows.FindControl("lblMilkQuality");
            Label lblMilkQuantity = (Label)rows.FindControl("lblMilkQuantity");
            Label lblFat = (Label)rows.FindControl("lblFat");
            Label lblClr = (Label)rows.FindControl("lblClr");
            Label lblSnf = (Label)rows.FindControl("lblSnf");
            Label lblTemp = (Label)rows.FindControl("lblTemp");
            string strFatinKG = CM_Obj.GetFATInKg(Convert.ToDecimal(lblMilkQuantity.Text), (Convert.ToDecimal(lblFat.Text))).ToString();
            string strSNFinKG = CM_Obj.GetSNFInKg(Convert.ToDecimal(lblMilkQuantity.Text), (Convert.ToDecimal(lblSnf.Text))).ToString();

            dr = dt.NewRow();
            dr[0] = lblOffice_ID.Text;
            dr[1] = lblMilkType.Text;
            dr[2] = lblTemp.Text;
            dr[3] = lblMilkQuantity.Text;
            dr[4] = lblMilkQuality.Text; 
            dr[5] = lblFat.Text;
            dr[6] = lblSnf.Text;
            dr[7] = lblClr.Text;
            dr[8] = strFatinKG;
            dr[9] = strSNFinKG;
            dt.Rows.Add(dr);
        }
            
        
        return dt;
    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("DCSMilkReceive.aspx", false);


    }

    

    protected void btnYes_Click(object sender, EventArgs e)
    {

    try
    {
        if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
        {
            lblMsg.Text = ""; string MilkRate = "0"; string MilkAmount = "0";
           

            //decimal milkinkg = CM_Obj.GetLtrToKg(Convert.ToDecimal(txtCLR.Text),Convert.ToDecimal(txtI_MilkQuantity.Text)); 


            string strFatinKG = CM_Obj.GetFATInKg(Convert.ToDecimal(txtSelfMilkQuantity.Text), (Convert.ToDecimal(txtSelfFat.Text))).ToString();
            string strSNFinKG = CM_Obj.GetSNFInKg(Convert.ToDecimal(txtSelfMilkQuantity.Text), (Convert.ToDecimal(txtSelfSNF.Text))).ToString();
            DataTable dtMilkCollection = new DataTable();
            dtMilkCollection = GetMilkCollectionData();

            
                ds = null;
                ds = objdb.ByProcedure("Usp_SocietyMilkCollectionEntry",
                        new string[] { "flag", 
                                       "EntryDate",
				                       "Office_ID", 
				                       "AttachedBMC_ID", 
				                       "Shift", 
				                       "MilkType", 
				                       "Temp", 
				                       "MilkQuantity", 
				                       "MilkQuality", 
				                       "Fat", 
				                       "Snf", 
				                       "Clr", 
				                       "FatInKg", 
				                       "SnfInKg", 
				                       "IsActive", 
				                       "CreatedBy",  
				                       "CreatedByIP"
                                             },

                                            new string[] { "1",  
                                            Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                            objdb.Office_ID(), 
                                            objdb.Office_ID(),
                                            ddlShift.SelectedValue,
                                            ddlSelfMilkType.SelectedValue,
                                            txtSelfTemp.Text,
                                            txtSelfMilkQuantity.Text,
                                            ddlSelfMilKQuality.SelectedValue,
                                            txtSelfFat.Text,
                                            txtSelfSNF.Text,
                                            txtSelfCLR.Text,
                                            strFatinKG,
                                            strSNFinKG,
                                            "1",
                                            objdb.createdBy(),
                                            objdb.GetLocalIPAddress()
                                            },
                                         new string[] { "type_SocietyMilkCollectionEntry"},
                                         new DataTable[] { dtMilkCollection }, "TableSave");

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    Session["IsSuccess"] = true;
                    Response.Redirect("DCSMilkReceive.aspx", false);

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    Session["IsSuccess"] = false;
                }
            




            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
    }

    catch (Exception ex)
    {
        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

    }

    }


    protected void txtFat_Cow_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtFat_Cow.Text != "" && txtCLR_Cow.Text != "")
            {
                txtSNF_Cow.Text = CM_Obj.GetSNFPer_DCS(Convert.ToDecimal(txtFat_Cow.Text), Convert.ToDecimal(txtCLR_Cow.Text)).ToString();
                txtCLR_Cow.Focus();
            }
            else
            {
                txtCLR_Cow.Focus();
                txtSNF_Cow.Text = "0";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }

    protected void txtCLR_Cow_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtFat_Cow.Text != "" && txtCLR_Cow.Text != "")
            {
                txtSNF_Cow.Text = CM_Obj.GetSNFPer_DCS(Convert.ToDecimal(txtFat_Cow.Text), Convert.ToDecimal(txtCLR_Cow.Text)).ToString();
                
            }
            else
            {
                
                txtSNF_Cow.Text = "0";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
                     
    
    protected void txtSelfFat_TextChanged(object sender, EventArgs e)
    {
        if(txtSelfCLR.Text == "")
        {
            txtSelfCLR.Text = "0";
        }
        if (txtSelfFat.Text == "")
        {
            txtSelfFat.Text = "0";
        }
        txtSelfSNF.Text = CM_Obj.GetSNFPer_DCS(Convert.ToDecimal(txtSelfFat.Text), Convert.ToDecimal(txtSelfCLR.Text)).ToString();
        
        SetFocus(txtSelfCLR.Text);
    }
    protected void txtSelfCLR_TextChanged(object sender, EventArgs e)
    {
        if (txtSelfFat.Text == "")
        {
            txtSelfFat.Text = "0";
        }
        if (txtSelfCLR.Text == "")
        {
            txtSelfCLR.Text = "0";
        }
        txtSelfSNF.Text = CM_Obj.GetSNFPer_DCS(Convert.ToDecimal(txtSelfFat.Text), Convert.ToDecimal(txtSelfCLR.Text)).ToString();
    }

    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            
            if (ViewState["InsertRecord"].ToString() == "")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Office_ID", typeof(int));
                dt.Columns.Add("Office_Name", typeof(string));
                dt.Columns.Add("MilkType", typeof(string));
                dt.Columns.Add("MilkQuality", typeof(string));
                dt.Columns.Add("MilkQuantity", typeof(decimal));
                dt.Columns.Add("Fat", typeof(decimal));
                dt.Columns.Add("Clr", typeof(decimal));
                dt.Columns.Add("Snf", typeof(decimal));
                dt.Columns.Add("Temp", typeof(decimal));

                
                
               dt.Rows.Add(ddlDCS.SelectedValue, ddlDCS.SelectedItem.Text, ddlMilkType.SelectedValue, ddlMQuality_Cow.SelectedValue, txtMQty_Cow.Text, txtFat_Cow.Text, txtCLR_Cow.Text, txtSNF_Cow.Text,txtTemp_Cow.Text);
                
                
                ViewState["InsertRecord"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            else
            {
                DataTable dt = (DataTable)ViewState["InsertRecord"];
                dt.Rows.Add(ddlDCS.SelectedValue, ddlDCS.SelectedItem.Text, ddlMilkType.SelectedValue, ddlMQuality_Cow.SelectedValue, txtMQty_Cow.Text, txtFat_Cow.Text, txtCLR_Cow.Text, txtSNF_Cow.Text, txtTemp_Cow.Text);
                ViewState["InsertRecord"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();


            }
            ddlMilkType.ClearSelection();
            txtFat_Cow.Text = "";
            txtMQty_Cow.Text = "";
            txtSNF_Cow.Text = "";
            txtCLR_Cow.Text = "";
            
            SetFocus(ddlDCS);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt2 = ViewState["InsertRecord"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord"] = dt2;
            GridView1.DataSource = dt2;
            GridView1.DataBind();

           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void txtFilterDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdMilkCollectionDetails.DataSource = string.Empty;
            GrdMilkCollectionDetails.DataBind();

            ds = null;
            ds = objdb.ByProcedure("Usp_SocietyMilkCollectionEntry",
                     new string[] { "flag", "EntryDate", "Office_ID" },
                     new string[] { "2", Convert.ToDateTime(txtFilterDate.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID() }, "dataset");

            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    GrdMilkCollectionDetails.DataSource = ds.Tables[0];
                    GrdMilkCollectionDetails.DataBind();
                    //btnSubmit.Enabled = false;
                    
                }
                else
                {
                    GrdMilkCollectionDetails.DataSource =string.Empty;
                    GrdMilkCollectionDetails.DataBind();
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlSelfMilkType_SelectedIndexChanged(object sender, EventArgs e)
    {
     //if(ddlSelfMilkType.SelectedIndex > 0)
     //{
     //    if (ddlSelfMilkType.SelectedValue == "Buf")
     //    {
     //        RangeValidator2.MinimumValue = "5.6";
     //        RangeValidator2.MinimumValue = "10";
     //        RangeValidator2.Text = "i class='fa fa-exclamation-circle' title='Minimum FAT % required 5.6 and maximum 10.!";
     //        RangeValidator2.ErrorMessage = "Minimum FAT % required 5.6 and maximum 10.";
     //    }
     //    else
     //    {
     //        RangeValidator2.MinimumValue = "3.2";
     //        RangeValidator2.MinimumValue = "5.5";
     //        RangeValidator2.Text = "i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 5.5.!";
     //        RangeValidator2.ErrorMessage = "Minimum FAT % required 3.2 and maximum 5.5.";
     //    }
     //}
     

    }
}