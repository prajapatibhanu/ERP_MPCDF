<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptRetailerPaymentSheetForMilk.aspx.cs" Inherits="mis_Demand_RptRetailerPaymentSheetForMilk" %>

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

        /*thead tr th {
            background: #9e9e9ea3 !important;
        }*/

        /*tbody tr td:not(:first-child), tfoot tr td:not(:first-child) {
            text-align: right !important;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Retailer Payment Sheet Report</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>Retailer Payment Sheet Report</legend>
                        <div class="row">


                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>From Date<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                            ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                            ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Select From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>To Date<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                            ControlToValidate="txtToDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                            ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Select From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Retailer<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Retailer" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                            ControlToValidate="ddlRetailer" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlRetailer" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 20px;">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 pull-right">
                                <div class="form-group">
                                    <asp:Button ID="btnExport" CssClass="btn btn-success" Text="Export" Visible="false" OnClick="btnExport_Click" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="datatable table table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="false" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelivary_Date" runat="server" Text='<%# Eval("Delivary_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Retailer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBName" runat="server" Text='<%# Eval("BName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Milk Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMilkAmount" runat="server" Text='<%# Eval("MilkAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Mode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPayment" runat="server" Text='<%# Eval("PaymentMode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Eval("PaymentDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChequeNo" runat="server" Text='<%# Eval("PaymentNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# Eval("PaymentAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("RunningBalance") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                    </fieldset>

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

        //alert($('.printtext').attr('title'));

        $('.datatable').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            sorting: false,
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
                    //title: $('h1').text(),
                    title: $('Distributor Payment Sheet').attr('title'),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    //title: $('.exceltext').attr('title'),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
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


        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
    </script>

</asp:Content>
