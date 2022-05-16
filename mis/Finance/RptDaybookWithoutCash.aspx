<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptDaybookWithoutCash.aspx.cs" Inherits="mis_Finance_RptDaybookWithoutCash" %>

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
            .Hiderow, .main-footer, .dataTables_filter, .dataTables_length, .dt-buttons, .dataTables_info, .dataTables_paginate paging_simple_numbers, .pagination {
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

            .lblheadingFirst p {
                text-align: center !important;
                font-size: 10px !important;
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
                    <h3 class="box-title">Day Book Without Cash (Single Date)</h3>
                    <asp:HyperLink ID="btnDtlDayBook" NavigateUrl="RptDetailedDaybook.aspx" Text="Detailed DayBook" Visible="false" Target="_blank" runat="server" CssClass="btn btn-primary pull-right hidden no-print"></asp:HyperLink><asp:Button ID="btngraphical" runat="server" Style="margin-right: 10px;" CssClass="btn btn-primary pull-right" Text="Graphical Report" OnClick="btngraphical_Click"></asp:Button>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row Hiderow">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label><span style="color: red">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>

                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblheadingFirst" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime Hiderow"></asp:Label>
                            <asp:GridView ID="GridView1" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" ShowHeaderWhenEmpty="true" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Voucher Date." ItemStyle-Width="12%">
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
                                    <asp:TemplateField HeaderText="Office Name." Visible="false">
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
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" HeaderStyle-CssClass="Hiderow" ItemStyle-CssClass="Hiderow">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                            <asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                            <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" Visible='<%# Eval("Status").ToString() == "0" ? true :false  %>' OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger"></asp:LinkButton>
                                            <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                            <asp:LinkButton ID="bankreceiptprint" CssClass="label label-primary" runat="server" Text="Receiving" CommandName="ReceiptPrint" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                            <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />

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
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
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
                    title: $('.lblheadingFirst').html(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblheadingFirst').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
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
            var msg = "";
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
    </script>
</asp:Content>







