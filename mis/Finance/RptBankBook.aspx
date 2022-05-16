<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptBankBook.aspx.cs" Inherits="mis_Finance_RptBankBook" %>

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
            .hide_print, .Hiderow, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
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


            .box {
                border: 1px solid #eff3f7;
            }


            span.Ledger_Name {
                overflow-wrap: break-word;
            }

            body {
                /*font-size: 11px;*/
            }

            .content, .box-body {
                padding-right: 0px;
            }

            .table {
                font-family: sans-serif;
                font-stretch: condensed;
                font-size: 14px;
            }
        }
        /*body{
                font-size:11px;
            }*/
        .align-right {
            text-align: right !important;
            width: 10% !important;
        }

        .align-center {
            text-align: center !important;
        }

        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
            text-align: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            float: left;
            text-align: left;
        }

        p.subledger {
            /*border-top: 1px solid #ccc;*/
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

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 2px;
        }

        .pagebreak {
            page-break-before: always;
        }

        th, td {
            border-color: none !important;
        }

        /*.table-bordered {
            border: 1px solid #eff3f7 !important;
        }*/
        .table {
            font-family: sans-serif;
            font-stretch: condensed;
            font-size: 14px;
        }

        .AdrCSS {
            font-size: 14px;
            font-weight: 600;
        }

        .ClosingBal {
            padding-left: 25px !important;
        }

        .table > thead > tr > th {
            border-top: 2px solid #dddddd!important;
            border-bottom: 2px solid #dddddd !important;
        }
        /*thead {
            border-top: 2px solid #6b6565 !important;
            border-bottom: 3px solid #6b6565 !important;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Bank Book
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default pull-right" Text="Print" Style="margin-left: 10px;" OnClientClick="window.print();"></asp:Button>
                    </h3>


                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row Hiderow">

                        <div class="col-md-2">
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
                        <div class="col-md-2">
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
                        <div class="col-md-5 hidden">
                            <div class="form-group">
                                <label>Filter Amount :</label><span style="color: red">*</span>
                                <br />
                                <asp:CheckBox ID="chkOpeningBal" runat="server" Text="Opening Bal.&nbsp;&nbsp;" />
                                <asp:CheckBox ID="chkDebitAmt" runat="server" Text="&nbsp;Debit Amt.&nbsp;&nbsp;" />
                                <asp:CheckBox ID="chkCreditAmt" runat="server" Text="Credit Amt.&nbsp;&nbsp;" />

                            </div>
                        </div>

                        <%-- </div>
                    <div class="row Hiderow">--%>
                        <div class="col-md-5 hidden">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>List Of Ledger</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlLedger" CssClass="form-control select2" AutoPostBack="false" OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <asp:CheckBox ID="chkClosingBal" runat="server" Text="Closing Bal.&nbsp;&nbsp;" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 21px;" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-12">
                            <p class="report-title hidden">
                                <asp:Label ID="lblReportName" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblTab" runat="server" Style="font-weight: 700; font-size: 20px; color: red;" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime hidden"></asp:Label>
                            </p>

                            <div id="DivTable" runat="server"></div>

                        </div>
                    </div>

                </div>

            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../../../mis/HR/js/jquery.dataTables.min.js"></script>
    <script src="../../../mis/HR/js/dataTables.bootstrap.min.js"></script>
    <script src="../../../mis/HR/js/dataTables.buttons.min.js"></script>
    <%-- <script src="js/buttons.flash.min.js"></script>--%>
    <script src="../../../mis/HR/js/jszip.min.js"></script>
    <%-- <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>--%>
    <script src="../../../mis/HR/js/buttons.html5.min.js"></script>
    <script src="../../../mis/HR/js/buttons.print.min.js"></script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.19/api/sum().js"></script>


    <script>

        $(document).ready(function () {


            $('.datatable').DataTable({
                /******Footer sum********/


                /**************/

                paging: false,

                columnDefs: [{
                    orderable: false
                }],
                "bSort": false,
                // "order": [[0, 'asc']],

                dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                  '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                  '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                fixedHeader: {
                    header: true
                },
                buttons: {


                    buttons: [{
                        extend: 'print',
                        text: '<i class="fa fa-print"></i> Print',
                        title: $('.report-title').text(),
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel', footer: true,
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('.report-title').text(),
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            stripHtml: false,
                            format: {
                                body: function (data, column, row) {
                                    return (column === 1 && column === 5) ? data.replace(/\n/g, '"&CHAR(10)&CHAR(13)&"') : data.replace(/(&nbsp;|<([^>]+)>)/ig, "");;
                                }
                            }
                        },
                        customize: function (xlsx) {
                            var sheet = xlsx.xl.worksheets['sheet1.xml'];
                            $('row c', sheet).attr('s', '55');
                        },

                        footer: true
                    }],

                    dom: {
                        container: {
                            className: 'dt-buttons'
                        },
                        button: {
                            className: 'btn btn-default'
                        }
                    }
                },

            });
        });

    </script>
    <script>
        $('.lastdatatable').DataTable({
            paging: false,
            dom: 'Bfrtip',
            ordering: false,
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
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
</asp:Content>



