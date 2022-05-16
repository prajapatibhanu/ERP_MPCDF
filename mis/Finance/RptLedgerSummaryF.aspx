<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptLedgerSummaryF.aspx.cs" Inherits="mis_Finance_RptLedgerSummaryF" %>

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
            .hide_print, .Hiderow, .main-footer, .dt-buttons, .dataTables_filter, .dataTables_info {
                display: none;
            }

            .box-body {
                padding: 0px;
                border: none;
            }

            .box {
                border: none;
            }
            /*.box {
                border: none;
            }*/

            th {
                background-color: #ddd;
                text-decoration: solid;
            }

            .tblheadingslip {
                font-size: 8px !important;
                background: black;
                color: red;
            }

            footer {
                position: relative;
                bottom: 0;
            }

            body {
                font-size: 13px;
            }
        }

        .align-right {
            text-align: right !important;
            width: 10% !important;
        }

        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            float: left;
        }

        p.subledger {
            border-top: 1px solid #ccc;
            margin: 0px;
        }

        .report-title {
            font-weight: 600;
            font-size: 15px;
            color: #123456;
        }

        .Scut {
            color: tomato;
        }

        .dataTables_filter {
            visibility: hidden !important;
        }

        td {
            padding: 3px !important;
        }
    </style>
    <style>
        .select2 {
            width: 100% !important;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 250px;
            overflow-y: auto;
        }

        .OpenCSS {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Ledger</h3>
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default pull-right" Text="Print" Style="margin-left: 10px;" OnClientClick="window.print();"></asp:Button>
                    <asp:Button ID="btngraphical" runat="server" CssClass="btn btn-primary pull-right hidden" Text="Graphical Report" Style="margin-right: 10px;" OnClick="btngraphical_Click"></asp:Button>
                    <p>
                        <span>[<span class="Scut">Alt+w</span> - Detailed View],[<span class="Scut">Alt+b</span> - Condensed View] ,[<span class="Scut">Alt+Q</span> - Daily Breakup & Detailed View]
                        </span>
                    </p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row Hiderow">

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
                                <label>To Date</label><span style="color: red">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Filter Amount :</label><span style="color: red">*</span>
                                <br />
                                <asp:CheckBox ID="chkOpeningBal" runat="server" Text="Opening Bal.&nbsp;&nbsp;" />
                                <asp:CheckBox ID="chkDebitAmt" runat="server" Text="&nbsp;Debit Amt.&nbsp;&nbsp;" />
                                <asp:CheckBox ID="chkCreditAmt" runat="server" Text="Credit Amt.&nbsp;&nbsp;" />
                                <asp:CheckBox ID="chkClosingBal" runat="server" Text="Closing Bal.&nbsp;&nbsp;" />
                                <%--<input id="chkClosingBal" type="checkbox"  name="ClosingBal" /> <label>Closing Bal.</label>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row Hiderow">
                        <div class="col-md-4 hidden">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>List Of Ledger</label><span style="color: red">*</span>
                                <asp:TextBox runat="server" CssClass="form-control capitalize ui-autocomplete-12" placeholder="Enter Ledger Name" ID="txtLedgerName" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                <asp:HiddenField ID="hfLedgerName" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hfLedgerID" runat="server" ClientIDMode="Static" />
                                <asp:DropDownList runat="server" ID="ddlLedger" CssClass="form-control hidden" AutoPostBack="false" OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 20px;" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 Hiderow">
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-block btn-success hidden Aselect1" Text="<< BACK " OnClick="btnBack_Click" AccessKey="W" />
                            <asp:Button ID="btnBackN" runat="server" CssClass="btn btn-block btn-success hidden Aselect1" Text="<< BACK " OnClick="btnBackN_Click" AccessKey="B" />
                            <asp:Button ID="btnShowDetailBook" runat="server" CssClass="hidden" Text="Show Bank Detail" OnClick="btnShowDetailBook_Click" AccessKey="Q" />
                        </div>
                    </div>
                    <div class="row pull-right Hiderow">
                        <div class="col-md-12">
                            <div class="form-group" style="margin-top: 28px;">
                                <label>Filter: &nbsp;&nbsp;</label>
                                <asp:CheckBox ID="chkNarration" ClientIDMode="Static" runat="server" Checked="false" Text="Narration.&nbsp;&nbsp;" onchange="HideNarration();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div runat="server" id="divExcel">
                                <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" class="no-print" value="Export to Excel">
                                <input type="button" onclick="window.print();" class="no-print" value="Print" />
                            </div>
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
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime hidden"></asp:Label>
                        </div>
                    </div>
                    <div id="tableData">
                        <div class="row">
                            <div class="col-md-12">
                                <table style="width:100%; text-align:center;">
                                    <tr>
                                        <td colspan="7" style="text-align:center;">
                                            <p class="report-title">
                                                <asp:Label ID="lblReportName" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="lblTab" runat="server" Style="font-weight: 700; font-size: 20px; color: red;" Text=""></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 table-responsive">

                                <div id="DivTable" runat="server"></div>

                                <asp:GridView ID="GridView4" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="lastdatatable table table-hover table-bordered">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" Text='<%# Eval("Date").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
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
    <%--<script src="../HR/js/jquery.dataTables.min.js"></script>
    <script src="../HR/js/dataTables.bootstrap.min.js"></script>
    <script src="../HR/js/dataTables.buttons.min.js"></script>
    <%-- <script src="js/buttons.flash.min.js"></script>
    <script src="../HR/js/jszip.min.js"></script>
    <%-- <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="../HR/js/buttons.html5.min.js"></script>
    <script src="../HR/js/buttons.print.min.js"></script>
    <script src="../HR/js/sum().js"></script>
    <script src="../HR/js/buttons.colVis.min.js"></script>--%>
    <%-- <script src="https://cdn.datatables.net/plug-ins/1.10.19/api/sum().js"></script>--%>

    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script src="js/fromkeycode.js"></script>
    <script>

        $(document).ready(function () {
            $('.datatable').DataTable({
                paging: false,
                dom: 'Bfrtip',
                ordering: false,
                buttons: [
                    {
                        extend: 'colvis',
                        collectionLayout: 'fixed two-column',
                        text: '<i class="fa fa-eye"></i> Columns'
                    },
                ]
            });
        });

    </script>
    <script>
        //$('.lastdatatable').DataTable({
        //    paging: false,
        //    dom: 'Bfrtip',
        //    ordering: false,
        //    buttons: [
        //        {
        //            extend: 'colvis',
        //            collectionLayout: 'fixed two-column',
        //            text: '<i class="fa fa-eye"></i> Columns'
        //        },

        //        {
        //            extend: 'excel',
        //            text: '<i class="fa fa-file-excel-o"></i> Excel',
        //            title: $('h1').text(),
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5]
        //            },
        //            footer: false
        //        }

        //    ]
        //});
    </script>
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
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }

        }
        function PrintPage() {
            window.print();
        }



        //$('#txtFromDate').change(function () {
        //    debugger;
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');

        //    if ($('#txtToDate').val() != "") {
        //        if (start > end) {

        //            if ($('#txtFromDate').val() != "") {
        //                alert("From date should not be greater than To Date.");
        //                $('#txtFromDate').val("");
        //            }
        //        }
        //    }
        //});
        //$('#txtToDate').change(function () {
        //    debugger;
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');

        //    if (start > end) {

        //        if ($('#txtToDate').val() != "") {
        //            alert("To Date can not be less than From Date.");
        //            $('#txtToDate').val("");
        //        }
        //    }

        //});
    </script>

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

        $(window).on('load', function () {
            $(".HideRecord").css("display", "none");
            $(".Narration").css("display", "none");

        })
        function HideNarration() {

            if (document.getElementById('<%= chkNarration.ClientID%>').checked) {


                $(".Narration").css("display", "block");
            }
            else {
                $(".Narration").css("display", "none");
            }
        }
        function handleKeyDown(e) {
            var ctrlPressed = 0;
            var altPressed = 0;
            var shiftPressed = 0;
            var evt = (e == null ? event : e);

            shiftPressed = evt.shiftKey;
            altPressed = evt.altKey;
            ctrlPressed = evt.ctrlKey;
            self.status = ""
               + "shiftKey=" + shiftPressed
               + ", altKey=" + altPressed
               + ", ctrlKey=" + ctrlPressed

            if ((altPressed) && (evt.keyCode == 87)) {
                if ($('.HideRecord').is(':visible')) {
                    $(".HideRecord").css("display", "none");
                    $("p.subledger").css("border-top", "none");
                }
                else {
                    $(".HideRecord").css("display", "block");

                    document.getElementById('<%= chkNarration.ClientID%>').checked = false;


                    //$("p.subledger").css("border-top", "1px solid #ccc");
                }
            }
            //alert("You pressed the " + fromKeyCode(evt.keyCode)
            // + " key (keyCode " + evt.keyCode + ")\n"
            // + "together with the following keys:\n"
            // + (shiftPressed ? "Shift " : "")
            // + (altPressed ? "Alt " : "")
            // + (ctrlPressed ? "Ctrl " : "")
            //)            

            return true;
        }
        $(document).ready(function () {

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
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $("#<%=txtLedgerName.ClientID %>").autocomplete({

                source: function (request, response) {
                    $.ajax({

                        url: '<%=ResolveUrl("RptLedgerSummaryF.aspx/SearchLedger") %>',
                        data: "{ 'Ledger_Name': '" + $('#txtLedgerName').val() + "'}",
                        //  var param = { ItemName: $('#txtItem').val() };
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {

                            response($.map(data.d, function (item) {

                                return {
                                    label: item.split('-Ledger_Name-')[0],
                                    val: item.split('-Ledger_Name-')[1]
                                    //label: item,
                                    //val: item
                                    //val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {

                    $("#<%=hfLedgerName.ClientID %>").val(i.item.label);
                    $("#<%=hfLedgerID.ClientID %>").val(i.item.val);

                },
                minLength: 1

            });

        });

    </script>
	<script>
        (function (document) {
            'use strict';

            var TableFilter = (function (myArray) {
                var search_input;

                function _onInputSearch(e) {
                    search_input = e.target;
                    var tables = document.getElementsByClassName(search_input.getAttribute('data-table'));
                    myArray.forEach.call(tables, function (table) {
                        myArray.forEach.call(table.tBodies, function (tbody) {
                            myArray.forEach.call(tbody.rows, function (row) {
                                var text_content = row.textContent.toLowerCase();
                                var search_val = search_input.value.toLowerCase();
                                row.style.display = text_content.indexOf(search_val) > -1 ? '' : 'none';
                            });
                        });
                    });
                }

                return {
                    init: function () {
                        var inputs = document.getElementsByClassName('search-input');
                        myArray.forEach.call(inputs, function (input) {
                            input.oninput = _onInputSearch;
                        });
                    }
                };
            })(Array.prototype);

            document.addEventListener('readystatechange', function () {
                if (document.readyState === 'complete') {
                    TableFilter.init();
                }
            });

        })(document);
    </script>
</asp:Content>


