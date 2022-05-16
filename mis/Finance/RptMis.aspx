<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="RptMis.aspx.cs" Inherits="mis_Finance_RptMis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--<style>
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
            padding: 5px 5px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 8px 10px !important;
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

        tbody tr td:not(:first-child), tfoot tr td:not(:first-child) {
            text-align: right !important;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>MIS Report
                   <small></small>
            </h1>
        </section>
        <section class="content">
            <div class="box box-pramod" style="background-color: #FFFFFF;">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="box-body">
                                <legend>MIS Report</legend>
                                <div class="row">
                                    <div class="col-md-2">
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
                                    <div class="col-md-2">
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
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Office Name</label><span style="color: red">*</span>
                                            <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Group Name</label><span style="color: red">*</span>
                                            <asp:ListBox runat="server" ID="ddlGroup" ClientIDMode="Static" CssClass="form-control"  SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Report Type</label><span style="color: red">*</span>
                                            <asp:DropDownList ID="ddlReportType" runat="server" class="form-control select2">
                                                <asp:ListItem Value="Group Wise">Group Wise</asp:ListItem>
                                                <asp:ListItem Value="Item Wise">Item Wise</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail Aselect1" runat="server" Style="margin-top: 25px;" Text="Show Mis Report" OnClick="btn_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <div class="table-responsive">
                                                <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" EmptyDataText="No Record Found" ShowFooter="True" OnRowCommand="GridView1_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-BackColor="#EAEAEA">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle BackColor="#FFFFCC" Font-Bold="True"></ItemStyle>
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Group">
                                                            
                                                            <ItemTemplate>                                                             
                                                                 <asp:LinkButton ID="lnkgroup" runat="server" Text='<%# Eval("ItemTypeName") %>' CommandArgument='<%# Eval("ItemType_id") %>' CommandName="View"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#000666" />
                                                            <ItemStyle BackColor="#FFFFCC" Font-Bold="True" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Purchase Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("PurchaseQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#CFDCC1" />
                                                            <HeaderStyle BackColor="#CFDCC1" />
                                                            <ItemStyle BackColor="#CFDCC1" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Purchase Value Without Tax" DataField="PurchaseValueWithoutTax">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="GST" DataField="PurchaseTaxAmount">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Gross Purchase" DataField="PurchaseValuewithTax">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Sale Quantity">

                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("SaleQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#FFB88F" />
                                                            <HeaderStyle BackColor="#FFB88F" />
                                                            <ItemStyle BackColor="#FFB88F" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Sale Value Without Tax" DataField="SaleValueWithoutTax">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="GST" DataField="SaleTaxAmount">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Gross Sale" DataField="SaleValuewithTax">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>

                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" EmptyDataText="No Record Found" ShowFooter="True" OnRowCommand="GridView2_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-BackColor="#EAEAEA">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle BackColor="#FFFFCC" Font-Bold="True"></ItemStyle>
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Group" DataField="ItemTypeName">
                                                            <ItemStyle Font-Bold="true" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkItem" runat="server" Text='<%# Eval("ItemName") %>' CommandArgument='<%# Eval("Item_id") %>' CommandName="View"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#000666" />
                                                            <ItemStyle BackColor="#FFFFCC" Font-Bold="True" Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Purchase Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("PurchaseQuantity") + " " + Eval("UQCCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#CFDCC1" />
                                                            <HeaderStyle BackColor="#CFDCC1" />
                                                            <ItemStyle BackColor="#CFDCC1" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Purchase Value Without Tax" DataField="PurchaseValueWithoutTax">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="GST" DataField="PurchaseTaxAmount">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Gross Purchase" DataField="PurchaseValuewithTax">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Sale Quantity">

                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("SaleQuantity")+ " " + Eval("UQCCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#FFB88F" />
                                                            <HeaderStyle BackColor="#FFB88F" />
                                                            <ItemStyle BackColor="#FFB88F" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Sale Value Without Tax" DataField="SaleValueWithoutTax">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="GST" DataField="SaleTaxAmount">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Gross Sale" DataField="SaleValuewithTax">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#666" />
                                                        </asp:BoundField>

                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="GridView3" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowCommand="GridView3_RowCommand">
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
                                                        <asp:TemplateField HeaderText="Office Name.">
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
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>                                                    
                                                                <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
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
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script>
        $('.GridView4, .GridView2, .GridView3').DataTable({
            paging: false,
            ordering: false,
            oSearch: { bSmart: false, bRegex: true },
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
                }, {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    footer: true
                }
            ]
        });
    </script>
    <script>

        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
        $(function () {
            $('[id*=ddlGroup]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>
    <script>
        $('table tr td').each(function () {
            if ($(this).text() == '0') {
                $(this).css('color', 'red');
            }
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
