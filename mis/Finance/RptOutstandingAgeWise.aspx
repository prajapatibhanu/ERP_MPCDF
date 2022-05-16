<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptOutstandingAgeWise.aspx.cs" Inherits="mis_Finance_RptOutstandingAgeWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .show_detail {
            margin-top: 24px;
        }

        .table > tbody > tr > th {
            padding: 5px;
        }

        a:hover {
            color: red;
        }

        /*tr:hover td {
            background-color: #fefefe !important;
        }*/
        table.dataTable tbody td, table.dataTable thead td {
            padding: 1px 1px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 2px 3px !important;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 5px 7px;
            border-bottom: none !important;
        }

        table.dataTable tfoot th, table.dataTable tfoot td {
            border-bottom: none !important;
        }

        table.dataTable.no-footer {
            border-bottom: none !important;
        }

        a.dt-button.buttons-collection.buttons-colvis, a.dt-button.buttons-collection.buttons-colvis:hover {
            background: #EF5350;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-excel.buttons-html5, a.dt-button.buttons-excel.buttons-html5:hover {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-print, a.dt-button.buttons-print:hover {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

        thead tr th {
            background: #9e9e9ea3 !important;
        }

        tbody tr td(:fifth-child), tfoot tr td(:fifth-child) {
            text-align: right !important;
        }

        table.dataTable tbody tr {
            background-color: #ddd4d1 !important;
        }
        .explode {
            padding: 4px;
            background: #8bc34a;
            border-radius: 50%;
            color:white;
            max-width: 17px;
        }
        .implode {
            padding: 4px;
            background: #ff9800;
            border-radius: 50%;
            color:white;
            display:none;
            max-width: 17px;
        }

        .edit_link, .edit_link:hover{
            color: #286090 !important;
        }
        .view_link, .view_link:hover{
            color: #00c0ef !important;
        }
    </style>
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


        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            /*float: left;*/
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

        .align-right {
            text-align: right !important;
        }

        .Scut {
            color: tomato;
        }


        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }
        }

        .voucherColumn {
            width: 150px !important;
        }

        .btn-flat {
            background: #1ca79a;
            color: white;
            box-shadow: 1px 1px 1px #808080;
        }

        .mainbutton {
            background: #2b7770;
        }
    </style>
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            max-height: 200px;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        ul.multiselect-container.dropdown-menu {
            overflow-y: scroll;
            overflow-x: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Outstanding Age Wise</h3>
                    <p class="hide_print">
                        <span runat="server" id="spnAltB">[<span class="Scut">Alt+b</span> - Back View] </span><span runat="server" id="spnAltW">[<span class="Scut">Alt+w</span> - Condensed & Detailed View]</span>

                    </p>
                    <asp:label id="lblMsg" runat="server" text=""></asp:label>

                </div>

                <div class="box-body">
                    <div class="hide_print">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>From Date<span style="color: red;">*</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:textbox id="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" clientidmode="Static"></asp:textbox>
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
                                        <asp:textbox id="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" clientidmode="Static"></asp:textbox>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Outstanding Type</label><span style="color: red">*</span>
                                    <asp:dropdownlist id="ddlOutstandingType" runat="server" class="form-control select2" autopostback="true" onselectedindexchanged="ddlOutstandingType_SelectedIndexChanged">
                                        <asp:ListItem Value="Payable">Payable</asp:ListItem>
                                        <asp:ListItem Value="Receivable">Receivable</asp:ListItem>
                                        <asp:ListItem Value="Groups">Groups</asp:ListItem>
                                    </asp:dropdownlist>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <%-- <div class="form-group">
                                    <br />
                                    <label>Show Bill Of Type</label>
                                    : &nbsp;&nbsp;
                                     <asp:CheckBox ID="chkOpeningBal" runat="server" Text="All Bills&nbsp;&nbsp;" />
                                    <asp:CheckBox ID="chkTransactionAmt" runat="server" Text="CR&nbsp;&nbsp;" />
                                    <asp:CheckBox ID="chkClosingBal" runat="server" Text="DR&nbsp;&nbsp;" />
                                </div>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <asp:listbox runat="server" id="ddlOffice" clientidmode="Static" cssclass="form-control" selectionmode="Multiple"></asp:listbox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>List Of Group</label><span style="color: red">*</span>
                                    <asp:dropdownlist runat="server" id="ddlGroup" cssclass="form-control select2" autopostback="true" onselectedindexchanged="ddlGroup_SelectedIndexChanged">
                                    </asp:dropdownlist>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Bill/Party Wise</label><span style="color: red">*</span>
                                    <asp:dropdownlist id="ddlPartywise" runat="server" class="form-control select2">
                                        <asp:ListItem Value="BillWise">Bill Wise</asp:ListItem>
                                        <asp:ListItem Value="BillPartyWise">Bill-Party Wise</asp:ListItem>
                                    </asp:dropdownlist>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Show Bill Type</label><span style="color: red">*</span>
                                    <asp:dropdownlist id="ddlBillType" runat="server" class="form-control select2">
                                        <asp:ListItem Value="AllBill">All Bills </asp:ListItem>
                                        <%--<asp:ListItem Value="AllBillZero" Selected="True" >All Bills Including Zero</asp:ListItem>--%>
                                        <asp:ListItem Value="Cr">CR </asp:ListItem>
                                        <%--<asp:ListItem Value="CrZero">CR Including Zero</asp:ListItem>--%>
                                        <asp:ListItem Value="Dr">DR </asp:ListItem>
                                        <%-- <asp:ListItem Value="DrZero">DR Including Zero</asp:ListItem>--%>
                                    </asp:dropdownlist>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <asp:button id="btnSearch" runat="server" cssclass="btn btn-block btn-success btn-flat" style="margin-top: 24px;" text="Search" onclick="btnSearch_Click" onclientclick="return validateform();" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                             <div class="form-group">
                                    <label>Ageing</label>
                                    : &nbsp;&nbsp;
                                    <asp:CheckBox ID="chk0_30" runat="server" checked="true" Text="0 To 30 Days &nbsp;&nbsp;&nbsp;&nbsp;" />
                                    <asp:CheckBox ID="chk30_60" runat="server" checked="true" Text="30 To 60 Days &nbsp;&nbsp;&nbsp;&nbsp" />
                                    <asp:CheckBox ID="chk60_90" runat="server" checked="true" Text="60 To 90 Days &nbsp;&nbsp;&nbsp;&nbsp" />
                                    <asp:CheckBox ID="chk90_180" runat="server" checked="true" Text="90 To 180 Days &nbsp;&nbsp;&nbsp;&nbsp" />
                                    <asp:CheckBox ID="chk180_365" runat="server" checked="true" Text="180 To 365 Days &nbsp;&nbsp;&nbsp;&nbsp" />
                                    <asp:CheckBox ID="chk365_0" runat="server" checked="true" Text="Older than 365 Days &nbsp;&nbsp;&nbsp;&nbsp" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:label id="lblheadingFirst" runat="server" cssclass="pageheading" text=""></asp:label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong style="height: 40px;">
                                <asp:label id="lblHeadName" runat="server" style="font-size: 20px;" text=""></asp:label>
                                <br />
                                <div class="hide_print">
                                    <asp:button id="btnHeadExcel" runat="server" cssclass="btn btn-default" text="Excel" onclick="btnHeadExcel_Click" />
                                </div>
                            </strong>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                            <br /><br />
                            <%--LEDGER DETAIL MONTH WISE--%>
                            <div id="DivTable" runat="server" class="table-responsive"></div>

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
                        else if (FromYear != ToYear && FromMonth >= 3 && ToMonth <= 3) {
                        }
                        else {
                            msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                        }
                    }
                    else {
                        msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                    }

                }
            }

            if (document.getElementById('<%=ddlGroup.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Group. \n";
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

    <script>
        $('.datatable').DataTable({
            paging: false,
            dom: 'Bfrtip',
            ordering: false,
            oSearch: { bSmart: false, bRegex: true },
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
                },
                {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    //autoPrint: true
                },
                {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true
                }

            ]
        });
    </script>
    <script src="js/fromkeycode.js" type="text/javascript"></script>
    <script>
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
                if ($('.GroupChildren').is(':visible')) {
                    $(".GroupChildren").css("display", "none");
                    $(".explode").css("display", "block");
                    $(".implode").css("display", "none");
                }
                else {
                    $(".GroupChildren").css("display", "table-row");
                    $(".explode").css("display", "none");
                    $(".implode").css("display", "block");
                }
            }
            return true;
        }

        document.onkeydown = handleKeyDown;




        //$(document).ready(function () {
        //    $('table thead th').each(function (i) {
        //        calculateColumn(i);
        //    });
        //});

        //function calculateColumn(index) {
        //    var total = 0;
        //    $('table tr').each(function () {
        //        var value = parseInt($('td', this).eq(index).text());
        //        if (!isNaN(value)) {
        //            total += value;
        //        }
        //    });
        //    $('table tfoot td').eq(index).text('Total: ' + total);
        //}

        $(document).on("click", ".explode", function () {
            //$(this).find("tr.GroupChildren").remove();
            //console.log(this);
            //console.log($(this).parent());
            // if ($(this).parent().parent().find('.GroupChildren').is(':visible')) {
            //$(this).parent().parent().find(".GroupChildren").css("display", "none");
            // }
            //else {
            $(this).parent().parent().find(".GroupChildren").css("display", "table-row");
            $(this).parent().find(".explode").css("display", "none");
            $(this).parent().find(".implode").css("display", "block");
            // }

        });

        $(document).on("click", ".implode", function () {
            //if ($(this).parent().parent().find('.GroupChildren').is(':visible')) {
            $(this).parent().parent().find(".GroupChildren").css("display", "none");
            $(this).parent().find(".explode").css("display", "block");
            $(this).parent().find(".implode").css("display", "none");
            // }
            // else {
            //  $(this).parent().parent().find(".GroupChildren").css("display", "table-row");
            // }

        });

    </script>

</asp:Content>

