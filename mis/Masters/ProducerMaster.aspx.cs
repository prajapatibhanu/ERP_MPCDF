using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Drawing;
using QRCoder;
using System.Globalization;

public partial class mis_Masters_ProducerMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
            {
                Fillds();
                GetBank();
                FillGrid();

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                if (Session["IsUpdated"] != null)
                {
                    if ((Boolean)Session["IsUpdated"] == true)
                    {
                        Session["IsUpdated"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Updated Successfully');", true);
                    }
                }

                if (Session["IsExists"] != null)
                {
                    if ((Boolean)Session["IsExists"] == true)
                    {
                        Session["IsExists"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record already Exists!');", true);
                    }
                }
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }


    protected void FillGrid()
    {
        ds = null;
        ds = objdb.ByProcedure("SpProducerMaster",
                        new string[] { "flag", "DCSId" },
                        new string[] { "6", DdlDCS.SelectedValue }, "dataset");
        GridDetails.DataSource = ds;
        GridDetails.DataBind();
		GridDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        GridDetails.UseAccessibleHeader = true;
    }
    protected void GetBank()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpProducerMaster",
                            new string[] { "flag" },
                            new string[] { "3" }, "dataset");
            DdlBank.DataTextField = "BankName";
            DdlBank.DataValueField = "Bank_Id";
            DdlBank.DataSource = ds;
            DdlBank.DataBind();
            DdlBank.Items.Insert(0, new ListItem("Select", "0"));
            DdlBank.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void DdlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlBankBranch.ClearSelection();
            //ds = null;
            //ds = objdb.ByProcedure("SpProducerMaster",
            //                new string[] { "flag", "BankId" },
            //                new string[] { "4", DdlBank.SelectedValue }, "dataset");
            //ddlBankBranch.DataTextField = "BranchName";
            //ddlBankBranch.DataValueField = "Branch_id";
            //ddlBankBranch.DataSource = ds;
            //ddlBankBranch.DataBind();
            //ddlBankBranch.Items.Insert(0, new ListItem("Select", "0"));
            //ddlBankBranch.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBankBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //txtIFSC.Text = "";
            //ds = null;
            //ds = objdb.ByProcedure("SpProducerMaster",
            //                new string[] { "flag", "BankBranchId" },
            //                new string[] { "5", ddlBankBranch.SelectedValue }, "dataset");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    txtIFSC.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void Fillds()
    {
        ds = null;
        ds = objdb.ByProcedure("SpProducerMaster",
                        new string[] { "flag" },
                        new string[] { "8" }, "dataset");
        //Fill DS
        if (ds.Tables.Count > 0)
        {
            ddlDS.DataSource = ds.Tables[0];
            ddlDS.DataTextField = "Office_Name";
            ddlDS.DataValueField = "Office_ID";
            ddlDS.DataBind();
            ddlDS.Items.Insert(0, new ListItem("Select", "0"));
            FillDCS();
        }
    }
    protected void FillDCS()
    {
        ds = null;
        ds = objdb.ByProcedure("SpProducerMaster",
                        new string[] { "flag", "DCSId" },
                        new string[] { "12", objdb.Office_ID() }, "dataset");
        if (ds.Tables.Count > 0)
        {
            ddlDS.SelectedValue = ds.Tables[0].Rows[0]["parent"].ToString();
            DdlDCS.DataSource = ds.Tables[1];
            DdlDCS.DataTextField = "DCS";
            DdlDCS.DataValueField = "Office_ID";
            DdlDCS.DataBind();
            //DataSet ds1 = objdb.ByProcedure("SpProducerMaster",
            //             new string[] { "flag", "Office_ID" },
            //             new string[] { "10", objdb.Office_ID() }, "dataset"); //Query used at multiple places. Please do not change...
            //if (ds1.Tables.Count > 0)
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        DdlDCS.DataSource = ds1.Tables[0];
            //        DdlDCS.DataTextField = "Office_Name";
            //        DdlDCS.DataValueField = "Office_ID";
            //        DdlDCS.DataBind();
            //    }
            //}
            ddlDS.Enabled = false;
            DdlDCS.Enabled = false;
            Areadetails();
        }
    }

    protected void Areadetails()
    {
        ds = null;
        ds = objdb.ByProcedure("SpProducerMaster",
                       new string[] { "flag", "Office_ID" },
                       new string[] { "10", objdb.Office_ID() }, "dataset");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtdivision.Text = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                txtdistrict.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                txtdistrict.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                txtAssembly.Text = "";
                txtGram.Text = "";
                pnljila.Visible = true;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string milkproduce = "0.00";
                if (txtMilkProduce.Text.Trim() != "" || txtMilkProduce.Text.Trim() != string.Empty)
                {
                    milkproduce = txtMilkProduce.Text.Trim();
                }
                if (Convert.ToInt32(txtCattleNo.Text.Trim()) != Convert.ToInt32(txtCowNo.Text.Trim()) + Convert.ToInt32(txtBuffelowNo.Text.Trim()))
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Total No. of animal should match with Total Cow + Total Buffelow", "");
                }
                else
                {
                    if (btnSubmit.Text == "Submit")
                    {
                        string FileAadharCard = "";
                        string FilePassbook = "";
                        if (FUAadharCard.HasFile)
                        {
                            string fileName = FUAadharCard.PostedFile.FileName.ToString();
                            //sets the image path
                            FileAadharCard = "~/FileUpload/" + fileName;
                            //save it to folder
                            FUAadharCard.SaveAs(Server.MapPath(FileAadharCard));
                        }
                        if (FUPassBook.HasFile)
                        {
                            string fileName1 = FUPassBook.PostedFile.FileName.ToString();
                            //sets the image path
                            FilePassbook = "~/FileUpload/" + fileName1;
                            //save it to folder
                            FUPassBook.SaveAs(Server.MapPath(FilePassbook));
                        }

                        if (ds != null)
                        {
                            ds.Clear();
                        }
                        //To be updated when Masters are ready
                        string DCSId = DdlDCS.SelectedValue;
                        string CreatedBy = objdb.createdBy();

                        ds = objdb.ByProcedure("SpProducerMaster",
                                 new string[] { "flag", "DCSId", "ProducerName", "FatherHusbandName", "DOB" ,
                                 "CategoryId", "Gender", "Mobile", "FamilyMembers", "BhumiStithi", "FarmerType", 
                                 "Education", "Email", "CattleNo", "CowNo", "Cowbreed", "BuffelowNo", "BuffBreed", 
                                 "MilkProduce", "BankId", "BankBranch","IFSC", "AccountNo", "AadharPath", "PassbookPath","CreatedBy","CardNo","AadharNo","Address","UserTypeId","ProducerNameEnglish" },
                                 new string[] { "1", DCSId, txtProducerName.Text.Trim(), txtFatherHusbandName.Text.Trim(), 
                                 Convert.ToDateTime(txtDOB.Text.Trim(),cult).ToString("yyyy/MM/dd"), DdlCategory.SelectedValue, ddlGender.SelectedItem.Text.Trim(),
                                 txtMobile.Text.Trim(), txtFamilyMembers.Text.Trim(),ddlBhumiStithi.SelectedItem.Text.Trim(),
                                 ddlFarmerType.SelectedItem.Text.Trim(),ddlEducation.SelectedItem.Text,txtEmail.Text.Trim(),txtCattleNo.Text.Trim(),txtCowNo.Text.Trim(),txtCowbreed.Text.Trim(),
                             txtBuffelowNo.Text.Trim(),txtBuffBreed.Text.Trim(),milkproduce,DdlBank.SelectedValue,txtBankBranch.Text.Trim(),txtIFSC.Text.Trim(),
                             txtAccountNo.Text.Trim(),FileAadharCard,FilePassbook,CreatedBy,txtCard.Text.Trim(),txtAadhar.Text.Trim(),txtAddress.Text.Trim(),objdb.UserTypeID(),txtPnameInEnglish.Text}, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            //string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Uploaded");
                            //ClearAll();
                            //GetProdcuerDetails();
                            Session["IsSuccess"] = true;
                            Response.Redirect("ProducerMaster.aspx", false);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["Msg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error);
                            Session["IsSuccess"] = false;
                        }
                        if (ds != null) { ds.Clear(); }
                    }
                    else if (btnSubmit.Text == "Update")
                    {
                        string FileAadharCard = ViewState["AadharPath"].ToString();
                        string FilePassbook = ViewState["PassbookPath"].ToString();
                        if (FUAadharCard.HasFile)
                        {
                            string fileName = FUAadharCard.PostedFile.FileName.ToString();
                            //sets the image path
                            FileAadharCard = "~/FileUpload/" + fileName;
                            //save it to folder
                            FUAadharCard.SaveAs(Server.MapPath(FileAadharCard));
                        }
                        if (FUPassBook.HasFile)
                        {
                            string fileName1 = FUPassBook.PostedFile.FileName.ToString();
                            //sets the image path
                            FilePassbook = "~/FileUpload/" + fileName1;
                            //save it to folder
                            FUPassBook.SaveAs(Server.MapPath(FilePassbook));
                        }

                        if (ds != null)
                        {
                            ds.Clear();
                        }
                        //To be updated when Masters are ready
                        string DCSId = DdlDCS.SelectedValue;
                        string CreatedBy = objdb.createdBy();

                        ds = objdb.ByProcedure("SpProducerMaster",
                                 new string[] { "flag", "DCSId", "ProducerName", "FatherHusbandName", "DOB", 
                                 "CategoryId", "Gender", "Mobile", "FamilyMembers", "BhumiStithi", "FarmerType", 
                                 "Education", "Email", "CattleNo", "CowNo", "Cowbreed", "BuffelowNo", "BuffBreed", 
                                 "MilkProduce", "BankId",  "BankBranch","IFSC", "AccountNo", "AadharPath", "PassbookPath","CreatedBy", "Producerid","CardNo","AadharNo","Address","ProducerNameEnglish"},
                                 new string[] { "7", DCSId, txtProducerName.Text.Trim(), txtFatherHusbandName.Text.Trim(), 
                                 Convert.ToDateTime(txtDOB.Text.Trim(), cult).ToString("yyyy/MM/dd"), DdlCategory.SelectedValue, ddlGender.SelectedItem.Text.Trim(),
                                 txtMobile.Text.Trim(), txtFamilyMembers.Text.Trim(),ddlBhumiStithi.SelectedItem.Text.Trim(),
                                 ddlFarmerType.SelectedItem.Text.Trim(),ddlEducation.SelectedItem.Text,txtEmail.Text.Trim(),txtCattleNo.Text.Trim(),txtCowNo.Text.Trim(),txtCowbreed.Text.Trim(),
                             txtBuffelowNo.Text.Trim(),txtBuffBreed.Text.Trim(),milkproduce,DdlBank.SelectedValue,txtBankBranch.Text.Trim(),txtIFSC.Text.Trim(),
                             txtAccountNo.Text.Trim(),FileAadharCard,FilePassbook,CreatedBy,ViewState["ProducerId"].ToString(),txtCard.Text.Trim(),txtAadhar.Text.Trim(),txtAddress.Text.Trim(),txtPnameInEnglish.Text}, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            //string success = ds.Tables[0].Rows[0]["Msg"].ToString();
                            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Uploaded");
                            // ClearAll();
                            //GetProdcuerDetails();
                            Session["IsUpdated"] = true;
                            Response.Redirect("ProducerMaster.aspx", false);
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Exists")
                        {
                            //string success = ds.Tables[0].Rows[0]["Msg"].ToString();
                            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Uploaded");
                            // ClearAll();
                            //GetProdcuerDetails();
                            Session["IsExists"] = true;
                            Response.Redirect("ProducerMaster.aspx", false);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["Msg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error);
                            Session["IsUpdated"] = false;
                        }
                        if (ds != null) { ds.Clear(); }
                    }
                    FillGrid();
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
                Session["IsSuccess"] = false;
                Session["IsUpdated"] = false;
                Session["IsExists"] = false;
            }
        }

    }
    protected void ClearAll()
    {
        txtProducerName.Text = "";
        txtFatherHusbandName.Text = "";
        txtDOB.Text = "";
        DdlCategory.SelectedIndex = 0;
        ddlGender.SelectedIndex = 0;
        txtMobile.Text = "";
        txtFamilyMembers.Text = "";
        ddlBhumiStithi.SelectedIndex = 0;
        ddlFarmerType.SelectedIndex = 0;
        ddlEducation.SelectedIndex = 0;
        txtEmail.Text = "";
        txtAadhar.Text = "";
        txtCard.Text = "";
        txtCattleNo.Text = "";
        txtCowNo.Text = "";
        txtCowbreed.Text = "";
        txtBuffelowNo.Text = "";
        txtBuffBreed.Text = "";
        txtMilkProduce.Text = "";
        DdlBank.SelectedIndex = 0;
        txtBankBranch.Text = "";
        txtIFSC.Text = "";
        txtAccountNo.Text = "";
        txtPnameInEnglish.Text = "";
		btnSubmit.Text = "Submit";
    }
    //CODE COMMENT STARTED AGAINST ORACLE CHANGES
    public static void OracleLog(string strvalidationmsg)
    {
        try
        {
            var line = Environment.NewLine;
            string filepath = System.Configuration.ConfigurationManager.AppSettings["ExceptionFilePath"].ToString();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            filepath = filepath + "oracle log - " + DateTime.Today.ToString("dd-mm-yy") + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + ".txt";   //text file name

            if (!File.Exists(filepath))
            {
                File.Create(filepath).Dispose();
            }

            using (StreamWriter sw = File.AppendText(filepath))
            {
                string error = "------------------------*start*------------------------" + line + "log written date:" + " " + DateTime.Now.ToString() + line + "error message:" + " " + strvalidationmsg;
                sw.WriteLine(error);
                sw.WriteLine("------------------------*end*------------------------" + line);
                sw.Flush();
                sw.Close();
            }
        }
        catch (Exception e)
        {
            e.ToString();
        }
    }
    //CODE COMMENT ENDED AGAINST ORACLE CHANGES

    protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //ClearText();
            ds = null;
            string ProducerId = Convert.ToString(e.CommandArgument.ToString());
            ViewState["ProducerId"] = ProducerId;
            if (e.CommandName == "EditRequest")
            {
                ds = objdb.ByProcedure("SpProducerMaster",
                    new string[] { "flag", "ProducerId" },
                    new string[] { "2", ProducerId }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtProducerName.Text = ds.Tables[0].Rows[0]["ProducerName"].ToString();
                    txtFatherHusbandName.Text = ds.Tables[0].Rows[0]["FatherHusbandName"].ToString();
                    txtDOB.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    // txtAge.Text = ds.Tables[0].Rows[0]["Age"].ToString();
                    DdlCategory.SelectedValue = ds.Tables[0].Rows[0]["CategoryId"].ToString();
                    ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByText(ds.Tables[0].Rows[0]["Gender"].ToString()));
                    txtMobile.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    txtFamilyMembers.Text = ds.Tables[0].Rows[0]["FamilyMembers"].ToString();
                    if (ds.Tables[0].Rows[0]["BhumiStithi"].ToString() != "" && ds.Tables[0].Rows[0]["BhumiStithi"].ToString() != null)
                    {
                        ddlBhumiStithi.SelectedIndex = ddlBhumiStithi.Items.IndexOf(ddlBhumiStithi.Items.FindByText(ds.Tables[0].Rows[0]["BhumiStithi"].ToString()));
                    }
                    if (ds.Tables[0].Rows[0]["FarmerType"].ToString() != "" && ds.Tables[0].Rows[0]["FarmerType"].ToString() != null)
                    {
                        ddlFarmerType.SelectedIndex = ddlFarmerType.Items.IndexOf(ddlFarmerType.Items.FindByText(ds.Tables[0].Rows[0]["FarmerType"].ToString()));
                    }
                    if (ds.Tables[0].Rows[0]["Education"].ToString() != "" && ds.Tables[0].Rows[0]["Education"].ToString() != null)
                    {
                        ddlEducation.SelectedIndex = ddlEducation.Items.IndexOf(ddlEducation.Items.FindByText(ds.Tables[0].Rows[0]["Education"].ToString()));
                    }
                    txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    txtCattleNo.Text = ds.Tables[0].Rows[0]["CattleNo"].ToString();
                    txtCowNo.Text = ds.Tables[0].Rows[0]["CowNo"].ToString();
                    txtCowbreed.Text = ds.Tables[0].Rows[0]["Cowbreed"].ToString();
                    txtBuffelowNo.Text = ds.Tables[0].Rows[0]["BuffelowNo"].ToString();
                    txtBuffBreed.Text = ds.Tables[0].Rows[0]["BuffBreed"].ToString();
                    txtMilkProduce.Text = ds.Tables[0].Rows[0]["MilkProduce"].ToString();
                    txtAadhar.Text = ds.Tables[0].Rows[0]["AadharNo"].ToString();
                    if (ds.Tables[0].Rows[0]["BankId"].ToString() != "" && ds.Tables[0].Rows[0]["BankId"].ToString() != null)
                    {
                        DdlBank.SelectedValue = ds.Tables[0].Rows[0]["BankId"].ToString();
                    }
                    txtBankBranch.Text = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                    txtIFSC.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
                    txtAccountNo.Text = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                    ViewState["AadharPath"] = ds.Tables[0].Rows[0]["AadharPath"].ToString();
                    ViewState["PassbookPath"] = ds.Tables[0].Rows[0]["PassbookPath"].ToString();
                    txtCard.Text = ds.Tables[0].Rows[0]["ProducerCardNo"].ToString();

                    txtPnameInEnglish.Text = ds.Tables[0].Rows[0]["ProducerNameEnglish"].ToString();

                    // DataSet ds1 = objdb.ByProcedure("SpProducerMaster", new string[] { "flag", "BankId" },
                    //new string[] { "4", DdlBank.SelectedValue }, "dataset");
                    // ddlBankBranch.DataTextField = "BranchName";
                    // ddlBankBranch.DataValueField = "Branch_id";
                    // ddlBankBranch.DataSource = ds1;
                    // ddlBankBranch.DataBind();
                    //ddlBankBranch.Items.Insert(0, new ListItem("--Select Division--", "0"));
                    //ddlBankBranch.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["BankBranchId"].ToString());
                    //txtIFSC.Text = ds1.Tables[0].Rows[0]["IFSC"].ToString();


                    //if (ds.Tables[0].Rows[0]["Division_IsActive"].ToString() == "1")
                    //{
                    //    chkIsActive.Checked = true;
                    //}
                    //else
                    //{
                    //    chkIsActive.Checked = false;
                    //}
                    btnSubmit.Text = "Update";
                }
            }
            else if (e.CommandName == "PrintRequest")
            {
                ds = objdb.ByProcedure("SpProducerMaster",
                       new string[] { "flag", "ProducerId" },
                       new string[] { "11", ProducerId }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string QR = null;
                        string dcsid = ds.Tables[0].Rows[0]["DCSId"].ToString();
                        DataSet ds1 = objdb.ByProcedure("SpProducerMaster",
                                            new string[] { "flag", "DCSId" },
                                            new string[] { "12", dcsid }, "dataset");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblds.Text = ds1.Tables[0].Rows[0]["DS"].ToString();
                                lblSociety.Text = ds1.Tables[1].Rows[0]["DCS"].ToString();
                                lblDS1.Text = ds1.Tables[0].Rows[0]["DS"].ToString();
                            }
                        }
                        lblProducer.Text = ds.Tables[0].Rows[0]["ProducerName"].ToString();
                        lblSociety.Text = ds.Tables[0].Rows[0]["SocietyName"].ToString();
                        lblBlock.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                        lblMobile.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        lblCode.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                        //lblCardNumber.Text = ds.Tables[0].Rows[0]["ProducerCardNo"].ToString();
                        //QR = ds1.Tables[0].Rows[0]["parent"].ToString() + "." + ds.Tables[0].Rows[0]["DCSId"].ToString() + "." + ds.Tables[0].Rows[0]["UserName"].ToString();
                        QR = "http://erpdairy.com/pd.aspx?P_Id=" + objdb.Encrypt(ViewState["ProducerId"].ToString());
						QRGenerator(QR);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void QRGenerator(string QR)
    {
        string Code = QR;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(Code, QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgQRCode = new System.Web.UI.WebControls.Image();
        imgQRCode.Height = 80;
        imgQRCode.Width = 80;
        using (Bitmap bitmap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                imgQRCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
            PlaceHolder1.Controls.Add(imgQRCode);
        }
    }

    protected void GridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMsg.Text = "";
        GridDetails.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void cbSelect_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void GridDetails_DataBinding(object sender, EventArgs e)
    {

    }
    protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell statusCell = e.Row.Cells[3];
            if (statusCell.Text.Trim() == "YES")
            {
                statusCell.Text = "एक्टिव";
            }
            if (statusCell.Text.Trim() == "NO")
            {
                statusCell.Text = "इनएक्टिव";
            }
        }
    }
}