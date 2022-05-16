<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptStatistics.aspx.cs" Inherits="mis_Finance_RptStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .show_detail {
            margin-top: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Statistics
                   <small></small>
            </h1>
        </section>
        <section class="content">
            <div class="box box-pramod" style="background-color: #FFFFFF;">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="box-body">
                                <legend>Statistics</legend>
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
                                            <label>To Date</label><span style="color: red">*</span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Office Name</label><span style="color: red">*</span>
                                            <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail Aselect1" runat="server" Text="Show Statistics" OnClick="btn_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <div class="col-md-6">
                                                <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                                                <br />
                                                <br />
                                                <asp:GridView ID="GridView2" runat="server" ShowFooter="true" AutoGenerateColumns="false" class="table table-hover table-bordered" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" CausesValidation="false" CommandName="Select" Text='<%# Eval("VoucherTx_Type") %>' runat="server" CssClass="Aselect1">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:ButtonField ButtonType="Link" CommandName="View" HeaderText="Type OF Vouchers" DataTextField="VoucherTx_Type" />--%>
                                                        <asp:BoundField DataField="TotalVouchers" HeaderText="Total Vouchers" />
                                                    </Columns>
                                                </asp:GridView>
                                                <%--<table class="table">
                                                    <thead>
                                                        <tr>
                                                            <th>Type OF Vouchers</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <div id="lblTOV" runat="server">
                                                    </div>
                                                    <tfoot>
                                                        <tr>
                                                            <th>Total</th>
                                                            <th>
                                                                <asp:Label ID="lblTotalVouchers" runat="server" Text="0"></asp:Label></th>
                                                        </tr>
                                                    </tfoot>
                                                </table>--%>
                                            </div>
                                            <div class="col-md-6">
                                                <br />
                                                <br />
                                                <table class="table">
                                                    <thead>
                                                        <tr>
                                                            <th>Type Of Accounts</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>Groups</td>
                                                            <td>
                                                                <asp:Label ID="lblGroups" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Ledgers</td>
                                                            <td>
                                                                <asp:Label ID="lblLedgers" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Stock Groups</td>
                                                            <td>
                                                                <asp:Label ID="lblStockGroups" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Stock Items</td>
                                                            <td>
                                                                <asp:Label ID="lblStockIems" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Voucher Types</td>
                                                            <td>
                                                                <asp:Label ID="lblVoucherType" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Units</td>
                                                            <td>
                                                                <asp:Label ID="lblUnits" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Currencies</td>
                                                            <td>
                                                                <asp:Label ID="lblcurrencies" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table datatable table-hover table-bordered" OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="VoucherTx_Date" HeaderText="VchDate" />
                                                <asp:BoundField DataField="Particular" HeaderText="Particulars" />
                                                <asp:BoundField DataField="VoucherTx_Type" HeaderText="VchType" />
                                                <asp:BoundField DataField="VoucherTx_No" HeaderText="VchNo" />
                                                <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                                <asp:BoundField DataField="DebitAmount" HeaderText="Debit (Amount)" />
                                                <asp:BoundField DataField="CreditAmount" HeaderText="Credit (Amount)" />
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="hpView" runat="server" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);" CssClass="label label-info">View</asp:LinkButton>
                                                        <asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                        <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%-- start data table--%>
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
    <script>
        $('.datatable').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
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

        //end data table

        $('#txtFromDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if ($('#txtToDate').val() != "") {
                if (start > end) {
                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
        });
        $('#txtToDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if (start > end) {
                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }

        });
    </script>
</asp:Content>
