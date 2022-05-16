<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEmpMasterData.aspx.cs" Inherits="mis_Payroll_PayrollEmpMasterData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .Grid td {
            padding: 3px !important;
        }

            .Grid td input {
                padding: 3px 3px !important;
                text-align: right !important;
                font-size: 12px !important;
                height: 26px !important;
            }

        .Grid th {
            text-align: center;
        }

        .ss {
            text-align: left !important;
        }

        .bgcolor {
            background-color: #eeeeee !important;
        }

        .box {
            min-height: initial !important;
        }

        .basic_detail th, .basic_detail td {
            font-size: 11px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Set Master Data</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label>&nbsp;</label>
                            <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateSearch();" />
                        </div>
                    </div>

                    <div id="DivDetail" runat="server">


                        <div class="row">

                            <div class="col-md-12">

                                <div class=" table-responsive">
                                    <table class="table table-bordered basic_detail">
                                        <tbody>
                                            <tr>
                                                <th style="width: 10%;">BANK A/C No</th>
                                                <td style="width: 15%;">
                                                    <asp:Label ID="lblbankacc" runat="server" Text=""></asp:Label></td>
                                                <th style="width: 10%;">UAN No</th>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lbluan" runat="server" Text=""></asp:Label></td>
                                                <th style="width: 10%;">G.Ins No</th>
                                                <td style="width: 10%;">
                                                    <asp:Label ID="lblgis" runat="server" Text=""></asp:Label></td>
                                                <th>PAY COMMISION</th>
                                                <td>
                                                    <asp:Label ID="lblpaycommision" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lbldarate" CssClass="hidden" runat="server" Text=""></asp:Label>
                                                </td>
                                                <th style="width: 10%;">Emp No</th>
                                                <td style="width: 5%;">
                                                    <asp:Label ID="lblemp" runat="server" Text=""></asp:Label></td>

                                            </tr>
                                            <tr>
                                                <th>BANK NAME</th>
                                                <td>
                                                    <asp:Label ID="lblbankname" runat="server" Text=""></asp:Label></td>
                                                <th>IFSC CODE</th>
                                                <td>
                                                    <asp:Label ID="lblifsc" runat="server" Text=""></asp:Label></td>
                                                <th>PAN NO</th>
                                                <td>
                                                    <asp:Label ID="lblpan" runat="server" Text=""></asp:Label></td>
                                                <th>BASIC SALARY</th>
                                                <td>
                                                    <asp:Label ID="lblbasic" runat="server" Text=""></asp:Label></td>
                                                <th>SEC No</th>
                                                <td>
                                                    <asp:Label ID="lblsec" runat="server" Text=""></asp:Label></td>

                                            </tr>
                                        </tbody>
                                    </table>
                                    <%-- <p class="para_des">
                                        =&gt; कृपया पहले यह सुनिश्चित करें की अटेंडेंस सेट की जा चुकी है |<br>
                                        =&gt; अटेंडेंस के अनुसार बेसिक वेतन बनेगा और उसके अनुसार Permanent कर्मचारी का DA 
                                             <span id="ctl00_ContentBody_lblPermanent_DARate">12</span>% एवं स्थाई कर्मी का DA  
                                            <span id="ctl00_ContentBody_lblFixed_DARate">154</span>% स्वतः ही आजेगा |<br>
                                        =&gt; इसी प्रकार से EPF = BASIC+DA का 12% स्वतः ही आ जाएगा|<br>
                                        =&gt; प्रोफेशनल TAX 250-250 10 महीने तक स्वतः ही आजेगा|<br>
                                        =&gt; कुछ भी परिवर्तन करने के पश्चात या अटेंडेंस सेट करने के पश्चात , कृपया Save बटन में क्लिक करके इसे सुरक्षित करें |<br>
                                    </p>--%>
                                    <p class="para_des">
                                        <span id="ctl00_ContentBody_lblepfstatus"></span>
                                    </p>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="box-header">
                                    <h3 class="box-title">Earning Detail</h3>
                                </div>
                                <div class="table-responsive">
                                    <table id="tblEarning" class="table table-bordered table-striped Grid">
                                        <tr>
                                            <th scope="col">Earning Heads</th>
                                            <th scope="col" style="width: 100px;">Amount</th>
                                        </tr>
                                        <tr>
                                            <td>BASIC SALARY</td>
                                            <td>
                                                <asp:TextBox ID="txtEarnRemainingSalary_Basic" runat="server" Text="0" class="form-control" MaxLength="13" onfocusout="return CalculateEarning();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                        </tr>
                                        <asp:Repeater ID="RepeaterEarning" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="max-width: 200px !important">
                                                        <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                        <small>(
                                                            <asp:Label ForeColor="Blue" ID="lblContributionType" runat="server" Text='<%# Eval("ContributionType").ToString()%>'></asp:Label>
                                                            )</small>
                                                        <asp:Label ID="lblEarnDeduction_ID" runat="server" CssClass="hidden" Text='<%# Eval("EarnDeduction_ID").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblCss" runat="server" CssClass="hidden  EarningHeadValue" Text='<%# "Earning_"+Eval("EarnDeduction_ID").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblCalculationType" runat="server" CssClass="hidden" Text='<%# Eval("EarnDeduction_Calculation").ToString()%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemainingEarning" runat="server" Text='<%# Eval("EarnDeductionAmount").ToString()%>' class="form-control" MaxLength="13" onfocusout="return CalculateEarning();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td><b>Total Earning</b></td>
                                            <td>
                                                <asp:TextBox ID="txtETotalRemaining" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box-header">
                                    <h3 class="box-title">Deduction Detail</h3>
                                </div>
                                <div class="table-responsive">
                                    <table id="tblDeduction" class="table table-bordered table-striped Grid">
                                        <tr>
                                            <th scope="col">Deduction Heads</th>
                                            <th scope="col" style="width: 100px;">Amount</th>
                                        </tr>
                                        <asp:Repeater ID="RepeaterDeduction" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="max-width: 200px">
                                                        <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                        <small>(
                                                            <asp:Label ForeColor="Blue" ID="lblContributionType" runat="server" Text='<%# Eval("ContributionType").ToString()%>'></asp:Label>
                                                            )</small>
                                                        <asp:Label ID="lblEarnDeduction_ID" runat="server" CssClass="hidden" Text='<%# Eval("EarnDeduction_ID").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblCalculationType" runat="server" CssClass="hidden" Text='<%# Eval("EarnDeduction_Calculation").ToString()%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemainingDeduction" runat="server" Text='<%# Eval("EarnDeductionAmount").ToString()%>' class="form-control" MaxLength="13" onfocusout="return CalculateDeduction();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td><b>Total Deduction</b></td>
                                            <td>
                                                <asp:TextBox ID="txtDTotalRemaining" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Total Earning Amount<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtTotalEarning" ClientIDMode="Static" runat="server" ReadOnly="true" Text='0' class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Total Deduction Amount<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtTotalDeduction" ClientIDMode="Static" runat="server" ReadOnly="true" Text='0' class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Net Payment Amount<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtNetPayment" ClientIDMode="Static" runat="server" ReadOnly="true" Text='0' class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                            <div class="col-md-2">
                                <a class="btn btn-block btn-default" href="PayrollEmpMasterData.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        CalculateEarning();
        CalculateDeduction();

        function CalculateEarning() {

            var i = 0;
            var TotalPaidEarning = 0;
            var TotalRemaining = 0;
            var trCount = $('#tblEarning tr').length - 1;

            $('#tblEarning tr').each(function (index) {
                if (i > 0 && i < trCount) {
                    debugger;
                    var PaidEarning = $(this).children("td").eq(1).find('input[type="text"]').val();

                    if (PaidEarning.trim() == "")
                        PaidEarning = 0;

                    var TotPaidEarning = TotalPaidEarning;

                    TotalPaidEarning = parseFloat(parseFloat(TotPaidEarning) + parseFloat(PaidEarning)).toFixed(2);

                }
                i++;
            });

            document.getElementById("txtTotalEarning").value = TotalPaidEarning;
            document.getElementById("txtETotalRemaining").value = TotalPaidEarning;

            var TotalDeduction = document.getElementById("txtTotalDeduction").value.trim();
            if (TotalDeduction.trim() == "")
                TotalDeduction = 0;

            document.getElementById("txtNetPayment").value = parseFloat(parseFloat(TotalPaidEarning) - parseFloat(TotalDeduction)).toFixed(2);

            return true;
        }

        function CalculateDeduction() {

            var i = 0;
            var TotalPaidDeduction = 0;
            var TotalRemaining = 0;

            var trCount = $('#tblDeduction tr').length - 1;

            $('#tblDeduction tr').each(function (index) {
                if (i > 0 && i < trCount) {
                    debugger;
                    var PaidDeduction = $(this).children("td").eq(1).find('input[type="text"]').val();

                    if (PaidDeduction.trim() == "")
                        PaidDeduction = 0;

                    var TotPaidDeduction = TotalPaidDeduction;

                    TotalPaidDeduction = parseFloat(parseFloat(TotPaidDeduction) + parseFloat(PaidDeduction)).toFixed(2);

                }
                i++;
            });

            document.getElementById("txtTotalDeduction").value = TotalPaidDeduction;
            document.getElementById("txtDTotalRemaining").value = TotalPaidDeduction;

            var TotalEarning = document.getElementById("txtTotalEarning").value.trim();
            if (TotalEarning.trim() == "")
                TotalEarning = 0;

            document.getElementById("txtNetPayment").value = parseFloat(parseFloat(TotalEarning) - parseFloat(TotalPaidDeduction)).toFixed(2);

            return true;
        }

        function validateSearch() {
            var msg = "";
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

    </script>
</asp:Content>

