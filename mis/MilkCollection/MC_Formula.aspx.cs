using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_MilkCollection_MC_Formula : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure apiprocedure = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass obj_MC = new MilkCalculationClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }

    }
     
    protected void btnSubmit_Click(object sender, EventArgs e)
    { 
        if (txtNetFat.Text == "")
        {
            txtNetFat.Text = "0";
        }
        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Text = "0";
        }

        if (txtI_MilkQuantity.Text == "")
        {
            txtI_MilkQuantity.Text = "0";
        }


        // Get SNF Per DCS/BMC/MDP Milk Collection/Dispatch and CC Milk Receive
        txtnetsnf_Dcs.Text = obj_MC.GetSNFPer_DCS(Convert.ToDecimal(txtNetFat.Text), Convert.ToDecimal(txtNetCLR.Text)).ToString();


        // Get SNF Per CC Dispatch & DS Receive
        txtnetsnf.Text = obj_MC.GetSNFPer(Convert.ToDecimal(txtNetFat.Text), Convert.ToDecimal(txtNetCLR.Text)).ToString();


        // Get Convert Milk Ltr To Kg 
        txtmilkQuantityInkg.Text = obj_MC.GetLtrToKg(Convert.ToDecimal(txtNetCLR.Text), Convert.ToDecimal(txtI_MilkQuantity.Text)).ToString();



        // Get Fat In KG 
        if (txtmilkQuantityInkg.Text == "" || txtmilkQuantityInkg.Text == "0")
        {
            txtmilkQuantityInkg.Text = "0";
        }
        txtfatinKG.Text = obj_MC.GetFATInKg(Convert.ToDecimal(txtmilkQuantityInkg.Text), Convert.ToDecimal(txtNetFat.Text)).ToString();



        // Get SNF In KG CC / DCS/BMC/MDP
        if (txtnetsnf.Text == "" || txtnetsnf.Text == "0")
        {
            txtnetsnf.Text = "0";
        }
        txtsnfinkg_Dcs.Text = obj_MC.GetSNFInKg(Convert.ToDecimal(txtmilkQuantityInkg.Text), Convert.ToDecimal(txtnetsnf_Dcs.Text)).ToString();



        // Get SNF In KG CC / DS
        if (txtnetsnf.Text == "" || txtnetsnf.Text == "0")
        {
            txtnetsnf.Text = "0";
        }
        txtsnfinkg.Text = obj_MC.GetSNFInKg(Convert.ToDecimal(txtmilkQuantityInkg.Text), Convert.ToDecimal(txtnetsnf.Text)).ToString();






    }
}