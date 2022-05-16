using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_RMRD_Challan_Entry_Edit : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
               
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                //ddlItemBillingHead_Type.ClearSelection();
                //ddlHeaddetails.Items.Clear();
                //ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
                gv_HeadDetails.DataSource = string.Empty;
                gv_HeadDetails.DataBind();
                FillBMCRoot();
                //ddlMilkCollectionUnit_SelectedIndexChanged(sender, e);
                
                //ddlMilkCollectionUnit.Enabled = false;
                GetCCDetails();
                FillSociety();
                ddlBMCTankerRootName.Enabled = false;
				ddlShift.Focus();
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

    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }

    protected void FillSociety()
    {
        try
        {
            if (ddlCC.SelectedValue != "0")
            {
                ds = null;
                ds = null;
                //ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                //                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                //                  new string[] { "18", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");
                ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry",
                         new string[] { "flag", "Office_ID", "Office_Parant_ID", "OfficeType_ID" },
                         new string[] { "18", ddlCC.SelectedValue.ToString(), objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));


                    }
                    else
                    {
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
               // Response.Redirect("SocietywiseMilkCollectionInvoice.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillBBMCDCS()
    {
        try
        {
            if (ddlCC.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("SpAdminOffice",
                                  new string[] { "flag", "Office_ID" },
                                  new string[] { "25", ddlSociety.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBMCDCS.DataTextField = "Office_Name";
                        ddlBMCDCS.DataValueField = "Office_ID";
                        ddlBMCDCS.DataSource = ds.Tables[0];
                        ddlBMCDCS.DataBind();
                        ddlBMCDCS.Items.Insert(0, new ListItem("Select", "0"));


                    }
                    else
                    {
                        ddlBMCDCS.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    ddlBMCDCS.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                //Response.Redirect("SocietywiseMilkCollectionInvoice.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    public void FillBMCRoot()
    {

        try
        {
            ds = null;
           ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                      new string[] { "flag", "Office_ID", "OfficeType_ID" },
                      new string[] { "11",objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlBMCTankerRootName.DataSource = ds;
                ddlBMCTankerRootName.DataBind();
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                   new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                   new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCC.DataTextField = "Office_Name";
                        ddlCC.DataValueField = "Office_ID";
                        ddlCC.DataSource = ds;
                        ddlCC.DataBind();
                        ddlCC.Items.Insert(0, new ListItem("Select", "0"));

                        //ddlFltrCC.DataTextField = "Office_Name";
                        //ddlFltrCC.DataValueField = "Office_ID";
                        //ddlFltrCC.DataSource = ds;
                        //ddlFltrCC.DataBind();
                        //ddlFltrCC.Items.Insert(0, new ListItem("Select", "0"));
                        //if (objdb.OfficeType_ID() == "4" || objdb.OfficeType_ID() == "5")
                        //{
                        //    ddlCC.SelectedValue = objdb.Office_ID();
                        //    ddlFltrCC.SelectedValue = objdb.Office_ID();
                        //}

                    }
                }
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlHeaddetails_Init(object sender, EventArgs e)
    {
        try
        {
            // ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }

    protected void ddlItemBillingHead_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            //if (ddlItemBillingHead_Type.SelectedValue != "0")
            //{
            //    ds = null;
            //    ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
            //   new string[] { "flag", "ItemBillingHead_Type" },
            //   new string[] { "8", ddlItemBillingHead_Type.SelectedValue }, "dataset");

            //    ddlHeaddetails.DataTextField = "ItemBillingHead_Name";
            //    ddlHeaddetails.DataValueField = "ItemBillingHead_ID";
            //    ddlHeaddetails.DataSource = ds;
            //    ddlHeaddetails.DataBind();
            //    ddlHeaddetails.Items.Insert(0, new ListItem("Select", "0"));

            //}
            //else
            //{
            //    ddlItemBillingHead_Type.ClearSelection();
            //    ddlHeaddetails.Items.Clear();
            //    ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            //}


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }


    }

    private decimal GetSNF()
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (txtNetCLR.Text != "")
            { clr = Convert.ToDecimal(txtNetCLR.Text); }
            if (txtNetFat.Text != "")
            { fat = Convert.ToDecimal(txtNetFat.Text); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            //snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
            snf = Obj_MC.GetSNFPer_DCS(fat, clr);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 3);
    }

    private decimal GetSNF_InKG()
    {
        decimal clr = 0, fat = 0, snf_Per = 0, MilkQty = 0, SNF_InKG = 0;

        try
        {
            if (txtI_MilkQuantity.Text == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(txtI_MilkQuantity.Text); }

            if (txtNetFat.Text == "") { fat = 0; } else { fat = Convert.ToDecimal(txtNetFat.Text); }

            if (txtNetCLR.Text == "") { clr = 0; } else { clr = Convert.ToDecimal(txtNetCLR.Text); }

            snf_Per = Obj_MC.GetSNFPer_DCS(fat, clr);

            SNF_InKG = Obj_MC.GetSNFInKg(MilkQty, snf_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(SNF_InKG, 3);
    }

    private decimal GetFAT_InKG()
    {
        decimal fat_Per = 0, MilkQty = 0, FAT_InKG = 0;

        try
        {
            if (txtI_MilkQuantity.Text == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(txtI_MilkQuantity.Text); }

            if (txtNetFat.Text == "") { fat_Per = 0; } else { fat_Per = Convert.ToDecimal(txtNetFat.Text); }

            FAT_InKG = Obj_MC.GetSNFInKg(MilkQty, fat_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(FAT_InKG, 3);
    }

    protected void txtI_MilkQuantity_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();
        txtfatinkg.Text = GetFAT_InKG().ToString();
        txtsnfinkg.Text = GetSNF_InKG().ToString();

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }

    }

    protected void txtNetFat_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();
        txtfatinkg.Text = GetFAT_InKG().ToString();
        txtsnfinkg.Text = GetSNF_InKG().ToString();

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }
		
    }

    protected void txtNetCLR_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();
        txtfatinkg.Text = GetFAT_InKG().ToString();
        txtsnfinkg.Text = GetSNF_InKG().ToString();

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }
		btnAdd.Focus();
    }


    protected void btnAddSocietyDetails_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddBMCDCSDetails();
		ddlBMCDCS.Focus();
    }

    private void AddBMCDCSDetails()
    {
        try
        {
            //int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                

            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("Office_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Fat", typeof(decimal)));
                dt.Columns.Add(new DataColumn("LR", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Snf", typeof(decimal)));
                dt.Columns.Add(new DataColumn("kgFat", typeof(decimal)));
                dt.Columns.Add(new DataColumn("kgSnf", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Shift", typeof(string)));
                dt.Columns.Add(new DataColumn("Quality", typeof(string)));
                dt.Columns.Add(new DataColumn("FirstEntry", typeof(string)));
                dt.Columns.Add(new DataColumn("Temp", typeof(string)));
                DT = (DataTable)ViewState["InsertRecord"];
                int Count = DT.Rows.Count;
                dr = dt.NewRow();
                dr[0] = Count + 1;
                dr[1] = ddlBMCDCS.SelectedValue;
                dr[2] = ddlBMCDCS.SelectedItem.Text;
                dr[3] = ddlMilkType.SelectedItem.Text;
                dr[4] = txtQuantity.Text;
                dr[5] = txtFAT.Text;
                dr[6] = txtLR.Text;
                decimal SNF = GetGridSNF();
                decimal snfinkg = GetGridSNF_InKG();
                decimal fatinkg = GetGridFAT_InKG();
                dr[7] = SNF;

                dr[8] = fatinkg;
                dr[9] = snfinkg;
                dr[10] = ddlBMCDCSShift.SelectedItem.Text;
                dr[11] = ddlBMCDCSMilkQuality.SelectedItem.Text;
                dr[12] = "No";
                dr[13] = txtBMCDCSTemp.Text;
                dt.Rows.Add(dr);
                foreach (DataRow tr in DT.Rows)
                {
                    if (tr["Office_ID"].ToString() == ddlSociety.SelectedValue && tr["FirstEntry"].ToString() == "Yes")
                    {

                        tr["Quantity"] = decimal.Parse(tr["Quantity"].ToString()) - decimal.Parse(txtQuantity.Text);

                        tr["kgFat"] = decimal.Parse(tr["kgFat"].ToString()) - fatinkg;
                        tr["kgSnf"] = decimal.Parse(tr["kgSnf"].ToString()) - snfinkg;
                        ds = objdb.ByProcedure("MilkCalculation", new string[] { "flag", "MilkQtyInKg", "FATInPer" }, new string[] { "3", "591", txtNetFat.Text }, "dataset");

                        tr["Fat"] = Math.Round((decimal.Parse(tr["kgFat"].ToString()) * 100 / (decimal.Parse(tr["Quantity"].ToString()))), 1);

                        tr["Snf"] = Math.Round((decimal.Parse(tr["kgSnf"].ToString()) * 100) / (decimal.Parse(tr["Quantity"].ToString())), 3);
                        tr["LR"] = Math.Round(4 * (decimal.Parse(tr["Snf"].ToString()) - (Convert.ToDecimal(0.2) * decimal.Parse(tr["Fat"].ToString())) - Convert.ToDecimal(0.7)), 0);

                    }
                    dt.Rows.Add(tr.ItemArray);

                }
                dt.DefaultView.Sort = "S.No asc";
                dt = dt.DefaultView.ToTable();
                ViewState["InsertRecord"] = dt;
               
                gv_HeadDetails.DataSource = dt;
                gv_HeadDetails.DataBind();



            }
            decimal TotalQTY = 0;
            decimal TotalFAT = 0;
            decimal TotalSnf = 0;
            decimal TotalClr = 0;
            decimal Totalfatinkg = 0;
            decimal TotalSnfinkg = 0;
            int i = 0;
            foreach (GridViewRow rows in gv_HeadDetails.Rows)
            {
                i += 1;
                Label lblQuantity = (Label)rows.FindControl("lblQuantity");
                Label lblFat = (Label)rows.FindControl("lblFat");
                Label lblSnf = (Label)rows.FindControl("lblSnf");
                Label lblLR = (Label)rows.FindControl("lblLR");
                Label lblkgFat = (Label)rows.FindControl("lblkgFat");
                Label lblkgSnf = (Label)rows.FindControl("lblkgSnf");
                Label lblEntry = (Label)rows.FindControl("lblEntry");
                LinkButton lnkbtnDelete = (LinkButton)rows.FindControl("lnkbtnDelete");
                if (lblEntry.Text == "Yes")
                {
                    lnkbtnDelete.Visible = false;
                }
                TotalQTY += decimal.Parse(lblQuantity.Text);
                //TotalFAT += decimal.Parse(lblFat.Text);
               // TotalSnf += decimal.Parse(lblSnf.Text);
                //TotalClr += decimal.Parse(lblLR.Text);
                Totalfatinkg += decimal.Parse(lblkgFat.Text);
                TotalSnfinkg += decimal.Parse(lblkgSnf.Text);


            }
			TotalFAT = (Totalfatinkg / TotalQTY) * 100;
			TotalSnf = (TotalSnfinkg / TotalQTY) * 100;
			TotalClr = Obj_MC.GetCLR_DCS(TotalFAT, TotalSnf);
            gv_HeadDetails.FooterRow.Cells[5].Text = "<b>Sub Total : </b>";
            gv_HeadDetails.FooterRow.Cells[6].Text = "<b>" + Math.Round(TotalQTY, 3) + "</b>";
             //gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(((TotalFAT) / i), 2) + "</b>";
		    gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(TotalFAT, 2) + "</b>";
            //gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(((TotalClr) / i), 1) + "</b>";
			gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(TotalClr, 2) + "</b>";
            //gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(((TotalSnf) / i), 2) + "</b>";
			gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(TotalSnf, 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[10].Text = "<b>" + Math.Round(Totalfatinkg, 3) + "</b>";
            gv_HeadDetails.FooterRow.Cells[11].Text = "<b>" + Math.Round(TotalSnfinkg, 3) + "</b>";

            ddlBMCDCSMilkQuality.SelectedValue = "Good";
            ddlBMCDCSShift.SelectedValue = ddlShift.SelectedValue;
            txtQuantity.Text = "";
            txtFAT.Text = "";
            txtLR.Text = "";



        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }

    }

    private decimal GetGridSNF()
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (txtLR.Text != "")
            { clr = Convert.ToDecimal(txtLR.Text); }
            if (txtFAT.Text != "")
            { fat = Convert.ToDecimal(txtFAT.Text); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            //snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
            snf = Obj_MC.GetSNFPer_DCS(fat, clr);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 3);
    }

    private decimal GetGridSNF_InKG()
    {
        decimal clr = 0, fat = 0, snf_Per = 0, MilkQty = 0, SNF_InKG = 0;

        try
        {
            if (txtQuantity.Text == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(txtQuantity.Text); }

            if (txtFAT.Text == "") { fat = 0; } else { fat = Convert.ToDecimal(txtFAT.Text); }

            if (txtLR.Text == "") { clr = 0; } else { clr = Convert.ToDecimal(txtLR.Text); }

            snf_Per = Obj_MC.GetSNFPer_DCS(fat, clr);

            SNF_InKG = Obj_MC.GetSNFInKg(MilkQty, snf_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(SNF_InKG, 3);
    }

    private decimal GetGridFAT_InKG()
    {
        decimal fat_Per = 0, MilkQty = 0, FAT_InKG = 0;

        try
        {
            if (txtQuantity.Text == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(txtQuantity.Text); }

            if (txtFAT.Text == "") { fat_Per = 0; } else { fat_Per = Convert.ToDecimal(txtFAT.Text); }

            FAT_InKG = Obj_MC.GetSNFInKg(MilkQty, fat_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(FAT_InKG, 3);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            panel2.Visible = true;
            FillBBMCDCS();
            if(btnAdd.Text == "Edit")
            
            {
                
                DataTable DT = new DataTable();
                DataRow dr;
                DT = (DataTable)ViewState["InsertRecord"];
                decimal Quantity = 0;
                
                decimal KgFat = 0;
                decimal KgSnf = 0;
               
                foreach (DataRow tr in DT.Rows)
                {
                    if (tr["FirstEntry"].ToString() != "Yes")
                    {
                        Quantity += decimal.Parse(tr["Quantity"].ToString());
                        KgFat += decimal.Parse(tr["kgFat"].ToString());
                        KgSnf += decimal.Parse(tr["kgSnf"].ToString());
                        
                        

                    }
                    

                }
                foreach (DataRow tr in DT.Rows)
                {
                    if (tr["Office_ID"].ToString() == ddlSociety.SelectedValue && tr["FirstEntry"].ToString() == "Yes")
                    {

                        tr["Quantity"] = decimal.Parse(txtI_MilkQuantity.Text) - Quantity;

                        tr["kgFat"] = decimal.Parse(txtfatinkg.Text) - KgFat;
                        tr["kgSnf"] = decimal.Parse(txtsnfinkg.Text) - KgSnf;
                       ds = objdb.ByProcedure("MilkCalculation", new string[] { "flag", "MilkQtyInKg", "FATInPer" }, new string[] { "3", "591", txtNetFat.Text }, "dataset");

                       tr["Fat"] = Math.Round((decimal.Parse(tr["kgFat"].ToString()) * 100 / (decimal.Parse(tr["Quantity"].ToString()))), 1);

                       tr["Snf"] = Math.Round((decimal.Parse(tr["kgSnf"].ToString()) * 100) / (decimal.Parse(tr["Quantity"].ToString())), 3);
                       tr["LR"] = Math.Round(4 * (decimal.Parse(tr["Snf"].ToString()) - (Convert.ToDecimal(0.2) * decimal.Parse(tr["Fat"].ToString())) - Convert.ToDecimal(0.7)), 0);

                    }
                    DT.AcceptChanges();

                }
                DT.DefaultView.Sort = "S.No asc";
                DT = DT.DefaultView.ToTable();
                ViewState["InsertRecord"] = DT;
               
                gv_HeadDetails.DataSource = DT;
                gv_HeadDetails.DataBind();
            }
            decimal TotalQTY = 0;
            decimal TotalFAT = 0;
            decimal TotalSnf = 0;
            decimal TotalClr = 0;
            decimal Totalfatinkg = 0;
            decimal TotalSnfinkg = 0;
            int i = 0;
            foreach (GridViewRow rows in gv_HeadDetails.Rows)
            {
                i += 1;
                Label lblQuantity = (Label)rows.FindControl("lblQuantity");
                Label lblFat = (Label)rows.FindControl("lblFat");
                Label lblSnf = (Label)rows.FindControl("lblSnf");
                Label lblLR = (Label)rows.FindControl("lblLR");
                Label lblkgFat = (Label)rows.FindControl("lblkgFat");
                Label lblkgSnf = (Label)rows.FindControl("lblkgSnf");
                Label lblEntry = (Label)rows.FindControl("lblEntry");
                LinkButton lnkbtnDelete = (LinkButton)rows.FindControl("lnkbtnDelete");
                if (lblEntry.Text == "Yes")
                {
                    lnkbtnDelete.Visible = false;
                }
                TotalQTY += decimal.Parse(lblQuantity.Text);
                //TotalFAT += decimal.Parse(lblFat.Text);
                //TotalSnf += decimal.Parse(lblSnf.Text);
                //TotalClr += decimal.Parse(lblLR.Text);
                Totalfatinkg += decimal.Parse(lblkgFat.Text);
                TotalSnfinkg += decimal.Parse(lblkgSnf.Text);


            }
            TotalFAT = (Totalfatinkg / TotalQTY) * 100;
            TotalSnf = (TotalSnfinkg / TotalQTY) * 100;
            TotalClr = Obj_MC.GetCLR_DCS(TotalFAT, TotalSnf);
            gv_HeadDetails.FooterRow.Cells[5].Text = "<b>Sub Total : </b>";
            gv_HeadDetails.FooterRow.Cells[6].Text = "<b>" + Math.Round(TotalQTY, 3) + "</b>";
            //gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(((TotalFAT) / i), 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(TotalFAT, 2) + "</b>";
            //gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(((TotalClr) / i), 1) + "</b>";
            gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(TotalClr, 2) + "</b>";
            //gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(((TotalSnf) / i), 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(TotalSnf, 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[10].Text = "<b>" + Math.Round(Totalfatinkg, 3) + "</b>";
            gv_HeadDetails.FooterRow.Cells[11].Text = "<b>" + Math.Round(TotalSnfinkg, 3) + "</b>";
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
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

           if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 18 && ((Convert.ToInt32(s[0]) < 18 && Convert.ToInt32(s[1]) <= 59) || (Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1))))
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                lblMsg.Text = "";
                DataTable dt2 = new DataTable();
                dt2 = GetSocietyDetails();
                if (dt2.Rows.Count > 0)
                {
                    ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntry"
                                           , new string[]{"flag"
                                                      ,"EntryDate"
                                                     ,"Office_ID"
                                                     ,"CreatedBy"
                                                     ,"CreatedByIP"
													 ,"Created_Office_ID"
													 ,"BMCTankerRoot_Id"
                                                     ,"AttachedBMC_ID"
                                                     ,"CC_ID"
                                                     }
                                             , new string[]{"7"
                                                      ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                                                      ,ddlSociety.SelectedValue
                                                      ,ViewState["Emp_ID"].ToString()
                                                      ,objdb.GetLocalIPAddress()   
                                                      ,objdb.Office_ID()   
                                                      ,ddlBMCTankerRootName.SelectedValue.ToString()	
												      ,ddlSociety.SelectedValue
                                                      ,ddlCC.SelectedValue
                                                       }

                                            , new string[] { "type_MilkCollectionChallanEntry" },
                                            new DataTable[] { dt2 }, "TableSave");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                        ClearText();
                        

                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", Warning.ToString());
                        ClearText();


                    }
                    else
                    {
                        string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Warning.ToString());
                        ClearText();
                    }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    private DataTable GetSocietyDetails()
    {
        string Office_ID = "";
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("Office_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("AttachedBMC_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("Shift", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dt.Columns.Add(new DataColumn("Temp", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkQuantity", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dt.Columns.Add(new DataColumn("Fat", typeof(string)));
        dt.Columns.Add(new DataColumn("Snf", typeof(string)));
        dt.Columns.Add(new DataColumn("Clr", typeof(string)));
        dt.Columns.Add(new DataColumn("FatInKg", typeof(string)));
        dt.Columns.Add(new DataColumn("SnfInKg", typeof(string)));

        foreach (GridViewRow row in gv_HeadDetails.Rows)
        {
            Label lblOffice_ID = (Label)row.FindControl("lblOffice_ID");
            Label lblMilkType = (Label)row.FindControl("lblMilkType");
            Label lblTemp = (Label)row.FindControl("lblTemp");
            Label lblShift = (Label)row.FindControl("lblShift");
            Label lblQuality = (Label)row.FindControl("lblQuality");
            Label lblQuantity = (Label)row.FindControl("lblQuantity");
            Label lblFat = (Label)row.FindControl("lblFat");
            Label lblLR = (Label)row.FindControl("lblLR");
            Label lblSnf = (Label)row.FindControl("lblSnf");
            Label lblkgFat = (Label)row.FindControl("lblkgFat");
            Label lblkgSnf = (Label)row.FindControl("lblkgSnf");

            if (decimal.Parse(lblQuantity.Text) < 0)
            {
                Office_ID = lblOffice_ID.Text;
            }
            else
            {
                dr = dt.NewRow();
                dr[0] = lblOffice_ID.Text;
                dr[1] = ddlSociety.SelectedValue;
                dr[2] = lblShift.Text;
                dr[3] = lblMilkType.Text;
                dr[4] = lblTemp.Text;
                dr[5] = lblQuantity.Text;
                dr[6] = lblQuality.Text;
                dr[7] = lblFat.Text;
                dr[8] = lblSnf.Text;
                dr[9] = lblLR.Text;
                dr[10]= lblkgFat.Text;
                dr[11] = lblkgSnf.Text;
                dt.Rows.Add(dr);
            }

        }
        if (Office_ID.ToString() != "")
        {
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow drrow = dt.Rows[i];
                if (drrow["Office_ID"].ToString() == Office_ID.ToString())
                {
                    dt.Rows.Remove(dt.Rows[i]);
                }
            }
            dt.AcceptChanges();
        }

        return dt;
    }
    protected void ddlSociety_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDetails();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
		
    }
    protected void ClearText()
    {
        ViewState["InsertRecord"] = null;
        gv_HeadDetails.DataSource = string.Empty;
        gv_HeadDetails.DataBind();
        
        panel2.Visible = false;
        ddlSociety.ClearSelection();
        ddlMilkType.ClearSelection();
        txtI_MilkQuantity.Text = "";
        txtNetFat.Text = "";
        txtnetsnf.Text = "";
        txtNetCLR.Text = "";
        txtfatinkg.Text = "";
        txtsnfinkg.Text = "";
        ddlBMCTankerRootName.ClearSelection();

        btnAdd.Visible = false;
    }
    protected void gv_HeadDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl_Quantity = (Label)e.Row.FindControl("lblQuantity");
            if (decimal.Parse(lbl_Quantity.Text) < 1)
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC00");
            }
        }
        //if (e.Row.Cells[5].Text.Contains("-"))
        //{
        //    e.Row.BackColor = System.Drawing.Color.DarkRed;

        // }
    }
    protected void ddlBMCTankerRootName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //lblMsg.Text = "";
            //ds = null;
            //ddlSociety.Items.Clear();
            //if (ddlBMCTankerRootName.SelectedIndex > 0)
            //{

            //    ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
            //               new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID" },
            //               new string[] { "10", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {

            //        ddlSociety.DataTextField = "Office_Name";
            //        ddlSociety.DataValueField = "I_OfficeID";
            //        ddlSociety.DataSource = ds.Tables[0];
            //        ddlSociety.DataBind();
            //        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
            //    }
            //    else
            //    {
            //        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
            //    }

            //}
            //else
            //{

            //    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
   
    protected void lnkbtnDelete_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        DataTable dt2 = ViewState["InsertRecord"] as DataTable;
        decimal Quantity = decimal.Parse(dt2.Rows[row.RowIndex]["Quantity"].ToString());
        decimal fatinkg = decimal.Parse(dt2.Rows[row.RowIndex]["kgFat"].ToString());
        decimal snfinkg = decimal.Parse(dt2.Rows[row.RowIndex]["kgSnf"].ToString());
        decimal Fat = decimal.Parse(dt2.Rows[row.RowIndex]["Fat"].ToString());
        decimal Snf = decimal.Parse(dt2.Rows[row.RowIndex]["Snf"].ToString());
        decimal LR = decimal.Parse(dt2.Rows[row.RowIndex]["LR"].ToString());

        foreach (DataRow tr in dt2.Rows)
        {
            if (tr["Office_ID"].ToString() == ddlSociety.SelectedValue && tr["FirstEntry"].ToString() == "Yes")
            {

                tr["Quantity"] = decimal.Parse(tr["Quantity"].ToString()) + Quantity;

                tr["kgFat"] = decimal.Parse(tr["kgFat"].ToString()) + fatinkg;
                tr["kgSnf"] = decimal.Parse(tr["kgSnf"].ToString()) + snfinkg;


                tr["Fat"] = Math.Round((decimal.Parse(tr["kgFat"].ToString()) * 100 / (decimal.Parse(tr["Quantity"].ToString()))), 1);

                tr["Snf"] = Math.Round((decimal.Parse(tr["kgSnf"].ToString()) * 100) / (decimal.Parse(tr["Quantity"].ToString())), 3);
                tr["LR"] = Math.Round(4 * (decimal.Parse(tr["Snf"].ToString()) - (Convert.ToDecimal(0.2) * decimal.Parse(tr["Fat"].ToString())) - Convert.ToDecimal(0.7)), 0);
                //dt2.Rows.Add(tr.ItemArray);
            }


        }
        dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
        dt2.AcceptChanges();
        ViewState["InsertRecord"] = dt2;
        gv_HeadDetails.DataSource = dt2;
        gv_HeadDetails.DataBind();
        decimal TotalQTY = 0;
        decimal TotalFAT = 0;
        decimal TotalSnf = 0;
        decimal TotalClr = 0;
        decimal Totalfatinkg = 0;
        decimal TotalSnfinkg = 0;
        int i = 0;
        foreach (GridViewRow rows in gv_HeadDetails.Rows)
        {
            i += 1;
            Label lblQuantity = (Label)rows.FindControl("lblQuantity");
            Label lblFat = (Label)rows.FindControl("lblFat");
            Label lblSnf = (Label)rows.FindControl("lblSnf");
            Label lblLR = (Label)rows.FindControl("lblLR");
            Label lblkgFat = (Label)rows.FindControl("lblkgFat");
            Label lblkgSnf = (Label)rows.FindControl("lblkgSnf");
            Label lblEntry = (Label)rows.FindControl("lblEntry");
            LinkButton lnkbtnDelete = (LinkButton)rows.FindControl("lnkbtnDelete");
            if (lblEntry.Text == "Yes")
            {
                lnkbtnDelete.Visible = false;
            }
            TotalQTY += decimal.Parse(lblQuantity.Text);
           // TotalFAT += decimal.Parse(lblFat.Text);
            //TotalSnf += decimal.Parse(lblSnf.Text);
            TotalClr += decimal.Parse(lblLR.Text);
            Totalfatinkg += decimal.Parse(lblkgFat.Text);
            TotalSnfinkg += decimal.Parse(lblkgSnf.Text);


        }
		    TotalFAT = (Totalfatinkg / TotalQTY) * 100;
			TotalSnf = (TotalSnfinkg / TotalQTY) * 100;
			TotalClr = Obj_MC.GetCLR_DCS(TotalFAT, TotalSnf);
            gv_HeadDetails.FooterRow.Cells[5].Text = "<b>Sub Total : </b>";
            gv_HeadDetails.FooterRow.Cells[6].Text = "<b>" + Math.Round(TotalQTY, 3) + "</b>";
             //gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(((TotalFAT) / i), 2) + "</b>";
		    gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(TotalFAT, 2) + "</b>";
            //gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(((TotalClr) / i), 1) + "</b>";
			gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(TotalClr, 2) + "</b>";
            //gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(((TotalSnf) / i), 2) + "</b>";
			gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(TotalSnf, 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[10].Text = "<b>" + Math.Round(Totalfatinkg, 3) + "</b>";
            gv_HeadDetails.FooterRow.Cells[11].Text = "<b>" + Math.Round(TotalSnfinkg, 3) + "</b>";
    }
	protected void txtFAT_TextChanged(object sender, EventArgs e)
    {
        if (txtFAT.Text == "") { txtFAT.Text = "0"; }

        if (Convert.ToDecimal(txtFAT.Text) > Convert.ToDecimal(5.5))
        {
            ddlMilkType.ClearSelection();
            ddlMilkType.SelectedValue = "1";
        }
        else
        {
            ddlMilkType.ClearSelection();
            ddlMilkType.SelectedValue = "2";
        }
		if (txtLR.Text == "")
        {
            txtLR.Focus();
        }

        if (txtFAT.Text == "")
        {
            txtFAT.Focus();
        }
    }
    protected void FillDetails()
    {
        try
        {
            lblMsg1.Text = "";
            panel2.Visible = false;
            btnAdd.Visible = false;
            ViewState["InsertRecord"] = null;
            txtI_MilkQuantity.Text = "";
            txtfatinkg.Text = "";
            txtsnfinkg.Text = "";
            txtNetFat.Text = "";
            txtnetsnf.Text = "";
            txtNetCLR.Text = "";
            FillBBMCDCS();
            if(ddlSociety.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntry", new string[] { "flag", "EntryDate", "AttachedBMC_ID" }, new string[] { "6", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ddlSociety.SelectedValue }, "dataset");
                if(ds != null)
                {
                    if(ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if(ds.Tables[0].Rows[0]["Status"].ToString() == "true")
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    txtI_MilkQuantity.Text = ds.Tables[1].Rows[0]["TotalMilkQty"].ToString();
                                    txtfatinkg.Text = ds.Tables[1].Rows[0]["TotalFatInKg"].ToString();
                                    txtsnfinkg.Text = ds.Tables[1].Rows[0]["TotalSnfInKg"].ToString();
                                    txtNetFat.Text = ds.Tables[1].Rows[0]["TotalFat"].ToString();
                                    txtnetsnf.Text = ds.Tables[1].Rows[0]["TotalSnf"].ToString();
                                    ddlBMCTankerRootName.ClearSelection();
                                    ddlBMCTankerRootName.Items.FindByValue(ds.Tables[1].Rows[0]["BMCTankerRoot_Id"].ToString()).Selected = true;
                                    txtNetCLR.Text = Obj_MC.GetCLR_DCS(decimal.Parse(txtNetFat.Text), decimal.Parse(txtnetsnf.Text)).ToString();
                                    btnAdd.Text = "Edit";
                                    panel2.Visible = true;
                                    btnAdd.Visible = true;
                                }
                               

                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    DataTable dt = new DataTable();

                                    dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                                    dt.Columns.Add(new DataColumn("Office_ID", typeof(int)));
                                    dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
                                    dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
                                    dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
                                    dt.Columns.Add(new DataColumn("Fat", typeof(decimal)));
                                    dt.Columns.Add(new DataColumn("LR", typeof(decimal)));
                                    dt.Columns.Add(new DataColumn("Snf", typeof(decimal)));
                                    dt.Columns.Add(new DataColumn("kgFat", typeof(decimal)));
                                    dt.Columns.Add(new DataColumn("kgSnf", typeof(decimal)));
                                    dt.Columns.Add(new DataColumn("Shift", typeof(string)));
                                    dt.Columns.Add(new DataColumn("Quality", typeof(string)));
                                    dt.Columns.Add(new DataColumn("FirstEntry", typeof(string)));
                                    dt.Columns.Add(new DataColumn("Temp", typeof(string)));
                                    int Count = ds.Tables[2].Rows.Count;
                                    for (int i = 0; i < Count; i++)
                                    {
                                        DataRow dr;
                                        dr = dt.NewRow();
                                        dr[0] = (i + 1);
                                        dr[1] = ds.Tables[2].Rows[i]["Office_ID"].ToString();
                                        dr[2] = ds.Tables[2].Rows[i]["Office_Name"].ToString();
                                        dr[3] = ds.Tables[2].Rows[i]["MilkType"].ToString();
                                        dr[4] = ds.Tables[2].Rows[i]["Quantity"].ToString();
                                        dr[5] = ds.Tables[2].Rows[i]["Fat"].ToString();
                                        dr[6] = ds.Tables[2].Rows[i]["LR"].ToString();

                                        dr[7] = ds.Tables[2].Rows[i]["Snf"].ToString();
                                        dr[8] = ds.Tables[2].Rows[i]["kgFat"].ToString();
                                        dr[9] = ds.Tables[2].Rows[i]["kgSnf"].ToString();
                                        dr[10] = ds.Tables[2].Rows[i]["Shift"].ToString();
                                        dr[11] = ds.Tables[2].Rows[i]["Quality"].ToString();
                                        dr[12] = ds.Tables[2].Rows[i]["FirstEntry"].ToString();
                                        dr[13] = ds.Tables[2].Rows[i]["Temp"].ToString();
                                        dt.Rows.Add(dr);


                                    }
                                    dt.DefaultView.Sort = "FirstEntry desc";
                                    dt = dt.DefaultView.ToTable();
                                    ViewState["InsertRecord"] = dt;
                                    gv_HeadDetails.DataSource = dt;

                                    gv_HeadDetails.DataBind();
                                    decimal TotalQTY = 0;
                                    decimal TotalFAT = 0;
                                    decimal TotalSnf = 0;
                                    decimal TotalClr = 0;
                                    decimal Totalfatinkg = 0;
                                    decimal TotalSnfinkg = 0;
                                    int j = 0;
                                    foreach (GridViewRow rows in gv_HeadDetails.Rows)
                                    {
                                        j += 1;
                                        Label lblQuantity = (Label)rows.FindControl("lblQuantity");
                                        Label lblFat = (Label)rows.FindControl("lblFat");
                                        Label lblSnf = (Label)rows.FindControl("lblSnf");
                                        Label lblLR = (Label)rows.FindControl("lblLR");
                                        Label lblkgFat = (Label)rows.FindControl("lblkgFat");
                                        Label lblkgSnf = (Label)rows.FindControl("lblkgSnf");
                                        Label lblEntry = (Label)rows.FindControl("lblEntry");
                                        LinkButton lnkbtnDelete = (LinkButton)rows.FindControl("lnkbtnDelete");
                                        if (lblEntry.Text == "Yes")
                                        {
                                            lnkbtnDelete.Visible = false;
                                        }
                                        TotalQTY += decimal.Parse(lblQuantity.Text);
                                        //TotalFAT += decimal.Parse(lblFat.Text);
                                        //TotalSnf += decimal.Parse(lblSnf.Text);
                                        //TotalClr += decimal.Parse(lblLR.Text);
                                        Totalfatinkg += decimal.Parse(lblkgFat.Text);
                                        TotalSnfinkg += decimal.Parse(lblkgSnf.Text);


                                    }
                                    TotalFAT = (Totalfatinkg / TotalQTY) * 100;
                                    TotalSnf = (TotalSnfinkg / TotalQTY) * 100;
                                    TotalClr = Obj_MC.GetCLR_DCS(TotalFAT, TotalSnf);
                                    gv_HeadDetails.FooterRow.Cells[5].Text = "<b>Sub Total : </b>";
                                    gv_HeadDetails.FooterRow.Cells[6].Text = "<b>" + Math.Round(TotalQTY, 3) + "</b>";
                                    //gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(((TotalFAT) / i), 2) + "</b>";
                                    gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(TotalFAT, 2) + "</b>";
                                    //gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(((TotalClr) / i), 1) + "</b>";
                                    gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(TotalClr, 2) + "</b>";
                                    //gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(((TotalSnf) / i), 2) + "</b>";
                                    gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(TotalSnf, 2) + "</b>";
                                    gv_HeadDetails.FooterRow.Cells[10].Text = "<b>" + Math.Round(Totalfatinkg, 3) + "</b>";
                                    gv_HeadDetails.FooterRow.Cells[11].Text = "<b>" + Math.Round(TotalSnfinkg, 3) + "</b>";

                                }
                            }
							else if (ds.Tables[0].Rows[0]["Status"].ToString() == "Invoice Genarated")
                            {
                                lblMsg1.Text = "Society Payment is done for the selected society,so you are not allowed to edit this entry.";
                                panel2.Visible = false;
                                btnAdd.Visible = false;
                            }
                            else
                            {
                                lblMsg1.Text = "No Record Found";
                                panel2.Visible = false;
                                btnAdd.Visible = false;
                                
                            }
                        }
                        
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void ddlCC_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
}