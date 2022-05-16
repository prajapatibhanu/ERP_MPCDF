<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BRptB2B_Details.aspx.cs" Inherits="mis_E_Invoice_BRptB2B_Details" %>


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
    </style>
    <style>
        .inline-rb label {
            margin-left: 5px;
        }

        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">B2B Voucher Details</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row hidden-print">
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block" Style="margin-top: 25px;" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                            <asp:GridView ID="GridView1" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" ClientIDMode="Static">
                                <Columns>
                                    <asp:TemplateField HeaderText="Voucher Date" ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                            <asp:HiddenField ID="HF_VoucherTx_ID" runat="server" Value='<%# Eval("VoucherTx_ID").ToString() %>' />
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
                                    <asp:TemplateField HeaderText="Office Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name").ToString() %>' runat="server" />
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
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# "E_Invoice.aspx" + "?VoucherTx_No=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_No").ToString())+"&VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString()) %>' CssClass="label label-info"   Visible='<%# Eval("FeedbackStatus").ToString() == "Yes" ? false : true %>'>Generate</asp:HyperLink>
                                             <asp:Label ID="lblStatus"  CssClass="label label-success" Text='Generated' runat="server"  Visible='<%# Eval("FeedbackStatus").ToString() == "Yes" ?  true : false %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>

            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

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
            paging: true,
            //columnDefs: [{
            //    targets: 'no-sort',
            //    orderable: false
            //}],
            columnDefs: [{
                orderable: false
            }],
            "bSort": false,
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
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
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
            }
        });

        function validateform() {
            debugger;
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
                        //if (FromYear == ToYear && ToMonth <= 12 && ToMonth > 3 && FromMonth >= 4) {
                        //}
                        //if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        //}
                        //else if (FromYear < ToYear && ToMonth <= 3 && FromMonth >= 4) {
                        //}
                        if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        }
                        else if (FromYear == ToYear && FromMonth >= 4 && ToMonth <= 12) {
                        }
                        else if (FromYear != ToYear && FromMonth > 3 && ToMonth <= 3) {
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
           <%-- if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }

        }
        //function PrintPage() {
        //    window.print();
        //}
    </script>
</asp:Content>
