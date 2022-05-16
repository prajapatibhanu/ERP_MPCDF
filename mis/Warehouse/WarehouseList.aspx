<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="WarehouseList.aspx.cs" Inherits="mis_Warehouse_WarehouseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <!-- general form elements -->

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label2">Office wise Warehouses list / कार्यालय अनुसार गोदामों की सूची</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office (Supervision By) <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddloffice" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddloffice_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GVWarehouseList" EmptyDataText="Record Not Found!" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-ForeColor="Red" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Warehouse_id" runat="server" OnSelectedIndexChanged="GVWarehouseList_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Warehouse_id" Visible="false" HeaderText="Warehouse Id" />
                                                <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse Name" />
                                                <asp:BoundField DataField="Area" HeaderText="Area (In Sqft.)" />
                                                <asp:BoundField DataField="WarehouseCapacity" HeaderText="Capacity (In Tonne)" />
                                                <asp:BoundField DataField="Address" Visible="false" HeaderText="Address" />
                                                <asp:BoundField DataField="InchargeName" HeaderText="Incharge Name" />
                                                <asp:BoundField DataField="InchargeMobile" HeaderText="Incharge Mobile" />
                                                <asp:BoundField DataField="TypeOfWarehouse" HeaderText="Owned/Rented" />
                                                <asp:CommandField HeaderText="Action" SelectText="Edit" ControlStyle-CssClass="label label-info" ShowSelectButton="True" />
                                                <asp:TemplateField HeaderText="Stock&nbsp;Detail" ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:DynamicHyperLink runat="server" Text="View Stock" ControlStyle-CssClass="label label-success" ID="dhypViewStock" NavigateUrl='<%# "~/mis/Warehouse/WarehouseDetail.aspx?id=" + APIProcedure.Client_Encrypt(Eval("Warehouse_id").ToString())%>'></asp:DynamicHyperLink>
                                                        <asp:DynamicHyperLink runat="server" Text="Audit Stock" ControlStyle-CssClass="label label-warning" ID="dhypAudit" NavigateUrl='<%# "~/mis/Warehouse/WarehouseAudit.aspx?id=" + APIProcedure.Client_Encrypt(Eval("Warehouse_id").ToString())%>'></asp:DynamicHyperLink>
                                                        <asp:DynamicHyperLink runat="server" Text="Audit History" ControlStyle-CssClass="label label-default" ID="dhypAuditHistory" NavigateUrl='<%# "~/mis/Warehouse/WarehouseAuditHistory.aspx?id=" + APIProcedure.Client_Encrypt(Eval("Warehouse_id").ToString())%>'></asp:DynamicHyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <div id="myModalNew" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Warehouse Detail</h4>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-striped table-hover text-center table-responsive modal-lg" DataKeyNames="WrId" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("WrId").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse Name" />
                                        <asp:BoundField DataField="Address" HeaderText="Address" />
                                        <asp:BoundField DataField="Area" HeaderText="Area" />
                                        <asp:BoundField DataField="WarehouseCapacity" HeaderText="Capacity" />
                                        <asp:BoundField DataField="Address" Visible="false" HeaderText="Address" />
                                        <asp:BoundField DataField="InchargeName" HeaderText="Incharge Name" />
                                        <asp:BoundField DataField="InchargeMobile" HeaderText="Incharge Mobile" />
                                        <asp:BoundField DataField="InchargeEmail" HeaderText="Incharge Email" />
                                        <asp:BoundField DataField="Office_Name" HeaderText="Office (Supervision By)" />
                                        <asp:BoundField DataField="TypeOfWarehouse" HeaderText="Owned/Rented" />
                                        <asp:BoundField DataField="Occupancy_form" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Occupancy From" />

                                        <asp:BoundField DataField="PeriodsInMonth" HeaderText="Period (In Months)" />
                                        <asp:HyperLinkField DataNavigateUrlFields="Attachment" DataTextFormatString="{0:c}" ControlStyle-CssClass="label label-primary" Text="Download" HeaderText="Agreement" />
                                        <asp:BoundField DataField="MonthlyRent" HeaderText="Monthly Rent" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
        function myModal() {
            $('#myModalNew').modal('show');
            return false;
        }

        $(document).ready(function () {

            $('.datatable').DataTable({
                paging: false,
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
                        title: $('h3').text().toUpperCase().fontsize(5),
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        footer: false,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        filename: 'Warehouse_List_Report',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h3').text(),
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
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
        });
    </script>

</asp:Content>

