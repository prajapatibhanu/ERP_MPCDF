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

public partial class mis_MilkCollection_RMRD_Challan_EntryForDirectBMC : System.Web.UI.Page
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
                txtEntryDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
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
                FillGrid();
                FillSociety();
                ddlBMCTankerRootName.Enabled = false;
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
            if (ddlMilkCollectionUnit.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                                  new string[] { "18", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

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
                Response.Redirect("SocietywiseMilkCollectionInvoice.aspx", false);
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
            if (ddlMilkCollectionUnit.SelectedValue != "0")
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

    }


    protected void btnAddSocietyDetails_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddBMCDCSDetails();
    }

    private void AddBMCDCSDetails()
    {
        try
        {
            //int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
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
                dr = dt.NewRow();
                dr[0] = 1;
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
                dr[12] = "Yes";
                dr[13] = txtTEMP.Text;
                dt.Rows.Add(dr);
                ViewState["InsertRecord"] = dt;
                gv_HeadDetails.DataSource = dt;
                gv_HeadDetails.DataBind();

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

                dr = dt.NewRow();
                dr[0] = 1;
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
                TotalFAT += decimal.Parse(lblFat.Text);
                TotalSnf += decimal.Parse(lblSnf.Text);
                TotalClr += decimal.Parse(lblLR.Text);
                Totalfatinkg += decimal.Parse(lblkgFat.Text);
                TotalSnfinkg += decimal.Parse(lblkgSnf.Text);


            }
            gv_HeadDetails.FooterRow.Cells[5].Text = "<b>Sub Total : </b>";
            gv_HeadDetails.FooterRow.Cells[6].Text = "<b>" + Math.Round(TotalQTY, 3) + "</b>";
            gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(((TotalFAT) / i), 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(((TotalClr) / i), 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(((TotalSnf) / i), 2) + "</b>";
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
            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
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
                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlSociety.SelectedValue;
                dr[2] = ddlSociety.SelectedItem.Text;
                if (float.Parse(txtNetFat.Text) > 5.5)
                {
                    dr[3] = "Buf";
                }
                else
                {
                    dr[3] = "Cow";
                }
                dr[4] = txtI_MilkQuantity.Text;
                dr[5] = txtNetFat.Text;
                dr[6] = txtNetCLR.Text;
                //decimal SNF = GetGridSNF();
                //decimal snfinkg = GetGridSNF_InKG();
                //decimal fatinkg = GetGridFAT_InKG();
                dr[7] = txtnetsnf.Text;
                dr[8] = txtfatinkg.Text;
                dr[9] = txtsnfinkg.Text;
                dr[10] = ddlShift.SelectedItem.Text;
                dr[11] = ddlMilkQuality.SelectedItem.Text;
                dr[12] = "Yes";
                dr[13] = txtTEMP.Text;
                dt.Rows.Add(dr);
                ViewState["InsertRecord"] = dt;
                gv_HeadDetails.DataSource = dt;
                gv_HeadDetails.DataBind();

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

                //for (int i = 0; i < DT.Rows.Count; i++)
                //{
                //    if (ddlBMCDCS.SelectedValue == DT.Rows[i]["Office_ID"].ToString() && ddlMilkType.SelectedItem.Text == DT.Rows[i]["MilkType"].ToString())
                //    {
                //        CompartmentType = 1;
                //    }
                //}

                //if (CompartmentType == 1)
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Head \"" + ddlHeaddetails.SelectedItem.Text + "\" already exist.");
                //}
                //else
                //{
                //    dr = dt.NewRow();
                //    dr[0] = 1;
                //    dr[1] = ddlItemBillingHead_Type.SelectedValue;
                //    dr[2] = ddlHeaddetails.SelectedValue;
                //    dr[3] = ddlHeaddetails.SelectedItem.Text;
                //    dr[4] = txtHeadAmount.Text;

                //    dt.Rows.Add(dr);
                //}
                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlSociety.SelectedValue;
                dr[2] = ddlSociety.SelectedItem.Text;

                if (float.Parse(txtNetFat.Text) > 5.5)
                {
                    dr[3] = "Buf";
                }
                else
                {
                    dr[3] = "Cow";
                }

                dr[4] = txtI_MilkQuantity.Text;
                dr[5] = txtNetFat.Text;
                dr[6] = txtNetCLR.Text;
                //decimal SNF = GetGridSNF();
                //decimal snfinkg = GetGridSNF_InKG();
                //decimal fatinkg = GetGridFAT_InKG();
                dr[7] = txtnetsnf.Text;
                dr[8] = txtfatinkg.Text;
                dr[9] = txtsnfinkg.Text;
                dr[10] = ddlShift.SelectedItem.Text;
                dr[11] = ddlMilkQuality.SelectedItem.Text;
                dr[12] = "No";
                dr[13] = txtBMCDCSTemp.Text;
                dt.Rows.Add(dr);
                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);

                }

                ViewState["InsertRecord"] = dt;
                gv_HeadDetails.DataSource = dt;
                gv_HeadDetails.DataBind();

            }
            ddlBMCDCS.SelectedValue = ddlSociety.SelectedValue;
            ddlMilkQuality.SelectedValue = "Good";
            ddlBMCDCSShift.SelectedValue = ddlShift.SelectedValue;
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
                TotalFAT += decimal.Parse(lblFat.Text);
                TotalSnf += decimal.Parse(lblSnf.Text);
                TotalClr += decimal.Parse(lblLR.Text);
                Totalfatinkg += decimal.Parse(lblkgFat.Text);
                TotalSnfinkg += decimal.Parse(lblkgSnf.Text);


            }
            gv_HeadDetails.FooterRow.Cells[5].Text = "<b>Sub Total : </b>";
            gv_HeadDetails.FooterRow.Cells[6].Text = "<b>" + Math.Round(TotalQTY, 3) + "</b>";
            gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(((TotalFAT) / i), 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(((TotalClr) / i), 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(((TotalSnf) / i), 2) + "</b>";
            gv_HeadDetails.FooterRow.Cells[10].Text = "<b>" + Math.Round(Totalfatinkg, 3) + "</b>";
            gv_HeadDetails.FooterRow.Cells[11].Text = "<b>" + Math.Round(TotalSnfinkg, 3) + "</b>";
            panel1.Enabled = false;
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
                                                     }
                                             , new string[]{"0"
                                                      ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                                                      ,ddlSociety.SelectedValue
                                                      ,ViewState["Emp_ID"].ToString()
                                                      ,objdb.GetLocalIPAddress()   
                                                      ,objdb.Office_ID()  
                                                      ,ddlBMCTankerRootName.SelectedValue													  
                                                       }

                                            , new string[] { "type_MilkCollectionChallanEntry" },
                                            new DataTable[] { dt2 }, "TableSave");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                        ClearText();
                        FillGrid();

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
                dr[1] = lblShift.Text;
                dr[2] = lblMilkType.Text;
                dr[3] = lblTemp.Text;
                dr[4] = lblQuantity.Text;
                dr[5] = lblQuality.Text;
                dr[6] = lblFat.Text;
                dr[7] = lblSnf.Text;
                dr[8] = lblLR.Text;
                dr[9] = lblkgFat.Text;
                dr[10] = lblkgSnf.Text;
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
            ddlBMCTankerRootName.ClearSelection();
            ddlBMCTankerRootName.Enabled = false;
            if (ddlSociety.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("Usp_Trn_MilkCollectionBMCEntryAtQC", new string[] { "flag", "Office_ID", "EntryDate" }, new string[] { "4", ddlSociety.SelectedValue,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlBMCTankerRootName.SelectedValue = ds.Tables[0].Rows[0]["BMCTankerRoot_Id"].ToString();
                    txtI_MilkQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    txtTEMP.Text = ds.Tables[0].Rows[0]["Temp"].ToString();
                    txtNetFat.Text = ds.Tables[0].Rows[0]["FAT"].ToString();
                    txtNetCLR.Text = ds.Tables[0].Rows[0]["CLR"].ToString();
                    txtnetsnf.Text = ds.Tables[0].Rows[0]["SNF"].ToString();
                    txtfatinkg.Text = ds.Tables[0].Rows[0]["FatKg"].ToString();
                    txtsnfinkg.Text = ds.Tables[0].Rows[0]["SnfKg"].ToString();
                    
                   
                }
            }
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
        panel1.Enabled = true;
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
    protected void txtEntryDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void FillGrid()
    {
        try
        {
           
            string Entrydate = Convert.ToDateTime(txtEntryDate.Text, cult).ToString("yyyy/MM/dd");
            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntry", new string[] { "flag", "EntryDate", "Created_Office_ID" }, new string[] { "3", Entrydate, objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                    gv_MilkCollectionChallanEntryDetails.DataSource = ds;
                    gv_MilkCollectionChallanEntryDetails.DataBind();
					decimal TotalMilkQuantity = 0;
                  
                    decimal TotalFATInKg = 0;
                    decimal TotalSnfInKg = 0;

                    TotalMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                    TotalFATInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    TotalSnfInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[6].Text = "<b>Total : </b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[7].Text = "<b>" + TotalMilkQuantity.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[8].Text = "<b>" + TotalFATInKg.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[9].Text = "<b>" + TotalSnfInKg.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                    GetDatatableHeaderDesign();
					GetDatatableFooterDesign();

                }
                else
                {
                    gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                    gv_MilkCollectionChallanEntryDetails.DataBind();
                }
            }
            else
            {
                gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                gv_MilkCollectionChallanEntryDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (gv_MilkCollectionChallanEntryDetails.Rows.Count > 0)
            {
                gv_MilkCollectionChallanEntryDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_MilkCollectionChallanEntryDetails.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
	private void GetDatatableFooterDesign()
    {
        try
        {
            if (gv_MilkCollectionChallanEntryDetails.Rows.Count > 0)
            {
                gv_MilkCollectionChallanEntryDetails.FooterRow.TableSection = TableRowSection.TableFooter;
                //gv_MilkCollectionChallanEntryDetails.foo = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
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
            TotalFAT += decimal.Parse(lblFat.Text);
            TotalSnf += decimal.Parse(lblSnf.Text);
            TotalClr += decimal.Parse(lblLR.Text);
            Totalfatinkg += decimal.Parse(lblkgFat.Text);
            TotalSnfinkg += decimal.Parse(lblkgSnf.Text);


        }
        gv_HeadDetails.FooterRow.Cells[5].Text = "<b>Sub Total : </b>";
        gv_HeadDetails.FooterRow.Cells[6].Text = "<b>" + Math.Round(TotalQTY, 3) + "</b>";
        gv_HeadDetails.FooterRow.Cells[7].Text = "<b>" + Math.Round(((TotalFAT) / i), 2) + "</b>";
        gv_HeadDetails.FooterRow.Cells[8].Text = "<b>" + Math.Round(((TotalClr) / i), 2) + "</b>";
        gv_HeadDetails.FooterRow.Cells[9].Text = "<b>" + Math.Round(((TotalSnf) / i), 2) + "</b>";     
        gv_HeadDetails.FooterRow.Cells[10].Text = "<b>" + Math.Round(Totalfatinkg, 3) + "</b>";
        gv_HeadDetails.FooterRow.Cells[11].Text = "<b>" + Math.Round(TotalSnfinkg, 3) + "</b>";
    }
}