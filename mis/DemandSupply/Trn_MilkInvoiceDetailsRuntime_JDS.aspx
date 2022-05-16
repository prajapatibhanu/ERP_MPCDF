<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Trn_MilkInvoiceDetailsRuntime_JDS.aspx.cs" Inherits="mis_DemandSupply_Trn_MilkInvoiceDetailsRuntime_JDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
      <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <style>
        .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            .noprint {
                display: none;
            }

            .pagebreak {
                page-break-before: always;
            }
        }

        .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }

        .thead {
            display: table-header-group;
        }

        .text-center {
            text-align: center;
        }

        .text-right {
            text-align: right;
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 2px 5px;
        }

        .lblpagenote {
            display: block;
            background: #e0eae7;
            text-align: -webkit-center;
            margin-bottom: 12px;
            padding: 10px;
        }

        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
    padding: 1px 2px 1px 1px;
    font-size: 12px;
    vertical-align: middle;
    text-align: right;
}
             .table > thead > tr > td {
    padding: 1px 2px 1px 1px;
    font-size: 11px;
    vertical-align: middle;
    text-align: left;
    font-weight:600;
    
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content no-print">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Distributor Wise Invoice View</h3>
                           
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Month,Year,Location,Distributor
                                </legend>
                                <div class="row no-print">
                                      <div class="col-lg-12">
                                    <div class="pull-right">
                                        <asp:HyperLink runat="server" href="DistInvoice_JBL_datewise.aspx" ToolTip="Back" class="btn btn-primary"><i class="fa fa-arrow-left"> Back</i></asp:HyperLink>
                                    </div>
                                </div>
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row no-print">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Month <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                                    ControlToValidate="txtMonth" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtMonth" MaxLength="8" placeholder="Select Month" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location</label>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Distributor Name</label>
                                            <asp:DropDownList runat="server" ID="ddlDitributor" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand"
                                        EmptyDataText="No Record Found." PageSize="50" runat="server" class="datatable table table-hover table-bordered pagination-ys"
                                        ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                        <Columns>

                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPANNo" runat="server" Text='<%# Eval("InvoiceNo_R") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Period">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFromDate" runat="server" Text='<%# (Eval("FromDate") + "-" + Eval("ToDate")).ToString() %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Distributor Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDName" runat="server" Text='<%# Eval("DName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Milk Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetMilkSaleAmt" runat="server" Text='<%# Eval("NetMilkSaleAmt") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CommandName="RecordPrint" CommandArgument='<%#Eval("Invoice_Rid") %>' runat="server" ToolTip="Print"><i class="fa fa-print"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("Invoice_Rid") %>' OnClientClick="return confirm('Are you sure to Delete?')" CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;"><i class="fa fa-trash"></i></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </fieldset>

                        </div>

                    </div>
                </div>

            </div>
        </section>
        <!-- /.content -->
           <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>   
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);

        $(document).ready(function () {
            $('.loader').fadeOut();
            $("#<%=btnSearch.ClientID%>").click((function () {

                if (Page_IsValid) {
                    $('.loader').show();
                    return true;

                }
            }));

         });
        $('.datatable').DataTable({
            paging: true,
            iDisplayLength: 100,
            lengthMenu: [10, 25, 50, 100],
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
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
                    title: ('Distributor Invoice Details').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2,3,4]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Distributor Invoice Details').bold().fontsize(5).toUpperCase(),
                    filename: 'DistributorInvoiceDetails',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3,4]
                    },
                    footer: false
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
        $(document).ready(function () {
            $('.loader').fadeOut();
            $("#<%=btnSearch.ClientID%>").click((function () {

                if (Page_IsValid) {
                    $('.loader').show();
                    return true;

                }
            }));



        });
        
        $("#txtMonth").datepicker({
            format: "mm/yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
        });
    </script>
</asp:Content>
