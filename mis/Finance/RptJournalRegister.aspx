<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptJournalRegister.aspx.cs" Inherits="mis_Finance_RptJournalRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <style>
        .pay-sheet table tr th, .pay-sheet table tr td {
            font-size: 12px;
            width: 10%;
            border: 1px dashed #ddd;
            padding-left: 1px;
            padding-top: 1px;
            line-height: 14px;
            font-family: monospace;
            overflow: hidden;
        }

        .pay-sheet table {
            width: 100%;
        }

            .pay-sheet table thead {
                background: #eee;
            }

        /*.pay-sheet table {
            border: 1px solid #ddd;
        }*/

        @media print {
            .Hiderow, .main-footer {
                display: none;
            }

            .box {
                border: none;
            }

            th {
                background-color: #ddd;
                text-decoration: solid;
            }

            .tblheadingslip {
                font-size: 8px !important;
                background: black;
                color: red;
            }
        }

        .align-right {
            text-align: right !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Journal Register</h3>
                    <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" class="no-print btn btn-info pull-right" value="Export to Excel" />
                    <input type="button" onclick="window.print();" class="no-print btn btn-info pull-right" value="Print" />
                    [<span class="Scut">Alt+b</span> - Back View]
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row no-print">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>From Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>To Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <%--  <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                            </asp:DropDownList>--%>
                                <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-2 no-print">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">

                            <script type="text/javascript">
                                var tableToExcel = (function () {
                                    var uri = 'data:application/vnd.ms-excel;base64,'
                                      , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
                                      , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                                      , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                                    return function (table, name) {
                                        if (!table.nodeType) table = document.getElementById(table)
                                        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                                        window.location.href = uri + base64(format(template, ctx))
                                    }
                                })()
                            </script>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-block btn-success hidden" Text="<< BACK " OnClick="btnBack_Click" AccessKey="B" />
                        </div>
                    </div>
                    <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime no-print"></asp:Label>
                    <div id="tableData">
                        <div class="row">
                            <div class="col-md-12">
                                <table style="text-align:center; width:100%;">
                                    <tr>
                                       <td colspan="5">
                                            <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">

                            <%--LEDGER DETAIL MONTH WISE--%>
                            <div class="col-md-12">

                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowCommand="GridView3_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                <asp:HiddenField runat="server" ID="HFMonthID" Value='<%# Eval("MonthID").ToString() %>' />
                                                <%--  <asp:Label ID="lblMonthID" CssClass="hidden" Text='<%# Eval("MonthID").ToString() %>' runat="server" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Link" CommandName="View" HeaderText="Month Name" DataTextField="MonthName" />

                                        <asp:TemplateField HeaderText="Total Vouchers" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("VcCount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <%--LEDGER DETAIL MONTH WISE--%>
                                <asp:GridView ID="GridView4" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowDeleting="GridView4_RowDeleting" OnRowCommand="GridView1_RowCommand" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Date" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch Type" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info">View</asp:HyperLink>
                                            <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1") %>' CssClass="label label-info">View</asp:HyperLink>
                                            <asp:HyperLink ID="hpEdit" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2") %>' CssClass="label label-primary">Edit</asp:HyperLink>
                                            <asp:LinkButton ID="Delete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print" FooterStyle-CssClass="no-print">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <%--  <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger"></asp:LinkButton>--%>
                                                <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select To Date. \n";
            }
            var Fromday = 0;
            var FromMonth = 0;
            var FromYear = 0;
            var Today = 0;
            var ToMonth = 0;
            var ToYear = 0;
            var y = document.getElementById("txtFromDate").value; //This is a STRING, not a Date
            if (y != "") {
                var dateParts = y.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];

                Fromday = dateParts[0];
                FromMonth = dateParts[1];
                FromYear = dateParts[2];

                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {
                var yd = "";
            }

            var z = document.getElementById("txtToDate").value; //This is a STRING, not a Date
            if (z != "") {
                var dateParts = z.split("/");   //Will split in 3 parts: day, month and year
                var zday = dateParts[0];
                var zmonth = dateParts[1];
                var zyear = dateParts[2];

                Today = dateParts[0];
                ToMonth = dateParts[1];
                ToYear = dateParts[2];

                var zd = new Date(zyear, parseInt(zmonth, 10) - 1, zday);
            }
            else {
                var zd = "";
            }
            if (yd != "" && zd != "") {
                if (yd > zd) {
                    msg += "To Date should be greater than From Date ";
                }
                else {

                    if ((FromYear == ToYear - 1) || (FromYear == ToYear)) {
                        if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        }
                        else if (FromYear == ToYear && FromMonth >= 4 && ToMonth <= 12) {
                        }
                        else if (FromYear == (ToYear - 1) && FromMonth > 3 && ToMonth <= 3) {
                        }
                        else {
                            msg += "select Valid Date";
                        }
                    }
                    else {
                        msg += "select Valid Date";
                    }

                }
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
        }

        function PrintPage() {
            window.print();
        }
    </script>

    <%--<script src="../../../mis/js/jquery.js" type="text/javascript"></script>--%>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>

        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
</asp:Content>

