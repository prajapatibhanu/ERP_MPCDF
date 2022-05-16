using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_MilkCollection_SocietyWiseMilkProcess : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass obj_MC = new MilkCalculationClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (Session.SessionID != null)
            {
                

                if (!IsPostBack)
                {
					//txtDate.Attributes.Add("readonly", "readonly");
                      txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
				
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    lblSNF1.Text = "((CLR / 4) + (FAT * 0.2) + 0.7)";
                    getdcsdetails();
                    MilkCollection();
                    FillSociety();
                }
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }


    protected void FillSociety()
    {
        try
        {
            // lblMsg.Text = "";
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSociatyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    txtBlock.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                    lblblockname.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                    lblDcsname.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    lbldate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    shift.Text = ddlShift.SelectedValue;

                    txtBlock.Enabled = false;
                }
                else
                {
                    // ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                // ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void getdcsdetails()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                     new string[] { "flag", "OfficeId" },
                     new string[] { "2", Session["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //  lblSamiti.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
     
    protected void txtgvV_Clr_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            decimal SNF = 0, CLR = 0, FAT = 1, FATTotal = 0, fatval = 0, SNFTotal = 0, QuantityVal = 0;

            int selRowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
            TextBox FatID = (TextBox)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("txtgvV_Fat");
            Label Quantity = (Label)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("lblgvV_Quantity");
            TextBox ClrID = (TextBox)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("txtgvV_Clr");
            Label SNFID = (Label)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("lblgvV_SNF");
            Label lblmilkRate = (Label)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("lblmilkRate");
            Label lblnetAmount = (Label)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("lblnetAmount");

            TextBox txtgvV_Remark = (TextBox)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("txtgvV_Remark");

            if (FatID.Text != "" && ClrID.Text != "")
            {
                QuantityVal = QuantityVal + Convert.ToDecimal(Quantity.Text);
                FAT = Convert.ToDecimal(FatID.Text);
                CLR = Convert.ToDecimal(ClrID.Text);
                fatval = fatval + FAT;
                SNF = obj_MC.GetSNFPer_DCS(FAT, CLR); //((CLR / 4) + (FAT * Convert.ToDecimal(0.2)) + (Convert.ToDecimal(0.7)));
                SNFTotal = SNFTotal + SNF;
                SNFID.Text = SNF.ToString();

                lblmilkRate.Text = obj_MC.GetRatePerLtrOrKg(FAT, CLR, objdb.Office_ID());
                lblnetAmount.Text = Math.Round((Convert.ToDecimal(Quantity.Text)) * (Convert.ToDecimal(lblmilkRate.Text)), 3).ToString();
                Quantity.ToolTip = lblnetAmount.Text + " @ " + lblmilkRate.Text + " Per Ltr";
                txtgvV_Remark.Text = lblnetAmount.Text + " @ " + lblmilkRate.Text + " Per Ltr";
            }
            else
            {
                SNFID.Text = "";
                lblmilkRate.Text = "";
                Quantity.ToolTip = "";
                lblnetAmount.Text = "";
                txtgvV_Remark.Text = "";
            }
            FATTotal = ((QuantityVal * fatval) / 100);
            SNFTotal = ((QuantityVal * SNFTotal) / 100);


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtgvV_Fat_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            decimal SNF = 0, CLR = 0, FAT = 1, FATTotal = 0, fatval = 0, SNFTotal = 0, QuantityVal = 0;


            int selRowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
            TextBox FatID = (TextBox)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("txtgvV_Fat");
            Label Quantity = (Label)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("lblgvV_Quantity");
            TextBox ClrID = (TextBox)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("txtgvV_Clr");
            Label SNFID = (Label)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("lblgvV_SNF");
            Label lblmilkRate = (Label)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("lblmilkRate");
            Label lblnetAmount = (Label)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("lblnetAmount");

            TextBox txtgvV_Remark = (TextBox)gvSocietyMilkCollection.Rows[selRowIndex].FindControl("txtgvV_Remark");

            if (FatID.Text != "" && ClrID.Text != "")
            {
                QuantityVal = QuantityVal + Convert.ToDecimal(Quantity.Text);
                FAT = Convert.ToDecimal(FatID.Text);
                CLR = Convert.ToDecimal(ClrID.Text);
                fatval = fatval + FAT;
                SNF = ((CLR / 4) + (FAT * Convert.ToDecimal(0.2)) + (Convert.ToDecimal(0.7)));
                SNFTotal = SNFTotal + SNF;
                SNFID.Text = SNF.ToString();

                lblmilkRate.Text = obj_MC.GetRatePerLtrOrKg(FAT, CLR, objdb.Office_ID()); 
                lblnetAmount.Text = Math.Round((Convert.ToDecimal(Quantity.Text)) * (Convert.ToDecimal(lblmilkRate.Text)), 3).ToString();
                Quantity.ToolTip = lblnetAmount.Text + " @ " + lblmilkRate.Text + " Per Ltr";
                txtgvV_Remark.Text = lblnetAmount.Text + " @ " + lblmilkRate.Text + " Per Ltr";


            }
            else
            {
                SNFID.Text = "";
                lblmilkRate.Text = "";
                Quantity.ToolTip = "";
                lblnetAmount.Text = "";
                txtgvV_Remark.Text = "";

            }
            FATTotal = ((QuantityVal * fatval) / 100);
            SNFTotal = ((QuantityVal * SNFTotal) / 100);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            ddlShift.ClearSelection();
            gvSocietyMilkCollection.DataSource = null;
            gvSocietyMilkCollection.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        MilkCollection();
    }

    protected void MilkCollection()
    {
        try
        {
            //lblMsg.Text = "";

            if (ddlShift.SelectedIndex > 0)
            {
                ds = null;
                ds = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                         new string[] { "flag", "EntryDate", "EntryShift", "OfficeId" },
                         new string[] { "5", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd"), ddlShift.SelectedValue.ToString(), objdb.Office_ID() }, "dataset");

                if (ds.Tables.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DivGrid.Visible = true;
                        gvSocietyMilkCollection.DataSource = ds.Tables[0];
                        gvSocietyMilkCollection.DataBind();
                    }

                    else
                    {

                        DivGrid.Visible = false;
                        gvSocietyMilkCollection.DataSource = null;
                        gvSocietyMilkCollection.DataBind();

                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        // Div1.Visible = true;
                        div_milkdetails.Visible = true;
                        GrdCollectionDetails.DataSource = ds.Tables[1];
                        GrdCollectionDetails.DataBind();
                    }

                    else
                    {
                        div_milkdetails.Visible = false;
                        // Div1.Visible = false;
                        GrdCollectionDetails.DataSource = null;
                        GrdCollectionDetails.DataBind();
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        //div1MilkDetail.Visible = true;
                        lblFAT_IN_KG.Text = ds.Tables[2].Rows[0]["FAT_IN_KG"].ToString();
                        lblSNF_IN_KG.Text = ds.Tables[2].Rows[0]["SNF_IN_KG"].ToString();
                        lblMqinkg.Text = ds.Tables[2].Rows[0]["Milk_InKG"].ToString();
                    }
                    else
                    {
                        // div1MilkDetail.Visible = false;
                    }


                }
                else
                {
                    div_milkdetails.Visible = false;
                    gvSocietyMilkCollection.DataSource = null;
                    gvSocietyMilkCollection.DataBind();
                    GrdCollectionDetails.DataSource = null;
                    GrdCollectionDetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private DataTable GetMilkProcess()
    {
        decimal TFAT1 = 0;
        decimal TCLR1 = 0;
        decimal TSNF1 = 0;

        decimal Mrate = 0;
        decimal MNAmount = 0;

        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("Producer_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Quality", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkType", typeof(string)));

        dt.Columns.Add(new DataColumn("Fat", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CLR", typeof(decimal)));
        dt.Columns.Add(new DataColumn("SNF", typeof(decimal)));

        dt.Columns.Add(new DataColumn("Rate", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Amount", typeof(decimal)));

        dt.Columns.Add(new DataColumn("Remark", typeof(string)));

        dt.Columns.Add(new DataColumn("TotalSNFInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TotalFatInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("I_CollectionID", typeof(string)));


        foreach (GridViewRow row in gvSocietyMilkCollection.Rows)
        {
            Label FarmerID = (Label)row.FindControl("lblgvI_Producer_ID");
            DropDownList Quality = (DropDownList)row.FindControl("ddlQuality");
            Label MilkType = (Label)row.FindControl("lblgvV_MilkType");
            TextBox FatID = (TextBox)row.FindControl("txtgvV_Fat");
            TextBox ClrID = (TextBox)row.FindControl("txtgvV_Clr");
            Label SNFID = (Label)row.FindControl("lblgvV_SNF");
            TextBox txtgvV_Remark = (TextBox)row.FindControl("txtgvV_Remark");
            Label lblgvI_CollectionID = (Label)row.FindControl("lblgvI_CollectionID");

            Label lblmilkRate = (Label)row.FindControl("lblmilkRate");
            Label lblnetAmount = (Label)row.FindControl("lblnetAmount");


            if (lblmilkRate.Text != "0" && lblmilkRate.Text != "0.00" && lblmilkRate.Text != "0.0" && lblmilkRate.Text != "")
            {
                Mrate = Convert.ToDecimal(lblmilkRate.Text);
            }
            else
            {
                Mrate = 0;
            }

            if (lblnetAmount.Text != "0" && lblnetAmount.Text != "0.00" && lblnetAmount.Text != "0.0" && lblnetAmount.Text != "")
            {
                MNAmount = Convert.ToDecimal(lblnetAmount.Text);
            }
            else
            {
                MNAmount = 0;
            }
             

            if (FatID.Text != "0" && FatID.Text != "0.00" && FatID.Text != "0.0" && FatID.Text != "")
            {
                TFAT1 = Convert.ToDecimal(FatID.Text);
            }
            else
            {
                TFAT1 = 0;
            }

            if (ClrID.Text != "0" && ClrID.Text != "0.00" && ClrID.Text != "0.0" && ClrID.Text != "")
            {
                TCLR1 = Convert.ToDecimal(ClrID.Text);
            }
            else
            {
                TCLR1 = 0;
            }

            if (SNFID.Text != "0" && SNFID.Text != "0.00" && SNFID.Text != "0.0" && SNFID.Text != "")
            {
                TSNF1 = Convert.ToDecimal(SNFID.Text);
            }
            else
            {
                TSNF1 = 0;
            }



            if (TSNF1 != 0 && TCLR1 != 0 && TSNF1 != 0)
            {
                dr = dt.NewRow();
                dr[0] = FarmerID.Text;
                dr[1] = Quality.Text;
                dr[2] = MilkType.Text;
                dr[3] = TFAT1;
                dr[4] = TCLR1;
                dr[5] = TSNF1;
                dr[6] = Mrate;
                dr[7] = MNAmount;
                dr[8] = txtgvV_Remark.Text;
                dr[9] = 0.00;
                dr[10] = 0.00;
                dr[11] = lblgvI_CollectionID.Text;
                dt.Rows.Add(dr);
            }


        }

        return dt;

    }

    protected void rfvShift_Init(object sender, EventArgs e)
    {
        try
        {
            DataSet dsct = objdb.ByProcedure("USP_GetServerDatetime",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");

            string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

            string[] s = currrentime.Split(':');

            if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
            {
                ddlShift.SelectedValue = "Morning";
            }
            else
            {
                ddlShift.SelectedValue = "Evening";
            }

            //txtDate.Attributes.Add("readonly", "readonly");
            //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //ddlShift.Enabled = false;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                int TFAT = 0;
                int TCLR = 0;
                int TSNF = 0;

                foreach (GridViewRow row in gvSocietyMilkCollection.Rows)
                {

                    TextBox FatID = (TextBox)row.FindControl("txtgvV_Fat");
                    TextBox ClrID = (TextBox)row.FindControl("txtgvV_Clr");
                    Label SNFID = (Label)row.FindControl("lblgvV_SNF");

                    if (FatID.Text == "0" || FatID.Text == "0.00" || FatID.Text == "0.0")
                    {
                        TFAT = 1;
                    }

                    if (ClrID.Text == "0" || ClrID.Text == "0.00" || ClrID.Text == "0.0")
                    {
                        TCLR = 1;
                    }

                    if (SNFID.Text == "0" || SNFID.Text == "0.00" || SNFID.Text == "0.0")
                    {
                        TSNF = 1;
                    }

                }

                if (TFAT == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "FAT Can't Blank or '0' or '0.00' or '0.0'");
                    return;
                }

                if (TCLR == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "CLR Can't Blank or '0' or '0.00' or '0.0'");
                    return;
                }

                if (TSNF == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "SNF Can't Blank or '0' or '0.00' or '0.0'");
                    return;
                }


                DataTable dtIDF = new DataTable();
                dtIDF = GetMilkProcess();


                if (dtIDF.Rows.Count > 0)
                {

                    ds = null;
                    ds = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                                              new string[] { "Flag" 
				                                ,"OfficeId"
				                                ,"EntryDate" 
				                                ,"EntryShift" 
				                                ,"CreatedBy"
				                                ,"CreatedIP" 
                                    },
                                              new string[] { "1"
                                              ,ViewState["Office_ID"].ToString()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                              , ddlShift.SelectedValue.ToString()
                                              , ViewState["Emp_ID"].ToString()
                                              , objdb.GetLocalIPAddress()
                                    },
                                             new string[] { "type_Mst_SocietyWiseMilkProcess" },
                                             new DataTable[] { dtIDF }, "TableSave");

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you! Record Save Successfully", "");
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Record already Exist.", "");
                        }
                    }


                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Data Can't Insert Without Any Value");
                    return;
                }


                MilkCollection();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



}
