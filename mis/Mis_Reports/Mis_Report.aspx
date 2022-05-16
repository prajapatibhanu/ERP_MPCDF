<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mis_Report.aspx.cs" Inherits="mis_Mis_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-Manish">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Daily Report</h3>
                </div>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-12">

                            <fieldset>
                                <legend>Total Milk Collection</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;">*</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Current TGT<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtCurrentTGT" runat="server" placeholder="Current TGT" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>On Reporting Date<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtOnReportingDate" runat="server" placeholder="On Reporting Date" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Fat %<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtFat" runat="server" placeholder="Fat%" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>SNF %<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtSNF" runat="server" placeholder="SNF %" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Monthly Average Till date<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtMonthlyAverageTillDate" runat="server" placeholder="Monthly Average Till date" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Last Year Same Month<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtLastYearSameMonth" runat="server" placeholder="Last Year Same Month" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Last Month(Provisional)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtLastMonth_Provisional" runat="server" placeholder="Last Month(Provisional)" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Cummulative Till Date (Current Year)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtCummulativeTillDate_CurrentYear" runat="server" placeholder="Cummulative Till Date (Current Year)" CssClass="form-control" MaxLength="20" onpaste="return false;" onchange="return CalculateAmount()" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Cummulative Till Date (Last Year)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtCummulativeTillDate_LastYear" runat="server" placeholder="Cummulative Till Date (Last Year)" CssClass="form-control" MaxLength="20" onpaste="return false;" onchange="return CalculateAmount()" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Growth %<span style="color: red;"></span></label>
                                            <asp:HiddenField ID="hdnTotalCollection" runat="server" />
                                            <asp:TextBox ID="txtGrowthPercentage" runat="server" placeholder="Growth %" ReadOnly="true" CssClass="form-control" MaxLength="20" onpaste="return false;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Total Milk Sale</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Current Month Target<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtSaleCurrentMonthTarget" runat="server" placeholder="Current MONTH TARGET" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>On Reporting Date<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtSaleOnReportingDate" runat="server" placeholder="On Reporting Date" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Monthly Average Till Date<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtSaleMonthlyAverageTillDate" runat="server" placeholder="Monthly Average Till Date" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Last Year Same Month<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtSaleLastYearSameMonth" runat="server" placeholder="Last Year Same Month" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Last Month(Provisional)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtSaleLastMonth_Provisional" runat="server" placeholder="Last Month(Provisional)" CssClass="form-control" MaxLength="20" onpaste="return false;" onchange="return CalculateAmount2()" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Cummulative Till Date (Current Year)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtSaleCummulativeTillDate_CurrentYear" runat="server" placeholder="Cummulative Till Date (Current Year)" CssClass="form-control" MaxLength="20" onpaste="return false;" onchange="return CalculateAmount2()" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Cummulative Till Date (Last Year)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtSaleCummulativeTillDate_LastYear" runat="server" placeholder="Cummulative Till Date (Last Year)" CssClass="form-control" MaxLength="20" onpaste="return false;" onchange="return CalculateAmount2()" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Growth %<span style="color: red;"></span></label>
                                            <asp:HiddenField ID="hdnSaleGrowth" runat="server" />
                                            <asp:TextBox ID="txtSaleGrowth" runat="server" placeholder="Growth %" ReadOnly="true" CssClass="form-control" MaxLength="20" onpaste="return false;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Milk Collection Price</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Buffalo Milk (Rs./KG Fat)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtMCPriceBuffaloMilk" runat="server" placeholder="Buffalo Milk (Rs./KG Fat)" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Last Year Same Month (BM)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtMCPriceLastYearSameMonthBuffalo" runat="server" placeholder="Last Year Same Month (BM)" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Cow Milk<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtMCPriceCowMilk" runat="server" placeholder="Cow Milk" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Last Year Same Month (CM)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtMCPriceLastYearSameMonthCow" runat="server" placeholder="Last Year Same Month (CM)" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>

                    </div>

                    

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Sale Price</legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Full Cream Milk (Current Month)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceFullCreamMilk" runat="server" placeholder="Full Cream Milk" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Last Year Same Month (Full Cream Milk)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceLastYearSameMonthFullCreamMilk" runat="server" placeholder="Last Year Same Month" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Standard Milk (Current Month)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceStandardMilk" runat="server" placeholder="Standard Milk" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Last Year Same Month (Standard Milk)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceLastYearSameMonthStandardMilk" runat="server" placeholder="Last Year Same Month" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Tonned Milk (Current Month)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceTonnedMilk" runat="server" placeholder="Tonned Milk" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Last Year Same Month (Tonned Milk)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceLastYearSameMonthTonnedMilk" runat="server" placeholder="Last Year Same Month" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Double Tonned Milk (Current Month)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceDoubleTonnedMilk" runat="server" placeholder="Double Tonned Milk" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Last Year Same Month (Double Tonned Milk)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceLastYearSameMonthTonnedDoubleTonnedMilk" runat="server" placeholder="Last Year Same Month" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Skim Milk (Current Month)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceSkimMilk" runat="server" placeholder="Skim Milk" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Last Year Same Month (Skim Milk)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceLastYearSameMonthSkimMilk" runat="server" placeholder="Last Year Same Month" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Chah Milk (Current Month)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceChahMilk" runat="server" placeholder="Chah Milk" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Last Year Same Month (Chah Milk)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceLastYearSameMonthChahMilk" runat="server" placeholder="Last Year Same Month" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Tea Specail (Current Month)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceTeaSpecail" runat="server" placeholder="Tea Specail" CssClass="form-control" onpaste="return false;" MaxLength="20" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Cow Milk (Current Month)<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtS_PriceCowMilk" runat="server" placeholder="Cow Milk" CssClass="form-control" onpaste="return false;" MaxLength="20" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>


                                </div>
                            </fieldset>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Payment Made Upto</legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Payment Made Upto<span style="color: red;">*</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtPaymentMadeUpto" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </fieldset>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Product Sale</legend>
                                <table class="table table-bordered">
                                    <tr>
                                        <th colspan="2">Product Sale</th>
                                    </tr>
                                    <tr>
                                        <td>Ghee</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_Ghee" CssClass="form-control" placeholder="Enter No Of Ghee Sale" Text="0" onpaste="return false;" runat="server" MaxLength="20" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>SMP+WMP</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_SMP_WMP" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>White Butter</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_WhiteButter" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Table Butter</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_TableButter" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Sweet SMP</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_SweetSMP" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>ShriKhand</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_ShriKhand" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Paneer</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_Paneer" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Flavoured Milk</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_FlavouredMilk" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Butter Milk</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_ButterMilk" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Lassi</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_Lassi" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Peda</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_Peda" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>SweetCurd</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_SweetCurd" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Plain Curd</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_PlainCurd" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>Probiotic Curd</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_ProbioticCurd" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Chhena Kheer/ Rabadi</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_ChhenaKheer_Rabadi" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Khowa(Mawa)</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_Khowa_Mawa" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Rasgulla</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_Rasgulla" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Gulab Jamun</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_GulabJamun" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Milk Cake</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_MilkCake" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Chakka</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_Chakka" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Thandai Pet Bottle</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_Thandai" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>F/Milk Bottle</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_F_MilkBottle" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Lassi Lite</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_LassiLite" CssClass="form-control" Text="0" runat="server" MaxLength="20" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Nariyal Barfi</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_NariyalBarfi" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Gulab Jamun Mix</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_GulabJamunMix" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Paneer Achaar</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_PaneerAchaar" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Coffee Mix Powder</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_CoffeeMixPowder" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Cooking Butter</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_CookingButter" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Low Fat Paneer</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_LowFatPaneer" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Peda Prasad</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_PedaPrasad" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Sanchi Ice- Cream</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_SanchiIceCream" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Sanchi Golden Milk</td>
                                        <td>
                                            <asp:TextBox ID="txtProductSale_SanchiGoldenMilk" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox></td>
                                    </tr>
                                </table>

                            </fieldset>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Closing Stock (Kgs)</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>SMP<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtClosingSMP" runat="server" placeholder="SMP" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>White Butter<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtClosingWhiteButter" runat="server" placeholder="White Butter" CssClass="form-control" onpaste="return false;" MaxLength="20" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Ghee<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtClosingGhee" runat="server" placeholder="Ghee" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Commodity Used  (Kgs)</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>SMP Today<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtCommoditySMPToday" runat="server" placeholder="SMP Today" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>SMP Cummulative<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtCommoditySMPCummulative" runat="server" placeholder="SMP Cummulative" CssClass="form-control" onpaste="return false;" MaxLength="20" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>White Butter To<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtCommodityWhiteButterTo" runat="server" placeholder="White Butter To" CssClass="form-control" onpaste="return false;" MaxLength="20" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>White Butter CU<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtCommodityWhiteButterCU" runat="server" placeholder="White Butter CU" CssClass="form-control" onpaste="return false;" MaxLength="20" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Milk Used For Indigenous Product</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Milk Used For Indigenous Product<span style="color: red;"></span></label>
                                            <asp:TextBox ID="txtMilkUsedForIndigenousProduct" runat="server" placeholder="Milk Used For Indigenous Product" CssClass="form-control" MaxLength="20" onpaste="return false;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-block btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="Mis_Report.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-8"></div>
                    </div>
                    <hr />

                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function CalculateAmount() {
            var txtCummulativeTillDate_CurrentYear, txtCummulativeTillDate_LastYear
            debugger;
            txtCummulativeTillDate_CurrentYear = document.getElementById('<%=txtCummulativeTillDate_CurrentYear.ClientID%>').value.trim();
            txtCummulativeTillDate_LastYear = document.getElementById('<%=txtCummulativeTillDate_LastYear.ClientID%>').value.trim();


            if (txtCummulativeTillDate_CurrentYear == "")
                txtCummulativeTillDate_CurrentYear = "0";
            if (txtCummulativeTillDate_LastYear == "")
                txtCummulativeTillDate_LastYear = "0";
            if (txtCummulativeTillDate_LastYear != 0) {
                var sub = ((parseFloat(txtCummulativeTillDate_CurrentYear) - parseFloat(txtCummulativeTillDate_LastYear)) / parseFloat(txtCummulativeTillDate_LastYear)) * 100
                document.getElementById('<%=hdnTotalCollection.ClientID%>').value = sub.toFixed(2).toString();
                document.getElementById('<%=txtGrowthPercentage.ClientID%>').value = sub.toFixed(2).toString();
            }
            else {
                document.getElementById('<%=hdnTotalCollection.ClientID%>').value = "0";
                document.getElementById('<%=txtGrowthPercentage.ClientID%>').value = "0";
            }


        }
    </script>

    <script>
        function CalculateAmount2() {
            var txtSaleCummulativeTillDate_CurrentYear, txtSaleCummulativeTillDate_LastYear
            debugger;
            txtSaleCummulativeTillDate_CurrentYear = document.getElementById('<%=txtSaleCummulativeTillDate_CurrentYear.ClientID%>').value.trim();
            txtSaleCummulativeTillDate_LastYear = document.getElementById('<%=txtSaleCummulativeTillDate_LastYear.ClientID%>').value.trim();


            if (txtSaleCummulativeTillDate_CurrentYear == "")
                txtSaleCummulativeTillDate_CurrentYear = "0";
            if (txtSaleCummulativeTillDate_LastYear == "")
                txtSaleCummulativeTillDate_LastYear = "0";
            if (txtSaleCummulativeTillDate_LastYear != 0) {
                var sub = ((parseFloat(txtSaleCummulativeTillDate_CurrentYear) - parseFloat(txtSaleCummulativeTillDate_LastYear)) / parseFloat(txtSaleCummulativeTillDate_LastYear)) * 100
                document.getElementById('<%=hdnSaleGrowth.ClientID%>').value = sub.toFixed(2).toString();
                document.getElementById('<%=txtSaleGrowth.ClientID%>').value = sub.toFixed(2).toString();
            }
            else {
                document.getElementById('<%=hdnSaleGrowth.ClientID%>').value = "0";
                document.getElementById('<%=txtSaleGrowth.ClientID%>').value = "0";
            }


        }
    </script>
    <script>
        function isNumberKey(evt, element) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
                return false;
            else {
                var len = $(element).val().length;
                var index = $(element).val().indexOf('.');
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }
    </script>




    <script>
        function validateform() {
            var msg = "";

            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter From Date. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
    </script>
</asp:Content>

